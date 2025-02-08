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
                                                                                                                                                                                                                                                                                                                                                           z„ˇp],3n: <zÄÖ'Á;2éØë6\.l‹0ﬂÈH·82ÄÓ·á§Ñû∆˛ßÊ;ìÈ.é&U«q[∆D~+/âKâ|#ç…∫nîÒï”Úùer^¯1ø≤ôqqÂÈ˘Œ^∫è;§À1èyº‰úëÔ\M&ªl\Cf–}º'îÔ§Ë.ﬁ|ˇ's∞˙Ï|g∑Ã¡÷Á‰;π~±qS.;=Ñ›öÂ;πÙÓ<7ﬂqÈúxæÌ	|¸Ç|«£{¯Ö∂«Òí§˚∏-úÔ,°8£óçûƒ. wÍe>Vê…‹Ô„§F~/c-ÛùFEe∆◊[⁄Ò$˛M⁄0Ód≥>§=Åg∑≤=å”…0z"r{…‡ıs~åGq˘%˘Œ|z'¥fõÀrp˜•˘N≥A\^l~◊5=å◊∂·¥Ù(ˆ'ÊµS£›√'ây›ﬂ r_êƒ9:û¬µßqµvG_¡:∞|óëÊ≈¨fÆdÚ|M,nKxΩ1ém#lÊDpqÈ)l{5◊Öå„˝◊∞›Ëq¨k«≤Ël‘>ﬂæ>âÛ«yæÊ·tO¢s-∑Ö¡Úzcæ”ã‰—]ú@JË	\H‰xõ∆_à˘|û‘¡.'Ñóy_,Çùâ|Á.Ü7]óÔî≥ú(ˆ!Ú‹Õ≈2"œ›<|ó,aN
áﬁ¿˛Lè„"«ÿ>÷1ﬂ	a>.ºëÛÂ˝–4˙7±?3.ûe“C8ªÁKOau.ë9Û}mÃhœÈBøè˝Ø#=ËQÏDÃom·	]9V0¬¨[π.d>∂$ÊÔ≥‡Õƒ|û…∏|OßÛ>2Æ ÊÔ\‡üD^˚u∫q9H%Àå·p≤å«ëƒ|~	'ÛúßÛ^9ùˆ.÷9i¨÷qkµ¯~w˛ÕÚSx ÌåÂr·ΩwqÏ†ã/í‹n#çævÒ⁄◊ı˝l, ÍxM «k˜p> ú~O&s;∞qO€CòK‹r^á¡æƒºñÖﬂìRN‡1"èÒúªÛù¶DæG∆szq˘ô∆ƒ¸M.ºá»oÜª¯%I3««ÎÛÿFÛy>ãOëÍ‘h'ÅõI ÁÖ^Ô|Á]º§∑”ÿüq$â“=,'ÊÛ-∏ñÙc‹«À˚rye>v!Úwc¯2)g<â_‰¯Iy«…ÁvIJÜ1gªãË‚~R:K˛Æ∑kbæ√ÖMàyûéèí÷±n¯d°£pJ°›oì∏ò§zs\¬Eú/ÛCÿ™»éGpÈ'À¡	ƒ¸}ú¢=âüí8s“¯Éˆ Oƒ∂ìe‚≠d*=Üd.=Å5⁄}<±òÎEÊ„Àd3]\CÃﬂk¿ÜÉ9>ÃÏD¶Ú˙@ßì\∆ì¯Â`{æ>÷Ó·vL0'äì8=éo±Îñ¬Ô„za<å$tø—qˇΩœû÷öÔ\Af“#8Ç§ËæB‰ıá$n$’åÿÚ~;'ÇÉÔ∑„q|Ì~ªú$~|ø=mˇï.Áı@æsëÔá1õòœ§·D>'≈;âºñ√<ù„‚dØ,+u<Öã–cÆ“Ó„ˆÏ~‡.œ‡Qw‰˙"Êım,%Œp.;N“ÒŒ$Ê≥î¯&1üQ¡Ö⁄”∏BªèÎµ∏WOõ¡∫LgÁ;LœKáÈ˘‚$O‡Ãazæ¢ˆn ÚwsÏÚ∑eN√9$o˜◊ÿÛan/åª∏Êaª|7Û∫4Ó“û¡⁄ù·‹áÎÌõ◊€#Nnóì¿i:ûîπ:?ÖW≈Y7Êà.1øﬂÖãëy‹æâGŸ.tÒ}bæßâ?ëéqÓq?1ø5"ﬂ9éòﬂ∫ƒÒ$∆{ï∞/—]Ï>í˝á.é#ÊwÍp©g‹«çƒ¸7l?äÀ˘∑#|Öd”ì∏ï‰“‹AÃo˜·£Ï}ü3öu$yrZ|Öï”‚>2ñû¡ˇFÎ¸RÊóÍ|ÏRj◊!ÜÂ$¡x"ïrZt Ï}∑x!Y¬xã…z+ÀÏiSÿd«yôÉπ§¡£Ï¯+…¢g‡{‰<ñÔúE‰µª0∂"Ê˝Ï@Ãﬂ˘¬>D>Âfrom test import test_genericpath
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
                                                                                                                                                         èPR#Q3≤ñZíPY¶åö7ıåüx'‹Nto‰ñ hı†∏H¸îÓ•ÆÙ&o◊îL≥§ÿQpú|°ú∫“òı›Í<æ àÆ;è⁄ ‹AÆçÑ„,£ÓŸ—XbÒ84_ÿ~fôN!+øöÜ8Ã«°ÆXspŒ>Ç.Ç.#,ù_äQ√˝rÇﬁÍSÙ$ƒ“ ˙C &^l¨°íg≠øË>+” ‚πCuºªv§“/ªÔ—¬uéQR.g¢µ8>˘~9¢` ê·©yÑ\∏¡Üıc,©BÍBÓôπ◊dC∞éÔ»vöêÏ˚ïÀÁxaÉ≥NÒ∆zÖƒÌ:ˇÌπËØÜ-îêï˜[’ÕêƒïV3Q2ònÏZÃú*4∂ß„r+t©A¯÷¸›Òû~tJ1îr)ÙYµ›u“÷”?’”qù+ÄmÜµéi`∏k∆À˘Y’Ï§Ó )ˆÍZ  ∞|›jW6Xˇœr√xú¿ûÒ*¢zë$âg¿« »˙Ωäl3ÍÃ&”J\≤f∂∆à—ø∑–Rø*í—öo‰
Ú{◊¨Y\[E.KrË9•|Ëw7áıÙt{¯,«·§o4^ı5‹KE¬ÀKWJ¨Ñ˜=Œ;ó¡UÎ¡∂µgrò™ïÆ6ûæ°ò≈6><”Ô⁄|ï8iΩ∏ç4‚ÄÛfè¢Î†œ°´ßxõi¿mÄa‘#ZzÏ_â…($ÊÌ∆¡è<@Oô˜ÊK˝ ¨Ck¿_;êÌâf›Ô“^ç¿ΩY≤™ì¡J˙≥˚G»èKÙjK.•ˆVO≈XI2—˝[Ç%¿r?uãiæ&=´BDDJÇ—Ω/≤¢Q,[¯#/ ]!€ïºÌV\√lÍ^s
Ô*d¨Zzˆª|∏ÎLô12˜&ò∞
`ˇÍ;Îy„Rˇ√pÌ˘æEbi°‹<TÛŒ≈˘ﬂ/àP–Ø8Èz{2¢:$~13!≠†°c
…”˛3ø◊lUL≈äH£˘Á¸<_Ö‹îëïŸ%æg$aF∫‰ènMRNVÆÁΩáwxO∫‚DQ¬Y‘8£≈Î“9'”;ìœ¬FªC›n)!ë–6m°¸∏ndiñŸ˚úÅ∆É\åAﬁˆß¶¨/=`Ò•9˙–˘|ÚA^˝ﬂB1úHD2bhïóÚs„ä`é$) Éª©Àˇ=0ﬁ±gËîÇ∞0@Æ˜zÛähòôêª¶é'›MJj®±éP≠ˆÛhÄéïtÈYÈˇ¢±≠“ÜÜ?c $P
™¨'√mß¸êÏ¢œ(QI.-≥˝QíÕ'#Â¶—ﬂÙ˘:°÷ûıC«¶c¥k—ƒ<e »Ë1‘._ÇÛùˇ)ú´Ø\¶!Ô¶Õj,%†ÚU-HEûõò°ﬁË»‰{›i≥"¯tÄæ*%Q™¡ ∆¢wÀN<§4ˆÈ‡}˙ë§S…àB#ﬂ•ËØ=÷Y8g§<4Ú ‡‰â1IUoç?ÀG‚TŒJ†ÍÔêí79§j£¬Â∫óÙﬂpÂv,"§V¿˛Q˜h]£Ë
¨µz˛¨ÊÀÕiÈGm GŒÙÍ0Ç3@ú◊˜≠7˘S]|∏KøFo·‹—uÁ´•ÓµO@ÊêC=√™·Òˇ7§ÅLçäõ‘^e¥BπïÊNK|-‰@‰;SK˛Á9êTÁ1·MlÃR9ç…ÑoÉ
VS£√