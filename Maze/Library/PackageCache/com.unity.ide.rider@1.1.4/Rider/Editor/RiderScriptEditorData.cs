 {
                                string siteName = ProcessHostConfigUtils.GetSiteNameFromId(siteIDValue);
                                physicalPath = ProcessHostConfigUtils.MapPathActual(siteName, path);
                            }
                            if (physicalPath != null && physicalPath.Length == 2 && physicalPath[1] == ':')
                                physicalPath += "\\";
                            // Throw if the resulting physical path is not canonical, to prevent potential
                            // security issues (VSWhidbey 418125)

                            if (HttpRuntime.IsMapPathRelaxed) {
                                physicalPath = HttpRuntime.GetRelaxedMapPathResult(physicalPath);
                            }

                            if (FileUtil.IsSuspiciousPhysicalPath(physicalPath)) {
                                if (HttpRuntime.IsMapPathRelaxed) {
                         