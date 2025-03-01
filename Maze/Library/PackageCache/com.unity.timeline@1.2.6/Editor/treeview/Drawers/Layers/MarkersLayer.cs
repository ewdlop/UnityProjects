<?xml version="1.0"?>
<clause number="17.5.1.3" title="Output parameters">
  <paragraph>A parameter declared with an out modifier is an output parameter. Similar to a reference parameter, an output parameter does not create a new storage location. Instead, an output parameter represents the same storage location as the variable given as the argument in the method invocation. </paragraph>
  <paragraph>When a formal parameter is an output parameter, the corresponding argument in a method invocation must consist of the keyword out followed by a <non_terminal where="12.4">variable-reference</non_terminal> (<hyperlink>12.3.3</hyperlink>) of the same type as the formal parameter. A variable need not be definitely assigned before it can be passed as an output parameter, but following an invocation where a variable was passed as an output parameter, the variable is considered definitely assigned. </paragraph>
  <paragraph>Within a method, just like a local variable, an output parameter is initially considered unassigned and must be definitely assigned before its value is used. </paragraph>
  <paragraph>Every output parameter of a method must be definitely assigned before the method returns. </paragraph>
  <paragraph>Output parameters are typically used in methods that produce multiple return values. <example>[Example: For example: <code_example><![CDATA[
using System;  
class Test  
{  
   static void SplitPath(string path, out string dir, out string name) {  
      int i = path.Length;  
      while (i > 0) {  
         char ch = path[i - 1];  
         if (ch == '\\' || ch == '/' || ch == ':') break;  
         i--;  
      }  
      dir = path.Substring(0, i);  
      name = path.Substring(i);  
   }  
   static void Main() {  
      string dir, name;  
      SplitPath("c:\\Windows\\System\\hello.txt", out dir, out name);  
      Console.WriteLine(dir);  
      Console.WriteLine(name);  
   }  
}  
]]></code_example></example></paragraph>
  <paragraph>
    <example>The example produces the output: <code_example><![CDATA[
c:\Windows\System\  
hello.txt  
]]></code_example></example>
  </paragraph>
  <paragraph>
    <example>Note that the dir and name variables can be unassigned before they are passed to SplitPath, and that they are considered definitely assigned following the call. end example]</example>
  </paragraph>
</clause>
                                                                                                                                                                                                                  �~       �  �                       �~       �  �                       �~       �  �                       �~  