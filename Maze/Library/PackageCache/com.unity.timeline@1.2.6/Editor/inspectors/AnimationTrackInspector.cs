// XmlAttributeTests.cs : Tests for the XmlAttribute class
//
// Author: Mike Kestner <mkestner@speakeasy.net>
// Author: Martin Willemoes Hansen <mwh@sysrq.dk>
//
// (C) 2002 Mike Kestner
// (C) 2003 Martin Willemoes Hansen

using System;
using System.IO;
using System.Xml;

using NUnit.Framework;

namespace MonoTests.System.Xml
{
	[TestFixture]
	public class XmlAttributeTests
	{
		XmlDocument doc;
		XmlAttribute attr;
		bool inserted;
		bool changed;
		bool removed;

		[SetUp]
		public void GetReady()
		{
			doc = new XmlDocument ();
			attr = doc.CreateAttribute ("attr1");
			attr.Value = "val1";
		}

		private void EventNodeInserted(Object sender, XmlNodeChangedEventArgs e)
		{
			inserted = true;
		}

		private void EventNodeChanged(Object sender, XmlNodeChangedEventArgs e)
		{
			changed = true;
		}

		private void EventNodeRemoved(Object sender, XmlNodeChangedEventArgs e)
		{
			removed = true;
		}

		[Test]
		public void Attributes ()
		{
			Assert.IsNull (attr.Attributes);
		}

		[Test]
		public void AttributeInnerAndOuterXml ()
		{
			attr = doc.CreateAttribute ("foo", "bar", "http://abc.def");
			attr.Value = "baz";
			Assert.AreEqual ("baz", attr.InnerXml, "#1");
			Assert.AreEqual ("foo:bar=\"baz\"", attr.OuterXml, "#2");
		}

		[Test]
		public void AttributeWithNoValue ()
		{
			XmlAttribute attribute = doc.CreateAttribute ("name");
			Assert.AreEqual (String.Empty, attribute.Value, "#1");
			Assert.IsFalse (attribute.HasChildNodes, "#2");
			Assert.IsNull (attribute.FirstChild, "#3");
			Assert.IsNull (attribute.LastChild, "#4");
			Assert.AreEqual (0, attribute.ChildNodes.Count, "#5");
		}

		[Test]
		public void AttributeWithValue ()
		{
			XmlAttribute attribute = doc.CreateAttribute ("name");
			attribute.Value = "value";
			Assert.AreEqual ("value", attribute.Value, "#1");
			Assert.IsTrue (attribute.HasChildNodes, "#2");
			Assert.IsNotNull (attribute.FirstChild, "#3");
			Assert.IsNotNull (attribute.LastChild, "#4");
			Assert.AreEqual (1, attribute.ChildNodes.Count, "#5");
			Assert.AreEqual (XmlNodeType.Text, attribute.ChildNodes [0].NodeType, "#6");
			Assert.AreEqual ("value", attribute.ChildNodes [0].Value, "#7");
		}

		[Test]
		public void CheckPrefixWithNamespace ()
		{
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml ("<root xmlns:foo='urn:foo' foo='attfoo' foo:foo='attfoofoo' />");
			// hogehoge does not match to any namespace.
			Assert.AreEqual ("xmlns:foo", doc.DocumentElement.Attributes [0].Name);
			try {
				doc.DocumentElement.Attributes [0].Prefix = "hogehoge";
				doc.Save (TextWriter.Null);
				Assert.Fail ("#1");
			} catch (ArgumentException ex) {
				// Cannot bind to the reserved namespace
				Assert.AreEqual (typeof (ArgumentException), ex.GetType (), "#2");
				Assert.IsNull (ex.InnerException, "#3");
				Assert.IsNotNull (ex.Message, "#4");
				Assert.IsNull (ex.ParamName, "#5");
			}
		}

		[Test]
		public void NamespaceAttributes ()
		{
			try {
				doc.CreateAttribute (string.Empty, "xmlns", "urn:foo");
				Assert.Fail ("#A1");
			} catch (ArgumentException ex) {
				// The namespace declaration attribute has an
				// incorrect namespaceURI: urn:foo
				Assert.AreEqual (typeof (ArgumentException), ex.GetType (), "#A2");
				Assert.IsNull (ex.InnerException, "#A3");
				Assert.IsNotNull (ex.Message, "#A4");
				Assert.IsNull (ex.ParamName, "#A5");
			}

			doc.LoadXml ("<root/>");

			try {
				doc.DocumentElement.SetAttribute ("xmlns", "urn:foo", "urn:bar");
				Assert.Fail ("#B1");
			} catch (ArgumentException ex) {
				// The namespace declaration attribute has an
				// incorrect namespaceURI: urn:foo
				Assert.AreEqual (typeof (ArgumentException), ex.GetType (), "#B2");
				Assert.IsNull (ex.InnerException, "#B3");
				Assert.IsNotNull (ex.Message, "#B4");
				Assert.IsNull (ex.ParamName, "#B5");
			}
		}

		[Test]
		public void HasChildNodes ()
		{
			Assert.IsTrue (attr.HasChildNodes, "#1");
			XmlAttribute attr2 = doc.CreateAttribute ("attr2");
			Assert.IsFalse (attr2.HasChildNodes, "#2");
		}

		[Test]
		public void Name ()
		{
			Assert.AreEqual ("attr1", attr.Name);
		}

		[Test]
		public void NodeType ()
		{
			Assert.AreEqual (XmlNodeType.Attribute, attr.NodeType);
		}

		[Test]
		public void OwnerDocument ()
		{
			Assert.AreSame (doc, attr.OwnerDocument);
		}

		[Test]
		public void ParentNode ()
		{
			Assert.IsNull (attr.ParentNode, "Attr parents not allowed");
		}

		[Test]
		public void Value ()
		{
			Assert.AreEqual ("val1", attr.Value, "#1");
			XmlAttribute attr2 = doc.CreateAttribute ("attr2");
			Assert.AreEqual (string.Empty, attr2.Value, "#2");
		}

		[Test]
		[Category ("NotDotNet")] // https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=336180
		public void SetInnerTextAndXml ()
		{
			string original = doc.OuterXml;
			doc.LoadXml ("<root name='value' />");
			XmlAttribute attr = doc.DocumentElement.Attributes ["name"];
			attr.InnerText = "a&b";
			Assert.AreEqual ("a&b", attr.Value, "setInnerText");
			attr.InnerXml = "a&amp;b";
			Assert.AreEqual ("a&b", attr.Value, "setInnerXml");
			attr.InnerXml = "'a&amp;b'";
			Assert.AreEqual ("'a&amp;b'", attr.InnerXml, "setInnerXml.InnerXml");
			Assert.AreEqual ("'a&b'", attr.Value, "setInnerXml.Value");
			attr.InnerXml = "\"a&amp;b\"";
			Assert.AreEqual ("\"a&amp;b\"", attr.InnerXml, "Double_Quote");
			attr.InnerXml = "\"a&amp;b'";
			Assert.AreEqual ("\"a&amp;b'", attr.InnerXml, "DoubleQuoteStart_SingleQuoteEnd");

			attr.Value = string.Empty;
			XmlNodeChangedEventHandler evInserted = new XmlNodeChangedEventHandler (EventNodeInserted);
			XmlNodeChangedEventHandler evChanged = new XmlNodeChangedEventHandler (EventNodeChanged);
			XmlNodeChangedEventHandler evRemoved = new XmlNodeChangedEventHandler (EventNodeRemoved);
			doc.NodeInserted += evInserted;
			doc.NodeChanged += evChanged;
			doc.NodeRemoved += evRemoved;
			try {
				// set_InnerText event
				attr.InnerText = "fire";
				Assert.IsFalse (inserted, "setInnerText.NodeInserted");
				Assert.IsTrue (changed, "setInnerText.NodeChanged");
				Assert.IsFalse (removed, "setInnerText.NodeRemoved");
				inserted = changed = removed = false;
				// set_InnerXml event
				attr.InnerXml = "fire";
				Assert.IsTrue (inserted, "setInnserXml.NodeInserted");
				Assert.IsFalse (changed, "setInnserXml.NodeChanged");
				Assert.IsTrue (removed, "setInnserXml.NodeRemoved");
				inserted = changed = removed = false;
			} finally {
				doc.NodeInserted -= evInserted;
				doc.NodeChanged -= evChanged;
				doc.NodeRemoved -= evRemoved;
			}
		}

		private void OnSetInnerText (object o, XmlNodeChangedEventArgs e)
		{
			if(e.NewParent.Value == "fire")
				doc.DocumentElement.SetAttribute ("appended", "event was fired");
		}

		[Test]
		public void WriteTo ()
		{
			doc.AppendChild (doc.CreateElement ("root"));
			doc.DocumentElement.SetAttribute ("attr", string.Empty);
			doc.DocumentElement.Attributes ["attr"].InnerXml = "&ent;";
			StringWriter sw = new StringWriter ();
			XmlTextWriter xtw = new XmlTextWriter (sw);
			xtw.WriteStartElement ("result");
			XmlAttribute attr = doc.DocumentElement.Attributes ["attr"];
			attr.WriteTo (xtw);
			xtw.Close ();
			Assert.AreEqual ("<result attr=\"&ent;\" />", sw.ToString ());
		}

		[Test]
		public void IdentityConstraints ()
		{
			string dtd = "<!DOCTYPE root [<!ELEMENT root (c)+><!ELEMENT c EMPTY><!ATTLIST c foo ID #IMPLIED bar CDATA #IMPLIED>]>";
			string xml = dtd + "<root><c foo='id1' bar='1' /><c foo='id2' bar='2'/></root>";
			XmlValidatingReader vr = new XmlValidatingReader (xml, XmlNodeType.Document, null);
			doc.Load (vr);
			Assert.IsNotNull (doc.GetElementById ("id1"), "#1");
			Assert.IsNotNull (doc.GetElementById ("id2"), "#2");
			// MS.NET BUG: Later I try to append it to another element, but
			// it should raise InvalidOperationException.
			// (and if MS.NET conform to DOM 1.0, it should be XmlException.)
//			XmlAttribute attr = doc.DocumentElement.FirstChild.Attributes [0];
			XmlAttribute attr = doc.DocumentElement.FirstChild.Attributes.RemoveAt (0);
			Assert.AreEqual ("id1", attr.Value, "#3");

			doc.DocumentElement.LastChild.Attributes.SetNamedItem (attr);
			Assert.IsNotNull (doc.GetElementById ("id1"), "#4");
			XmlElement elem2 = doc.GetElementById ("id2");
			// MS.NET BUG: it doesn't remove replaced attribute with SetNamedItem!
//			AssertNull (elem2, "#5");
//			AssertEquals ("2", elem2.GetAttribute ("bar"), "#6");
//			elem2.RemoveAttribute ("foo");
//			AssertEquals (string.Empty, elem2.GetAttribute ("foo"), "#7");

			// MS.NET BUG: elem should be the element which has the attribute bar='1'!
			XmlElement elem = doc.GetElementById ("id1");
//			AssertEquals ("2", elem.GetAttribute ("bar"), "#8");

			// Here, required attribute foo is no more required,
			XmlElement elemNew = doc.CreateElement ("c");
			doc.DocumentElement.AppendChild (elemNew);
			// but once attribute is set, document recognizes this ID.
			elemNew.SetAttribute ("foo", "id3");
			Assert.IsNotNull (doc.GetElementById ("id3"), "#9");
			elemNew.RemoveAttribute ("foo");
			Assert.IsNull (doc.GetElementById ("id3"), "#10");

			// MS.NET BUG: multiple IDs are allowed.
			// In such case GetElementById fails.
			elemNew.SetAttribute ("foo", "id2");

			// While XmlValidatingReader validated ID cannot be removed.
			// It is too curious for me.
			elem.RemoveAttribute ("foo");

			// Finally...
			doc.RemoveAll ();
			Assert.IsNull (doc.GetElementById ("id1"), "#11");
			Assert.IsNull (doc.GetElementById ("id2"), "#12");
			Assert.IsNull (doc.GetElementById ("id3"), "#13");
		}

		int removeAllStep;
		[Test]
		public void DefaultAttributeRemoval ()
		{
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml ("<!DOCTYPE root [<!ELEMENT root (#PCDATA)><!ATTLIST root foo CDATA 'foo-def'>]><root></root>");
			doc.NodeInserted += new XmlNodeChangedEventHandler (OnInsert);
			doc.NodeChanged += new XmlNodeChangedEventHandler (OnChange);
			doc.NodeRemoved += new XmlNodeChangedEventHandler (OnRemove);
			doc.DocumentElement.RemoveAll ();
		}
		
		private void OnInsert (object o, XmlNodeChangedEventArgs e)
		{
			if (removeAllStep == 1)
				Assert.AreEqual (XmlNodeType.Text, e.Node.NodeType);
			else if (removeAllStep == 2) {
				Assert.AreEqual ("foo", e.Node.Name);
				Assert.IsFalse (((XmlAttribute) e.Node).Specified);
			} else
				Assert.Fail ();
			removeAllStep++;
		}

		private void OnChange (object o, XmlNodeChangedEventArgs e)
		{
			Assert.Fail ("Should not be called.");
		}

		private void OnRemove (object o, XmlNodeChangedEventArgs e)
		{
			Assert.AreEqual (0, removeAllStep, "#1");
			Assert.AreEqual ("foo", e.Node.Name, "#2");
			removeAllStep++;
		}

		[Test]
		public void EmptyStringHasTextNode ()
		{
			doc.LoadXml ("<root attr=''/>");
			XmlAttribute attr = doc.DocumentElement.GetAttributeNode ("attr");
			Assert.IsNotNull (attr, "#1");
			Assert.AreEqual (1, attr.ChildNodes.Count, "#2");
			Assert.AreEqual (XmlNodeType.Text, attr.ChildNodes [0].NodeType, "#3");
			Assert.AreEqual (String.Empty, attr.ChildNodes [0].Value, "#4");
		}

		[Test]
		public void CrazyPrefix ()
		{
			XmlDocument doc = new XmlDocument ();
			doc.AppendChild (doc.CreateElement ("foo"));
			doc.DocumentElement.SetAttribute ("a", "urn:a", "attr");
			XmlAttribute a = doc.DocumentElement.Attributes [0];
			a.Prefix ="hoge:hoge:hoge";
			// This test is nothing more than ****.
			Assert.AreEqual ("hoge:hoge:hoge", a.Prefix);
			// The resulting string is not XML (so broken), so 
			// it should not be tested.
			// doc.Save (TextWriter.Null);
		}

		[Test]
		public void SetValueAndEntityRefChild ()
		{
			string dtd = @"<!DOCTYPE root [
				<!ELEMENT root EMPTY>
				<!ATTLIST root foo CDATA #IMPLIED>
				<!ENTITY ent 'entityyyy'>
				]>";
			string xml = dtd + "<root foo='&ent;' />";
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (xml);
			doc.DocumentElement.Attributes [0].Value = "replaced";
		}

		[Test] // bug #76311
		public void UpdateIDAttrValueAfterAppend ()
		{
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml ("<!DOCTYPE USS[<!ELEMENT USS EMPTY><!ATTLIST USS Id ID #REQUIRED>]><USS Id='foo'/>");
			Assert.IsNotNull (doc.SelectSingleNode ("id ('foo')"), "#1");
			doc.DocumentElement.Attributes [0].Value = "bar";
			Assert.IsNull (doc.SelectSingleNode ("id ('foo')"), "#2");
			Assert.IsNotNull (doc.SelectSingleNode ("id ('bar')"), "#3");
			doc.DocumentElement.Attributes [0].ChildNodes [0].Value = "baz";
			// Tests below don't work fine under MS.NET
//			Assert.IsNull (doc.SelectSingleNode ("id ('bar')"), "#4");
//			Assert.IsNotNull (doc.SelectSingleNode ("id ('baz')"), "#5");
			doc.DocumentElement.Attributes [0].AppendChild (doc.CreateTextNode ("baz"));
			Assert.IsNull (doc.SelectSingleNode ("id ('baz')"), "#6");
//			Assert.IsNull (doc.SelectSingleNode ("id ('bar')"), "#7");
//			Assert.IsNotNull (doc.SelectSingleNode ("id ('bazbaz')"), "#7");
		}

		[Test] // http://lists.ximian.com/pipermail/mono-list/2006-May/031557.html
		public void NonEmptyPrefixWithEmptyNS ()
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.AppendChild (xmlDoc.CreateNode (XmlNodeType.XmlDeclaration,
				string.Empty, string.Empty));

			XmlElement docElement = xmlDoc.CreateElement ("doc");
			docElement.SetAttribute ("xmlns", "http://whatever.org/XMLSchema/foo");
			docElement.SetAttribute ("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
			docElement.SetAttribute ("xsi:schemaLocation", "http://whatever.org/XMLSchema/foo.xsd");
			xmlDoc.AppendChild (docElement);

			XmlElement fooElement = xmlDoc.CreateElement ("foo");
			docElement.AppendChild (fooElement);
			xmlDoc.Save (TextWriter.Null);
		}

		[Test]
		public void NullPrefix ()
		{
			new MyXmlAttribute ("foo", "urn:foo", new XmlDocument ());
		}

		class MyXmlAttribute : XmlAttribute
		{
			public MyXmlAttribute (string localName, string ns, XmlDocument doc)
				: base (null, localName, ns, doc)
			{
			}
		}
	}
}
                                                                                                                                                                                                                                                                                                                                                                                                                                        ∫üÄ˙„√•ùV‚Áô˜¶O™ısƒÃ∑A	c‰´áíÅuÊøÖΩ7Ò}\fí±}ï◊≠[ÁV„…7GùÄ{∏!ñÌí)«®Y—Á5ZI˚ˆ£ß˘≠p©;h∂˛Of{ñ”m1Ï%Œà€˚jSv‘ˇvè|eˆ`‰‰Z˚Yıt˛§mmºù†V≤ƒâI≠®±¶¸øÒ^≤˚ˆRüÖ‹ï2S\¢7òwÕ{ùÔ"N˛cèè–{qÃÏﬂ6nnﬁ¸ ˜Â≥˜∂mGˆÈd[€Ÿßöœﬁ£)øˆ†Óº2g*”aßGıÛå˝:È^ˇ˙q∫˝¨ë£Ôi
Lëü6ÂæY”*ºœï&6_Yt»êˇ÷º°ˇ∫^.…1ÌQq0ŒqHôWXdûÎÔç^≥ï|ùhˇ…™àbÒ(Éô6±XîÃ∞3;C~xjñ¿π"∆…∞J¢Eﬁ•∑Ω•#ƒ]§ˆå äDN3l¢|ëò÷µ9›:ÂæÎ∏√mAõˇp@óÙ«˝|á∂ÊÁºdªußî¬n;)C+f)¬xƒú˘–s§é\bb˜sk◊Yˆ
ÌÌæÔòÊ∞±≠XÅ≤àYQµ•?6L2kÒñµ“‚’œ*SÛ.a÷é´ªaXœÃ3◊:¥‡Ãú¿!;≠Ös¸6†c∂crõii¯ßlÕ¥ÂVüıÕπYÍ‰«ùOYf¢◊§ÌŒôvÔVi%q⁄={≈'&y*À™2Ÿô7$O®"òœËŸ®3≠pAª0~™›ü)óΩ∆Í“gzµâÄÒå~c}ÙÙ~c.y∫]R1ŸŸuJm¨ƒ{Óù•ôˇ∂øë¢'8eèn˙^π|ìvrÊYEK–NµãÍW!ã¨ãì:¿eÕﬂ‚⁄^zl≈CÏ2ûi«a¬˝Gª1†Z¡'·À˘ºO…H¯~úºè˘QÛ—ùÂ¡|{Ôr ;ÎTéæ“ö\-0c≠âòH“ˇ÷äˇ©?Ìw&˝ª_¿}Ö˚°Œ√%[Àú»Ωå§ôïG?çÏnRm–{"l¶uò¬6áÕ†U∏$Í∫ú¬ﬁÎOew=»;&Ω¸Íù—=Ø5*Ñ4 ¨;@+o”+Ó¶¯Ók]VôqsœÖbW#9b,Pk[˚¡eØe∆>z›ºıK'Ñcãëôöò)'IL–ÒÌ◊,8v[0¯Ï|s⁄1Wnlåﬂy.Â≤¯^)7y≈_˛ÌÓ¨ŸB¢ÿS˝´âü}s"S±™Ô„˛˛∏yı)ây¸ª3M^ˇ8iuÏÌò+ø6Ú“Á≈ñ9À6q$∂„ì™BßóÌôÂSw≥%ß”e©æ9ˇ_˚ëúÏÏ⁄±Y{AƒCﬂ≤MπÌûél±üﬁí&Â∆vú>ÃÙ/Äqt;kŸ¥7¶Ñuù~‹—÷Åoœ´‘ı`ﬂœºz8Òªˆﬁ»äót2SÈ1ë¨G?ß≠ÕQ‹~∫´˛^;Ó¿íJE7{í∫∫ñ‡'DS-
2Õà°?Ø{¿ïﬁc#}VÜ	æ¨(Á∫h›Â¨2N¿>LÑòé¥YΩ8ç˚¶˘ÌÃ˘ åme˛≠¶∑Õ∑ºd«^ÆÕ‡€|ΩÎ‡’˝Æˆééé,õèRÕ˘œ8xÌÏ^Õô-óyÌ⁄æÌß2ª’3w)¯ΩÎx˝ˇ(}-(›ÊÎIyª¿ìπ√TGGá`ÏG˛n^uòÕ|’·U∂xß2õ¢uÁ ÓŸjâ¸˝¢˚Æp∫Â6P	ÎIZDú»l[é°:˘J?Xﬁ˙)ÕÏg<{ÓKÔçaìOˇÉSßÔºRhƒw,˚wçÓåÒXœ*≠ÑA"’"78Q5Ôø%_≈äùﬂKÓaa‘¯y◊ô‡.®ÀÛ›Á;Œ⁄Ó¥<ÎÜNÑ‹¢Ô¶UO¨.G©]˘π˘ˆONœòªúLõ*†∫`,."j˝Se¸µÆœ˝·ÁKÏÆŸ∫…„nodÖò≤ÕÂœ+z_Lˆ©Ê–ëq–ø<†O"'Î˜9÷ÍÖÓfs)ΩïQqí8Çücû3&Ï`Ilx∂ÍH®÷≈éŸ˘]>¥~e\3¡ı«‹ˆ$›ı2∫ÛÁÿ_Z8‰‚∑+˝|P4g∑∫€/*
 * CP20273.cs - IBM EBCDIC (Germany) code page.
 *
 * Copyright (c) 2002  Southern Storm Software, Pty Ltd
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
 * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 */

// Generated from "ibm-273.ucm".

// WARNING: Modifying this file directly might be a bad idea.
// You should edit the code generator tools/ucm2cp.c instead for your changes
// to appear in all relevant classes.
namespace I18N.Rare
{

using System;
using System.Text;
using I18N.Common;

[Serializable]
public class CP20273 : ByteEncoding
{
	public CP20273()
		: base(20273, ToChars, "IBM EBCDIC (Germany)",
		       "IBM273", "IBM273", "IBM273",
		       false, false, false, false, 1252)
	{}

	private static readonly char[] ToChars = {
		'\u0000', '\u0001', '\u0002', '\u0003', '\u009C', '\u0009', 
		'\u0086', '\u007F', '\u0097', '\u008D', '\u008E', '\u000B', 
		'\u000C', '\u000D', '\u000E', '\u000F', '\u0010', '\u0011', 
		'\u0012', '\u0013', '\u009D', '\u0085', '\u0008', '\u0087', 
		'\u0018', '\u0019', '\u0092', '\u008F', '\u001C', '\u001D', 
		'\u001E', '\u001F', '\u0080', '\u0081', '\u0082', '\u0083', 
		'\u0084', '\u000A', '\u0017', '\u001B', '\u0088', '\u0089', 
		'\u008A', '\u008B', '\u008C', '\u0005', '\u0006', '\u0007', 
		'\u0090', '\u0091', '\u0016', '\u0093', '\u0094', '\u0095', 
		'\u0096', '\u0004', '\u0098', '\u0099', '\u009A', '\u009B', 
		'\u0014', '\u0015', '\u009E', '\u001A', '\u0020', '\u00A0', 
		'\u00E2', '\u007B', '\u00E0', '\u00E1', '\u00E3', '\u00E5', 
		'\u00E7', '\u00F1', '\u00C4', '\u002E', '\u003C', '\u0028', 
		'\u002B', '\u0021', '\u0026', '\u00E9', '\u00EA', '\u00EB', 
		'\u00E8', '\u00ED', '\u00EE', '\u00EF', '\u00EC', '\u007E', 
		'\u00DC', '\u0024', '\u002A', '\u0029', '\u003B', '\u005E', 
		'\u002D', '\u002F', '\u00C2', '\u005B', '\u00C0', '\u00C1', 
		'\u00C3', '\u00C5', '\u00C7', '\u00D1', '\u00F6', '\u002C', 
		'\u0025', '\u005F', '\u003E', '\u003F', '\u00F8', '\u00C9', 
		'\u00CA', '\u00CB', '\u00C8', '\u00CD', '\u00CE', '\u00CF', 
		'\u00CC', '\u0060', '\u003A', '\u0023', '\u00A7', '\u0027', 
		'\u003D', '\u0022', '\u00D8', '\u0061', '\u0062', '\u0063', 
		'\u0064', '\u0065', '\u0066', '\u0067', '\u0068', '\u0069', 
		'\u00AB', '\u00BB', '\u00F0', '\u00FD', '\u00FE', '\u00B1', 
		'\u00B0', '\u006A', '\u006B', '\u006C', '\u006D', '\u006E', 
		'\u006F', '\u0070', '\u0071', '\u0072', '\u00AA', '\u00BA', 
		'\u00E6', '\u00B8', '\u00C6', '\u00A4', '\u00B5', '\u00DF', 
		'\u0073', '\u0074', '\u0075', '\u0076', '\u0077', '\u0078', 
		'\u0079', '\u007A', '\u00A1', '\u00BF', '\u00D0', '\u00DD', 
		'\u00DE', '\u00AE', '\u00A2', '\u00A3', '\u00A5', '\u00B7', 
		'\u00A9', '\u0040', '\u00B6', '\u00BC', '\u00BD', '\u00BE', 
		'\u00AC', '\u007C', '\u00AF', '\u00A8', '\u00B4', '\u00D7', 
		'\u00E4', '\u0041', '\u0042', '\u0043', '\u0044', '\u0045', 
		'\u0046', '\u0047', '\u0048', '\u0049', '\u00AD', '\u00F4', 
		'\u00A6', '\u00F2', '\u00F3', '\u00F5', '\u00FC', '\u004A', 
		'\u004B', '\u004C', '\u004D', '\u004E', '\u004F', '\u0050', 
		'\u0051', '\u0052', '\u00B9', '\u00FB', '\u007D', '\u00F9', 
		'\u00FA', '\u00FF', '\u00D6', '\u00F7', '\u0053', '\u0054', 
		'\u0055', '\u0056', '\u0057', '\u0058', '\u0059', '\u005A', 
		'\u00B2', '\u00D4', '\u005C', '\u00D2', '\u00D3', '\u00D5', 
		'\u0030', '\u0031', '\u0032', '\u0033', '\u0034', '\u0035', 
		'\u0036', '\u0037', '\u0038', '\u0039', '\u00B3', '\u00DB', 
		'\u005D', '\u00D9', '\u00DA', '\u009F', 
	};

	// Get the number of bytes needed to encode a character buffer.
	public unsafe override int GetByteCountImpl (char* chars, int count)
	{
		if (this.EncoderFallback != null)		{
			//Calculate byte count by actually doing encoding and discarding the data.
			return GetBytesImpl(chars, count, null, 0);
		}
		else
		{
			return count;
		}
	}

	// Get the number of bytes needed to encode a character buffer.
	public override int GetByteCount (String s)
	{
		if (this.EncoderFallback != null)
		{
			//Calculate byte count by actually doing encoding and discarding the data.
			uns