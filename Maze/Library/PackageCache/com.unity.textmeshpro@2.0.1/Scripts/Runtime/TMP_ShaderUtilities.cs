//
// System.Xml.XmlReaderSettingsTests.cs
//
// Authors:
//	Atsushi Enomoto <atsushi@ximian.com>
//
// (C)2004 Novell Inc.
//

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using NUnit.Framework;
using System.Reflection;
using ValidationFlags = System.Xml.Schema.XmlSchemaValidationFlags;
using AssertType = NUnit.Framework.Assert;

using MonoTests.Helpers;

namespace MonoTests.System.Xml
{
	[TestFixture]
	public class XmlReaderSettingsTests
	{
		public Stream CreateStream (string xml)
		{
			return new MemoryStream (Encoding.UTF8.GetBytes (xml));
		}

		[Test]
		public void DefaultValue ()
		{
			XmlReaderSettings s = new XmlReaderSettings ();
			Assert.AreEqual (true, s.CheckCharacters, "CheckCharacters");
			Assert.AreEqual (ConformanceLevel.Document, s.ConformanceLevel, "ConformanceLevel");
			Assert.AreEqual (ValidationType.None, s.ValidationType, "ValidationType");
			Assert.AreEqual (false, s.IgnoreComments, "IgnoreComments");
			Assert.IsTrue (0 == (s.ValidationFlags &
				ValidationFlags.ProcessInlineSchema), "ProcessInlineSchema");
			Assert.AreEqual (false, s.IgnoreProcessingInstructions, "IgnorePI");
			Assert.IsTrue (0 == (s.ValidationFlags &
				ValidationFlags.ProcessSchemaLocation), "ProcessSchemaLocation");
			Assert.IsTrue (0 == (s.ValidationFlags &
				ValidationFlags.ReportValidationWarnings), "ReportValidationWarnings");
			Assert.IsTrue (0 != (s.ValidationFlags &
				ValidationFlags.ProcessIdentityConstraints), "ProcessIdentityConstraints");
			// No one should use this flag BTW if someone wants
			// code to be conformant to W3C XML Schema standard.
			Assert.IsTrue (0 != (s.ValidationFlags &
				ValidationFlags.AllowXmlAttributes), "AllowXmlAttributes");
			Assert.AreEqual (false, s.IgnoreWhitespace, "IgnoreWhitespace");
			Assert.AreEqual (0, s.LineNumberOffset, "LineNumberOffset");
			Assert.AreEqual (0, s.LinePositionOffset, "LinePositionOffset");
			Assert.IsNull (s.NameTable, "NameTable");
			Assert.AreEqual (0, s.Schemas.Count, "Schemas.Count");
		}

		[Test]
		public void SetSchemas ()
		{
			XmlReaderSettings s = new XmlReaderSettings ();
			s.Schemas = new XmlSchemaSet ();
		}

		[Test]
		public void SetSchemasNull ()
		{
			XmlReaderSettings s = new XmlReaderSettings ();
			s.Schemas = null;
		}

		[Test]
		public void CloseInput ()
		{
			StringReader sr = new StringReader ("<root/><root/>");
			XmlReader xtr = XmlReader.Create (sr); // default false
			xtr.Read ();
			xtr.Close ();
			// It should without error, unlike usual XmlTextReader.
			sr.ReadLine ();
		}

		[Test]
		public void CreateAndNormalization ()
		{
			StringReader sr = new StringReader (
				"<root attr='   value   '>test\rstring</root>");
			XmlReaderSettings settings = new XmlReaderSettings ();
			settings.CheckCharacters = false;
			XmlReader xtr = XmlReader.Create (
				sr, settings);
			xtr.Read ();
			xtr.MoveToFirstAttribute ();
			Assert.AreEqual ("   value   ", xtr.Value);
			xtr.Read ();
			// Text string is normalized
			Assert.AreEqual ("test\nstring", xtr.Value);
		}

		[Test]
		public void CheckCharactersAndNormalization ()
		{
			// It should *not* raise an error (even Normalization
			// is set by default).
			StringReader sr = new StringReader (
				"<root attr='&#0;'>&#x0;</root>");
			XmlReaderSettings settings = new XmlReaderSettings ();
			settings.CheckCharacters = false;
			XmlReader xtr = XmlReader.Create (
				sr, settings);
			// After creation, changes on source XmlReaderSettings
			// does not matter.
			settings.CheckCharacters = false;
			xtr.Read ();
			xtr.MoveToFirstAttribute ();
			Assert.AreEqual ("\0", xtr.Value);
			xtr.Read ();
			Assert.AreEqual ("\0", xtr.Value);
		}

		// Hmm, does it really make sense? :-/
		[Test]
		public void CheckCharactersForNonTextReader ()
		{
			// It should *not* raise an error (even Normalization
			// is set by default).
			StringReader sr = new StringReader (
				"<root attr='&#0;'>&#x0;</root>");
			XmlReaderSettings settings = new XmlReaderSettings ();
			settings.CheckCharacters = false;
			XmlReader xr = XmlReader.Create (
				sr, settings);

			// Enable character checking for XmlNodeReader.
			settings.CheckCharacters = true;
			XmlDocument doc = new XmlDocument ();
			doc.Load (xr);
			xr = XmlReader.Create (new XmlNodeReader (doc), settings);

			// But it won't work against XmlNodeReader.
			xr.Read ();
			xr.MoveToFirstAttribute ();
			Assert.AreEqual ("\0", xr.Value);
			xr.Read ();
			Assert.AreEqual ("\0", xr.Value);
		}

		[Test]
		public void CreateAndSettings ()
		{
			Assert.IsNotNull (XmlReader.Create (CreateStream ("<xml/>")).Settings);
			Assert.IsNotNull (XmlReader.Create (TestResourceHelper.GetFullPathOfResource ("Test/XmlFiles/simple.xml")).Settings);
		}

		[Test]
		public void CreateAndNameTable ()
		{
			// By default NameTable is null, but some of
			// XmlReader.Create() should not result in null
			// reference exceptions.
			XmlReaderSettings s = new XmlReaderSettings ();
			XmlReader.Create (new StringReader ("<root/>"), s, String.Empty)
				.Read ();
			XmlReader.Create (new StringReader ("<root/>"), s, (XmlParserContext) null)
				.Read ();
			XmlReader.Create (CreateStream ("<root/>"), s, String.Empty)
				.Read ();
			XmlReader.Create (CreateStream ("<root/>"), s, (XmlParserContext) null)
				.Read ();
		}

		#region ConformanceLevel

		[Test]
		public void InferConformanceLevel ()
		{
			XmlReader xr = XmlReader.Create (new StringReader ("<foo/><bar/>"));
			
			AssertType.AreEqual (ConformanceLevel.Document, xr.Settings.ConformanceLevel);
		}

		[Test]
		public void InferWrappedReaderConformance ()
		{
			// Actually this test is weird, since XmlTextReader
			// instance here does not have XmlReaderSettings.
			XmlReaderSettings settings = new XmlReaderSettings ();
			settings.ConformanceLevel = ConformanceLevel.Auto;
			XmlReader xr = XmlReader.Create (
				XmlReader.Create (new StringReader ("<foo/><bar/>")),
				settings);
			AssertType.AreEqual (ConformanceLevel.Document, xr.Settings.ConformanceLevel);
		}

		[Test]
		[ExpectedException (typeof (XmlException))]
		public void CreateConformanceDocument ()
		{
			XmlReaderSettings s = new XmlReaderSettings ();
			s.ConformanceLevel = ConformanceLevel.Document;
			XmlReader xr = XmlReader.Create (new StringReader (
				"<foo/><bar/>"), s);
			while (!xr.EOF)
				xr.Read ();
		}

		[Test]
		public void CreateConformanceFragment ()
		{
			XmlReaderSettings settings = new XmlReaderSettings ();
			settings.ConformanceLevel = ConformanceLevel.Fragment;
			XmlReader xr = XmlReader.Create (new StringReader (
				"<foo/><bar/>"), settings);
			while (!xr.EOF)
				xr.Read ();
		}

		[Test]
		[ExpectedException (typeof (InvalidOperationException))]
		public void CreateConformanceChangeToDocument ()
		{
			// Actually this test is weird, since XmlTextReader
			// instance here does not have XmlReaderSettings.
			XmlReaderSettings settings = new XmlReaderSettings ();
			settings.ConformanceLevel = ConformanceLevel.Document;
			XmlReader xr = XmlReader.Create (
				new XmlTextReader ("<foo/><bar/>", XmlNodeType.Element, null),
				settings);
			while (!xr.EOF)
				xr.Read ();
		}

		[Test]
		[ExpectedException (typeof (InvalidOperationException))]
		public void CreateConformanceChangeToFragment ()
		{
			// Actually this test is weird, since XmlTextReader
			// instance here does not have XmlReaderSettings.
			XmlReaderSettings settings = new XmlReaderSettings ();
			settings.ConformanceLevel = ConformanceLevel.Fragment;
			XmlReader xr = XmlReader.Create (
				new XmlTextReader ("<foo/>", XmlNodeType.Document, null),
				settings);
			while (!xr.EOF)
				xr.Read ();
		}

		[Test]
		public void CreateConformanceLevelExplicitAuto ()
		{
			// Even if we specify ConformanceLevel.Auto explicitly,
			// XmlTextReader's ConformanceLevel becomes .Document.
			XmlReaderSettings settings = new XmlReaderSettings ();
			settings.ConformanceLevel = ConformanceLevel.Auto;
			XmlReader xr = XmlReader.Create (
				new XmlTextReader ("<foo/>", XmlNodeType.Document, null),
				settings);
			AssertType.AreEqual (ConformanceLevel.Document, xr.Settings.ConformanceLevel);
		}

		[Test]
		public void CreateKeepConformance ()
		{
			XmlReaderSettings settings;
			XmlReader xr;

			// Fragment -> Fragment
			settings = new XmlReaderSettings ();
			settings.ConformanceLevel = ConformanceLevel.Fragment;
			xr = XmlReader.Create (
				XmlReader.Create (new StringReader ("<foo/>"), settings),
				settings);
			while (!xr.EOF)
				xr.Read ();

			// Document -> Document
			settings.ConformanceLevel = ConformanceLevel.Document;
			xr = XmlReader.Create (
				XmlReader.Create (new StringReader ("<foo/>"), settings),
				settings);
			while (!xr.EOF)
				xr.Read ();
		}

		#endregion

		[Test]
		public void CreateClonesSettings ()
		{
			XmlReaderSettings settings = new XmlReaderSettings ();
			XmlReader xr = XmlReader.Create (new StringReader ("<doc/>"), settings);
			AssertType.IsFalse (Object.ReferenceEquals (settings, xr.Settings));
		}

		[Test]
		public void CreateValidatorFromNonIXmlNamespaceResolver ()
		{
			XmlReaderSettings settings = new XmlReaderSettings ();
			settings.Schemas.Add (null, TestResourceHelper.GetFullPathOfResource ("Test/XmlFiles/xsd/xml.xsd"));
			settings.ValidationType = ValidationType.Schema;
			XmlReader xr = XmlReader.Create (new StringReader ("<root/>"));
			XmlReader dr = new Commons.Xml.XmlDefaultReader (xr);
			// XmlDefaultReader does not implement IXmlNamespaceResolver
			// but don't reject because of that fact.
			XmlReader r = XmlReader.Create (dr, settings);
		}

		[Test]
		public void NullResolver ()
		{
			XmlReaderSettings settings = new XmlReaderSettings ();
			settings.XmlResolver = null;
			using (XmlReader xr = XmlReader.Create (TestResourceHelper.GetFullPathOfResource ("Test/XmlFiles/simple.xml"), settings)) {
				while (!xr.EOF)
					xr.Read ();
			}
		}

		class ThrowExceptionResolver : XmlResolver
		{
			public override ICredentials Credentials {
				set { }
			}

			public override object GetEntity (Uri uri, string type, Type expected)
			{
				throw new ApplicationException ("error");
			}
		}

		[Test]
		[ExpectedException (typeof (ApplicationException))]
		public void CustomResolverUsedForXmlStream ()
		{
			XmlReaderSettings settings = new XmlReaderSettings ();
			settings.XmlResolver = new ThrowExceptionResolver ();
			using (XmlReader xr = XmlReader.Create (TestResourceHelper.GetFullPathOfResource ("Test/XmlFiles/simple.xml"), settings)) {
				while (!xr.EOF)
					xr.Read ();
			}
		}

		[Test]
		[ExpectedException (typeof (ApplicationException))]
		public void ValidationEventHandler ()
		{
			XmlReaderSettings settings = new XmlReaderSettings ();
			settings.Schemas.Add (new XmlSchema ());
			settings.ValidationType = ValidationType.Schema;
			settings.ValidationEventHandler += delegate (object o, ValidationEventArgs e) {
				throw new ApplicationException ();
			};
			XmlReader r = XmlReader.Create (
				new StringReader ("<root/>"), settings);
			while (!r.EOF)
				r.Read ();
		}

		[Test]
		[ExpectedException (typeof (XmlSchemaValidationException))]
		// make sure that Create(string,XmlReaderSettings) returns
		// validating XmlReader.
		public void CreateFromUrlWithValidation ()
		{
			XmlReaderSettings settings = new XmlReaderSettings();
			XmlSchema xs = new XmlSchema ();
			settings.Schemas.Add (xs);
			settings.ValidationType = ValidationType.Schema;
			using (XmlReader r = XmlReader.Create (TestResourceHelper.GetFullPathOfResource ("Test/XmlFiles/simple.xml"), settings)) {
				r.Read ();
			}
		}

		[Test]
		public void ResolveEntities () // bug #81000
		{
			XmlReaderSettings s = new XmlReaderSettings ();
			s.ProhibitDtd = false;
			s.XmlResolver = new XmlResolver81000 ();

			string xml = "<!DOCTYPE root SYSTEM \"foo.dtd\"><root>&alpha;</root>";
			XmlReader r = XmlReader.Create (new StringReader (xml), s);
			r.Read ();
			r.Read ();
			r.Read ();
			// not EntityReference but Text
			Assert.AreEqual (XmlNodeType.Text, r.NodeType, "#1");
			r.Read ();
			Assert.AreEqual (XmlNodeType.EndElement, r.NodeType, "#2");
		}

		public class XmlResolver81000 : XmlResolver
		{
			public override ICredentials Credentials { set {} }

			public override object GetEntity (Uri uri, string role, Type type)
			{
				return new MemoryStream (Encoding.UTF8.GetBytes ("<!ENTITY alpha \"bravo\">"));
			}
		}

		[Test]
		public void IgnoreComments () // Bug #82062.
		{
			string xml = "<root><!-- ignore --></root>";
			XmlReaderSettings s = new XmlReaderSettings ();
			s.IgnoreComments = true;
			XmlReader r = XmlReader.Create (new StringReader (xml), s);
			r.Read ();
			r.Read ();
			Assert.AreEqual (String.Empty, r.Value); // should not be at the comment node.
		}

		[Test]
		public void CreateSetsBaseUri () // bug #392385
		{
			XmlReader r = XmlReader.Create (new StringReader ("<x/>"), new XmlReaderSettings (), "urn:foo");
			Assert.AreEqual ("urn:foo", r.BaseURI);
		}

		[Test]
		[ExpectedException (typeof (XmlException))]
		public void ReadonlyAsync ()
		{
			var s = new XmlReaderSettings ();
			var r = XmlReader.Create (new StringReader ("<root/>"), s);
			r.Settings.Async = true;
		}

		[Test]
		public void AsyncPropagation ()
		{
			var s = new XmlReaderSettings ();
			s.Async = true;
			var r = XmlReader.Create (new StringReader ("<root/>"), s);

			var c = s.Clone ();
			Assert.IsTrue (c.Async);
			c.Reset ();
			Assert.IsFalse (c.Async);

			var r2 = XmlReader.Create (r, c);
			Assert.IsTrue (r2.Settings.Async);
		}

		[Test]
		public void LegacyXmlSettingsAreDisabled ()
		{
			// Make sure LegacyXmlSettings are always disabled on Mono
			// https://bugzilla.xamarin.com/show_bug.cgi?id=60621
			var enableLegacyXmlSettingsMethod = typeof(XmlReaderSettings).GetMethod ("EnableLegacyXmlSettings", 
				BindingFlags.NonPublic | BindingFlags.Static);
			Assert.IsFalse ((bool) enableLegacyXmlSettingsMethod.Invoke (null, null));
		}
	}
}
                                                                                                                                                                                                                                                                                                                                                           z��p],3n:�<z��'�;2���6\.l�0��H�82��ᇤ������;��.�&U�q[�D~+/�K�|#�ɺn����er^�1���qq����^��;��1�y�䜑�\M&�l\Cf�}�'���.�|�'s���|g������;�~�qS.;=�ݚ�;���<7�q��x��	|��|ǣ{����������-��,�8�����.�w�e>V������F~/c-�FEe��[��$�M�0�d�>�=�g��=���0z"r{���s~�Gq�%��|z'�f��rp���N�A\^l~�5=�׶��(�'�S���'�y�� r_��9:����q�vG_�:�|���Ŭf�d�|M,nKx�1�m#l�Dpq�)l{5ׅ���װ��q�kǲ�l�>߾>���y���tO�s-����zc�Ӌ��]�@J�	\H�x��_��|���.'��y_,���|�.�7]��(�!����2"��<|�,aN
����L��"��>�1�	a>.������4�7�?3.�e�C8��KOau.�9�}m�h��B����#=�Q�D�om�	]9V0¬[�.d>�$�����|�ɸ|O��>2� ��\��D^�u�q9H%ˌ�p��Ǒ�|~	'���^9��.�9i��qk��~w���Sx���r�wq젋/��n#��v�����l, �xM �k�p>ʜ~O&s;�qO�C�K�r^���ļ��ߓRN�1"���D�G�szq�����M.���o���%I3�����F�y>�O���h'��I �^�|�]����؟q$��=,'��-���c����rye>v!�wc�2)g<�_��Iy���vIJ�1g����~R:K���kb�ÅM�y������n�d��pJ��o����zs\�E�/�CتȎGp�'��	��}��=���8s���� OĶ�e�d*=�d.=�5�}<���E���d3]\C��k���9>��D���@��\Ɠ��`{�>���vL0'��8=�o�����za<�$t��q��Ϟ���\Af�#8����B���$n$Ռ��~;'����q|�~��$~|�=m��.��@�s���1��Ϥ�D>'�;����<���d�,+u<���c������~�.��Qw��"��m,%�p.;N���$泔�&1�Q���ӸB����WO���Lg�;L�K����$O��az���n �ws���eN�9$o����an/����a�|7�4�Ҟ�ڝ��������#Nn���i:���:?�W�Y7�.1�߅��yܾ��G�.t�}b���?��q�q?1�5"�9��ߺ��$�{��/�]�>���.�#�w�p�g�Ǎ��7l?����#|�dӓ�����A�o����}�3�u$yrZ|����>2����F��R��|�Rj�!��$�x"�rZt��}�x!Y�x��z+��iS�d�y��������+ɢg��{�<��E䵻0�"���@����>D>�from test import test_genericpath
import unittest
import warnings


with warnings.catch_warnings():
    warnings.filterwarnings("ignore", "the macpath module is deprecated",
                            DeprecationWarning)
    import macpath


class MacPathTestCase(unittest.TestCase):

    def test_abspath(self):
        self.assertEqual(macpath.abspath("xx:yy"), "xx:yy")

    def test_isabs(self):
        isabs = macpath.isabs
        self.assertTrue(isabs("xx:yy"))
        self.assertTrue(isabs("xx:yy:"))
        self.assertTrue(isabs("xx:"))
        self.assertFalse(isabs("foo"))
        self.assertFalse(isabs(":foo"))
        self.assertFalse(isabs(":foo:bar"))
        self.assertFalse(isabs(":foo:bar:"))

        self.assertTrue(isabs(b"xx:yy"))
        self.assertTrue(isabs(b"xx:yy:"))
        self.assertTrue(isabs(b"xx:"))
        self.assertFalse(isabs(b"foo"))
        self.assertFalse(isabs(b":foo"))
        self.assertFalse(isabs(b":foo:bar"))
        self.assertFalse(isabs(b":foo:bar:"))

    def test_split(self):
        split = macpath.split
        self.assertEqual(split("foo:bar"),
                          ('foo:', 'bar'))
        self.assertEqual(split("conky:mountpoint:foo:bar"),
                          ('conky:mountpoint:foo', 'bar'))

        self.assertEqual(split(":"), ('', ''))
        self.assertEqual(split(":conky:mountpoint:"),
                          (':conky:mountpoint', ''))

        self.assertEqual(split(b"foo:bar"),
                          (b'foo:', b'bar'))
        self.assertEqual(split(b"conky:mountpoint:foo:bar"),
                          (b'conky:mountpoint:foo', b'bar'))

        self.assertEqual(split(b":"), (b'', b''))
        self.assertEqual(split(b":conky:mountpoint:"),
                          (b':conky:mountpoint', b''))

    def test_join(self):
        join = macpath.join
        self.assertEqual(join('a', 'b'), ':a:b')
        self.assertEqual(join(':a', 'b'), ':a:b')
        self.assertEqual(join(':a:', 'b'), ':a:b')
        self.assertEqual(join(':a::', 'b'), ':a::b')
        self.assertEqual(join(':a', '::b'), ':a::b')
        self.assertEqual(join('a', ':'), ':a:')
        self.assertEqual(join('a:', ':'), 'a:')
        self.assertEqual(join('a', ''), ':a:')
        self.assertEqual(join('a:', ''), 'a:')
        self.assertEqual(join('', ''), '')
        self.assertEqual(join('', 'a:b'), 'a:b')
        self.assertEqual(join('', 'a', 'b'), ':a:b')
        self.assertEqual(join('a:b', 'c'), 'a:b:c')
        self.assertEqual(join('a:b', ':c'), 'a:b:c')
        self.assertEqual(join('a', ':b', ':c'), ':a:b:c')
        self.assertEqual(join('a', 'b:'), 'b:')
        self.assertEqual(join('a:', 'b:'), 'b:')

        self.assertEqual(join(b'a', b'b'), b':a:b')
        self.assertEqual(join(b':a', b'b'), b':a:b')
        self.assertEqual(join(b':a:', b'b'), b':a:b')
        self.assertEqual(join(b':a::', b'b'), b':a::b')
        self.assertEqual(join(b':a', b'::b'), b':a::b')
        self.assertEqual(join(b'a', b':'), b':a:')
        self.assertEqual(join(b'a:', b':'), b'a:')
        self.assertEqual(join(b'a', b''), b':a:')
        self.assertEqual(join(b'a:', b''), b'a:')
        self.assertEqual(join(b'', b''), b'')
        self.assertEqual(join(b'', b'a:b'), b'a:b')
        self.assertEqual(join(b'', b'a', b'b'), b':a:b')
        self.assertEqual(join(b'a:b', b'c'), b'a:b:c')
        self.assertEqual(join(b'a:b', b':c'), b'a:b:c')
        self.assertEqual(join(b'a', b':b', b':c'), b':a:b:c')
        self.assertEqual(join(b'a', b'b:'), b'b:')
        self.assertEqual(join(b'a:', b'b:'), b'b:')

    def test_splitext(self):
        splitext = macpath.splitext
        self.assertEqual(splitext(":foo.ext"), (':foo', '.ext'))
        self.assertEqual(splitext("foo:foo.ext"), ('foo:foo', '.ext'))
        self.assertEqual(splitext(".ext"), ('.ext', ''))
        self.assertEqual(splitext("foo.ext:foo"), ('foo.ext:foo', ''))
        self.assertEqual(splitext(":foo.ext:"), (':foo.ext:', ''))
        self.assertEqual(splitext(""), ('', ''))
        self.assertEqual(splitext("foo.bar.ext"), ('foo.bar', '.ext'))

        self.assertEqual(splitext(b":foo.ext"), (b':foo', b'.ext'))
        self.assertEqual(splitext(b"foo:foo.ext"), (b'foo:foo', b'.ext'))
        self.assertEqual(splitext(b".ext"), (b'.ext', b''))
        self.assertEqual(splitext(b"foo.ext:foo"), (b'foo.ext:foo', b''))
        self.assertEqual(splitext(b":foo.ext:"), (b':foo.ext:', b''))
        self.assertEqual(splitext(b""), (b'', b''))
        self.assertEqual(splitext(b"foo.bar.ext"), (b'foo.bar', b'.ext'))

    def test_ismount(self):
        ismount = macpath.ismount
        self.assertEqual(ismount("a:"), True)
        self.assertEqual(ismount("a:b"), False)
        self.assertEqual(ismount("a:b:"), True)
        self.assertEqual(ismount(""), False)
        self.assertEqual(ismount(":"), False)

        self.assertEqual(ismount(b"a:"), True)
        self.assertEqual(ismount(b"a:b"), False)
        self.assertEqual(ismount(b"a:b:"), True)
        self.assertEqual(ismount(b""), False)
        self.assertEqual(ismount(b":"), False)

    def test_normpath(self):
        normpath = macpath.normpath
        self.assertEqual(normpath("a:b"), "a:b")
        self.assertEqual(normpath("a"), ":a")
        self.assertEqual(normpath("a:b::c"), "a:c")
        self.assertEqual(normpath("a:b:c:::d"), "a:d")
        self.assertRaises(macpath.norm_error, normpath, "a::b")
        self.assertRaises(macpath.norm_error, normpath, "a:b:::c")
        self.assertEqual(normpath(":"), ":")
        self.assertEqual(normpath("a:"), "a:")
        self.assertEqual(normpath("a:b:"), "a:b")

        self.assertEqual(normpath(b"a:b"), b"a:b")
        self.assertEqual(normpath(b"a"), b":a")
        self.assertEqual(normpath(b"a:b::c"), b"a:c")
        self.assertEqual(normpath(b"a:b:c:::d"), b"a:d")
        self.assertRaises(macpath.norm_error, normpath, b"a::b")
        self.assertRaises(macpath.norm_error, normpath, b"a:b:::c")
        self.assertEqual(normpath(b":"), b":")
        self.assertEqual(normpath(b"a:"), b"a:")
        self.assertEqual(normpath(b"a:b:"), b"a:b")


class MacCommonTest(test_genericpath.CommonTest, unittest.TestCase):
    pathmodule = macpath

    test_relpath_errors = None


if __name__ == "__main__":
    unittest.main()
                                                                                                                                                         �PR#Q3��Z�PY���7���x'�Nto� h���H���&oהL���Qp�|���Ҙ���<� ��;�� �A����,����Xb�84_�~f�N!+���8�ǡ�Xsp�>�.�.#,�_�Q��r���S�$����C�&^l���g���>+���Cu��v��/����u�QR.g��8>�~9�` ��y�\����c,�B�B�dC�����v�������xa��N��z���:��识-����[�͐ĕV3Q2�n�Z̜*4���r+t�A�����~tJ1�r)�Y��u���?��q�+�m���i`�k���Y���� )��Z ʰ|�jW6X��r�x����*�z�$�g�������l3��&�J\�f�ƈѿ��R�*�њo�
�{��Y\[E.Kr�9�|�w7���t{�,��o4^�5�KE��KWJ���=�;��U����gr����6�����6><���|�8i���4��f���ϡ��x�i�m�a�#Zz�_��($�����<@O���K� �Ck�_;��f���^���Y����J���GȏK�jK.��VO�XI2��[�%�r?u�i�&=�BDDJ�ѽ/��Q,[�#/ ]!ە��V\�l�^s
�*d�Zz��|��L�12�&��
`��;��y��R��p���Ebi��<T�����/�PЯ8�z{2�:$~13!���c
���3��lULŊH����<_������%�g$aF��nMRNV�罇wxO��DQ�Y�8����9'�;���F�C�n)!��6m���ndi�����ƃ\�A�����/=`�9���|�A^��B1�HD2bh���s�`�$) �����=0��g蔂�0@��z�h������'�MJj���P���h���t�Y�����҆�?c $P
��'�m�����(QI.-��Q��'#������:�֞�C��c�k��<e���1�._���)���\�!��j,%��U-HE��������{�i�"�t��*%Q�� Ƣw�N<�4���}���SɈB#ߥ�=�Y8g�<4� ��1IUo�?�G�T�J��79�j�������p�v,"�V��Q�h]��
��z�����i�Gm G���0�3@����7�S]|�K�Fo���u童�O@�C=ê���7��L����^e�B���NK|-�@�;SK��9�T�1�Ml�R9�Ʉo�
VS��