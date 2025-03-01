//
// Tests the foreach on strings, and tests the implicit use of foreach
// to pull the enumerator from the class and identify the pattern to be called
//
using System;
using System.Collections;

class Y {
	int count = 0;
	
	public bool MoveNext ()
	{
		count++;
		return count != 10;
	}
	
	public object Current {
		get {
			return count;
		}
	}
}

class X {

	static string [] a = {
		"one", "two", "three"
	};

	public Y GetEnumerator ()
	{
		return new Y ();
	}
	
	public static int Main ()
	{
		//
		// String test
		//
		string total = "";
		
		foreach (string s in a){
			total = total + s;
		}
		if (total != "onetwothree")
			return 1;

		//
		// Pattern test
		//
		X x = new X ();

		int t = 0;
		foreach (object o in x){
			t += (int) o;
		}
		if (t != 45)
			return 2;

		//
		// Looking for GetEnumerator on interfaces test
		//
		Hashtable xx = new Hashtable ();
		xx.Add ("A", 10);
		xx.Add ("B", 20);

		IDictionary vars = xx;
		string total2 = "";
		foreach (string name in vars.Keys){
			total2 = total2 + name;
		}

		if ((total2 != "AB") && (total2 != "BA"))
			return 3;

		ArrayList list = new ArrayList ();
		list.Add ("one");
		list.Add ("two");
		list.Add ("three");
		int count = 0;

		//
		// This test will make sure that `break' inside foreach will
		// actually use a `leave' opcode instead of a `br' opcode
		//
		foreach (string s in list){
			if (s == "two"){
				break;
			}
			count++;
		}
		if (count != 1)
			return 4;
		
		Console.WriteLine ("test passes");
		return 0;
	}
}
                     BB B(B0A8Dp������G8A0B(B BBAApT   �         ABB B(B0A8D`������0hApI`�8A0B(B BBAA`     L             0   `      <   ABA DP���/ ABA   H   �      y   ABB B(B0A8G�������]8A0B(B BBA  H   �      �   ABB B(B0A8G��������8A0B(B BBA  d   ,      �   ABB B(B0A8D�������^8A0B(B BBAE��8A0B(B BBAA� L   �         ABB B(B0A8G��������8A0B(B BBAA�  L   �      .   ABB B(B0A8D�������	8A0B(B BBAA�  H   4      �   ABB B(B0A8D��������8A0B(B BBA  H   �      /   ABB B(B0A8G�������8A0B(B BBA  L   �         ABB B(B0A8G��������8A0B(B BBAA�  L         �   ABB B(B0A8G�������q8A0B(B BBAA�  L   l      �   ABB B(B0A8G��������8A0B(B BBAA�  H   �      U   ABB B(B0A8G�������98A0B(B BBA  H            ABB B(B0A8D`�������8A0B(B BBA   H   T      C   ABB B(B0A8D�������*8A0B(B BBA  L   �      �   ABB B(B0A8G��������8A0B(B BBAA�      �      c    {�[B IA  L         �   ABB B(B0A8G��������8A0B(B BBAA�  8   d      �   ABB A(A0����|(A BBAA0L   �      _   ABB B(B0A8G��������8A0B(B BBAA�     �      �       @            ABB B(A0D@������0A(B BB