using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderWriterTest.iostuff
{
    class Reader : IReadable
    {
        // constants
        public static readonly byte[] AND_VALUES = { 0, 1, 3, 7, 15, 31, 63, 127 };
        public static readonly bool SYSTEM_LITTLE_ENDIEN = BitConverter.IsLittleEndian;

        // instance variables
        // the data input stream used in reading file
        private BinaryReader binaryReader;
        // name of file
        private string fileName;
        // stores if file is reading in little endian or not
        private bool littleEndian;     
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

        /// <summary>
        /// The 2-args constructor for this object.
        /// </summary>
        /// <param name="fileName">The name of file.</param>
        /// <param name="littleEndian">Will write little endian if true.</param>
        public Reader(string fileName, bool littleEndian)
        {
            binaryReader = new BinaryReader(File.Open(fileName, FileMode.Open));
            this.littleEndian = littleEndian;
            filePosition = 0;
            trailingBits = 0;
            this.fileName = fileName;
            reverse = (SYSTEM_LITTLE_ENDIEN != littleEndian);
        }

        /// <summary>
        /// The 1-arg constructor for this object.It can only read big-endian.
        /// </summary>
        /// <param name="fileName">The name of file.</param>
        public Reader(string fileName) : this(fileName, false) { }

        /// <summary>
        /// The accessor method for the binary reader. 
        /// </summary>
        public BinaryReader BinaryReader
        {
            get { return binaryReader; }

            set { binaryReader = value; }
        }

        /// <summary>
        /// Gets the file's position.
        /// </summary>
        /// <returns>The file position.</returns>
        public long GetFilePosition()
        {
            return filePosition;
        }

        /// <summary>
        /// Resets the file position.
        /// </summary>
        /// <param name="filePosition">The position to reset to</param>
        public void SetFilePosition(long filePosition)
        {
            this.filePosition = filePosition;
            binaryReader.BaseStream.Position = filePosition;
        }

        /// <summary>
        /// Sets endianess of file.
        /// </summary>
        /// <param name="littleEndian">Little endian if true.</param>
        public void SetLittleEndian(bool littleEndian)
        {
            this.littleEndian = littleEndian;
            reverse = (SYSTEM_LITTLE_ENDIEN != littleEndian);
        }

        

        /// <summary>
        /// Reads byte string from file.
        /// </summary>
        /// <param name="length">The length of string to read.</param>
        /// <returns>The string to return.</returns>
        public string GetByteString(int length)
        {

            // get bytes
            convertBytes = GetBytes(length);

            // char for conversion
            char aChar;

            // stringBuilder
            StringBuilder byteStringBuilder = new StringBuilder();

            // loop through bytes
            foreach (byte aByte in convertBytes)
            {

                // convert byte to char
                aChar = (char)aByte;

                // append aChar to the string builder
                byteStringBuilder.Append(aChar);
            }

            // return string
            return byteStringBuilder.ToString();
        }

        /// <summary>
        /// Reads 2-byte character string from file.
        /// </summary>
        /// <param name="length">The length of string to read.</param>
        /// <returns>The string to return.</returns>
        public string GetCharString(int length)
        {

            // stringBuilder
            StringBuilder charStringBuilder = new StringBuilder();

            // get characters
            char aChar;

            // read shorts for array
            for (int i = 0; i < length; i++)
            {

                // read char short
                aChar = (char)GetShort();

                // append aChar to the string builder
                charStringBuilder.Append(aChar);
            }

            // return string
            return charStringBuilder.ToString();
        }

        /// <summary>
        /// Reads a UTF-8 encoded string from file.
        /// </summary>
        /// <returns>The string to return.</returns>
        public string GetUTF8String()
        {

            // length of UTF8 data
            ushort length = GetUShort();

            // get bytes
            convertBytes = GetBytes(length);

            // string for conversion
            string utfDecodedString
                        = Encoding.UTF8.GetString(convertBytes);

            // return string
            return utfDecodedString;
        }

        /// <summary>
        /// Reads an arbitrary bit length value from file.
        /// </summary>
        /// <param name="bits">The bit-width of value to read.</param>
        /// <param name="signed">Defines whether signed or unsigned.</param>
        /// <returns></returns>
        public long GetArbitraryBitValue(byte bits, bool signed)
        {

            // method variables
            int bytesToExtract;
            long returnValue = 0;
            // used in byte overflow tracking
            byte leadingBits = 0;
            bool byteOverflowing = false;
            bool trailingBitsProcessed = false;
            bool byteAlign = false;

            // extract bytes
            bytesToExtract = bits / 8;

            // get extra bit offset
            bitOffset = (byte)(bits % 8);

            // deal with bit offsetted values
            if (trailingBits != 0)
            {

                if (trailingBits < 8)
                {

                    if ((bitOffset > trailingBits)
                            || (trailingBits > 0 && trailingBits + bits > 8
                            && bitOffset != trailingBits))
                    {

                        // byte overflows the trailing bits
                        byteOverflowing = true;
                        leadingBits = trailingBits;
                        if (bitOffset > leadingBits)
                        {
                            bytesToExtract++;
                        }
                    }
                    else
                    {
                        // use to extract a few bits from extra bits
                        trailingBits -= bitOffset;
                        trailingBitsProcessed = true;
                        byteAlign = (trailingBits == 0);
                    }
                }
            }
            else if (bitOffset != 0)
            {
                trailingBits = (byte)(8 - bitOffset);
                bytesToExtract++;
            }

            // extract bytes
            convertBytes = ExtractBytes(bytesToExtract, false, true);

            if (littleEndian)
            {
                // the bits read (used in little endian bit reading)
                byte readBits = 0;

                // read value in big endian
                for (int i = 0; i < convertBytes.Length; i++)
                {

                    // deal with a byte align
                    if (byteAlign)
                    {

                        returnValue = (extraBits);
                        extraBits = 0;
                        i--;
                    } // deal with byte overflow
                    else if (byteOverflowing)
                    {
                        returnValue = (extraBits);
                        trailingBits = (byte)(bytesToExtract * 8 - (bits
                                - leadingBits));
                        bitOffset = leadingBits;
                        i--;
                    } // append byte to return value if not last byte or if there 
                      // are no trailing bits
                    else if (i < convertBytes.Length - 1 || trailingBits == 0)
                    {
                        returnValue |= ((long)(convertBytes[i] & 0xff)
                                << readBits);
                    }
                    else
                    {

                        // append trailingBits bits to return value
                        returnValue |= ((convertBytes[i]
                                & AND_VALUES[8 - trailingBits]) << readBits);
                    }

                    // deal with the byte overlow
                    if (byteOverflowing)
                    {
                        readBits += extraBitCount;
                        extraBitCount = 0;
                        extraBits = 0;
                        byteOverflowing = false;
                    } // bitshift one byte to return value if not the second to last 
                      // byte or there are no traling bits or is byte aligned
                    else if (i < convertBytes.Length - 1 && !byteAlign)
                    {

                        readBits += 8;
                    }
                    else if (byteAlign)
                    {

                        readBits += extraBitCount;
                        extraBitCount = 0;
                        byteAlign = false;
                    }
                    else
                    {

                        // set extra bits to the leading bits of last read byte
                        extraBits = (byte)((convertBytes[i] >> (8 - trailingBits))
                                & AND_VALUES[trailingBits]);

                        // set extra bit count
                        extraBitCount = trailingBits;
                    }
                }

                // return extra bits only if convertBytes length is zero
                if (convertBytes.Length == 0)
                {

                    if (extraBitCount > bitOffset)
                    {

                        // append trailingBits bits to return value
                        returnValue = extraBits & AND_VALUES[bitOffset];
                        if (!trailingBitsProcessed)
                        {
                            trailingBits -= bitOffset;
                        }
                    }
                    else
                    {

                        // return extraBits
                        returnValue = (extraBits & AND_VALUES[extraBitCount]);
                    }

                    if (trailingBits > 0)
                    {

                        // bit shift to get rid of unneeded trailing bits
                        extraBits = (byte)((extraBits >> (bitOffset))
                                & AND_VALUES[trailingBits]);
                        extraBitCount = trailingBits;
                    }
                }
            }
            else
            {
                // read value in big endian
                for (int i = 0; i < convertBytes.Length; i++)
                {

                    // deal with a byte align
                    if (byteAlign)
                    {

                        returnValue = (extraBits);
                        extraBitCount = 0;
                        extraBits = 0;
                        i--;
                    } // deal with byte overflow
                    else if (byteOverflowing)
                    {
                        returnValue = (extraBits);
                        trailingBits = (byte)(bytesToExtract * 8 - (bits - leadingBits));
                        bitOffset = leadingBits;
                        extraBitCount = 0;
                        extraBits = 0;
                        byteOverflowing = false;
                        i--;
                    } // append byte to return value if not last byte or if there are
                      // no trailing bits
                    else if (i < convertBytes.Length - 1 || trailingBits == 0)
                    {
                        returnValue |= (convertBytes[i] & 0xff);
                    }
                    else
                    {

                        // append trailingBits bits to return value
                        returnValue |= ((convertBytes[i] >> trailingBits)
                                & AND_VALUES[8 - trailingBits]);
                    }


                    // bitshift one byte to return value if not the second to last byte
                    // or there are no traling bits or is byte aligned
                    if (i < convertBytes.Length - 2 || byteAlign)
                    {

                        returnValue <<= 8;
                        byteAlign = false;
                    }
                    else if (i == convertBytes.Length - 2)
                    {

                        // bitshift 8 - tralingBits if second to last
                        returnValue <<= (8 - trailingBits);
                    }
                    else
                    {

                        // set extra bits to the traling bits of last read byte
                        extraBits = (byte)(convertBytes[i] & AND_VALUES[trailingBits]);

                        // set extra bit count
                        extraBitCount = trailingBits;
                    }
                }

                // return extra bits only if convertBytes length is zero
                if (convertBytes.Length == 0)
                {

                    if (extraBitCount > bitOffset)
                    {

                        // append trailingBits bits to return value
                        returnValue = (extraBits >> extraBitCount - bitOffset)
                                & AND_VALUES[bitOffset];
                        if (!trailingBitsProcessed)
                        {
                            trailingBits -= bitOffset;
                        }
                    }
                    else
                    {

                        // return extraBits
                        returnValue = (extraBits & AND_VALUES[extraBitCount]);
                    }

                    if (trailingBits > 0)
                    {

                        // bit shift to get rid of unneeded trailing bits
                        extraBits = (byte)((extraBits >> extraBitCount - trailingBits)
                                & AND_VALUES[trailingBits]);
                        extraBitCount = trailingBits;
                    }
                }
            }

            if (trailingBits == 0)
            {

                // byte align
                ByteAlign();
            }

            // making header bits all 1 if signed and first bit is 1
            if (signed && returnValue >> (bits - 1) == 1)
            {

                // iterate through extra header bits and make them 1
                returnValue <<= (64 - bits);
                returnValue >>= (64 - bits);
            }

            return returnValue;
        }

        /// <summary>
        /// Byte aligns the file.
        /// </summary>
        public void ByteAlign()
        {

            trailingBits = 0;
            extraBitCount = 0;
            extraBits = 0;
        }

        /// <summary>
        /// Reads a double from file.
        /// </summary>
        /// <returns>The double to return.</returns>
        public double GetDouble()
        {

            // get 8 bytes
            convertBytes = ExtractBytes(8, reverse);

            // unrap value to return
            double returnValue = BitConverter.ToDouble(convertBytes, 0);

            return returnValue;
        }

        /// <summary>
        /// Reads an unsigned long from file.
        /// </summary>
        /// <returns>The ulong to return.</returns>
        public ulong GetULong()
        {

            // get 8 bytes
            convertBytes = ExtractBytes(8, reverse);

            // unrap value to return
            ulong returnValue = BitConverter.ToUInt64(convertBytes, 0);

            return returnValue;
        }

        /// <summary>
        /// Reads a long from file.
        /// </summary>
        /// <returns>The long to return.</returns>
        public long GetLong()
        {

            // get 8 bytes
            convertBytes = ExtractBytes(8, reverse);

            // unrap value to return
            long returnValue = BitConverter.ToInt64(convertBytes, 0);

            return returnValue;
        }

        /// <summary>
        /// Reads a float from file.
        /// </summary>
        /// <returns>The float to return.</returns>
        public float GetFloat()
        {
            // get 4 bytes
            convertBytes = ExtractBytes(4, reverse);

            // unrap value to return
            float returnValue = BitConverter.ToSingle(convertBytes, 0);

            return returnValue;
        }

        /// <summary>
        /// Reads an unsigned integer from file.
        /// </summary>
        /// <returns>The unsigned int to return.</returns>
        public uint GetUInt()
        {

            // get 4 bytes
            convertBytes = ExtractBytes(4, reverse);

            // unrap value to return
            uint returnValue = BitConverter.ToUInt32(convertBytes, 0);

            return returnValue;
        }

        /// <summary>
        /// Reads a signed integer from file.
        /// </summary>
        /// <returns>The int to return.</returns>
        public int GetInt()
        {

            // get 4 bytes
            convertBytes = ExtractBytes(4, reverse);

            // unrap value to return
            int returnValue = BitConverter.ToInt32(convertBytes, 0);

            return returnValue;
        }

        /// <summary>
        /// Reads an unsigned 24-bit value from file.
        /// </summary>
        /// <returns>The uint to return.</returns>
        public uint GetU24BitInt()
        {

            // get 3 bytes
            convertBytes = ExtractBytes(3, reverse);

            // array to buffer in
            byte[] intBytes = { convertBytes[0], convertBytes[1], convertBytes[2], 0 };

            // unrap value to return
            uint returnValue = BitConverter.ToUInt32(intBytes, 0);

            return returnValue;
        }

        /// <summary>
        /// Reads a signed 24-bit value from file.
        /// </summary>
        /// <returns>The int to return.</returns>
        public int Get24BitInt()
        {

            // get 3 bytes
            convertBytes = ExtractBytes(3, reverse);

            // array to buffer in
            byte[] intBytes = { convertBytes[0], convertBytes[1], convertBytes[2], 0 };

            // check if it is negative and then add 11111111 to set it negative
            if (((convertBytes[0] >> 7) & 1) == 1)
            {
                intBytes[3] = (byte)0xff;
            }

            // unrap value to return
            int returnValue = BitConverter.ToInt32(intBytes, 0);

            return returnValue;
        }

        /// <summary>
        /// Reads an unsigned short from file.
        /// </summary>
        /// <returns>The ushort to return.</returns>
        public ushort GetUShort()
        {

            // get 2 bytes
            convertBytes = ExtractBytes(2, reverse);

            // unrap value to return
            ushort returnValue = BitConverter.ToUInt16(convertBytes, 0);

            return returnValue;
        }

        /// <summary>
        /// Reads a signed short from file.
        /// </summary>
        /// <returns>The short to return.</returns>
        public short GetShort()
        {

            // get 2 bytes
            convertBytes = ExtractBytes(2, reverse);

            // unrap value to return
            short returnValue = BitConverter.ToInt16(convertBytes, 0);

            return returnValue;
        }

        /// <summary>
        /// Reads an unsigned byte from file.
        /// </summary>
        /// <returns>The byte to return.</returns>
        public byte GetUByte()
        {

            // get byte
            byte convertedByte = binaryReader.ReadByte();

            // increment file position
            filePosition++;

            // bit shift to right position if there are traling bits
            if (extraBitCount != 0)
            {

                convertedByte = ManageBitOffset(convertedByte, extraBitCount);
            }

            return convertedByte;
        }

        /// <summary>
        /// Reads a signed byte from file.
        /// </summary>
        /// <returns>The sbyte to return.</returns>
        public sbyte GetByte()
        {

            // get byte
            sbyte convertedByte = binaryReader.ReadSByte();

            // increment file position
            filePosition++;

            // bit shift to right position if there are traling bits
            if (extraBitCount != 0)
            {

                convertedByte = (sbyte)ManageBitOffset((byte)convertedByte, extraBitCount);
            }

            return convertedByte;
        }

        /// <summary>
        /// Reads a boolean value from file
        /// </summary>
        /// <returns>The boolean to return.</returns>
        public bool GetBoolean()
        {
            // return value
            bool returnValue = false;

            // value is true if read byte > 0
            if (GetUByte() != 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Reads a byte array from file
        /// </summary>
        /// <param name="bytesToExtract">The amount of bytes to extract.</param>
        /// <returns>The byte array to return.</returns>
        public byte[] GetBytes(int bytesToExtract)
        {

            // method variabls
            byte[] extractedBytes = new byte[bytesToExtract];
            byte bitsToShiftIn;

            // extract bytes
            binaryReader.Read(extractedBytes, 0, bytesToExtract);

            // increment file pos by number of bytes read
            filePosition += bytesToExtract;

            // deal with bit offset if any
            if (extraBitCount != 0)
            {

                // bitshift bytes tralingBits times
                for (int i = 0; i < extractedBytes.Length; i++)
                {

                    extractedBytes[i]
                            = ManageBitOffset(extractedBytes[i], extraBitCount);
                }
            }

            return extractedBytes;
        }

        /// <summary>
        /// Extracts a byte array to build other data type from.
        /// </summary>
        /// <param name="bytesToExtract">The number of bytes to extract.</param>
        /// <param name="reverse">Reversed bytes if true.</param>
        /// <param name="ignoreOffset">Ignores traling bits if true.</param>
        /// <returns>The byte array to return.</returns>
        private byte[] ExtractBytes(int bytesToExtract, bool reverse,
                bool ignoreOffset)
        {

            // define byte array
            byte[] extractedBytes = new byte[bytesToExtract];

            // extract bytes
            binaryReader.Read(extractedBytes, 0, bytesToExtract);

            // increment file pos by number of bytes read
            filePosition += bytesToExtract;

            // deal with bit offset if any
            if (!ignoreOffset && extraBitCount != 0)
            {

                // bitshift bytes tralingBits times
                for (int i = 0; i < extractedBytes.Length; i++)
                {

                    extractedBytes[i]
                            = ManageBitOffset(extractedBytes[i], extraBitCount);
                }
            }

            // if reverse
            if (reverse)
            {

                // reverse the order of bytes for specified endian output
                Array.Reverse(extractedBytes);
            }

            return extractedBytes;
        }

        /**
         * Extracts a byte array to build other data type from.
         */
        private byte[] ExtractBytes(int bytesToExtract, bool reverse)
        {
            return ExtractBytes(bytesToExtract, reverse, false);
        }

        /// <summary>
        /// Skip a provided number of bytes.
        /// </summary>
        /// <param name="bytes">The amount of bytes to skip.</param>
        /// <returns>returns true if skip is successful.</returns>
        public bool SkipBytes(long bytes)
        {

            bool skipped = false;

            if (binaryReader.BaseStream.Length > bytes)
            {
                binaryReader.BaseStream.Position = bytes;
                filePosition += bytes;
                skipped = true;
            }

            return skipped;
        }

        /// <summary>
        /// Returns available bytes.
        /// </summary>
        /// <returns>Returns the length of the file.</returns>
        public long Available()
        {
            return binaryReader.BaseStream.Length;
        }

        /// <summary>
        /// Closes the reader.
        /// </summary>
        public void Close()
        {
            binaryReader.Close();
        }

        /// <summary>
        /// Processes any bit-shifting needed on the value.
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

                // bit shift a byte
                value = (byte)((value >> bitCount) & AND_VALUES[8 - bitCount]);

                // set extra bit count
                extraBitCount = bitCount;

                value = (byte)(value | (bitsToShiftIn << (8 - bitCount)));
            }

            return value;
        }
    }
}
