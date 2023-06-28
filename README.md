# CSReaderWriterTest
This C# reader and writer allows you to write binary files with many more features than the BinaryWriter and Reader. This project allows you to write file that require more complex commands than what C#'s streams allow. You can 1, Read unsigned values, 2, Read and write 24-bit values 3, Alter the endieness of the file in case you need to write in big endian 4, Read and write non-byte aligned bit values up to 64 bits long.