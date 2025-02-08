//
// System.ComponentModel.PropertyDescriptor test cases
//
// Authors:
//	Chris Toshok (toshok@ximian.com)
//	Gert Driesen (drieseng@users.sourceforge.net)
//
// (c) 2006 Novell, Inc. (http://www.novell.com/)
//

using System;
using System.Collections;
using System.ComponentModel;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
#if !MOBILE && !XAMMAC_4_5
using System.Drawing.Design;
#endif
using NUnit.Framework;

namespace MonoTests.System.ComponentModel
{
	internal class MyVersionTypeConverter : TypeConverter
	{
	}

	class VirtPropParent
	{
		string _someProperty;

		public virtual string SomeProperty {
			get { return _someProperty; }
			set { _someProperty = value; }
		}
	}

	class VirtPropChildNoSetter : VirtPropParent
	{
		public override string SomeProperty {
			get { return base.SomeProperty + ": modified"; }
		}
	}

	class VirtPropChildNoGetter : VirtPropParent
	{
		public override string SomeProperty {
			get { return base.SomeProperty + ": modified"; }
		}
	}


	[TestFixture]
	public class PropertyDescriptorTests
	{
		class MissingConverterType_test
		{
			public class NestedClass { }

			[TypeConverter ("missing-type-name")]
			public NestedClass Prop {
				get { return null; }
			}

			[TypeConverter ("missing-type-name")]
			public int IntProp {
				get { return 5; }
			}

			[TypeConverter ("missing-type-name")]
			public string StringProp {
				get { return ""; }
			}
		}

		class ReadOnlyProperty_test
		{
			public int Prop {
				get { return 5; }
			}
		}

		class ReadOnlyAttribute_test
		{
			[ReadOnly (true)]
			public int Prop {
				get { return 5; }
				set { }
			}
		}

		class ConflictingReadOnly_test
		{
			[ReadOnly (false)]
			public int Prop {
				get { return 5; }
			}
		}

		class ShouldSerialize_public_test
		{
			public int Prop {
				get { return 5; }
			}

			public bool ShouldSerializeProp()
			{
				return false;
			}
		}

		class ShouldSerialize_protected_test
		{
			public int Prop {
				get { return 5; }
			}

			protected bool ShouldSerializeProp()
			{
				return false;
			}
		}

		class ShouldSerialize_private_test
		{
			public int Prop {
				get { return 5; }
			}

			private bool ShouldSerializeProp()
			{
				return false;
			}
		}

		class ShouldSerializeFalseEffectOnCanReset_test
		{
			public int Prop {
				get { return 5; }
				set { }
			}

			public bool ShouldSerializeProp()
			{
				return false;
			}

			public void ResetProp()
			{
			}
		}

		class ShouldSerialize_Null_Default
		{
			[DefaultValue (null)]
			public string Prop {
				get { return _prop; }
				set { _prop = value; }
			}

			public bool SerializeProp {
				get { return _serializeProp; }
				set { _serializeProp = value; }
			}

			public bool ShouldSerializeProp ()
			{
				return _serializeProp;
			}

			private string _prop;
			private bool _serializeProp;
		}

		class ShouldSerialize_No_Default
		{
			public string Prop {
				get { return _prop; }
				set { _prop = value; }
			}

			private string _prop;
		}

		class ShouldSerialize_ReadOnly
		{
			[ReadOnly (true)]
			[DefaultValue ("ok")]
			public string Prop1 {
				get { return _prop1; }
				set { _prop1 = value; }
			}

			[ReadOnly (false)]
			public string Prop2 {
				get { return _prop2; }
				set { _prop2 = value; }
			}

			[ReadOnly (true)]
			public string Prop3 {
				get { return _prop3; }
				set { _prop3 = value; }
			}

			[ReadOnly (false)]
			public string Prop4 {
				get { return _prop4; }
				set { _prop4 = value; }
			}

			public string Prop5 {
				get { return _prop5; }
			}

			[DefaultValue ("bad")]
			public string Prop6 {
				get { return _prop6; }
			}

			[ReadOnly (true)]
			[DefaultValue ("good")]
			public string Prop7 {
				get { return _prop7; }
				set { _prop7 = value; }
			}

			[ReadOnly (true)]
			[DesignerSerializationVisibility (DesignerSerializationVisibility.Content)]
			public string Prop8 {
				get { return null; }
			}

			[ReadOnly (true)]
			[DesignerSerializationVisibility (DesignerSerializationVisibility.Content)]
			public string Prop9 {
				get { return null; }
			}

			public bool SerializeProp3 {
				get { return _serializeProp3; }
				set { _serializeProp3 = value; }
			}

			public bool SerializeProp4 {
				get { return _serializeProp4; }
				set { _serializeProp4 = value; }
			}

			public bool SerializeProp5 {
				get { return _serializeProp5; }
				set { _serializeProp5 = value; }
			}

			public bool SerializeProp6 {
				get { return _serializeProp6; }
				set { _serializeProp6 = value; }
			}

			public bool SerializeProp7 {
				get { return _serializeProp7; }
				set { _serializeProp7 = value; }
			}

			public bool ShouldSerializeProp3 ()
			{
				return _serializeProp3;
			}

			public bool ShouldSerializeProp4 ()
			{
				return _serializeProp4;
			}

			public bool ShouldSerializeProp5 ()
			{
				return _serializeProp5;
			}

			public bool ShouldSerializeProp6 ()
			{
				return _serializeProp6;
			}

			public bool ShouldSerializeProp7 ()
			{
				return _serializeProp7;
			}

			public bool ShouldSerializeProp8 ()
			{
				return false;
			}

			private string _prop1;
			private string _prop2;
			private string _prop3;
			private string _prop4;
			private string _prop5 = "good";
			private string _prop6 = "bad";
			private string _prop7;
			private bool _serializeProp3;
			private bool _serializeProp4;
			private bool _serializeProp5;
			private bool _serializeProp6;
			private bool _serializeProp7;
		}

		class NoSerializeOrResetProp_test
		{
			public int Prop {
				get { return 5; }
			}
		}

		class CanReset_public_test
		{
			int prop = 5;
			public int Prop {
				get { return prop; }
				set { prop = value; }
			}

			public void ResetProp()
			{
				prop = 10;
			}
		}

		class CanReset_protected_test
		{
			int prop = 5;
			public int Prop {
				get { return prop; }
				set { prop = value; }
			}

			protected void ResetProp()
			{
				prop = 10;
			}
		}

		class CanReset_private_test
		{
			int prop = 5;
			publ