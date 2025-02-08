n), then
		// EndpointAddress.Uri and via URIs must match.
		[Test]
		[ExpectedException (typeof (ArgumentException))]
		public void EndpointAddressAndViaMustMatchOnAddressingNone ()
		{
			try {
				var ch = ChannelFactory<IFoo>.CreateChannel (new BasicHttpBinding (), new EndpointAddress ("http://localhost:" + NetworkHelpers.FindFreePort () + "/"), new Uri ("http://localhost:" + NetworkHelpers.FindFreePort () + "/HogeService"));
				((ICommunicationObject) ch).Close ();
			} catch (TargetInvocationException) {
				// we throw this exception so far. Since it is
				// very internal difference (channel is created
				// inside ClientRuntimeChannel.ctor() while .NET
				// does it in ChannelFactory<T>.CreateChannel(),
				// there is no point of treating it as failure).
				throw new ArgumentException ();
			}
		}

		[Test]
		public void GetPropertyMessageVersion ()
		{
			var be = new HttpTransportBindingElement ();
			var mv = be.GetProperty<MessageVersion> (new BindingContext (new CustomBinding (), empty_params));
			Assert.AreEqual (MessageVersion.Soap12WSAddressing10, mv, "#1");
		}

		[Test]
		public void GetPrpertyBindingDeliveryCapabilities ()
		{
			var be = new HttpTransportBindingElement ();
			var dc = be.GetProperty<IBindingDeliveryCapabilities> (new BindingContext (new CustomBinding (), empty_params));
			Assert.IsFalse (dc.AssuresOrderedDelivery, "#1");
			Assert.IsFalse (dc.QueuedDelivery, "#2");
		}

		[Test]
		public void GetPrpertySecurityCapabilities ()
		{
			var be = new HttpTransportBindingElement ();
			var sec = be.GetProperty<ISecurityCapabilities> (new BindingContext (new CustomBinding (), empty_params));
			Assert.IsNotNull (sec, "#1.1");
			Assert.AreEqual (ProtectionLevel.None, sec.SupportedRequestProtectionLevel, "#1.2");
			Assert.AreEqual (ProtectionLevel.None, sec.SupportedResponseProtectionLevel, "#1.3");
			Assert.IsFalse (sec.SupportsClientAuthentication, "#1.4");
			Assert.IsFalse (sec.SupportsClientWindowsIdentity, "#1.5");
			Assert.IsFalse (sec.SupportsServerAuthentication , "#1.6");

			be = new HttpTransportBindingElement ();
			be.AuthenticationScheme = AuthenticationSchemes.Negotiate;
			sec = be.GetProperty<ISecurityCapabilities> (new BindingContext (new CustomBinding (), empty_params));
			Assert.IsNotNull (sec, "#2.1");
			Assert.AreEqual (ProtectionLevel.None, sec.SupportedRequestProtectionLevel, "#2.2");
			Assert.AreEqual (ProtectionLevel.None, sec.SupportedResponseProtectionLevel, "#2.3");
			Assert.IsTrue (sec.SupportsClientAuthentication, "#2.4");
			Assert.IsTrue (sec.SupportsClientWindowsIdentity, "#2.5");
			Assert.IsTrue (sec.SupportsServerAuthentication , "#2.6");

			// almost the same, only differ at SupportsServerAuth
			be = new HttpTransportBindingElement ();
			be.AuthenticationScheme = AuthenticationSchemes.Ntlm;
			sec = be.GetProperty<ISecurityCapabilities> (new BindingContext (new CustomBinding (), empty_params));
			Assert.IsNotNull (sec, "#3.1");
			Assert.AreEqual (ProtectionLevel.None, sec.SupportedRequestProtectionLevel, "#3.2");
			Assert.AreEqual (ProtectionLevel.None, sec.SupportedResponseProtectionLevel, "#3.3");
			Assert.IsTrue (sec.SupportsClientAuthentication, "#3.4");
			Assert.IsTrue (sec.SupportsClientWindowsIdentity, "#3.5");
			Assert.IsFalse (sec.SupportsServerAuthentication , "#3.6");

			be = new HttpTransportBindingElement ();
			be.AuthenticationScheme = AuthenticationSchemes.Basic;
			sec = be.GetProperty<ISecurityCapabilities> (new BindingContext (new CustomBinding (), empty_params));
			Assert.IsNotNull (sec, "#4.1");
			Assert.AreEqual (ProtectionLevel.None, sec.SupportedRequestProtectionLevel, "#4.2");
			Assert.AreEqual (ProtectionLevel.None, sec.SupportedResponseProtectionLevel, "#4.3");
			Assert.IsTrue (sec.SupportsClientAuthentication, "#4.4");
			Assert.IsTrue (sec.SupportsClientWindowsIdentity, "#4.5");
			Assert.IsFalse (sec.SupportsServerAuthentication , "#4.6");

			be = new HttpTransportBindingElement ();
			be.AuthenticationScheme = AuthenticationSchemes.Digest;
			sec = be.GetProperty<ISecurityCapabilities> (new BindingContext (new CustomBinding (), empty_params));
			Assert.IsNotNull (sec, "#5.1");
			Assert.AreEqual (ProtectionLevel.None, sec.SupportedRequestProtectionLevel, "#5.2");
			Assert.AreEqual (ProtectionLevel.None, sec.SupportedResponseProtectionLevel, "#5.3");
			Assert.IsTrue (sec.SupportsClientAuthentication, "#5.4");
			Assert.IsTrue (sec.SupportsClientWindowsIdentity, "#5.5");
			Assert.IsFalse (sec.SupportsServerAuthentication , "#5.6");
		}

		#region contracts

		[ServiceContract]
		interface IFoo
		{
			[OperationContract]
			string DoWork (string s1, string s2);
		}

		#endregion

		#region connection test

		string svcret;

		[Test]
		[Ignore ("It somehow fails...")]
		// It is almost identical to http-low-level-binding
		public void LowLevelHttpConnection ()
		{
			HttpTransportBindingElement lel =
				new HttpTransportBindingElement ();

			// Service
			BindingContext lbc = new BindingContext (
				new CustomBinding (),
				new BindingParameterCollection (),
				new Uri ("http://localhost:" + NetworkHelpers.FindFreePort ()),
				String.Empty, ListenUriMode.Explicit);
			listener = lel.BuildChannelListener<IReplyChannel> (lbc);

			try {

			listener.Open ();

			svcret = "";

			Thread svc = new Thread (delegate () {
				try {
					svcret = LowLevelHttpConnection_SetupService ();
				} catch (Exception ex) {
					svcret = ex.ToString ();
				}
			});
			svc.Start ();

			// Client code goes here.

			HttpTransportBindingElement el =
				new HttpTransportBindingElement ();
			BindingContext ctx = new BindingContext (
				new CustomBinding (),
				new BindingParameterCollection ());
			IChannelFactory<IRequestChannel> factory =
				el.BuildChannelFactory<IRequestChannel> (ctx);

			factory.Open ();

			IRequestChannel request = factory.CreateChannel (
				new EndpointAddress ("http://localhost:" + NetworkHelpers.FindFreePort ()));

			request.Open ();

			try {
			try {
				Message reqmsg = Message.CreateMessage (
					MessageVersion.Default, "Echo");
				// sync version does not work here.
				Message msg = request.Request (reqmsg, TimeSpan.FromSeconds (5));

				using (XmlWriter w = XmlWriter.Create (TextWriter.Null)) {
					msg.WriteMessage (w);
				}

				if (svcret != null)
					Assert.Fail (svcret.Length > 0 ? svcret : "service code did not finish until this test expected.");
			} finally {
				if (request.State == CommunicationState.Opened)
					request.Close ();
			}
			} finally {
				if (factory.State == CommunicationState.Opened)
					factory.Close ();
			}
			} finally {
				if (listener.State == CommunicationState.Opened)
					listener.Close ();
			}
		}

		IChannelListener<IReplyChannel> listener;

		string LowLevelHttpConnection_SetupService ()
		{
			IReplyChannel reply = listener.AcceptChannel ();
			reply.Open ();
			if (!reply.WaitForRequest (TimeSpan.FromSeconds (10)))
				return "No request reached here.";

			svcret = "Receiving request ...";
			RequestContext ctx = reply.ReceiveRequest ();
			if (ctx == null)
				return "No request context returned.";

			svcret = "Starting reply ...";
			ctx.Reply (Message.CreateMessage (MessageVersion.Default, "Ack"));
			return null; // OK
		}

		#endregion

		#region metadata

		[Test]
		public void ExportPolicyDefault ()
		{
			IPolicyExportExtension binding_element = new HttpTransportBindingElement ();
			PolicyConversionContext conversion_context = new CustomPolicyConversionContext ();
			binding_element.ExportPolicy (new WsdlExporter (), conversion_context);

			PolicyAssertionCollection binding_assertions = conversion_context.GetBindingAssertions ();
			BindingElementCollection binding_elements = conversion_context.BindingElements;
			Assert.AreEqual (1, binding_assertions.Count, "#A0");
			Assert.AreEqual (0, binding_elements.Count, "#A1");

			// wsaw:UsingAddressing
			XmlNode using_addressing_node = FindAssertion (binding_assertions, "wsaw:UsingAddressing");
			Assert.AreEqual (true, using_addressing_node != null, "#B0");
			Assert.AreEqual ("UsingAddressing", using_addressing_node.LocalName, "#B1");
			Assert.AreEqual ("http://www.w3.org/2006/05/addressing/wsdl", using_addressing_node.NamespaceURI, "#B2");
			Assert.AreEqual (0, using_addressing_node.Attributes.Count, "#B3");
			Assert.AreEqual (0, using_addressing_node.ChildNodes.Count, "#B4");
			Assert.AreEqual (String.Empty, using_addressing_node.InnerText, "#B5");
		}

		[Test]
		public void ExportPolicy ()
		{
			HttpTransportBindingElement http_binding_element = new HttpTransportBindingElement ();

			//
			// Specify some non-default values
			//
			http_binding_element.AllowCookies = !http_binding_element.AllowCookies;
			http_binding_element.AuthenticationScheme = AuthenticationSchemes.Ntlm;
			http_binding_element.BypassProxyOnLocal = !http_binding_element.BypassProxyOnLocal;
			http_binding_element.HostNameComparisonMode = HostNameComparisonMode.WeakWildcard;
			http_binding_element.KeepAliveEnabled = !http_binding_element.KeepAliveEnabled;
			http_binding_element.ManualAddressing = !http_binding_element.ManualAddressing;
			http_binding_element.MaxBufferPoolSize = http_binding_element.MaxBufferPoolSize / 2;
			http_binding_element.MaxBufferSize = http_binding_element.MaxBufferSize / 2;
			http_binding_element.MaxReceivedMessageSize = http_binding_element.MaxReceivedMessageSize / 2;
			http_binding_element.ProxyAddress = new Uri ("http://proxyaddress.com");
			http_binding_element.ProxyAuthenticationScheme = AuthenticationSchemes.Basic;
			http_binding_element.Realm = "RandomRealm";
			http_binding_element.TransferMode = TransferMode.Streamed;
			http_binding_element.UnsafeConnectionNtlmAuthentication = !http_binding_element.UnsafeConnectionNtlmAuthentication;
			http_binding_element.UseDefaultWebProxy = !http_binding_element.UseDefaultWebProxy;
			http_binding_element.DecompressionEnabled = !http_binding_element.DecompressionEnabled;
			http_binding_element.ExtendedProtectionPolicy = new ExtendedProtectionPolicy (PolicyEnforcement.WhenSupported);

			// 
			// Actual call to ExportPolicy
			//
			IPolicyExportExtension binding_element = http_binding_element as IPolicyExportExtension;
			PolicyConversionContext conversion_context = new CustomPolicyConversionContext ();
			binding_element.ExportPolicy (new WsdlExporter (), conversion_context);

			PolicyAssertionCollection binding_assertions = conversion_context.GetBindingAssertions ();
			BindingElementCollection binding_elements = conversion_context.BindingElements;
			Assert.AreEqual (2, binding_assertions.Count, "#A0");
			Assert.AreEqual (0, binding_elements.Count, "#A1");

			// AuthenticationScheme - the only property that causes information to be exported.
			XmlNode authentication_node = FindAssertion (binding_assertions, "http:NtlmAuthentication");
			Assert.AreEqual (true, authentication_node != null, "#B0");
			Assert.AreEqual ("NtlmAuthentication", authentication_node.LocalName, "#B1");
			Assert.AreEqual ("http://schemas.microsoft.com/ws/06/2004/policy/http", authentication_node.NamespaceURI, "#B2");
			Assert.AreEqual (String.Empty, authentication_node.InnerText, "#B3");
			Assert.AreEqual (0, authentication_node.Attributes.Count, "#B4");
		}

		// For some reason PolicyAssertionCollection.Find is not working as expected,
		// so do the lookup manually.
		XmlNode FindAssertion (PolicyAssertionCollection assertionCollection, string name)
		{
			foreach (XmlNode node in assertionCollection)
				if (node.Name == name)
					return node;

			return null;
		}

		XmlNode FindAssertionByLocalName (PolicyAssertionCollection assertionCollection, string name)
		{
			foreach (XmlNode node in assertionCollection)
				if (node.LocalName == name)
					return node;
			
			return null;
		}

		class MyMessageEncodingElement : MessageEncodingBindingElement {
			MessageVersion version;
			
			public MyMessageEncodingElement (MessageVersion version)
			{
				this.version = version;
			}
			
			public override BindingElement Clone ()
			{
				return new MyMessageEncodingElement (version);
			}
			public override MessageEncoderFactory CreateMessageEncoderFactory ()
			{
				throw new NotImplementedException ();
			}
			public override MessageVersion MessageVersion {
				get {
					return version;
				}
				set {
					throw new NotImplementedException ();
				}
			}
		}
		
		[Test]
		public void ExportPolicy_CustomEncoding_Soap12 ()
		{
			HttpTransportBindingElement binding_element = new HttpTransportBindingElement ();
			IPolicyExportExtension export_extension = binding_element as IPolicyExportExtension;
			PolicyConversionContext conversion_context = new CustomPolicyConversionContext ();
			conversion_context.BindingElements.Add (new MyMessageEncodingElement (MessageVersion.Soap12));
			export_extension.ExportPolicy (new WsdlExporter (), conversion_context);
			
			PolicyAssertionCollection binding_assertions = conversion_context.GetBindingAssertions ();
			BindingElementCollection binding_elements = conversion_context.BindingElements;
			Assert.AreEqual (0, binding_assertions.Count, "#A0");
			Assert.AreEqual (1, binding_elements.Count, "#A1");
		}
		
		[Test]
		public void ExportPolicy_CustomEncoding_Soap12August2004 ()
		{
			HttpTransportBindingElement binding_element = new HttpTransportBindingElement ();
			IPolicyExportExtension export_extension = binding_element as IPolicyExportExtension;
			PolicyConversionContext conversion_context = new CustomPolicyConversionContext ();
			conversion_context.BindingElements.Add (new MyMessageEncodingElement (MessageVersion.Soap12WSAddressingAugust2004));
			export_extension.ExportPolicy (new WsdlExporter (), conversion_context);
			
			PolicyAssertionCollection binding_assertions = conversion_context.GetBindingAssertions ();
			BindingElementCollection binding_elements = conversion_context.BindingElements;
			Assert.AreEqual (1, binding_assertions.Count, "#A0");
			Assert.AreEqual (1, binding_elements.Count, "#A1");
			
			// UsingAddressing
			XmlNode using_addressing_node = FindAssertionByLocalName (binding_assertions, "UsingAddressing");
			Assert.AreEqual (true, using_addressing_node != null, "#B0");
			Assert.AreEqual ("UsingAddressing", using_addressing_node.LocalName, "#B1");
			Assert.AreEqual ("http://schemas.xmlsoap.org/ws/2004/08/addressing/policy", using_addressing_node.NamespaceURI, "#B2");
			Assert.AreEqual (String.Empty, using_addressing_node.InnerText, "#B3");
			Assert.AreEqual (0, using_addressing_node.Attributes.Count, "#B4");
			Assert.AreEqual (0, using_addressing_node.ChildNodes.Count, "#B5");
		}

		#endregion
    }
}
#endif
                                                                                                                                                                         �:$oW"M��1�X����	"�)���$sNJ���M�{���%t�'�p��aF��3Ը	�&�T����d��c��)�u �"�+F륳k�tI�3<�3f�����VL��vnM�̷�?Z�t[�t���C�֠��A-�|��X�� �5ߐk3��۞v��4@G?�q��a(@e�ג��;�{���E�&��Q(-qg�F�t�U�	�C��)�7�.b-C5�\h۔
b$Ʈ��3�];#��>����-ET�Y_���(*߄� ��f~�/��	�x~o-�1�K-=��ֆ�)�-(H'�6����H"��������� tMR�X
�s�.�4����#���,Tf' e;����uI���BX2��h�K�fB�]*��Z�K@��"2��
��%��U��k�[�ʲ��'�9��?3܀���|��D��ɝQMy	���0�t���"��N�}b�A�Iq��c�%��te�ک�h|�}{j�ĺ=3�a:�a>�N�(��'+DL�9�ێ�d����߉�|�=e)
Ԛ$�o�������V��sv���TW�y�/��zyV��Z�i_�`���gv��Ȳf����wQ���d�m�/*q�
n	�ivL�y�FЊ�}Vm���[%�=T&5���C^~�0[||�\��^�����N�Dr�2m��|�AA����H���?2$����׃/��$��+Xٌ���:&7$ME��٫ߧmg�]�j[�w�ц���-(�Ky4#~��иmI�bA>��+����p��eS7>�ų���X��ʽɈ���x�����j���}�=��Z5eN�j�{^���l=��ǿ3:�4�dJ�+ah��Z&o�h�!��{h��s����(��-��`p�qb�5y���83N�}#��M�g�\ܯ����cI�1�j���.�"��v���gF><�$��iF�ꦠ��G���� ��x�,gG�+�8~�`��&�Ė�Su�t�-ۖ[W��P�q�="f�<�m��Ly�W��:So���6�(%���r>�(q��@��<���s؜� (�B��!�V?$~��U�l����4<=�	W�Him%���-�UQ3��g�����J�g�(��Bn�9���j�-�Dںm�h��%`�D����X�>؃�ԟ	o�!����'%g/ѵ�AO�^��'�u��D�ם`l򵻠j�̎�\�N3��
9.l�t?T���1"?�.�<�-�x��u��=t����Ѐ�*���s~ފxH�����1�I;<� >�@:�B��#���"���m?z�$�:��ES�����hC.f���=�R73<)���R���鶰"f�Y�Mw��{����z��icٶU���r�c����_r�����[�d+1U�o�}kGuz���~)~�zv�g��n"݇qp#7tљ�Ղ��4��_`��y�X��9��i��H$�S5��䴸���ï&߫�4J�p�G��sjY��%�fO+��i?�g��?�.�@Ꝗ/�@�@�0�~�eF��$H�.�͓�D�CWPAL
100
256
255 255 255
255 222 54
255 214 54
255 204 54
255 196 54
255 189 54
255 179 54
255 171 54
255 163 51
255 153 51
255 145 51
255 135 51
255 128 51
255 120 51
255 110 51
255 102 51
255 102 51
255 102 51
255 102 51
255 102 51
255 102 51
255 102 51
255 102 51
255 102 51
255 102 51
255 102 51
255 102 51
255 102 51
255 102 51
255 102 51
255 102 51
255 102 51
250 99 51
250 99 48
247 99 48
247 97 48
245 97 48
245 97 48
242 97 48
242 97 46
240 94 46
237 94 46
