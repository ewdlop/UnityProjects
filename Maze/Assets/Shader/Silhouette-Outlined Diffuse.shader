  // auto-generated
        internal static int CreateWellKnownSid(
            WellKnownSidType sidType,
            SecurityIdentifier domainSid,
            out byte[] resultSid
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

            uint length = ( uint )SecurityIdentifier.MaxBinaryLength;
            resultSid = new byte[ length ];

            if ( FALSE != Win32Native.CreateWellKnownSid(( int )sidType, domainSid == null ? null : domainSid.BinaryForm, resultSid, ref length ))
            {
                return Win32Native.ERROR_SUCCESS;
            }
            else
            {
                resultSid = null;

                return Marshal.GetLastWin32Error();
            }
        }

        //
        // Wrapper around advapi32.EqualDomainSid
        //

        [System.Security.SecurityCritical]  // auto-generated
        internal static bool IsEqualDomainSid( SecurityIdentifier sid1, SecurityIdentifier sid2 )
        {
            //
            // Check if the api is supported
            //
            if (!WellKnownSidApisSupported) {
                throw new PlatformNotSupportedException( Environment.GetResourceString( "PlatformNotSupported_RequiresW2kSP3" ));
            }
        
            if ( sid1 == null || sid2 == null )
            {
                return false;
            }
            else
            {
                bool result;
                
                byte[] BinaryForm1 = new Byte[sid1.BinaryLength];
                sid1.GetBinaryForm( BinaryForm1, 0 );

                byte[] BinaryForm2 = new Byte[sid2.BinaryLen