 (pd.GetValueChangedHandler (compB), "#I5");
		}

		[Test]
		public void RemoveValueChanged ()
		{
			MockPropertyDescriptor pd = new MockPropertyDescriptor (
				"Name", new Attribute [0]);
			object compA = new object ();
			object compB = new object ();
			EventHandler handlerA = new EventHandler (ValueChanged1);
			EventHandler handlerB = new EventHandler (ValueChanged1);
			EventHandler handlerC = new EventHandler (ValueChanged2);

			pd.AddValueChanged (compA, handlerA);
			pd.AddValueChanged (compA, handlerC);
			pd.AddValueChanged (compA, handlerC);
			pd.AddValueChanged (compA, handlerB);
			pd.AddValueChanged (compB, handlerC);

			pd.FireValueChanged (compA, new EventArgs ());
			Assert.AreEqual (4, _invokedHandlers.Count, "#A1");
			pd.RemoveValueChanged (new object (), handlerC);
			pd.FireValueChanged (compA, new EventArgs ());
			Assert.AreEqual (8, _invokedHandlers.Count, "#A2");

			Reset ();
			pd.RemoveValueChanged (compA, handlerC);

			pd.FireValueChanged (compA, new EventArgs ());
			Assert.AreEqual (3, _invokedHandlers.Count, "#B1");
			Assert.AreEqual ("ValueChanged1", _invokedHandlers [0], "#B2");
			Assert.AreEqual ("ValueChanged2", _invokedHandlers [1], "#B3");
			Assert.AreEqual ("ValueChanged1", _invokedHandlers [2], "#B4");

			Reset ();

			pd.FireValueChanged (compB, new EventArgs ());
			Assert.AreEqual (1, _invokedHandlers.Count, "#C1");
			Assert.AreEqual ("ValueChanged2", _invokedHandlers [0], "#C2");

			Reset ();
			pd.RemoveValueChanged (compB, handlerC);

			pd.FireValueChanged (compB, new EventArgs ());
			Assert.AreEqual (0, _invokedHandlers.Count, "#D");
		}

		[Test]
		public void RemoveValueChanged_Component_Null ()
		{
			MockPropertyDescriptor pd = new MockPropertyDescriptor (
				"Name", new Attribute [0]);
			try {
				pd.RemoveValueChanged (null, new EventHandler (ValueChanged1));
				Assert.Fail ("#1");
			} catch (ArgumentNullException ex) {
				Assert.AreEqual (typeof (ArgumentNullException), ex.GetType (), "#2");
				Assert.IsNull (ex.InnerException, "#3");
				Assert.IsNotNull (ex.Message, "#4");
				Assert.IsNotNull (ex.ParamName, "#5");
				Assert.AreEqual ("component", ex.ParamName, "#6");
			}
		}

		[Test]
		public void RemoveValueChanged_Handler_Null ()
		{
			MockPropertyDescriptor pd = new MockPropertyDescriptor (
				"Name", new Attribute [0]);
			try {
				pd.RemoveValueChanged (new object (), (EventHandler) null);
				Assert.Fail ("#1");
			} catch (ArgumentNullException ex) {
				Assert.AreEqual (typeof (ArgumentNullException), ex.GetType (), "#2");
				Assert.IsNull (ex.InnerException, "#3");
				Assert.IsNotNull (ex.Message, "#4");
				Assert.IsNotNull (ex.ParamName, "#5");
				Assert.AreEqual ("handler", ex.ParamName, "#6");
			}
		}

		void ValueChanged1 (object sender, EventArgs e)
		{
			_invokedHandlers.Add ("ValueChanged1");
		}

		void ValueChanged2 (object sender, EventArgs e)
		{
			_invokedHandlers.Add ("ValueChanged2");
		}

		static Attribute FindAttribute (PropertyDescriptor pd, Type type)
		{
			foreach (Attribute attr in pd.Attributes)
				if (attr.GetType () == type)
					return attr;
			return null;
		}
#if !MOBILE && !XAMMAC_4_5
		class GetEditor_test 
		{
			[Editor (typeof (UIEditor), typeof (UITypeEditor))]
			public string Property {
				get { return "abc"; }
				set { }
			}
		}

		class UIEditor : UITypeEditor
		{		
		}

		[Test]
		public void GetEditorTest ()
		{
			PropertyDescriptorCollection col;
			PropertyDescriptor pd;
			UITypeEditor ed;
			
			col = TypeDescriptor.GetProperties (typeof (GetEditor_test));
			pd = col [0];
			ed = pd.GetEditor (typeof (UITypeEditor)) as UITypeEditor;
			
			Assert.IsNotNull (ed, "#01");
			Assert.AreEqual (ed.GetType ().Name, "UIEditor", "#02");
		}
#endif

		class MockPropertyDescriptor : PropertyDescriptor
		{
			public MockPropertyDescriptor (MemberDescriptor reference)
				: base (reference)
			{
			}

			public MockPropertyDescriptor (MemberDescriptor reference, Attribute [] attrs)
				: base (reference, attrs)
			{
			}

			public MockPropertyDescriptor (string name, Attribute [] attrs)
				: base (name, attrs)
			{
			}

			public override Type ComponentType {
				get { return typeof (int); }
			}

			public override bool IsReadOnly {
				get { return false; }
			}

			public override Type PropertyType{
				get { return typeof (DateTime); }
			}

			public override object GetValue (object component)
			{
				return null;
			}

			public override void SetValue (object component, object value)
			{
			}

			public override void ResetValue (object component)
			{
			}

			public override bool CanResetValue (object component)
			{
				return true;
			}

			public override bool ShouldSerializeValue (object component)
			{
				return true;
			}

			public void FireValueChanged (object component, EventArgs e)
			{
				base.OnValueChanged (component, e);
			}

			public new object GetInvocationTarget (Type type, object instance)
			{
				return base.GetInvocationTarget (type, instance);
			}

			public new EventHandler GetValueChangedHandler (object component)
			{
				return base.GetValueChangedHandler (component);
			}
		}

		[AttributeUsage (AttributeTargets.Field | AttributeTargets.Property)]
		public class PropTestAttribute : Attribute
		{
			public PropTestAttribute ()
			{
			}
		}

		public class TestBase
		{
			[PropTest]
			public int PropBase1
			{
				get { return 0; }
				set { }
			}

			[PropTest]
			[Description ("whatever")]
			public string PropBase2
			{
				get { return ""; }
				set { }
			}

			[PropTest]
			public virtual string PropBase3
			{
				get { return ""; }
				set { }
			}
		}

		public class TestSub : TestBase
		{
			[PropTest]
			public int PropSub1
			{
				get { return 0; }
				set { }
			}

			[PropTest]
			public string PropSub2
			{
				get { return ""; }
				set { }
			}

			public override string PropBase3
			{
				get { return ""; }
				set { }
			}
		}
	}
}
                                                                                                                                                                                                           assert_equal 'märz'.upcase, I18n.l(@time, :format => '%^B', :locale => :de)
        end

        test "localize Time: given an abbreviated month name format it returns the correct abbreviated month name" do
          assert_equal 'Mär', I18n.l(@time, :format => '%b', :locale => :de)
        end

        test "localize Time: given an abbreviated and uppercased month name format it returns the correct abbreviated month name in upcase" do
          assert_equal 'mär'.upcase, I18n.l(@time, :format => '%^b', :locale => :de)
        end

        test "localize Time: given a date format with the month name upcased it returns the correct value" do
          assert_equal '1. FEBRUAR 2008', I18n.l(::Time.utc(2008, 2, 1, 6, 0), :format => "%-d. %^B %Y", :locale =