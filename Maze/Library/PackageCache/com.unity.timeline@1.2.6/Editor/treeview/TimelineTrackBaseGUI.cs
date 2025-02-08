es"*) config_file=Facades_$$config_file;; *"legacy"*) config_file=legacy_$$config_file;; esac; \
	echo $(thisdir):$$config_file >> $(topdir)/../msvc/scripts/order; \
	(echo $(is_boot); \
	echo $(USE_MCS_FLAGS) $(LIBRARY_FLAGS) $(LIB_MCS_FLAGS) $(KEYFILE_MCS_FLAGS); \
	echo $(LIBRARY); \
	echo $(LIBRARY_NAME); \
	echo $(BUILT_SOURCES_cmdline); \
	echo $(build_lib); \
	echo $(FRAMEWORK_VERSION); \
	echo $(PROFILE); \
	echo $(RESOURCE_DEFS); \
	echo $(response)) > $(topdir)/../msvc/scripts/inputs/$$config_file

csproj-test:

install-local: all-local
test-local: all-local
uninstall-local:

ifdef NO_INSTALL
install-local uninstall-local:
	@:

else

aot_lib = $(the_lib)$(PLATFORM_AOT_SUFFIX)
aot_libname = $(LIBRARY_NAME)$(PLATFORM_AOT_SUFFIX)

ifdef LIBRARY_INSTALL_DIR
install-local:
	$(MKINSTALLDIRS) $(DESTDIR)$(LIBRARY_INSTALL_DIR)
	$(INSTALL_LIB) $(the_lib) $(DESTDIR)$(LIBRARY_INSTALL_DIR)/$(LIBRARY_NAME)
	test ! -f $(the_lib).mdb || $(INSTALL_LIB) $(the_lib).mdb $(DESTDIR)$(LIBRARY_INSTALL_DIR)/$(LIBRARY_NAME).mdb
	test ! -f $(the_lib:.dll=.pdb) || $(INSTALL_LIB) $(the_lib:.dll=.pdb) $(DESTDIR)$(LIBRARY_INSTALL_DIR)/$(LIBRARY_NAME:.dll=.pdb)

ifdef PLATFORM_AOT_SUFFIX
	test ! -f $(aot_lib) || $(INSTALL_LIB) $(aot_lib) $(DESTDIR)$(LIBRARY_INSTALL_DIR)
endif

uninstall-local:
	-rm -f $(DESTDIR)$(LIBRARY_INSTALL_DIR)/$(LIBRARY_NAME) $(DESTDIR)$(LIBRARY_INSTALL_DIR)/$(LIBRARY_NAME).mdb $(DESTDIR)$(LIBRARY_INSTALL_DIR)/$(LIBRARY_NAME:.dll=.pdb)

else

# If RUNTIME_HAS_CONSISTENT_GACDIR is set, it implies that the internal GACDIR
# of the runtime is the same as the GACDIR we want.  So, we don't need to pass it
# to gacutil.  Note that the GACDIR we want may not be the same as the value of
# GACDIR set above, since the user could have overridden the value of $(prefix).
#
# This makes a difference only when we're building from the mono/ tree, since we
# have to ensure that the internal GACDIR of the in-tree runtime matches where we
# install the DLLs.

ifndef RUNTIME_HAS_CONSISTENT_GACDIR
gacdir_flag = /gacdir $(GACDIR)
endif

ifndef LIBRARY_PACKAGE
ifdef LIBRARY_COMPAT
LIBRARY_PACKAGE = compat-$(FRAMEWORK_VERSION)
else
LIBRARY_PACKAGE = $(FRAMEWORK_VERSION)
endif
endif

ifneq (none, $(LIBRARY_PACKAGE))
package_flag = /package $(LIBRARY_PACKAGE)
endif

install-local: $(gacutil)
	$(GACUTIL) /i $(the_lib) /f $(gacdir_flag) /root $(GACROOT) $(package_flag)

uninstall-local: $(gacutil)
	-$(GACUTIL) /u $(LIBRARY_NAME:.dll=) $(gacdir_flag) /root $(GACROOT) $(package_flag)

endif # LIBRARY_INSTALL_DIR
endif # NO_INSTALL

clean-local:
	-rm -f $(tests_CLEAN_FILES) $(library_CLEAN_FILES) $(CLEAN_FILES)

test-local run-test-local run-test-ondotnet-local:
	@:

#
# RESOURCES_DEFS is a list of ID,FILE pairs separated by spaces
# for each of those, generate a rule that produces ID.resource from
# FILE using the resgen tool, adds the generated file to CLENA_FILES and
# passes the resource to the compiler
#
ccomma = ,
define RESOURCE_template
$(1).resources: $(2)
	$$(RESGEN) "$$<" "$$@"

GEN_RESOURCE_DEPS += $(1).resources
GEN_RESOURCE_FLAGS += -resource:$(1).resources
CLEAN_FILES += $(1).resources
DIST_LISTED_RESOURCES += $(2)
endef

ifdef RESOURCE_DEFS
$(foreach pair,$(RESOURCE_DEFS), $(eval $(call RESOURCE_template,$(word 1, $(subst $(ccomma), ,$(pair))), $(word 2, $(subst $(ccomma), ,$(pair))))))
endif

DISTFILES = $(wildcard *.sources) $(EXTRA_DISTFILES) $(DIST_LISTED_RESOURCES)

ASSEMBLY      = $(LIBRARY)
ASSEMBLY_EXT  = .dll
the_assembly  = $(the_lib)

include $(topdir)/build/tests.make

ifdef HAVE_CS_TESTS
DISTFILES += $(test_sourcefile)

csproj-test:
	config_file=`basename $(LIBRARY) .dll`-tests-$(PROFILE).input; \
	echo $(thisdir):$$config_file >> $(topdir)/../msvc/scripts/order; \
	(echo false; \
	echo $(USE_MCS_FLAGS) -r:$(the_assembly) $(TEST_MCS_FLAGS); \
	echo $(LIBRARY); \
	echo $(test_lib); \
	echo $(BUILT_SOURCES_cmdline); \
	echo $(test_lib); \
	echo $(FRAMEWORK_VERSION); \
	echo $(PROFILE); \
	echo ""; \
	echo $(test_response)) > $(topdir)/../msvc/scripts/inputs/$$config_file

endif

# make dist will collect files in .sources files from all profiles
dist-local: dist-default
	subs=' ' ; \
	for f in `$(topdir)/tools/removecomments.sh $(filter-out $(wildcard *_test.dll.sources) $(wildcard *_xtest.dll.sources) $(wildcard *exclude.sources),$(wildcard *.sources))` $(TEST_FILES) ; do \
	  case $$f in \
	  ../*) : ;; \
	  *.g.cs) : ;; \
	  *) dest=`dirname "$$f"` ; \
	     case $$subs in *" $$dest "*) : ;; *) subs=" $$dest$$subs" ; $(MKINSTALLDIRS) $(distdir)/$$dest ;; esac ; \
	     cp -p "$$f" $(distdir)/$$dest || exit 1 ;; \
	  esac ; done ; \
	for d in . $$subs ; do \
	  case $$d in .) : ;; *) test ! -f $$d/ChangeLog || cp -p $$d/ChangeLog $(distdir)/$$d ;; esac ; done

ifndef LIBRARY_COMPILE
LIBRARY_COMPILE = $(CSCOMPILE)
endif

ifndef LIBRARY_SNK
LIBRARY_SNK = $(topdir)/class/mono.snk
endif

ifdef BUILT_SOURCES
library_CLEAN_FILES += $(BUILT_SOURCES)
ifeq (cat, $(PLATFORM_CHANGE_SEPARATOR_CMD))
BUILT_SOURCES_cmdline = $(BUILT_SOURCES)
else
BUILT_SOURCES_cmdline = `echo $(BUILT_SOURCES) | $(PLATFORM_CHANGE_SEPARATOR_CMD)`
endif
endif

# The library

# If the directory contains the per profile include file, generate list file.
# TODO: depend on all *.sources (except tests) for now and figure out how to list only needed files later
PROFILE_sources = $(filter-out %test.dll.exclude.sources %test.dll.sources, $(wildcard *.sources))
PROFILE_excludes = $(filter-out %test.dll.exclude.sources %test.dll.sources, $(wildcard *.exclude.sources))

sourcefile = $(depsdir)/$(PROFILE_PLATFORM)_$(PROFILE)_$(LIBRARY_SUBDIR)_$(LIBRARY).sources
$(sourcefile): $(PROFILE_sources) $(PROFILE_excludes) $(depsdir)/.stamp
	$(GENSOURCES) --strict --platformsdir:$(topdir)/build "$@" "$(LIBRARY)" "$(PROFILE_PLATFORM)" "$(PROFILE)"

library_CLEAN_FILES += $(sourcefile)

response = $(depsdir)/$(PROFILE_PLATFORM)_$(PROFILE)_$(LIBRARY_SUBDIR)_$(LIBRARY).response
$(response): $(sourcefile) $(topdir)/build/library.make $(depsdir)/.stamp
	$(PLATFORM_CHANGE_SEPARATOR_CMD) <$(sourcefile) >$@

library_CLEAN_FILES += $(response)

makefrag = $(depsdir)/$(PROFILE_PLATFORM)_$(PROFILE)_$(LIBRARY_SUBDIR)_$(LIBRARY).makefrag
$(makefrag): $(sourcefile) $(topdir)/build/library.make $(depsdir)/.stamp
#	@echo Creating $@ ...
	@sed 's,^,$(build_lib): ,' $< >$@
	@if test ! -f $(sourcefile).makefrag; then :; else \
	   cat $(sourcefile).makefrag >> $@ ; \
	   echo '$@: $(sourcefile).makefrag' >> $@; \
	   echo '$(sourcefile).makefrag:' >> $@; fi

library_CLEAN_FILES += $(makefrag)

ifndef NO_BUILD
all-local: $(makefrag)
endif

-include $(makefrag)

$(the_lib): $(the_libdir)/.stamp $(if $(PROFILE_PLATFORM),$(if $(filter $(HOST_PLATFORM),$(BUILD_PLATFORM)),$(topdir)/class/$(lib_dir)/$(PROFILE)/.stamp))

ifdef PROFILE_PLATFORM
$(topdir)/class/$(lib_dir)/$(PROFILE)/.stamp: | $(topdir)/class/$(lib_dir)/$(PROFILE)-$(HOST_PLATFORM)/.stamp
	$(if $(filter $(HOST_PLATFORM),$(BUILD_PLATFORM)),$(if $(filter $(BUILD_PLATFORM),win32),CYGWIN=winsymlinks:nativestrict) ln -s $(abspath $(topdir)/class/$(lib_dir)/$(PROFILE)-$(BUILD_PLATFORM)) $(abspath $(topdir)/class/$(lib_dir)/$(PROFILE)))
endif

ifndef NO_BUILD

$(build_lib): $(response) $(sn) $(BUILT_SOURCES) $(build_libdir)/.stamp $(GEN_RESOURCE_DEPS) $(MODULE_DEPS)
	$(LIBRARY_COMPILE) $(LIBRARY_FLAGS) $(LIB_MCS_FLAGS) $(KEYFILE_MCS_FLAGS) $(GEN_RESOURCE_FLAGS) -target:library -out:$@ $(BUILT_SOURCES_cmdline) @$(response)
ifdef RESOURCE_STRINGS_FILES
	$(Q) $(STRING_REPLACER) $(RESOURCE_STRINGS_FILES) $(IL_REPLACE_FILES) $@
endif
	$(Q) $(SN) -R $@ $(LIBRARY_SNK)

ifdef LIBRARY_USE_INTERMEDIATE_FILE
$(the_lib): $(build_lib)
	$(Q) cp $(build_lib) $@
	$(Q) $(SN) -v $@
	$(Q) test ! -f $(build_lib).mdb || mv $(build_lib).mdb $@.mdb
	$(Q) test ! -f $(build_lib:.dll=.pdb) || mv $(build_lib:.dll=.pdb) $(the_lib:.dll=.pdb)
endif

endif

library_CLEAN_FILES += $(PROFILE)_$(LIBRARY_NAME)_aot.log

ifdef PLATFORM_AOT_SUFFIX
$(the_lib)$(PLATFORM_AOT_SUFFIX): $(the_lib)
	$(Q_AOT) MONO_PATH='$(the_libdir_base)' $(RUNTIME) $(AOT_BUILD_FLAGS) --debug $(the_lib)

all-local-aot: $(the_lib)$(PLATFORM_AOT_SUFFIX)
endif

# for now, don't give any /lib flags or set MONO_PATH, since we
# give a full path to the assembly.

## Include corcompare stuff
include $(topdir)/build/corcompare.make

ifndef NO_BUILD
all-local: $(test_makefrag) $(btest_makefrag)
endif

$(test_response) $(test_makefrag) $(btest_response) $(btest_makefrag): $(topdir)/build/library.make $(depsdir)/.stamp

##