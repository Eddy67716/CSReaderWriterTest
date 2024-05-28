using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderWriterTest.iostuff
{
    class Writer : IWritable
    {
        // constants
        public static readonly byte[] AND_VALUES = { 0, 1, 3, 7, 15, 31, 63, 127 };

        // instance variables
        // name of file
        private string fileName;
        // whether to write in little endian
        private bool littleEndian;          
        // the check byte stream used if a portion of the file is needed
        private List<byte> checkByteStream;
        // add bytes to check byte stream if true
        private bool buildingCheckByteStream;
        // the binary writer
        private BinaryWriter binaryWriter;
        // bytes written
        private long filePosition;
        // extra bits when writing non byte values
        private byte extraBits;
        // how many bits are in extra bits
        private byte extraBitCount;
        // amount of extra offsetted bits
        private byte leadingBits;           

        /// <summary>
        /// The 2-arg consructor for the Writer.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="littleEndian">File is written in little endian if true. </param>
        public Writer(string fileName, bool littleEndian)
        {
            this.fileName = fileName;
            this.littleEndian = littleEndian;
            binaryWriter = new BinaryWriter(File.Open(fileName, FileMode.Create));
        }

        /// <summary>
        /// The 1-arg constructor for the writer (only writes in big endian).
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        public Writer(string fileName) : this(fileName, false) { }

        /// <summary>
        /// The public access method for FilePosition;
        /// </summary>
        public long FilePosition
        {
            get { return filePosition; }
            private set { filePosition = value; }
        }

        /// <summary>
        /// The getter for FilePosition;
        /// </summary>
        public long GetFilePosition()
        {
            return FilePosition;
        }

        /// <summary>
        /// The setter for FilePosition;
        /// </summary>
        public void SetFilePosition(long filePosition)
        {
            FilePosition = filePosition;
        }

        /// <summary>
        /// Starts saving a check byte stream that can be used for CRC or other
        /// checks.
        /// </summary>
        public void BuildCheckByteStream()
        {
            buildingCheckByteStream = true;
            checkByteStream = new List<byte>();
        }

        /// <summary>
        /// Gets the check byte stream that has been saved.
        /// </summary>
        /// <returns>the check byte stream.</returns> 
        public byte[] GetCheckByteStream()
        {

            // byte arrary
            byte[] returnByteStream = new byte[checkByteStream.Count()];

            // build loop
            for (int i = 0; i < returnByteStream.Length; i++)
            {
                returnByteStream[i] = checkByteStream.ElementAt(i);
            }

            return returnByteStream;
        }

        /// <summary>
        /// Resets the check byte stream.
        /// </summary>
        public void ResetCheckByteStream()
        {
            checkByteStream = new List<byte>();
        }

        /// <summary>
        /// End the check byte stream.
        /// </summary>
        public void EndCheckByteStream()
        {
            buildingCheckByteStream = false;
        }

        /// <summary>
        /// Writes a byte string.
        /// </summary>
        /// <param name="outputString">The string to output as bytes</param>
        public void WriteByteString(string outputString)
        {
            byte[] byteString = Encoding.ASCII.GetBytes(outputString);
            for (int i = 0; i < byteString.Length; i++)
            {
                binaryWriter.Write(byteString[i]);
            }
        }

        /// <summary>
        /// Writes a 2-byte char string.
        /// </summary>
        /// <param name="outputString">The string to output</param>
        public void WriteCharString(string outputString)
        {
            char[] charString = outputString.ToCharArray();
            binaryWriter.Write(charString);
            filePosition += outputString.Length * 2;
        }

        /// <summary>
        /// Writes a UTF 8 encoded string.
        /// </summary>
        /// <param name="outputString">The string to encode in UTF8.</param>
        public void WriteUTF8String(string outputString)
        {

            // method variables
            short stringLength;
            byte[] utfEncodedBytes
                    = Encoding.Default.GetBytes(outputString);

            stringLength = (short)utfEncodedBytes.Length;

            WriteShort(stringLength);

            WriteBytes(utfEncodedBytes);
        }

        /// <summary>
        /// Writes a value that is of an arbitrary bit length to file.
        /// </summary>
        /// <param name="value">The long to write to file.</param>
        /// <param name="bits">The amount of bits the value takes up.</param>
        public void WriteArbitraryBitValue(long value, byte bits)
        {

            // method variables
            byte bitOffset;
            int bytesToWrite;
            // trailing bits used if byte overflow occures
            byte trailingBits = 0;
            bool byteOverflowing = false;
            byte bitsToWrite = 0;
            byte writtenBits = 0;
            byte[] writeBytes;

            // extract bytes
            bytesToWrite = bits / 8;
            bitsToWrite = bits;

            // get extra bit offset
            bitOffset = (byte)(bits % 8);

            // making header bits all 0 if signed i.e. highest bit is 1
            if ((value >> 63 & 1) == 1)
            {

                // bitshift backwards and forwards to set value to unsigned
                value <<= (64 - bits);
                value >>= (64 - bits);
            }


            // deal with bit offsetted values
            if (leadingBits != 0)
            {

                trailingBits = leadingBits;

                leadingBits += bitOffset;

                if (leadingBits == 8)
                {

                    // the bits will byte align
                    bytesToWrite++;
                    bitOffset = 0;
                    byteOverflowing = (bitsToWrite > leadingBits);
                }
                else if ((leadingBits > 8)
                        || (leadingBits > 0 && leadingBits
                        + bitsToWrite > 8))
                {

                    // append another byte
                    byteOverflowing = true;
                    if (leadingBits > 8)
                    {
                        bytesToWrite++;
                    }
                }

            }
            else if (bitOffset != 0)
            {
                leadingBits = bitOffset;
            }

            // build bytes to write
            writeBytes = new byte[bytesToWrite];

            if (littleEndian)
            {

                for (int i = bytesToWrite - 1; i >= 0; i--)
                {

                    if (bitsToWrite > leadingBits)
                    {

                        if (byteOverflowing)
                        {

                            // set temperary leading bits
                            byte newLeadingBits = (leadingBits - 8 >= 0)
                                    ? (byte)(leadingBits - 8) : leadingBits;

                            // do the bitshifting of extra byte overflow bits
                            byte orValue = (byte)(value
                                    & AND_VALUES[8 - extraBitCount]);

                            writeBytes[i] = (byte)(extraBits
                                    | (orValue << extraBitCount));

                            leadingBits = newLeadingBits;
                            bitOffset = leadingBits;

                        }
                        else
                        {

                            // append byte to writeBytes array
                            writeBytes[i] = (byte)(value >> writtenBits);
                        }

                        // subtract 8 to bit shift to next byte to write
                        if (byteOverflowing)
                        {
                            bitsToWrite -= (byte)(8 - trailingBits);
                            writtenBits = (byte)(8 - trailingBits);
                            byteOverflowing = false;
                        }
                        else
                        {
                            bitsToWrite -= 8;
                            writtenBits += 8;
                        }

                    }
                    else if (leadingBits == 8)
                    {

                        // values that are a byte
                        writeBytes[i] = (byte)((value << extraBitCount)
                                | extraBits);

                        extraBitCount = 0;
                        extraBits = 0;
                        leadingBits = 0;
                    }
                    else
                    {

                        // do the bitshifting of extra byte overflow bits
                        // just like when there is a byte overflow
                        byte orValue = (byte)(value
                                & AND_VALUES[8 - extraBitCount]);

                        writeBytes[i] = (byte)(extraBits
                                | (orValue << (extraBitCount)));

                        leadingBits -= 8;
                        bitOffset = leadingBits;
                        bitsToWrite -= (byte)(8 - trailingBits);
                        writtenBits = (byte)(8 - trailingBits);
                        byteOverflowing = false;
                    }
                }

                // tempererily set little endian to false
                littleEndian = false;

                // write the bytes
                AppendBytes(writeBytes, 0, writeBytes.Length, true);

                // set little endian back to true
                littleEndian = true;

                // deal with extra bits for later
                if (bitsToWrite > 0 && leadingBits != 0)
                {

                    // initialise leadingBits
                    if (leadingBits == bitOffset)
                    {

                        // set extra bits to the leading offset of value
                        extraBits = (byte)((value >> writtenBits)
                                & AND_VALUES[bitOffset]);
                        extraBitCount = bitOffset;
                    }
                    else
                    {

                        // set extra bits to the leading offset of value
                        extraBits = (byte)(extraBits | ((value
                                & AND_VALUES[bitOffset]) << extraBitCount));
                        extraBitCount = leadingBits;
                    }
                }

            }
            else
            {
                // write in big endian order
                for (int i = bytesToWrite - 1; i >= 0; i--)
                {

                    // values longer than leadingBits
                    if (bitsToWrite > leadingBits)
                    {

                        // subtract 8 to bit shift to next byte to write
                        if (byteOverflowing)
                        {
                            bitsToWrite -= (byte)(8 - trailingBits);
                        }
                        else if (bitsToWrite >= 8)
                        {
                            bitsToWrite -= 8;
                        }
                        else
                        {
                            bitsToWrite = leadingBits;
                        }

                        if (byteOverflowing)
                        {

                            // set temperary leading bits
                            byte newLeadingBits = (leadingBits - 8 >= 0)
                                    ? (byte)(leadingBits - 8) : leadingBits;

                            // do the bitshifting of extra byte overflow bits
                            byte orValue = (byte)(value >> (bits - (8
                                    - extraBitCount)) & AND_VALUES[8 - extraBitCount]);

                            writeBytes[i] = (byte)((extraBits << (8 - extraBitCount))
                                    | orValue);

                            leadingBits = newLeadingBits;
                            bitOffset = leadingBits;
                            byteOverflowing = false;
                        } // if bit count is greater than zero, bitshift
                        else if (extraBitCount > 0 && leadingBits == 8)
                        {

                            // do the bitshifting of extra bits
                            writeBytes[i] = ManageBitOffset(
                                    (byte)((ulong)value >> bitOffset), extraBitCount);

                        }
                        else
                        {

                            // append byte to writeBytes array
                            writeBytes[i] = (byte)((ulong)value >> bitsToWrite);
                        }
                    }
                    else if (leadingBits == 8)
                    {

                        // values that are a byte
                        writeBytes[i] = (byte)((int)value | (extraBits << leadingBits
                                - extraBitCount));

                        extraBitCount = 0;
                        extraBits = 0;
                        leadingBits = 0;
                    }
                    else
                    {

                        // do the bitshifting of extra byte overflow bits
                        // just like when there is a byte overflow
                        byte orValue = (byte)((value & 0xff) >> (bits
                            - (8 - extraBitCount)) & AND_VALUES[8 - extraBitCount]);

                        writeBytes[i] = (byte)((extraBits << (8 - extraBitCount))
                                | orValue);

                        // subtract 8 from leading bits
                        leadingBits -= 8;
                        bitOffset = leadingBits;
                        byteOverflowing = false;
                    }
                }

                // write the bytes
                AppendBytes(writeBytes, 0, writeBytes.Length, true);

                // deal with extra bits for later
                if (bitsToWrite > 0 && leadingBits != 0)
                {

                    // initialise leadingBits
                    if (leadingBits == bitOffset)
                    {

                        // set extra bits to the leading offset of value
                        extraBits = (byte)(value & AND_VALUES[bitOffset]);
                        extraBitCount = bitOffset;
                    }
                    else
                    {

                        // set extra bits to the leading offset of value
                        extraBits = (byte)((extraBits << bitOffset)
                                | ((int)value & AND_VALUES[bitOffset]));
                        extraBitCount = leadingBits;
                    }
                }
            }
        }

        /// <summary>
        /// Writes leading bits if any and byte aligns the file.
        /// </summary>
        public void ByteAlign()
        {

            if (leadingBits > 0)
            {

                // write extra bits
                if (littleEndian)
                {
                    WriteByte(extraBits, true);
                }
                else
                {
                    WriteByte((byte)(extraBits << (8 - extraBitCount)), true);
                }

                leadingBits = 0;
                extraBitCount = 0;
                extraBits = 0;
            }
        }

        /// <summary>
        /// Writes a double to file.
        /// </summary>
        /// <param name="value">The double to write to file.</param>
        public void WriteDouble(double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            AppendBytes(buffer, 0, 8);
        }

        /// <summary>
        /// Writes a signed long value to file.
        /// </summary>
        /// <param name="value">The long to write to file.</param>
        public void WriteLong(long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            AppendBytes(buffer, 0, 8);
        }

        /// <summary>
        /// Writes an unsigned long value to file.
        /// </summary>
        /// <param name="value">The ulong to write to file.</param>
        public void WriteULong(ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            AppendBytes(buffer, 0, 8);
        }

        /// <summary>
        /// Writes a float to file.
        /// </summary>
        /// <param name="value">The float to write to file.</param>
        public void WriteFloat(float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            AppendBytes(buffer, 0, 4);
        }

        /// <summary>
        /// Writes an unsigned int to file.
        /// </summary>
        /// <param name="value">The uint to write to file.</param>
        public void WriteUInt(uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            AppendBytes(buffer, 0, 4);
        }

        /// <summary>
        /// Writes a signed int to file.
        /// </summary>
        /// <param name="value">The int to write to file.</param>
        public void WriteInt(int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            AppendBytes(buffer, 0, 4);
        }

        /// <summary>
        /// Writes an unsigned 24-bit int to file.
        /// </summary>
        /// <param name="value">The uint to write to file.</param>
        public void WriteUIntAsUTwentyFourBit(uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            AppendBytes(buffer, (littleEndian) ? 0 : 1, 3);
        }

        /// <summary>
        /// Writes a signed 24-bit int to file.
        /// </summary>
        /// <param name="value">The int to write to file.</param>
        public void WriteIntAsTwentyFourBit(int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            AppendBytes(buffer, (littleEndian) ? 0 : 1, 3);
        }

        /// <summary>
        /// Writes an unsigned short to file.
        /// </summary>
        /// <param name="value">The ushort to write to file.</param>
        public void WriteUShort(ushort value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            AppendBytes(buffer, 0, 2);
        }

        /// <summary>
        /// Writes a signed short to file.
        /// </summary>
        /// <param name="value">The short to write to file.</param>
        public void WriteShort(short value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            AppendBytes(buffer, 0, 2);
        }

        /// <summary>
        /// Writes an unsigned byte to file.
        /// </summary>
        /// <param name="value">The byte to write to file.</param>
        public void WriteUByte(byte value)
        {

            WriteByte(value, false);
        }

        /// <summary>
        /// Writes a signed byte to file.
        /// </summary>
        /// <param name="value">The sbyte to write to file.</param>
        public void WriteByte(sbyte value)
        {

            WriteByte((byte)value, false);
        }

        // writes a byte to file
        private void WriteByte(byte value, bool ignoreOffset)
        {

            if (!ignoreOffset && leadingBits != 0)
            {

                // bit shift a byte
                value = ManageBitOffset(value, leadingBits);
            }

            binaryWriter.Write(value);

            if (buildingCheckByteStream && checkByteStream != null)
            {
                checkByteStream.Add(value);
            }

            filePosition++;
        }

        /// <summary>
        /// Writes a boolean to file.
        /// </summary>
        /// <param name="value">The boolean to write to file.</param>
        public void WriteBool(bool value)
        {

            // value as byte
            byte byteValue = (value) ? (byte)1 : (byte)0;

            // write value
            WriteUByte(byteValue);
        }

        /// <summary>
        /// Writes an array of bytes to file.
        /// </summary>
        /// <param name="bytes">The bytes to write.</param>
        public void WriteBytes(byte[] bytes)
        {

            foreach (byte byteToWrite in bytes)
            {
                WriteUByte(byteToWrite);
            }
        }

        // Writes a byte array to the file.
        private void AppendBytes(byte[] bytesToAppend, int start, int end,
                        bool ignoreOffset)
        {

            // if reverse
            if (!littleEndian)
            {

                // reverse the order of bytes for Big endian output
                Array.Reverse(bytesToAppend);
            }

            // adjust file position
            filePosition += (end - start);

            // if leadingBits, deal with them
            if (!ignoreOffset && leadingBits != 0)
            {

                // bitshift bytes and add leadingBits
                for (int i = 0; i < end; i++)
                {

                    // bit shift a byte
                    bytesToAppend[i] = ManageBitOffset(bytesToAppend[i],
                            leadingBits);
                }

            }

            // write the bytes
            binaryWriter.Write(bytesToAppend, start, end);

            // append bytes to check byte stream
            if (buildingCheckByteStream && checkByteStream != null)
            {
                for (int i = start; i < end; i++)
                {
                    checkByteStream.Add(bytesToAppend[i]);
                }
            }
        }

        //  Writes a byte array to the file.
        private void AppendBytes(byte[] bytesToAppend, int start, int end)
        {

            AppendBytes(bytesToAppend, start, end, false);
        }

        /// <summary>
        /// Zero fill bytes till you reach the desired position.
        /// </summary>
        /// <param name="bytes">Amout of bytes to zero fill</param>
        public void SkipBytes(long bytes)
        {

            // method variables
            byte zeroFil = 0;

            for (int i = 0; i < bytes; i++)
            {
                WriteUByte(zeroFil);
            }

        }

        /// <summary>
        /// Closes the file.
        /// </summary>
        public void Close()
        {
            //byteAlign();
            binaryWriter.Close();
        }

        /// <summary>
        /// Manages a bit offset when bitshifting.
        /// </summary>
        /// <param name="value">The byte to deal with.</param>
        /// <param name="bitCount">The bits to offset by.</param>
        /// <returns></returns>
        private byte ManageBitOffset(byte value, byte bitCount)
        {

            // method variabls
            byte bitsToShiftIn;

            // set bitsToShiftIn
            bitsToShiftIn = extraBits;

            if (littleEndian)
            {

                // get extra bits from beginning of byte
                extraBits = (byte)((value >> (8 - bitCount))
                        & AND_VALUES[bitCount]);

                // bit shift left
                value = (byte)(value << bitCount);

                // set extra bit count
                extraBitCount = bitCount;

                value = (byte)(value | bitsToShiftIn);

            }
            else
            {

                // get extra bits from end of byte
                extraBits
                        = (byte)(value & AND_VALUES[bitCount]);

                // bit shift right
                value = (byte)(value >> bitCount);

                // set extra bit count
                extraBitCount = bitCount;

                value = (byte)(value | (bitsToShiftIn << (8 - bitCount)));
            }

            return value;
        }
    }
}
