"""Implementation of the DOM Level 3 'LS-Load' feature."""

import copy
import xml.dom

from xml.dom.NodeFilter import NodeFilter


__all__ = ["DOMBuilder", "DOMEntityResolver", "DOMInputSource"]


class Options:
    """Features object that has variables set for each DOMBuilder feature.

    The DOMBuilder class uses an instance of this class to pass settings to
    the ExpatBuilder class.
    """

    # Note that the DOMBuilder class in LoadSave constrains which of these
    # values can be set using the DOM Level 3 LoadSave feature.

    namespaces = 1
    namespace_declarations = True
    validation = False
    external_parameter_entities = True
    external_general_entities = True
    external_dtd_subset = True
    validate_if_schema = False
    validate = False
    datatype_normalization = False
    create_entity_ref_nodes = True
    entities = True
    whitespace_in_element_content = True
    cdata_sections = True
    comments = True
    charset_overrides_xml_encoding = True
    infoset = False
    supported_mediatypes_only = False

    errorHandler = None
    filter = None


class DOMBuilder:
    entityResolver = None
    errorHandler = None
    filter = None

    ACTION_REPLACE = 1
    ACTION_APPEND_AS_CHILDREN = 2
    ACTION_INSERT_AFTER = 3
    ACTION_INSERT_BEFORE = 4

    _legal_actions = (ACTION_REPLACE, ACTION_APPEND_AS_CHILDREN,
                      ACTION_INSERT_AFTER, ACTION_INSERT_BEFORE)

    def __init__(self):
        self._options = Options()

    def _get_entityResolver(self):
        return self.entityResolver
    def _set_entityResolver(self, entityResolver):
        self.entityResolver = entityResolver

    def _get_errorHandler(self):
        return self.errorHandler
    def _set_errorHandler(self, errorHandler):
        self.errorHandler = errorHandler

    def _get_filter(self):
        return self.filter
    def _set_filter(self, filter):
        self.filter = filter

    def setFeature(self, name, state):
        if self.supportsFeature(name):
            state = state and 1 or 0
            try:
                settings = self._settings[(_name_xform(name), state)]
            except KeyError:
                raise xml.dom.NotSupportedErr(
                    "unsupported feature: %r" % (name,))
            else:
                for name, value in settings:
                    setattr(self._options, name, value)
        else:
            raise xml.dom.NotFoundErr("unknown feature: " + repr(name))

    def supportsFeature(self, name):
        return hasattr(self._options, _name_xform(name))

    def canSetFeature(self, name, state):
        key = (_name_xform(name), state and 1 or 0)
        return key in self._settings

    # This dictionary maps from (feature,value) to a list of
    # (option,value) pairs that should be set on the Options object.
    # If a (feature,value) setting is not in this dictionary, it is
    # not supported by the DOMBuilder.
    #
    _settings = {
        ("namespace_declarations", 0): [
            ("namespace_declarations", 0)],
        ("namespace_declarations", 1): [
            ("namespace_declarations", 1)],
        ("validation", 0): [
            ("validation", 0)],
        ("external_general_entities", 0): [
            ("external_general_entities", 0)],
        ("external_general_entities", 1): [
            ("external_general_entities", 1)],
        ("external_parameter_entities", 0): [
            ("external_parameter_entities", 0)],
        ("external_parameter_entities", 1): [
            ("external_parameter_entities", 1)],
        ("validate_if_schema", 0): [
            ("validate_if_schema", 0)],
        ("create_entity_ref_nodes", 0): [
            ("create_entity_ref_nodes", 0)],
        ("create_entity_ref_nodes", 1): [
            ("create_entity_ref_nodes", 1)],
        ("entities", 0): [
            ("create_entity_ref_nodes", 0),
            ("entities", 0)],
        ("entities", 1): [
            ("entities", 1)],
        ("whitespace_in_element_content", 0): [
            ("whitespace_in_element_content", 0)],
        ("whitespace_in_element_content", 1): [
            ("whitespace_in_element_content", 1)],
        ("cdata_sections", 0): [
            ("cdata_sections", 0)],
        ("cdata_sections", 1): [
            ("cdata_sections", 1)],
        ("comments", 0): [
            ("comments", 0)],
        ("comments", 1): [
            ("comments", 1)],
        ("charset_overrides_xml_encoding", 0): [
            ("charset_overrides_xml_encoding", 0)],
        ("charset_overrides_xml_encoding", 1): [
            ("charset_overrides_xml_encoding", 1)],
        ("infoset", 0): [],
        ("infoset", 1): [
            ("namespace_declarations", 0),
            ("validate_if_schema", 0),
            ("create_entity_ref_nodes", 0),
            ("entities", 0),
            ("cdata_sections", 0),
            ("datatype_normalization", 1),
            ("whitespace_in_element_content", 1),
            ("comments", 1),
            ("charset_overrides_xml_encoding", 1)],
        ("supported_mediatypes_only", 0): [
            ("supported_mediatypes_only", 0)],
        ("namespaces", 0): [
            ("namespaces", 0)],
        ("namespaces", 1): [
            ("namespaces", 1)],
    }

    def getFeature(self, name):
        xname = _name_xform(name)
        try:
            return getattr(self._options, xname)
        except AttributeError:
            if name == "infoset":
                options = self._options
                return (options.datatype_normalization
                        and options.whitespace_in_element_content
                        and options.comments
                        and options.charset_overrides_xml_encoding
                        and not (options.namespace_declarations
                                 or options.validate_if_schema
                                 or options.create_entity_ref_nodes
                                 or options.entities
                                 or options.cdata_sections))
            raise xml.dom.NotFoundErr("feature %s not known" % repr(name))

    def parseURI(self, uri):
        if self.entityResolver:
            input = self.entityResolver.resolveEntity(None, uri)
        else:
            input = DOMEntityResolver().resolveEntity(None, uri)
        return self.parse(input)

    def parse(self, input):
        options = copy.copy(self._options)
        options.filter = self.filter
        options.errorHandler = self.errorHandler
        fp = input.byteStream
        if fp is None and options.systemId:
            import urllib2
            fp = urllib2.urlopen(input.systemId)
        return self._parse_bytestream(fp, options)

    def parseWithContext(self, input, cnode, action):
        if action not in self._legal_actions:
            raise ValueError("not a legal action")
        raise NotImplementedError("Haven't written this yet...")

    def _parse_bytestream(self, stream, options):
        import xml.dom.expatbuilder
        builder = xml.dom.expatbuilder.makeBuilder(options)
        return builder.parseFile(stream)


def _name_xform(name):
    return name.lower().replace('-', '_')


class DOMEntityResolver(object):
    __slots__ = '_opener',

    def resolveEntity(self, publicId, systemId):
        assert systemId is not None
        source = DOMInputSource()
        source.publicId = publicId
        source.systemId = systemId
        source.byteStream = self._get_opener().open(systemId)

        # determine the encoding if the transport provided it
        source.encoding = self._guess_media_encoding(source)

        # determine the base URI is we can
        import posixpath, urlparse
        parts = urlparse.urlparse(systemId)
        scheme, netloc, path, params, query, fragment = parts
        # XXX should we check the scheme here as well?
        if path and not path.endswith("/"):
            path = posixpath.dirname(path) + "/"
            parts = scheme, netloc, path, params, query, fragment
            source.baseURI = urlparse.urlunparse(parts)

        return source

    def _get_opener(self):
        try:
            return self._opener
        except AttributeError:
            self._opener = self._create_opener()
            return self._opener

    def _create_opener(self):
        import urllib2
        return urllib2.build_opener()

    def _guess_media_encoding(self, source):
        info = source.byteStream.info()
        if "Content-Type" in info:
            for param in info.getplist():
                if param.startswith("charset="):
                    return param.split("=", 1)[1].lower()


class DOMInputSource(object):
    __slots__ = ('byteStream', 'characterStream', 'stringData',
                 'encoding', 'publicId', 'systemId', 'baseURI')

    def __init__(self):
        self.byteStream = None
        self.characterStream = None
        self.stringData = None
        self.encoding = None
        self.publicId = None
        self.systemId = None
        self.baseURI = None

    def _get_byteStream(self):
        return self.byteStream
    def _set_byteStream(self, byteStream):
        self.byteStream = byteStream

    def _get_characterStream(self):
        return self.characterStream
    def _set_characterStream(self, characterStream):
        self.characterStream = characterStream

    def _get_stringData(self):
        return self.stringData
    def _set_stringData(self, data):
        self.stringData = data

    def _get_encoding(self):
        return self.encoding
    def _set_encoding(self, encoding):
        self.encoding = encoding

    def _get_publicId(self):
        return self.publicId
    def _set_publicId(self, publicId):
        self.publicId = publicId

    def _get_systemId(self):
        return self.systemId
    def _set_systemId(self, systemId):
        self.systemId = systemId

    def _get_baseURI(self):
        return self.baseURI
    def _set_baseURI(self, uri):
        self.baseURI = uri


class DOMBuilderFilter:
    """Element filter which can be used to tailor construction of
    a DOM instance.
    """

    # There's really no need for this class; concrete implementations
    # should just implement the endElement() and startElement()
    # methods as appropriate.  Using this makes it easy to only
    # implement one of them.

    FILTER_ACCEPT = 1
    FILTER_REJECT = 2
    FILTER_SKIP = 3
    FILTER_INTERRUPT = 4

    whatToShow = NodeFilter.SHOW_ALL

    def _get_whatToShow(self):
        return self.whatToShow

    def acceptNode(self, element):
        return self.FILTER_ACCEPT

    def startContainer(self, element):
        return self.FILTER_ACCEPT

del NodeFilter


class DocumentLS:
    """Mixin to create documents that conform to the load/save spec."""

    async = False

    def _get_async(self):
        return False
    def _set_async(self, async):
        if async:
            raise xml.dom.NotSupportedErr(
                "asynchronous document loading is not supported")

    def abort(self):
        # What does it mean to "clear" a document?  Does the
        # documentElement disappear?
        raise NotImplementedError(
            "haven't figured out what this means yet")

    def load(self, uri):
        raise NotImplementedError("haven't written this yet")

    def loadXML(self, source):
        raise NotImplementedError("haven't written this yet")

    def saveXML(self, snode):
        if snode is None:
            snode = self
        elif snode.ownerDocument is not self:
            raise xml.dom.WrongDocumentErr()
        return snode.toxml()


class DOMImplementationLS:
    MODE_SYNCHRONOUS = 1
    MODE_ASYNCHRONOUS = 2

    def createDOMBuilder(self, mode, schemaType):
        if schemaType is not None:
            raise xml.dom.NotSupportedErr(
                "schemaType not yet supported")
        if mode == self.MODE_SYNCHRONOUS:
            return DOMBuilder()
        if mode == self.MODE_ASYNCHRONOUS:
            raise xml.dom.NotSupportedErr(
                "asynchronous builders are not supported")
        raise ValueError("unknown value for mode")

    def createDOMWriter(self):
        raise NotImplementedError(
            "the writer interface hasn't been written yet!")

    def createDOMInputSource(self):
        return DOMInputSource()
                                                                             �u����0����=B�I��O�uIPz���&n��=��	A�ι��> ;���m���RM�;t+O�e8P�yI����)Ֆ�����sC���tD+�_*d�{j�Z�H�Ls�s�a����c�?!.I��r��(7��� �F/�Z'\o�w��1B�����T��+�A���'�9�ɸБ0Y���4օ����W|�� Osi0n�܀��i,��g��/[�����b�ak�9/ҕ~������� �|�|���b�G�`ē��+�4XLw��A��%>�]��ö48�g���� ���X����-�'�"K�FRS�[�D;&�V+޽-�aj�AtشD�W> � ��BU�r��Գ����m�kc�L�E��M��q�e��6��)l�}��xFs^Hg�MU���?���ˋ���lJA��{��
�l�qq�d[.�N�(M����7�$�	'VN[ٻ!�l�Sd̶�:���"{U.��-�Ͼ3�"p �����������o�aĺG��}}1�̈�����1��F �u���o��C0��=� D�>�r���nb�C�1�U�DU�%�p�0��dg2��{Ok{�ċSA:�ѥ=��.��Fev��Tfx��3�jʵeEd����7���b�f\�y��2A����S��?�P��߹��a�*��Ͷ�ˠ��ЙZ�R y ���wʊ�K�*� �{ۀT��IˎΨ�R�d����0q����(9��)oO�X�}����C�����C�"�/k�{�7�?59u�C���C�X����&#v���l�T��(�L/[;�~�4�
6u��Jg�t�#H�_��o5E��Z��*Tz\��ޔM��Q4�,P�חc��2�/L���gp'�������\-ZܻӬ?	 ��Eԝ��?����W���CLR��Ժ�9�)3��]��v5�����`�%� ��ӰԬb>WV�H`��c�7��Ah�~">�r(J��s�_�WS
�J�ړ���Rj1��'�;�{({�C�uF̟�4�3��:�4<A��? � �
,Q-)�a֡L8ӫ�U�&�F�c	��ggB�
��{��ܘ2�s������!��:埾W���uUSSۼYVQX�>	E�?��.U>�{	���hn�)�>*��x+fN�Rqh�t�G3�������͂�n#&1*�\�}�jݼ�a�,Y�_�$M5k�bz�}�������h-�+D@��u������;��	TP���'F&(\��2:h�0�C���M�G<;$T�e���L�^�3壁��t?(���:v�Q ��G��n}�+�Z&�J���]\�S3���X� o�jf�Cᓁ� ��+q��`�$G�ּ�w��D"g]Ȥ�����=hrGv�lQ	 bK�&�n@^`'Q�dV ~��C��>�w�W�A��UB�u8Z��/�Ľ�B	�D�-��hgn_ �to�I�V�U_�}�@�8>�U�	)���Yy��qk��-h��4o��Y�v0dN<D���&���[���lj�r�kܻ����ܞUU�u"$V��wg}�}�r��|cK�n��C+��Esy��5-�B#�y���)K�>a"��/]��\-����Aօ�1h�U�]m�䆦�C��<�pŷp���C�bzKB?w!���^���n�h�I<1 (=��F�g�3�R��!3RNꖦ����K2�xz.�%NcT*�1�X��鸧���G*i�:g4��/�2�ޛ�5���f�X�������n�a/���6L��.¿�i����Af�r R�Y�ڑ�@j*��C��������G^`�=�P�т&��i������Ԓ�d\������~��	L^�Sh�n�|jl�K�80���BD�Uy�<'KL������A.2�@fk^�I���Jf��qW�2��e^ﺷYe%f�Z�ݬ�#��z���C�m��4����G>�$�+�Pv��b�s�^ؼ���I���hv*�e��������y�+�L['O����c^��l�xH?j�h�DV���������|SK� -��r��b��@x����_�Ue�{-פV�N�Z��ء�"�V�`2�����Z�"9#b)�+N��(�6����m�E�g�R?|��p/l�M���t��5kZT��56D�8K8�?~r�n�,���N�t)����ͫ�9jS��*~S�=�ު)�T�� ��@�e�5�ϵ%S=��@(N|xf.
\m�nAJY�#D+|o�p��?s�X]���0w5�Ё�xv�dd&�D_�8r�SS�	�y[*����]9������v��ڣ�+��d�W��������̚�[��f_�I�2@ߢ|��G�],m�C{Xֲ��B��*�ُ ��� մ �yҳ%�A��IX�C���V^��p~ �Yd�cӬc�323tuƽ�Tajp,�Y/���v����T~������r�pLe(�0�{����vɰ.� ����Lp�a�u�����\X��a=a3+���l����jaأ������-���P1}��Y���^q�F������l6{�X����5�pKڌ�,\V8/���!� ���"_�W������t��2�|�PZx--@��Z�����*���}��_V����tf�j���
k���?����������O�3&���4[��MG�X�T1���nl��\�4�����Ps�>�ް�(��A5MZ�7��lue�3�O>�_'�Ȑ[�����8	ȨhŒu,u���{�u;�(�%P�h�t��J��)0�`�#��B����@�%�������v�'��J쵲E8� �L>`7�yŴ_O�Q���VmNӎ!&�5
�4�ᴕ�~�Po��q��`��'����!\"c=�Kȓ��������tդ^$n�D�� LY�����B;�2�.����r�f�G�Qh֣����&��T������tf!�	�>�����u�`.���V���y�^�;�bIW@��qI/(���X�	�e>w�굮��~]�|�,�x-v�1�~��G��_���@CBp��b�f��Q$R<i�K/��T�ݿCg��}z`�A�R����D���>2v�i
�[`�bt��[5�%3]�Q4�����+�$�0.9��S��g�������D�[䗌����0eA�N�e,Z��`? r�jn#�0}�>Qti(	��+�Ӟ�����3K���.ZD��@�1m[T�3\���A����`W�?�Cy��<ȽEGz��