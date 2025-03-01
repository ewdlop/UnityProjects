<Type Name="SyslogFacility" FullName="Mono.Unix.Native.SyslogFacility">
  <TypeSignature Language="C#" Value="public enum SyslogFacility" />
  <TypeSignature Language="ILAsm" Value=".class public auto ansi sealed SyslogFacility extends System.Enum" />
  <AssemblyInfo>
    <AssemblyName>Mono.Posix</AssemblyName>
    <AssemblyVersion>1.0.5000.0</AssemblyVersion>
    <AssemblyVersion>2.0.0.0</AssemblyVersion>
    <AssemblyVersion>4.0.0.0</AssemblyVersion>
  </AssemblyInfo>
  <Base>
    <BaseTypeName>System.Enum</BaseTypeName>
  </Base>
  <Attributes>
    <Attribute>
      <AttributeName>System.CLSCompliant(false)</AttributeName>
    </Attribute>
  </Attributes>
  <Docs>
    <summary>System logging facilities.</summary>
    <remarks>Possible <see cref="M:Mono.Unix.Native.Syscall.openlog" /><paramref name="facility" /> values.</remarks>
    <altmember cref="M:Mono.Unix.Native.Syscall.openlog" />
  </Docs>
  <Members>
    <Member MemberName="LOG_AUTH">
      <MemberSignature Language="C#" Value="LOG_AUTH" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_AUTH = int32(32)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>The authorization system: <c>login</c>(1), <c>su</c>(1),
        <c>getty</c>(8), etc.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_AUTHPRIV">
      <MemberSignature Language="C#" Value="LOG_AUTHPRIV" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_AUTHPRIV = int32(80)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>The same as 
        <see cref="F:Mono.Unix.Native.SyslogFacility.LOG_AUTH" />,
        but logged to a file readable only by selected individuals.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_CRON">
      <MemberSignature Language="C#" Value="LOG_CRON" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_CRON = int32(72)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>The cron daemon: <c>cron</c>(8).</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_DAEMON">
      <MemberSignature Language="C#" Value="LOG_DAEMON" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_DAEMON = int32(24)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>System daemons, such as <c>routed</c>(8), that are not
        provided for explicitly by other facilities.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_FTP">
      <MemberSignature Language="C#" Value="LOG_FTP" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_FTP = int32(88)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>The file transfer protocol daemons: <c>ftpd</c>(8),
        <c>tftpd</c>(8), etc.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_KERN">
      <MemberSignature Language="C#" Value="LOG_KERN" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_KERN = int32(0)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>Messages generated by the kernel.  These cannot be generated
        by any user processes.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_LOCAL0">
      <MemberSignature Language="C#" Value="LOG_LOCAL0" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_LOCAL0 = int32(128)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>To be added.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_LOCAL1">
      <MemberSignature Language="C#" Value="LOG_LOCAL1" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_LOCAL1 = int32(136)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>Reserved for local use.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_LOCAL2">
      <MemberSignature Language="C#" Value="LOG_LOCAL2" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_LOCAL2 = int32(144)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>Reserved for local use.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_LOCAL3">
      <MemberSignature Language="C#" Value="LOG_LOCAL3" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_LOCAL3 = int32(152)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>Reserved for local use.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_LOCAL4">
      <MemberSignature Language="C#" Value="LOG_LOCAL4" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_LOCAL4 = int32(160)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>Reserved for local use.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_LOCAL5">
      <MemberSignature Language="C#" Value="LOG_LOCAL5" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_LOCAL5 = int32(168)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>Reserved for local use.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_LOCAL6">
      <MemberSignature Language="C#" Value="LOG_LOCAL6" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_LOCAL6 = int32(176)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>Reserved for local use.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_LOCAL7">
      <MemberSignature Language="C#" Value="LOG_LOCAL7" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_LOCAL7 = int32(184)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>Reserved for local use.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_LPR">
      <MemberSignature Language="C#" Value="LOG_LPR" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_LPR = int32(48)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>The line printer spooling system: <c>lpr</c>(1),
        <c>lpc</c>(8), <c>lpd</c>(8), etc.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_MAIL">
      <MemberSignature Language="C#" Value="LOG_MAIL" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_MAIL = int32(16)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>The mail system.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_NEWS">
      <MemberSignature Language="C#" Value="LOG_NEWS" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_NEWS = int32(56)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>The network news system.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_SYSLOG">
      <MemberSignature Language="C#" Value="LOG_SYSLOG" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_SYSLOG = int32(40)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>Messages generated internally by <c>syslogd</c>(8).</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_USER">
      <MemberSignature Language="C#" Value="LOG_USER" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_USER = int32(8)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>Messages generated by random user processes.  This is the
        default facility identifier if non is specified.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_UUCP">
      <MemberSignature Language="C#" Value="LOG_UUCP" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogFacility LOG_UUCP = int32(64)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogFacility</ReturnType>
      </Retu