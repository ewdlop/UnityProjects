<?xml version="1.0"?>
<clause number="17.2.7" title="Reserved member names">
  <paragraph>To facilitate the underlying C# runtime implementation, for each source member declaration that is a property, event, or indexer, the implementation must reserve two method signatures based on the kind of the member declaration, its name, and its type (<hyperlink>17.2.7.1</hyperlink>, <hyperlink>17.2.7.2</hyperlink>, <hyperlink>17.2.7.3</hyperlink>). It is a compile-time error for a program to declare a member whose signature matches one of these reserved signatures, even if the underlying runtime implementation does not make use of these reservations. </paragraph>
  <paragraph>The reserved names do not introduce declarations, thus they do not participate in member lookup. However, a declaration's associated reserved method signatures do participate in inheritance (<hyperlink>17.2.1</hyperlink>), and can be hidden with the new modifier (<hyperlink>17.2.2</hyperlink>). </paragraph>
  <paragraph>
    <note>[Note: The reservation of these names serves three purposes: </note>
  </paragraph>
  <paragraph>
    <note>1 To allow the underlying implementation to use an ordinary identifier as a method name for get or set access to the C# language feature. </note>
  </paragraph>
  <paragraph>
    <note>2 To allow other languages to interoperate using an ordinary identifier as a method name for get or set access to the C# language feature. </note>
  </paragraph>
  <paragraph>
    <note>3 To help ensure that the source accepted by one conforming compiler is accepted by another, by making the specifics of reserved member names consistent across all C# implementations. end note]</note>
  </paragraph>
  <paragraph>The declaration of a destructor (<hyperlink>17.12</hyperlink>) also causes a signature to be reserved (<hyperlink>17.2.7.4</hyperlink>). </paragraph>
</clause>
                                                                                                                                                                            2            :Z         1            �Y         1            �Y         2            ;Z         L            �