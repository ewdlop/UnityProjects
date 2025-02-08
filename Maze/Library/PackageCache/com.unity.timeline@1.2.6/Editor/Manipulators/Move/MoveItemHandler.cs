l.CustomAttributeArgument, Options=System.Configuration.ConfigurationPropertyOptions.None)</AttributeName>
        </Attribute>
      </Attributes>
      <ReturnValue>
        <ReturnType>System.Boolean</ReturnType>
      </ReturnValue>
      <Docs>
        <value>To be added.</value>
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>If this property is set to true, messages are durable; otherwise, messages are volatile. Durable messages are not lost if the queue manager crashes. The default value for this property is true. When exactly-once assurances are required (<see cref="P:System.ServiceModel.Channels.MsmqBindingElementBase.ExactlyOnce" /> is set to true), this property must be set to true.</para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets or sets a value that specifies whether messages sent with this binding are durable or volatile.</para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="ExactlyOnce">
      <MemberSignature Language="C#" Value="public bool ExactlyOnce { get; set; }" />
      <MemberSignature Language="ILAsm" Value=".property instance bool ExactlyOnce" />
      <MemberType>Property</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <Attributes>
        <Attribute>
          <AttributeName>System.Configuration.ConfigurationProperty("exactlyOnce", DefaultValue=Mono.Cecil.CustomAttributeArgument, Options=System.Configuration.ConfigurationPropertyOptions.None)</AttributeName>
        </Attribute>
      </Attributes>
      <ReturnValue>
        <ReturnType>System.Boolean</ReturnType>
      </ReturnValue>
      <Docs>
        <value>To be added.</value>
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>
            <see cref="P:System.ServiceModel.Channels.MsmqBindingElementBase.ExactlyOnce" />, when set to true, indicates that Message Queuing (also known as MSMQ) ensures that a sent message is delivered to the receiving message queue once and only once. If delivery fails, the message is sent to the dead-letter queue depending on the DeadLetterQueue property setting. Setting <see cref="P:System.ServiceModel.Channels.MsmqBindingElementBase.ExactlyOnce" /> to true requires the queue to be transactional.</para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets or sets a value that specifies whether messages sent with this binding have exactly-once assurances.</para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="MaxReceivedMessageSize">
      <MemberSignature Language="C#" Value="public long MaxReceivedMessageSize { get; set; }" />
      <MemberSignature Language="ILAsm" Value=".property instance int64 MaxReceivedMessageSize" />
      <MemberType>Property</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <Attributes>
        <Attribute>
          <AttributeName>System.Configuration.ConfigurationProperty("maxReceivedMessageSize", DefaultValue=Mono.Cecil.CustomAttributeArgument, Options=System.Configuration.ConfigurationPropertyOptions.None)</AttributeName>
        </Attribute>
        <Attribute>
          <AttributeName>System.Configuration.LongValidator(ExcludeRange=false, MaxValue=9223372036854775807, MinValue=0)</AttributeName>
        </Attribute>
      </Attributes>
      <ReturnValue>
        <ReturnType>System.Int64</ReturnType>
      </ReturnValue>
      <Docs>
        <value>To be added.</value>
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>If an incoming message is larger than <see cref="P:System.ServiceModel.Configuration.MsmqBindingElementBase.MaxReceivedMessageSize" />, the message is dropped.</para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets or sets a value that indicates the maximum size, in bytes, allowed for a message.</para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="MaxRetryCycles">
      <MemberSignature Language="C#" Value="public int MaxRetryCycles { get; set; }" />
      <MemberSignature Language="ILAsm" Value=".property instance int32 MaxRetryCycles" />
      <MemberType>Property</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <Attributes>
        <Attribute>
          <AttributeName>System.Configuration.ConfigurationProperty("maxRetryCycles", DefaultValue=Mono.Cecil.CustomAttributeArgument, Options=System.Configuration.ConfigurationPropertyOptions.None)</AttributeName>
        </Attribute>
        <Attribute>
          <AttributeName>System.Configuration.IntegerValidator(ExcludeRange=false, MaxValue=2147483647, MinValue=0)</AttributeName>
        </Attribute>
      </Attributes>
      <ReturnValue>
        <ReturnType>System.Int32</ReturnType>
      </ReturnValue>
      <Docs>
        <value>To be added.</value>
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>A <newTerm>retry cycle</newTerm> is when a message is transferred from the retry queue to the application queue to attempt delivery to the application. Messages are placed in the retry queue after failing a number of immediate retries as specified by <see cref="P:System.ServiceModel.Configuration.MsmqBindingElementBase.ReceiveRetryCount" />. <see cref="P:System.ServiceModel.Configuration.MsmqBindingElementBase.MaxRetryCycles" /> specifies the number of retry cycles and does not include the initial attempt to send the message. An attempt to deliver a message is made a maximum of (1 + MaxRetryCycles) * (ReceiveRetryCount + 1) times. For example, if ReceiveRetryCount = 0 and MaxRetryCycles is 1, there is a maximum of two attempts to deliver the message. This property is available starting with the wv operating system.</para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets or sets the maximum number of retry cycles to attempt delivery of messages to the receiving application.</para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="Properties">
      <MemberSignature Language="C#" Value="protected override System.Configuration.ConfigurationPropertyCollection Properties { get; }" />
      <MemberSignature Language="ILAsm" Value=".property instance class System.Configuration.ConfigurationPropertyCollection Properties" />
      <MemberType>Property</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Configuration.ConfigurationPropertyCollection</ReturnType>
      </ReturnValue>
      <Docs>
        <value>To be added.</value>
        <remarks>To be added.</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets a <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> instance that contains a collection of <see cref="T:System.Configuration.ConfigurationProperty" /> objects that can be attributes or <see cref="T:System.Configuration.ConfigurationElement" /> objects of this configuration element.</para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="ReceiveErrorHandling">
      <MemberSignature Language="C#" Value="public System.ServiceModel.ReceiveErrorHandling ReceiveErrorHandling { get; set; }" />
      <MemberSignature Language="ILAsm" Value=".property instance valuetype System.ServiceModel.ReceiveErrorHandling ReceiveErrorHandling" />
      <MemberType>Property</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <Attributes>
        <Attribute>
          <AttributeName>System.Configuration.ConfigurationProperty("receiveErrorHandling", DefaultValue=Mono.Cecil.CustomAttributeArgument, Options=System.Configuration.ConfigurationPropertyOptions.None)</AttributeName>
        </Attribute>
      </Attributes>
      <ReturnValue>
        <ReturnType>System.ServiceModel.ReceiveErrorHandling</ReturnType>
      </ReturnValue>
      <Docs>
        <value>To be added.</value>
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>When this property is set to <see cref="F:System.ServiceModel.ReceiveErrorHandling.Fault" />, the listener is faulted and a <see cref="T:System.ServiceModel.ProtocolException" /> (wrapping a <see cref="T:System.ServiceModel.MsmqPoisonMessageException" />) is traced and thrown. The message is left in the queue and must be removed in a manual way. The <see cref="T:System.ServiceModel.MsmqPoisonMessageException" /> contains a <see cref="P:System.ServiceModel.MsmqPoisonMessageException.MessageLookupId" /> that can be used to identify and manually remove the message from the poison-message queue.</para>
          <para>When this property is set to <see cref="F:System.ServiceModel.ReceiveErrorHandling.Drop" />, the offending message is simply dropped.</para>
          <para>When this property is set to <see cref="F:System.ServiceModel.ReceiveErrorHandling.Reject" />, a negative acknowledgement is sent to the client and the message is removed from the poison-message queue. This option is available only on Message Queuing (MSMQ) 4.0.</para>
          <