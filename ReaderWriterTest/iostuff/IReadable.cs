using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderWriterTest.iostuff
{
    internal interface IReadable
    {

        /// <summary>
        /// Gets the file's position.
        /// </summary>
        /// <returns>The file position.</returns>
        public long GetFilePosition();

        /// <summary>
        /// Resets the file position.
        /// </summary>
        /// <param name="filePosition">The position to reset to</param>
        public void SetFilePosition(long filePosition);

        /// <summary>
        /// Sets endianess of file.
        /// </summary>
        /// <param name="littleEndian">Little endian if true.</param>
        public void SetLittleEndian(bool littleEndian);

        /// <summary>
        /// Reads byte string from file.
        /// </summary>
        /// <param name="length">The length of string to read.</param>
        /// <returns>The string to return.</returns>
        public string GetByteString(int length);

        /// <summary>
        /// Reads 2-byte character string from file.
        /// </summary>
        /// <param name="length">The length of string to read.</param>
        /// <returns>The string to return.</returns>
        public string GetCharString(int length);

        /// <summary>
        /// Reads a UTF-8 encoded string from file.
        /// </summary>
        /// <returns>The string to return.</returns>
        public string GetUTF8String();

        /// <summary>
        /// Reads an arbitrary bit length value from file.
        /// </summary>
        /// <param name="bits">The bit-width of value to read.</param>
        /// <param name="signed">Defines whether signed or unsigned.</param>
        /// <returns></returns>
        public long GetArbitraryBitValue(byte bits, bool signed);

        /// <summary>
        /// Byte aligns the file.
        /// </summary>
        public void ByteAlign();

        /// <summary>
        /// Reads a double from file.
        /// </summary>
        /// <returns>The double to return.</returns>
        public double GetDouble();

        /// <summary>
        /// Reads an unsigned long from file.
        /// </summary>
        /// <returns>The ulong to return.</returns>
        public ulong GetULong();

        /// <summary>
        /// Reads a long from file.
        /// </summary>
        /// <returns>The long to return.</returns>
        public long GetLong();

        /// <summary>
        /// Reads a float from file.
        /// </summary>
        /// <returns>The float to return.</returns>
        public float GetFloat();

        /// <summary>
        /// Reads an unsigned integer from file.
        /// </summary>
        /// <returns>The unsigned int to return.</returns>
        public uint GetUInt();

        /// <summary>
        /// Reads a signed integer from file.
        /// </summary>
        /// <returns>The int to return.</returns>
        public int GetInt();

        /// <summary>
        /// Reads an unsigned 24-bit value from file.
        /// </summary>
        /// <returns>The uint to return.</returns>
        public uint GetU24BitInt();

        /// <summary>
        /// Reads a signed 24-bit value from file.
        /// </summary>
        /// <returns>The int to return.</returns>
        public int Get24BitInt();

        /// <summary>
        /// Reads an unsigned short from file.
        /// </summary>
        /// <returns>The ushort to return.</returns>
        public ushort GetUShort();

        /// <summary>
        /// Reads a signed short from file.
        /// </summary>
        /// <returns>The short to return.</returns>
        public short GetShort();

        /// <summary>
        /// Reads an unsigned byte from file.
        /// </summary>
        /// <returns>The byte to return.</returns>
        public byte GetUByte();

        /// <summary>
        /// Reads a signed byte from file.
        /// </summary>
        /// <returns>The sbyte to return.</returns>
        public sbyte GetByte();

        /// <summary>
        /// Reads a boolean value from file
        /// </summary>
        /// <returns>The boolean to return.</returns>
        public bool GetBoolean();

        /// <summary>
        /// Reads a byte array from file
        /// </summary>
        /// <param name="bytesToExtract">The amount of bytes to extract.</param>
        /// <returns>The byte array to return.</returns>
        public byte[] GetBytes(int bytesToExtract);

        /// <summary>
        /// Skip a provided number of bytes.
        /// </summary>
        /// <param name="bytes">The amount of bytes to skip.</param>
        /// <returns>returns true if skip is successful.</returns>
        public bool SkipBytes(long bytes);

        /// <summary>
        /// Returns available bytes.
        /// </summary>
        /// <returns>Returns the length of the file.</returns>
        public long Available();

        /// <summary>
        /// Closes the reader.
        /// </summary>
        public void Close();
    }
}
