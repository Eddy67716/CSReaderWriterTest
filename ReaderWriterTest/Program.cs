using ReaderWriterTest.iostuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderWriterTest
{
    class Program
    {
        // values to write
        private static bool endianess = IOMethods.LITTLE_ENDIAN;
        private static string string1 = "This is a string";
        private static byte byteValueOne = 127;
        private static sbyte byteValueTwo = -1;
        private static sbyte byteValueThree = -64;
        private static byte byteValueFour = 67;
        private static ushort uShort1 = 2544;
        private static ushort uShort2 = 32769;
        private static int threeByteInt = 8000000;
        private static long fortyEightBitValue = 233474976711655L;
        private static byte oneBitValue = 1;
        private static byte twoBitValueOne = 3;
        private static byte twoBitValueTwo = 2;
        private static byte threeBitValue = 5;
        private static byte nybbleOne = 9;
        private static byte fiveBitValue = 17;
        private static sbyte fiveBitValueTwo = -16;
        private static byte sixBitValue = 33;
        private static byte sevenBitValueOne = 65;
        private static sbyte sevenBitValueTwo = -64;
        private static short nineBitValue = 257;
        private static short tenBitValue = 513;
        private static short twelveBitValueOne = 4095;
        private static short twelveBitValueTwo = 4000;
        private static short thirteenBitValue = 4097;
        private static short fifteenBitValueOne = 16385;
        private static short fifteenBitValueTwo = 32000;
        private static short fifteenBitValueThree = 16000;
        private static short fifteenBitValueFour = -16000;
        private static int twentyBitValue = 524289;
        private static int twentyThreeBitValue = 4194305;

        static void Main(string[] args)
        {

            Write();
            Read();
        }

        public static void Write()
        {

            // writer
            IWritable writer;

            // write
            writer = new Writer("bits.dat", endianess);

            writer.WriteUTF8String(string1);
            writer.WriteUShort(uShort1);
            writer.WriteIntAsTwentyFourBit(threeByteInt);
            writer.WriteArbitraryBitValue(fiveBitValue, 5);
            writer.WriteArbitraryBitValue(byteValueOne, 8);
            writer.WriteArbitraryBitValue(threeBitValue, 3);
            writer.WriteByte(byteValueTwo);
            writer.WriteArbitraryBitValue(twelveBitValueOne, 12);
            writer.WriteArbitraryBitValue(twelveBitValueTwo, 12);
            writer.WriteByte(byteValueThree);
            writer.WriteArbitraryBitValue(fifteenBitValueOne, 15);
            writer.WriteArbitraryBitValue(oneBitValue, 1);
            writer.WriteUByte(byteValueFour);
            writer.WriteArbitraryBitValue(twoBitValueOne, 2);
            writer.WriteArbitraryBitValue(twoBitValueTwo, 2);
            writer.ByteAlign();
            writer.WriteArbitraryBitValue(fortyEightBitValue, 48);
            writer.WriteArbitraryBitValue(sevenBitValueOne, 7);
            writer.WriteArbitraryBitValue(tenBitValue, 10);
            writer.WriteArbitraryBitValue(sevenBitValueOne, 7);
            writer.WriteArbitraryBitValue(twoBitValueOne, 2);
            writer.WriteArbitraryBitValue(twelveBitValueOne, 12);
            writer.WriteArbitraryBitValue(twoBitValueOne, 2);
            writer.WriteArbitraryBitValue(thirteenBitValue, 13);
            writer.WriteArbitraryBitValue(fifteenBitValueTwo, 15);
            writer.WriteArbitraryBitValue(nybbleOne, 4);
            writer.WriteArbitraryBitValue(twoBitValueOne, 2);
            writer.WriteArbitraryBitValue(fifteenBitValueTwo, 15);
            writer.WriteArbitraryBitValue(fifteenBitValueThree, 15);
            writer.WriteArbitraryBitValue(twentyThreeBitValue, 23);
            writer.WriteArbitraryBitValue(twentyThreeBitValue, 23);
            writer.ByteAlign();
            writer.WriteArbitraryBitValue(oneBitValue, 1);
            writer.WriteArbitraryBitValue(oneBitValue, 1);
            writer.WriteArbitraryBitValue(oneBitValue, 1);
            writer.WriteArbitraryBitValue(oneBitValue, 1);
            writer.WriteArbitraryBitValue(oneBitValue, 1);
            writer.WriteArbitraryBitValue(oneBitValue, 1);
            writer.WriteArbitraryBitValue(oneBitValue, 1);
            writer.WriteArbitraryBitValue(oneBitValue, 1);
            writer.WriteArbitraryBitValue(twoBitValueOne, 2);
            writer.WriteArbitraryBitValue(twoBitValueOne, 2);
            writer.WriteArbitraryBitValue(twoBitValueOne, 2);
            writer.WriteArbitraryBitValue(twoBitValueOne, 2);
            writer.WriteArbitraryBitValue(threeBitValue, 3);
            writer.WriteArbitraryBitValue(threeBitValue, 3);
            writer.WriteArbitraryBitValue(threeBitValue, 3);
            writer.WriteArbitraryBitValue(threeBitValue, 3);
            writer.WriteArbitraryBitValue(threeBitValue, 3);
            writer.WriteArbitraryBitValue(threeBitValue, 3);
            writer.WriteArbitraryBitValue(threeBitValue, 3);
            writer.WriteArbitraryBitValue(threeBitValue, 3);
            writer.WriteArbitraryBitValue(nybbleOne, 4);
            writer.WriteArbitraryBitValue(nybbleOne, 4);
            writer.WriteArbitraryBitValue(nybbleOne, 4);
            writer.WriteArbitraryBitValue(nybbleOne, 4);
            writer.WriteArbitraryBitValue(fiveBitValue, 5);
            writer.WriteArbitraryBitValue(fiveBitValue, 5);
            writer.WriteArbitraryBitValue(fiveBitValue, 5);
            writer.WriteArbitraryBitValue(fiveBitValue, 5);
            writer.WriteArbitraryBitValue(fiveBitValue, 5);
            writer.WriteArbitraryBitValue(fiveBitValue, 5);
            writer.WriteArbitraryBitValue(fiveBitValue, 5);
            writer.WriteArbitraryBitValue(fiveBitValue, 5);
            writer.WriteArbitraryBitValue(sevenBitValueOne, 7);
            writer.WriteArbitraryBitValue(sevenBitValueOne, 7);
            writer.WriteArbitraryBitValue(sevenBitValueOne, 7);
            writer.WriteArbitraryBitValue(sevenBitValueOne, 7);
            writer.WriteArbitraryBitValue(sevenBitValueOne, 7);
            writer.WriteArbitraryBitValue(sevenBitValueOne, 7);
            writer.WriteArbitraryBitValue(sevenBitValueOne, 7);
            writer.WriteArbitraryBitValue(sevenBitValueOne, 7);
            writer.WriteArbitraryBitValue(nineBitValue, 9);
            writer.WriteArbitraryBitValue(nineBitValue, 9);
            writer.WriteArbitraryBitValue(nineBitValue, 9);
            writer.WriteArbitraryBitValue(nineBitValue, 9);
            writer.WriteArbitraryBitValue(nineBitValue, 9);
            writer.WriteArbitraryBitValue(nineBitValue, 9);
            writer.WriteArbitraryBitValue(nineBitValue, 9);
            writer.WriteArbitraryBitValue(nineBitValue, 9);
            writer.WriteArbitraryBitValue(fifteenBitValueFour, 15);
            writer.WriteArbitraryBitValue(fifteenBitValueOne, 15);
            writer.WriteArbitraryBitValue(fifteenBitValueTwo, 15);
            writer.WriteArbitraryBitValue(fifteenBitValueThree, 15);
            writer.WriteArbitraryBitValue(fifteenBitValueFour, 15);
            writer.WriteArbitraryBitValue(fifteenBitValueOne, 15);
            writer.WriteArbitraryBitValue(fifteenBitValueTwo, 15);
            writer.WriteArbitraryBitValue(fifteenBitValueThree, 15);
            writer.WriteArbitraryBitValue(tenBitValue, 10);
            writer.WriteArbitraryBitValue(twelveBitValueOne, 12);
            writer.WriteArbitraryBitValue(twoBitValueOne, 2);
            writer.WriteArbitraryBitValue(fiveBitValueTwo, 5);
            writer.WriteArbitraryBitValue(uShort2, 16);
            writer.WriteArbitraryBitValue(sevenBitValueTwo, 7);
            writer.ByteAlign();
            writer.Close();
        }

        public static void Read()
        {
            // read
            IReadable reader = new Reader("bits.dat", endianess);
            string readString;
            ushort readShort;
            int readInt;
            long readValue;

            try
            {
                readString = reader.GetUTF8String();
                if (readString != string1)
                {
                    Console.WriteLine("Error, " + readString + " is not equal to :"
                            + fiveBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(readString);
                }
                readShort = reader.GetUShort();
                if (readShort != uShort1)
                {
                    Console.WriteLine("Error, " + readShort + " is not equal to :"
                            + uShort1 + ". ");
                }
                else
                {
                    Console.WriteLine(readShort);
                }
                readInt = reader.Get24BitInt();
                if (readInt != threeByteInt)
                {
                    Console.WriteLine("Error, " + readInt + " is not equal to :"
                            + threeByteInt + ". ");
                }
                else
                {
                    Console.WriteLine(threeByteInt);
                }
                readValue = reader.GetArbitraryBitValue(5, false);
                if (readValue != fiveBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fiveBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(fiveBitValue);
                }
                readValue = reader.GetByte();
                if (readValue != byteValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + byteValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(byteValueOne);
                }
                readValue = reader.GetArbitraryBitValue(3, false);
                if (readValue != threeBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + threeBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(threeBitValue);
                }
                readValue = reader.GetByte();
                if (readValue != byteValueTwo)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + byteValueTwo + ". ");
                }
                else
                {
                    Console.WriteLine(byteValueTwo);
                }
                readValue = reader.GetArbitraryBitValue(12, false);
                if (readValue != twelveBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twelveBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(twelveBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(12, false);
                if (readValue != twelveBitValueTwo)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twelveBitValueTwo + ". ");
                }
                else
                {
                    Console.WriteLine(twelveBitValueTwo);
                }
                readValue = reader.GetByte();
                if (readValue != byteValueThree)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + byteValueThree + ". ");
                }
                else
                {
                    Console.WriteLine(byteValueThree);
                }
                readValue = reader.GetArbitraryBitValue(15, false);
                if (readValue != fifteenBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fifteenBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(fifteenBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(1, false);
                if (readValue != oneBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + oneBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(oneBitValue);
                }
                readValue = reader.GetByte();
                if (readValue != byteValueFour)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + byteValueFour + ". ");
                }
                else
                {
                    Console.WriteLine(byteValueFour);
                }
                readValue = reader.GetArbitraryBitValue(2, false);
                if (readValue != twoBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twoBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(twoBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(2, false);
                if (readValue != twoBitValueTwo)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twoBitValueTwo + ". ");
                }
                else
                {
                    Console.WriteLine(twoBitValueTwo);
                }
                reader.ByteAlign();
                readValue = reader.GetArbitraryBitValue(48, false);
                if (readValue != fortyEightBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fortyEightBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(fortyEightBitValue);
                }
                readValue = reader.GetArbitraryBitValue(7, false);
                if (readValue != sevenBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + sevenBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(sevenBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(10, false);
                if (readValue != tenBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + tenBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(tenBitValue);
                }
                readValue = reader.GetArbitraryBitValue(7, false);
                if (readValue != sevenBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + sevenBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(sevenBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(2, false);
                if (readValue != twoBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twoBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(twoBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(12, false);
                if (readValue != twelveBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twelveBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(twelveBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(2, false);
                if (readValue != twoBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twoBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(twoBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(13, false);
                if (readValue != thirteenBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + thirteenBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(thirteenBitValue);
                }
                readValue = reader.GetArbitraryBitValue(15, false);
                if (readValue != fifteenBitValueTwo)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fifteenBitValueTwo + ". ");
                }
                else
                {
                    Console.WriteLine(fifteenBitValueTwo);
                }
                readValue = reader.GetArbitraryBitValue(4, false);
                if (readValue != nybbleOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + nybbleOne + ". ");
                }
                else
                {
                    Console.WriteLine(nybbleOne);
                }
                readValue = reader.GetArbitraryBitValue(2, false);
                if (readValue != twoBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twoBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(twoBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(15, false);
                if (readValue != fifteenBitValueTwo)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fifteenBitValueTwo + ". ");
                }
                else
                {
                    Console.WriteLine(fifteenBitValueTwo);
                }
                readValue = reader.GetArbitraryBitValue(15, false);
                if (readValue != fifteenBitValueThree)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fifteenBitValueThree + ". ");
                }
                else
                {
                    Console.WriteLine(fifteenBitValueThree);
                }
                readValue = reader.GetArbitraryBitValue(23, false);
                if (readValue != twentyThreeBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twentyThreeBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(twentyThreeBitValue);
                }
                readValue = reader.GetArbitraryBitValue(23, false);
                if (readValue != twentyThreeBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twentyThreeBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(twentyThreeBitValue);
                }
                reader.ByteAlign();
                readValue = reader.GetArbitraryBitValue(1, false);
                if (readValue != oneBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + oneBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(oneBitValue);
                }
                readValue = reader.GetArbitraryBitValue(1, false);
                if (readValue != oneBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + oneBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(oneBitValue);
                }
                readValue = reader.GetArbitraryBitValue(1, false);
                if (readValue != oneBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + oneBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(oneBitValue);
                }
                readValue = reader.GetArbitraryBitValue(1, false);
                if (readValue != oneBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + oneBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(oneBitValue);
                }
                readValue = reader.GetArbitraryBitValue(1, false);
                if (readValue != oneBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + oneBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(oneBitValue);
                }
                readValue = reader.GetArbitraryBitValue(1, false);
                if (readValue != oneBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + oneBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(oneBitValue);
                }
                readValue = reader.GetArbitraryBitValue(1, false);
                if (readValue != oneBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + oneBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(oneBitValue);
                }
                readValue = reader.GetArbitraryBitValue(1, false);
                if (readValue != oneBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + oneBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(oneBitValue);
                }
                readValue = reader.GetArbitraryBitValue(2, false);
                if (readValue != twoBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twoBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(twoBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(2, false);
                if (readValue != twoBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twoBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(twoBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(2, false);
                if (readValue != twoBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twoBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(twoBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(2, false);
                if (readValue != twoBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twoBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(twoBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(3, false);
                if (readValue != threeBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + threeBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(threeBitValue);
                }
                readValue = reader.GetArbitraryBitValue(3, false);
                if (readValue != threeBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + threeBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(threeBitValue);
                }
                readValue = reader.GetArbitraryBitValue(3, false);
                if (readValue != threeBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + threeBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(threeBitValue);
                }
                readValue = reader.GetArbitraryBitValue(3, false);
                if (readValue != threeBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + threeBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(threeBitValue);
                }
                readValue = reader.GetArbitraryBitValue(3, false);
                if (readValue != threeBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + threeBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(threeBitValue);
                }
                readValue = reader.GetArbitraryBitValue(3, false);
                if (readValue != threeBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + threeBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(threeBitValue);
                }
                readValue = reader.GetArbitraryBitValue(3, false);
                if (readValue != threeBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + threeBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(threeBitValue);
                }
                readValue = reader.GetArbitraryBitValue(3, false);
                if (readValue != threeBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + threeBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(threeBitValue);
                }

                readValue = reader.GetArbitraryBitValue(4, false);
                if (readValue != nybbleOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + nybbleOne + ". ");
                }
                else
                {
                    Console.WriteLine(nybbleOne);
                }
                readValue = reader.GetArbitraryBitValue(4, false);
                if (readValue != nybbleOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + nybbleOne + ". ");
                }
                else
                {
                    Console.WriteLine(nybbleOne);
                }
                readValue = reader.GetArbitraryBitValue(4, false);
                if (readValue != nybbleOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + nybbleOne + ". ");
                }
                else
                {
                    Console.WriteLine(nybbleOne);
                }
                readValue = reader.GetArbitraryBitValue(4, false);
                if (readValue != nybbleOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + nybbleOne + ". ");
                }
                else
                {
                    Console.WriteLine(nybbleOne);
                }
                readValue = reader.GetArbitraryBitValue(5, false);
                if (readValue != fiveBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fiveBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(fiveBitValue);
                }
                readValue = reader.GetArbitraryBitValue(5, false);
                if (readValue != fiveBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fiveBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(fiveBitValue);
                }
                readValue = reader.GetArbitraryBitValue(5, false);
                if (readValue != fiveBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fiveBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(fiveBitValue);
                }
                readValue = reader.GetArbitraryBitValue(5, false);
                if (readValue != fiveBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fiveBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(fiveBitValue);
                }
                readValue = reader.GetArbitraryBitValue(5, false);
                if (readValue != fiveBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fiveBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(fiveBitValue);
                }
                readValue = reader.GetArbitraryBitValue(5, false);
                if (readValue != fiveBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fiveBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(fiveBitValue);
                }
                readValue = reader.GetArbitraryBitValue(5, false);
                if (readValue != fiveBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fiveBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(fiveBitValue);
                }
                readValue = reader.GetArbitraryBitValue(5, false);
                if (readValue != fiveBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fiveBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(fiveBitValue);
                }
                readValue = reader.GetArbitraryBitValue(7, false);
                if (readValue != sevenBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + sevenBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(sevenBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(7, false);
                if (readValue != sevenBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + sevenBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(sevenBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(7, false);
                if (readValue != sevenBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + sevenBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(sevenBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(7, false);
                if (readValue != sevenBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + sevenBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(sevenBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(7, false);
                if (readValue != sevenBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + sevenBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(sevenBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(7, false);
                if (readValue != sevenBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + sevenBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(sevenBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(7, false);
                if (readValue != sevenBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + sevenBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(sevenBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(7, false);
                if (readValue != sevenBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + sevenBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(sevenBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(9, false);
                if (readValue != nineBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + nineBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(nineBitValue);
                }
                readValue = reader.GetArbitraryBitValue(9, false);
                if (readValue != nineBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + nineBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(nineBitValue);
                }
                readValue = reader.GetArbitraryBitValue(9, false);
                if (readValue != nineBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + nineBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(nineBitValue);
                }
                readValue = reader.GetArbitraryBitValue(9, false);
                if (readValue != nineBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + nineBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(nineBitValue);
                }
                readValue = reader.GetArbitraryBitValue(9, false);
                if (readValue != nineBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + nineBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(nineBitValue);
                }
                readValue = reader.GetArbitraryBitValue(9, false);
                if (readValue != nineBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + nineBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(nineBitValue);
                }
                readValue = reader.GetArbitraryBitValue(9, false);
                if (readValue != nineBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + nineBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(nineBitValue);
                }
                readValue = reader.GetArbitraryBitValue(9, false);
                if (readValue != nineBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + nineBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(nineBitValue);
                }
                readValue = reader.GetArbitraryBitValue(15, true);
                if (readValue != fifteenBitValueFour)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fifteenBitValueFour + ". ");
                }
                else
                {
                    Console.WriteLine(fifteenBitValueFour);
                }
                readValue = reader.GetArbitraryBitValue(15, false);
                if (readValue != fifteenBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fifteenBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(fifteenBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(15, false);
                if (readValue != fifteenBitValueTwo)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fifteenBitValueTwo + ". ");
                }
                else
                {
                    Console.WriteLine(fifteenBitValueTwo);
                }
                readValue = reader.GetArbitraryBitValue(15, true);
                if (readValue != fifteenBitValueThree)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fifteenBitValueThree + ". ");
                }
                else
                {
                    Console.WriteLine(fifteenBitValueThree);
                }
                readValue = reader.GetArbitraryBitValue(15, true);
                if (readValue != fifteenBitValueFour)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fifteenBitValueFour + ". ");
                }
                else
                {
                    Console.WriteLine(fifteenBitValueFour);
                }
                readValue = reader.GetArbitraryBitValue(15, false);
                if (readValue != fifteenBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fifteenBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(fifteenBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(15, false);
                if (readValue != fifteenBitValueTwo)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fifteenBitValueTwo + ". ");
                }
                else
                {
                    Console.WriteLine(fifteenBitValueTwo);
                }
                readValue = reader.GetArbitraryBitValue(15, true);
                if (readValue != fifteenBitValueThree)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fifteenBitValueThree + ". ");
                }
                else
                {
                    Console.WriteLine(fifteenBitValueThree);
                }
                readValue = reader.GetArbitraryBitValue(10, false);
                if (readValue != tenBitValue)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + tenBitValue + ". ");
                }
                else
                {
                    Console.WriteLine(tenBitValue);
                }
                readValue = reader.GetArbitraryBitValue(12, false);
                if (readValue != twelveBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twelveBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(twelveBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(2, false);
                if (readValue != twoBitValueOne)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + twoBitValueOne + ". ");
                }
                else
                {
                    Console.WriteLine(twoBitValueOne);
                }
                readValue = reader.GetArbitraryBitValue(5, true);
                if (readValue != fiveBitValueTwo)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + fiveBitValueTwo + ". ");
                }
                else
                {
                    Console.WriteLine(fiveBitValueTwo);
                }
                readValue = reader.GetArbitraryBitValue(16, false);
                if (readValue != uShort2)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + uShort2 + ". ");
                }
                else
                {
                    Console.WriteLine(uShort2);
                }
                readValue = reader.GetArbitraryBitValue(7, true);
                if (readValue != sevenBitValueTwo)
                {
                    Console.WriteLine("Error, " + readValue + " is not equal to :"
                            + sevenBitValueTwo + ". ");
                }
                else
                {
                    Console.WriteLine(sevenBitValueTwo);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                reader.Close();
            }
            Console.ReadKey();
        }
    }
}
