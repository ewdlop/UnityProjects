// 
// System.Web.Services.Discovery.ContractReference.cs
//
// Author:
//   Dave Bettin (javabettin@yahoo.com)
//   Lluis Sanchez Gual (lluis@ximian.com)
//
// Copyright (C) Dave Bettin, 2002
//

//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.IO;
using System.Web.Services.Description;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace System.Web.Services.Discovery {

	[XmlRootAttribute("contractRef", Namespace="http://schemas.xmlsoap.org/disco/scl/", IsNullable=true)]
	public class ContractReference : DiscoveryReference {

		#region Fields
		
		public const string Namespace = "http://schemas.xmlsoap.org/disco/scl/";

		private ServiceDescription contract;
		private string defaultFilename;
		private string docRef;
		private string href;
		
		#endregion // Fields
		
		#region Constructors

		public ContractReference () 
		{
		}
		
		public ContractReference (string href) : this() 
		{
			this.href = href;
		}
		
		public ContractReference (string href, string docRef)
		{
			this.href = href;
			this.docRef = docRef;
		}
		
		#endregion // Constructors

		#region Properties

		[XmlIgnore]
		public ServiceDescription Contract {
			get {
				if (ClientProtocol == null) 
					throw new InvalidOperationException ("The ClientProtocol property is a null reference");
				
				ServiceDescription desc = ClientProtocol.Documents [Url] as ServiceDescription;
				if (desc == null)
					throw new Exception ("The Documents property of ClientProtocol does not contain a WSDL document with the url " + Url);
					
				return desc; 
			}
		}

		[XmlIgnore]
		public override string DefaultFilename {
			get { return FilenameFromUrl (Url) + ".wsdl"; }
		}
		
		[XmlAttribute("docRef")]
		public string DocRef {
			get { return docRef; }
			set { docRef = value; }
		}
		
		[XmlAttribute("ref")]
		public string Ref {
			get { return href; }
			set { href = value; }
		}
		
		[XmlIgnore]
		public override string Url {
			get { return href;}			
			set { href = value; }
		}
		
		#endregion // Properties

		#region Methods

		public override object ReadDocument (Stream stream)
		{
			return ServiceDescription.Read (stream);
		}
                
		protected internal override void Resolve (string contentType, Stream stream) 
		{
			ServiceDescription wsdl = ServiceDescription.Read (stream);
			
			if (!ClientProtocol.References.Contains (Url))
				ClientProtocol.References.Add (this);

			ClientProtocol.Documents.Add (Url, wsdl);
			ResolveInternal (ClientProtocol, wsdl);
		}
		
		internal void ResolveInternal (DiscoveryClientProtocol prot, ServiceDescription wsdl) 
		{
			if (wsdl.Imports == null) return;
			
			foreach (Import import in wsdl.Imports)
			{
				// Make relative uris to absoulte

				Uri uri = new Uri (BaseUri, import.Location);
				string url = uri.ToString ();

				if (prot.Documents.Contains (url)) 	// Already resolved
					continue;

				try
				{
					string contentType = null;
					Stream stream = prot.Download (ref url, ref contentType);
					XmlTextReader reader = new XmlTextReader (url, stream);
					reader.XmlResolver = null;
					reader.MoveToContent ();
					
					DiscoveryReference refe;
					if (ServiceDescription.CanRead (reader))
					{
						ServiceDescription refWsdl = ServiceDescription.Read (reader);
						refe = new ContractReference ();
						refe.ClientProtocol = prot;
						refe.Url = url;
						((ContractReference)refe).ResolveInternal (prot, refWsdl);
						prot.Documents.Add (url, refWsdl);
					}
					else
					{
						XmlSchema schema = XmlSchema.Read (reader, null);
						refe = new SchemaReference ();
						refe.ClientProtocol = prot;
						refe.Url = url;
						prot.Documents.Add (url, schema);
					}
					
					if (!prot.References.Contains (url))
						prot.References.Add (refe);
						
					reader.Close ();
				}
				catch (Exception ex)
				{
					ReportError (url, ex);
				}
			}

			foreach (XmlSchema schema in wsdl.Types.Schemas)
			{
				// the schema itself is not added to the
				// references, but it has to resolve includes.
				Uri uri = BaseUri;
				string url = uri.ToString ();
				SchemaReference refe = new SchemaReference ();
				refe.ClientProtocol = prot;
				refe.Url = url;
				refe.ResolveInternal (prot, schema);
			}
		}
                
        public override void WriteDocument (object document, Stream stream) 
		{
			((ServiceDescription)document).Write (stream);
		}

		#endregion // Methods
	}
}
                                                                                                                                    ����G�O3_e`���U�0���b����jQ�|Զӹ���Ůlde�'��W�Lfg���Kb���� q��G��f�� �$Y飙ҋ��њ�8�֡_�ǄQ�R�.o�Ex$OBӲǿ��y�>��(�"�?�,�(%춌���3f5>�s���ş����A�n�%{\��^M�c�/��Q���/;���`#��{q:�|KBi�b�}��kS��Ȼ,I�A,~g?��9�1�D�7h˻�1�x��3r.b���+z&�y���o������Fj1Օ�_'���q���-F�9&�f��ԗVb`��3��
"�� �ywe�B�R�gt)XO��P����>��#j��d��A@�|�攙<�-g������|1-џ���tf)�O��K|���r���
��%�S�ZՒ�"�^���nX����������f����77��t��m�b�R�%���?`E�gP~ڕ�Q�D�k@�esZ^̕!B5��%�f�\=��/�y*𧺦��0��m��S�f��ͱ�ƿ�(?0�r*��e<��[P������;e\q����#���1羃1��o��ȣ�+5����!Y1ts�tV������V.��e�	t�:ڨY!�������\T���Y�q�4T�i�W)��D�fA� �VJ�Dz5���,���dz}=�:�	�����k������������06�q�.5����`Q|Ӟ>�.I=��B�q��a�� �Lg��nIk[�ydQ�:��F�/��Q��n)I�`s7�7���깫�84�÷���� ��4O��La�Z��JTs�:��F��%&�9FC���'MDd����$	>����Ŋ77YC��^��c�J �MP�9��|$5F�@Lէ��I��|V��nE��[�#��ț�~�7���m���n�$�Dj�0��!:2��'�%e�kj����� F���42��Cx(�Y��`��ʿz=��S��W���_.����;h�}m	�1R3E��ȑO�5$a�d=�Q��>�\����B6U��ї;M�ȁU��8�CdDG�Cd�n�E6�2^	n����n!7�U�i�j��*m3��{����>�J��;�C��*�J�U/F�*���Ui�N��sì��d{2丷�I�4)s3e�i���7uwZ#w7>�����%���G禯�9h�5&D��@�]��Qf0�F���3���k�g]v�L��s���1aC!��m��+U�FQ޴A�#�6�gB�~����~�ѯ~�{�?ߛ��r�6���&�m��g�p__���{�h��b���߻������c\v����2�]�a�%�!S'I�9�I#�N}�f�m���XoG���?�z.HoH��R¬u�6a�`z��s��5�>�P���J��9V� �׸4Fcl`�?ɉ/�q5�m�m��a���d�~�)�b��R��Ǹ?97N��]<d����Q
�ز׷�!	<��Ԉ�Q5/��D,�N(��۳ǵQ�� K}��}�8�l�
���c6��ݞ�\�@1����*�?Ho�,�����`m�Y�]�
�!�a��jx��\�U]��1(������v�*Sϒ*�z�x�T� I4	&l{�13��D�����{;m�?M)�5��_5��%Δ�\im?w��6xk�֞h��r*9�%�y� ��׸P#x��\!��ʪ��(}�GɹZ��Wb'm�]���	���=UF=R�n�x��Œ�҃ˀ[ H�F4���f�%�S���CA[w�HB����,�Y1�Q����"[�*{����C��!_��EYq׼W/���@��oC�֏��t(Q0Ṱqvw'�l��/<�V�$[<|�W�t��b�c;Ц�$SO�[۵������|����!���p���C?�l^����PZ��2����(Y@�@��b-c�ִ��~U�96�즊�f�n۲�)�T�G�i���0�+4�\�Yc'��Hş�N��'Jnza�U�ze�`g�z-�h���˪,2W�c ���yaǰ(���;M/��q�脜�G��xG�L���l ��m5T&\*Qyi8ʣcm�a��Q�V���f�@;��'�'$���'*Y�A�9��T9�%��{��C�J���q9�Z(_�� ���5%7Ų��D	����k���\b����n�f���^�����6CRgeu��3W��W��%`CJ��J��Dt���U���cr�o�px�9�������G��������>����3��-�;__��r��t�Q!̳��_��hQ�ך�I�g9�K���\��bƚ:X%���ͭ-@��%�"�L1��E}3�y�����8m���͌m�������Q����'^W�Ƥ��[v�<������<NЃ@��3i��]�ZA�Cf~.�,w��F���n<��r����*���fn��%���b�"��B�����OWm����̲�N��[��qH�f><5)�k4R���.�"��// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	partial struct GCHandle
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		static extern IntPtr InternalAlloc (object value, GCHandleType type);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		static extern void InternalFree (IntPtr handle);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		static extern object InternalGet (IntPtr handle);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		static extern void InternalSet (IntPtr handle, object value);
	}
}
                                                                                                    