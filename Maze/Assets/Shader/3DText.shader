rSize > 0, "bufferSize > 0");
                        referencedDomains.Initialize((ulong)bufferSize);
                    }
                }
                finally
                {
                    if (pRdl != null)
                        referencedDomains.ReleasePointer();
                }
            }
        }

        //
        // Wrapper around avdapi32.GetWindowsAccountDomainSid
        //

        [System.Security.SecurityCritical]  // auto-generated
        internal static int GetWindowsAccountDomainSid(
            SecurityIdentifier sid,
            out SecurityIdentifier resultSid
            )
        {

            //
            // Check if the api is supported
            //
            if (!WellKnownSidApisSupported) {
                throw new PlatformNotSupportedException( Environment.GetResourceString( "PlatformNotSupported_RequiresW2kSP3" ));
            }
        
            //
            // Passing an array as big as it can ever be is a small price to pay for
            // not having to P/Invoke twice (once to get the buffer, once to get the data)
            //

            byte[] BinaryForm = new Byte[sid.BinaryLength];
            sid.GetBinaryForm( BinaryForm, 0 );
            uint sidLength = ( uint )SecurityIdentifier.MaxBinaryLength;
            byte[] resultSidBinary 