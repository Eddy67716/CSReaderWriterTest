using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderWriterTest.iostuff
{
    internal class ByteArrayReader
    {
        // constants
        public static readonly byte[] AND_VALUES = { 0, 1, 3, 7, 15, 31, 63, 127 };

        // instance variables
        // name of file
        private string fileName;
        // stores if file is reading in little endian or not
        private bool littleEndian;
        // the main byte array
        private byte[] byteArray;
        // sotres currently read bytes
        private byte[] convertBytes;
        // stores the position in the file
        private long filePosition;
        // the extra bits that can't yet be written
        private byte extraBits;
        // count of extra bits
        private byte extraBitCount;
        // count of trailing bits
        private byte trailingBits;
        // the bit offset over the byte mark
        private byte bitOffset;
        // reverses read arrays if system and provided endians don't match
        private bool reverse;
    }
}
