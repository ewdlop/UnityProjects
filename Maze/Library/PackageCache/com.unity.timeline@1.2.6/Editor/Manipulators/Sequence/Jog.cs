using System;
using System.Diagnostics.Contracts;
using System.Runtime.Versioning;
using System.Text;

namespace System.IO
{
    // An iterator that returns a single line at-a-time from a given file.
    //
    // Known issues which cannot be changed to remain compatible with 4.0:
    //
    //  - The underlying StreamReader is allocated upfront for the IEnumerable<T> before 
    //    GetEnumerator has even been called. While this is good in that exceptions such as 
    //    DirectoryNotFoundException and FileNotFoundException are thrown directly by 
    //    File.ReadLines (which the user probably expects), it also means that the reader 
    //    will be leaked if the user never actually foreach's over the enumerable (and hence 
    //    calls Dispose on at least one IEnumerator<T> instance).
    //
    //  - Reading to the end of the IEnumerator<T> disposes it. This means that Dispose 
    //    is called twice in a normal foreach construct.
    //
    //  - IEnumerator<T> instances from the same IEnumerable<T> party on the same underlying 
    //    reader (Dev10 Bugs 904764).
    //
    internal class ReadLinesIterator : Iterator<string>
    {
        private readonly string _path;
        private readonly Encoding _encoding;
        private StreamReader _reader;

        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        private ReadLinesIterator(string path, Encoding encoding, StreamReader reader)
        {
            Contract.Requires(path != null);
            Contract.Requires(path.Length > 0);
            Contract.Requires(encoding != nu