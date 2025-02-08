_Reason (System.Web.Services.Protocols.Soap12FaultReason ob, string element, string namesp, bool isNullable, bool needType, bool writeWrappingElem)
		{
			if (((object)ob) == null)
			{
				if (isNullable)
					WriteNullTagLiteral(element, namesp);
				return;
			}

			System.Type type = ob.GetType ();
			if (type == typeof(System.Web.Services.Protocols.Soap12FaultReason))
			{ }
			else {
				throw CreateUnknownTypeException (ob);
			}

			if (writeWrappingElem) {
				WriteStartElement (element, namesp, ob);
			}

			if (needType) WriteXsiType("Reason", "http://www.w3.org/2003/05/soap-envelope");

			if (ob.@Texts != null) {
				for (int n15 = 0; n15 < ob.@Texts.Length; n15++) {
					WriteObject_Text (ob.@Texts[n15], "Text", "http://www.w3.org/2003/05/soap-envelope", false, false, true);
				}
			}
			if (writeWrappingElem) WriteEndElement (ob);
		}

		void WriteObject_Detail (System.Web.Services.Protocols.Soap12FaultDetail ob, string element, string namesp, bool isNullable, bool needType, bool writeWrappingElem)
		{
			if (((object)ob) == null)
			{
				if (isNullable)
					WriteNullTagLiteral(element, namesp);
				return;
			}

			System.Type type = ob.GetType ();
			if (type == typeof(System.Web.Services.Protocols.Soap12FaultDetail))
			{ }
			else {
				throw CreateUnknownTypeException (ob);
			}

			if (writeWrappingElem) {
				WriteStartElement (element, namesp, ob);
			}

			if (needType) WriteXsiType("Detail", "http://www.w3.org/2003/05/soap-envelope");

			ICollection o16 = ob.@Attributes;
			if (o16 != null) {
				foreach (XmlAttribute o17 in o16)
					if (o17.NamespaceURI != xmlNamespace)
						WriteXmlAttribute (o17, ob);
			}

			if (ob.@Children != null) {
				foreach (XmlNode o18 in ob.@Children) {
					XmlNode o19 = o18;
					if (o19 is XmlElement) {
					}
					else o19.WriteTo (Writer);
					WriteElementLiteral (o19, "", "", false, true);
				}
			}
			WriteValue (ob.@Text);
			if (writeWrappingElem) WriteEndElement (ob);
		}

		void WriteObject_Text (System.Web.Services.Protocols.Soap12FaultReasonText ob, string element, string namesp, bool isNullable, bool needType, bool writeWrappingElem)
		{
			if (((object)ob) == null)
			{
				if (isNullable)
					WriteNullTagLiteral(element, namesp);
				return;
			}

			System.Type type = ob.GetType ();
			if (type == typeof(System.Web.Services.Protocols.Soap12FaultReasonText))
			{ }
			else {
				throw CreateUnknownTypeException (ob);
			}

			if (writeWrappingElem) {
				WriteStartElement (element, namesp, ob);
			}

			if (needType) WriteXsiType("Text", "http://www.w3.org/2003/05/soap-envelope");

			WriteAttribute ("lang", "http://www.w3.org/XML/1998/namespace", ob.@XmlLang);

			WriteValue (ob.@Value);
			if (writeWrappingElem) WriteEndElement (ob);
		}

		protected override void InitCallbacks ()
		{
		}

	}


	internal class Soap12FaultBaseSerializer : System.Xml.Serialization.XmlSerializer
	{
		protected override System.Xml.Serialization.XmlSerializationReader CreateReader () {
			return new Soap12FaultReader ();
		}

		protected override System.Xml.Serialization.XmlSerializationWriter CreateWriter () {
			return new Soap12FaultWriter ();
		}

		public override bool CanDeserialize (System.Xml.XmlReader xmlReader) {
			return true;
		}
	}

	internal sealed class Fault12Serializer : Soap12FaultBaseSerializer
	{
		protected override void Serialize (object obj, System.Xml.Serialization.XmlSerializationWriter writer) {
			((Soap12FaultWriter)writer).WriteRoot_Soap12Fault(obj);
		}

		protected override object Deserialize (System.Xml.Serialization.XmlSerializationReader reader) {
			return ((Soap12FaultReader)reader).ReadRoot_Soap12Fault();
		}
	}

	internal class Soap12FaultSerializerImplementation : System.Xml.Serialization.XmlSerializerImplementation
	{
		System.Collections.Hashtable readMethods = null;
		System.Collections.Hashtable writeMethods = null;
		System.Collections.Hashtable typedSerializers = null;

		public override System.Xml.Serialization.XmlSerializationReader Reader {
			get {
				return new Soap12FaultReader();
			}
		}

		public override System.Xml.Serialization.XmlSerializationWriter Writer {
			get {
				return new Soap12FaultWriter();
			}
		}

		public override System.Collections.Hashtable ReadMethods {
			get {
				lock (this) {
					if (readMethods == null) {
						readMethods = new System.Collections.Hashtable ();
						readMethods.Add (@"", @"ReadRoot_Soap12Fault");
					}
					return readMethods;
				}
			}
		}

		public override System.Collections.Hashtable WriteMethods {
			get {
				lock (this) {
					if (writeMethods == null) {
						writeMethods = new System.Collections.Hashtable ();
						writeMethods.Add (@"", @"WriteRoot_Soap12Fault");
					}
					return writeMethods;
				}
			}
		}

		public override System.Collections.Hashtable TypedSerializers {
			get {
				lock (this) {
