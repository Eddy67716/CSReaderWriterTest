using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderWriterTest.iostuff
{
    internal interface IWritable
    {
        /// <summary>
        /// The getter for FilePosition;
        /// </summary>
        public long GetFilePosition();

        /// <summary>
        /// Starts saving a check byte stream that can be used for 
        /// CRC or other checks.
        /// </summary>
        public void BuildCheckByteStream();

        /// <summary>
        /// Gets the check byte stream that has been saved.
        /// </summary>
        /// <returns>the check byte stream.</returns> 
        public byte[] GetCheckByteStream();

        /// <summary>
        /// Resets the check byte stream.
        /// </summary>
        public void ResetCheckByteStream();

        /// <summary>
        /// End the check byte stream.
        /// </summary>
        public void EndCheckByteStream();

        /// <summary>
        /// The setter for FilePosition;
        /// </summary>
        public void SetFilePosition(long filePosition);

        /// <summary>
        /// Writes a byte string.
        /// </summary>
        /// <param name="outputString">The string to output as bytes</param>
        public void WriteByteString(string outputString);

        /// <summary>
        /// Writes a 2-byte char string.
        /// </summary>
        /// <param name="outputString">The string to output</param>
        public void WriteCharString(string outputString);

        /// <summary>
        /// Writes a UTF 8 encoded string.
        /// </summary>
        /// <param name="outputString">The string to encode in UTF8.</param>
        public void WriteUTF8String(string outputString);

        /// <summary>
        /// Writes a value that is of an arbitrary bit length to file.
        /// </summary>
        /// <param name="value">The long to write to file.</param>
        /// <param name="bits">The amount of bits the value takes up.</param>
        public void WriteArbitraryBitValue(long value, byte bits);

        /// <summary>
        /// Writes leading bits if any and byte aligns the file.
        /// </summary>
        public void ByteAlign();

        /// <summary>
        /// Writes a double to file.
        /// </summary>
        /// <param name="value">The double to write to file.</param>
        public void WriteDouble(double value);

        /// <summary>
        /// Writes a signed long value to file.
        /// </summary>
        /// <param name="value">The long to write to file.</param>
        public void WriteLong(long value);

        /// <summary>
        /// Writes an unsigned long value to file.
        /// </summary>
        /// <param name="value">The ulong to write to file.</param>
        public void WriteULong(ulong value);

        /// <summary>
        /// Writes a float to file.
        /// </summary>
        /// <param name="value">The float to write to file.</param>
        public void WriteFloat(float value);

        /// <summary>
        /// Writes an unsigned int to file.
        /// </summary>
        /// <param name="value">The uint to write to file.</param>
        public void WriteUInt(uint value);

        /// <summary>
        /// Writes a signed int to file.
        /// </summary>
        /// <param name="value">The int to write to file.</param>
        public void WriteInt(int value);

        /// <summary>
        /// Writes an unsigned 24-bit int to file.
        /// </summary>
        /// <param name="value">The uint to write to file.</param>
        public void WriteUIntAsUTwentyFourBit(uint value);

        /// <summary>
        /// Writes a signed 24-bit int to file.
        /// </summary>
        /// <param name="value">The int to write to file.</param>
        public void WriteIntAsTwentyFourBit(int value);

        /// <summary>
        /// Writes an unsigned short to file.
        /// </summary>
        /// <param name="value">The ushort to write to file.</param>
        public void WriteUShort(ushort value);

        /// <summary>
        /// Writes a signed short to file.
        /// </summary>
        /// <param name="value">The short to write to file.</param>
        public void WriteShort(short value);

        /// <summary>
        /// Writes an unsigned byte to file.
        /// </summary>
        /// <param name="value">The byte to write to file.</param>
        public void WriteUByte(byte value);

        /// <summary>
        /// Writes a signed byte to file.
        /// </summary>
        /// <param name="value">The sbyte to write to file.</param>
        public void WriteByte(sbyte value);



        /// <summary>
        /// Writes a boolean to file.
        /// </summary>
        /// <param name="value">The boolean to write to file.</param>
        public void WriteBool(bool value);

        /// <summary>
        /// Writes an array of bytes to file.
        /// </summary>
        /// <param name="bytes">The bytes to write.</param>
        public void WriteBytes(byte[] bytes);

        /// <summary>
        /// Zero fill bytes till you reach the desired position.
        /// </summary>
        /// <param name="bytes">Amout of bytes to zero fill</param>
        public void SkipBytes(long bytes);

        /// <summary>
        /// Closes the file.
        /// </summary>
        public void Close();
    }
}
