<Type Name="SyslogLevel" FullName="Mono.Unix.Native.SyslogLevel">
  <TypeSignature Language="C#" Value="public enum SyslogLevel" />
  <TypeSignature Language="ILAsm" Value=".class public auto ansi sealed SyslogLevel extends System.Enum" />
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
    <summary>System logging levels.</summary>
    <remarks>Possible <see cref="M:Mono.Unix.Native.Syscall.syslog" /><paramref name="level" /> values.</remarks>
    <altmember cref="M:Mono.Unix.Native.Syscall.syslog" />
    <altmember cref="M:Mono.Unix.Native.Syscall.setlogmask" />
  </Docs>
  <Members>
    <Member MemberName="LOG_ALERT">
      <MemberSignature Language="C#" Value="LOG_ALERT" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogLevel LOG_ALERT = int32(1)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogLevel</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>A condition that should be corrected immediately, such as a
        corrupted system database.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_CRIT">
      <MemberSignature Language="C#" Value="LOG_CRIT" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogLevel LOG_CRIT = int32(2)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogLevel</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>Critical conditions, e.g., hard device errors.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_DEBUG">
      <MemberSignature Language="C#" Value="LOG_DEBUG" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogLevel LOG_DEBUG = int32(7)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogLevel</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>Messages that contain information normally of use only when
        debugging a program.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_EMERG">
      <MemberSignature Language="C#" Value="LOG_EMERG" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogLevel LOG_EMERG = int32(0)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogLevel</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>A panic condition.  This is normally broadcast to all users.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_ERR">
      <MemberSignature Language="C#" Value="LOG_ERR" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogLevel LOG_ERR = int32(3)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogLevel</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>Errors.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_INFO">
      <MemberSignature Language="C#" Value="LOG_INFO" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogLevel LOG_INFO = int32(6)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogLevel</ReturnType>
      </ReturnValue>
      <Docs>
        <summary>Informational messages.</summary>
      </Docs>
    </Member>
    <Member MemberName="LOG_NOTICE">
      <MemberSignature Language="C#" Value="LOG_NOTICE" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype Mono.Unix.Native.SyslogLevel LOG_NOTICE = int32(5)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>Mono.Unix.Native.SyslogLevel<