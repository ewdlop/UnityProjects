
            PolicyRights rights )
        {
            uint ReturnCode;
            SafeLsaPolicyHandle Result;
            Win32Native.LSA_OBJECT_ATTRIBUTES Loa;

            Loa.Length = Marshal.SizeOf( typeof( Win32Native.LSA_OBJECT_ATTRIBUTES ));
            Loa.RootDirectory = IntPtr.Zero;
            Loa.ObjectName = IntPtr.Zero;
            Loa.Attributes = 0;
            Loa.SecurityDescriptor = IntPtr.Zero;
            Loa.SecurityQualityOfService = IntPtr.Zero;

            if ( 0 == ( ReturnCode = Win32Native.LsaOpenPolicy( systemName, ref Loa, ( int )rights, out Result )))
            {
                return Result;
            }
            else if ( ReturnCode == Win32Native.STATUS_ACCESS_DENIED ) 
            {
                throw new UnauthorizedAccessException();
         