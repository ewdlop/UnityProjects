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
                                                                                                                                                                         ¢:$oW"MŸŸ1âXπÜÙ	"î)ˆìˇ$sNJ–…◊MÂ{ô´˜%tá'£pôßaFΩÒ±3‘∏	Ó&πTıØÔ—dâ°c˘Ëç)µu Úä"¢+FÎ•≥kætIº3<È3fìŒ‹⁄˛VLíívnMÁΩÃ∑π?ZÍt[ñtªÙËC∑÷†åíA-ì|ÄæX¢Ñ ˛5ﬂêk3∫€ûvâŸ4@G?–qûøa(@e¥◊í››;¸{ˆ’‰E©&∂ÜQ(-qgËFtÓUô	¿CΩä)≈7¿.b-C5Ëª\h€î
b$∆ÆúÉ3©];#◊√>∑Àä¯-ETêY_ƒËı(*ﬂÑˆ ∏Œf~”/íö	˙x~o-ƒ1ØK-=€¬÷Ü≥)⁄-(H'ä6ùôÇ”H"ú˚ìÙ¬„“ˆ¥ tMR¸X
˜sª.´4†Ï≈Ì™#ö⁄»,Tf' e;û˜æÂuIÅ≤ÑBX2¶«h«K¸fBé]*±˝ZãK@Ô·"2üÌª
˙ë%»◊UÁ∆k‹[Ω ≤¶£'Ω9Å·?3‹ÄÁ¢  |“¬Dø¥…ùQMy	Æ©ã0ÔtóéÁ"˝€Ní}b˛A∑Iqˇ±c∞%ë≠teÆ⁄©Çh|÷}{j¥ƒ∫=3ﬁa:Âõa>‘Nç(§è'+DL¡9è€éØdÍÁ„‚ﬂâ¶|◊=e)
‘ö$o˛àìÜÏ‡…Vìsv¸ÿ‚TWÖy–/´¥zyV˙ıZ i_Ü`©˙«gvÅÇ»≤f◊∑ú∆wQºçÍdÊím◊/*q›
n	·ivLﬂy¥F–äÜ}Vmû≥„[%ª=T&5∂˝¶C^~ã0[||¢\ÇÜ^˝âÉÎÒN“DrË2m©‚|∂AAﬁ‹€·HµõÅ?2$ûÌﬂù◊É/åÅ$ˆó+XŸå©…Ó:&7$ME€ÌŸ´ﬂßmgÚ]≈j[ËwΩ—Ü∆∞Œ-(ÙäKy4#~ê£–∏mI£bA>úä+†ÖÚ≈pı—eS7>á≈≥ΩîºXÏƒ Ω…à∏˜xıÿ◊Ù¡j˙ü–} =˙¥Z5eN˝j⁄{^∞Ùµl=®ä«ø3:˙4·dJø+ahÏÖ‚Z&oŒhù!Øç{hÁÀsÿﬁ¿¶(üê-“”`pÙqbƒ5y¸ ·π83N¯}#˝¨MâgÌ\‹Ø Î“ÈcIú1ãj˚ÅÂ.˚"ôÏãv©Ç„gF><°$‘’iFåÍ¶†’«Gôã£∫ Ó£Êxé,gG¡+µ8~Ö`ò∑&∑ƒñ—SuÉtô-€ñ[W•≤PÚ∏®q»="fö<êm»…Ly≥WΩÚ:Soà˚∂6ó(%õù˘r>À(q¨‡@˙ã<ãË»sÿúˇ (åB©˜!ÀV?$~ù UùlíâÂË4<=ê	WﬁHim%Çü⁄-µUQ3ëΩg«”¬€ÍJÓÇg¨(‚ÕBnˆ9¿•ÙÉjÓ-ïD⁄∫m”h†Ω%` Dı‚‹∆Xó>ÿÉÈ‘ü	oÑ!çä‚‚'%g/—µÁAO•^†◊'˜uéÄDà◊ù`lÚµª†j∞Ãéù\‰N3»¯
9.lòt?T›Õ‰1"?™.™<¶-Àx§‚uâÈ≠=t¥‰Ãì–Äá*∂üÎs~ﬁäxH†õ¢ª°1°I;<® >ú@:©BÔÂ#µ⁄ˇ"¯Ê›m?z£$í:å”ESö∑“∆ΩhC.fª˘≤=ÒìR73<)∑à·RΩ≤ØÈ∂∞"fÎY≈MwØˇ{êéë›z≤ÊicŸ∂U¨íÜrîc®ÅıÁπ_r÷”√“˚[ód+1Uæo™}kGuzî∆~)~àzv·¢gá’n"›áqp#7t—ôø’ÇàË4–Ó_`ò¡yãXà≈9Œi™œH$ÕS5îÎ‰¥∏ﬁÛ—√Ø&ﬂ´¡4JêpÏG⁄ÙsjY˝°%ÙfO+⁄òi?ãg¨ÿ?ì.à@Íùñ/•@Œ@¥0é~¥eF∫»$H≠.ÛÕì¨DıCWPAL
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
