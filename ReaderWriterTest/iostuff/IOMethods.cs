using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderWriterTest.iostuff
{
    class IOMethods
    {

        // constants
        public const bool LITTLE_ENDIAN = true;
        public const bool BIG_ENDIAN = false;

        /// <summary>
        /// Set string to a specified length, adds " " if it is smaller than length
        /// </summary>
        /// <param name="oldString">The string to change the lengh of</param>
        /// <param name="length">The length to enlarge or cut down to</param>
        /// <returns>The new string with the specified length</returns>
        public static string SetStringToLength(string oldString, int length)
        {
            string newString;

            if (oldString.Length > length)
            {
                newString = oldString.Substring(0, length);
            }
            else if (oldString.Length < length)
            {
                StringBuilder sb = new StringBuilder(oldString);

                while (sb.Length < length)
                {
                    sb.Append(" ");
                }

                return sb.ToString();

            }
            else
            {
                newString = oldString;
            }

            return newString;
        }

        /// <summary>
        /// Set string to a specified length, adds [null] if it is smaller than length
        /// </summary>
        /// <param name="oldString">The string to change the lengh of</param>
        /// <param name="length">The length to enlarge or cut down to</param>
        /// <returns>The new string with the specified length</returns>
        public static string SetStringToNullLength(string oldString, int length)
        {
            string newString;

            if (oldString.Length > length)
            {
                newString = oldString.Substring(0, length);
            }
            else if (oldString.Length < length)
            {
                StringBuilder sb = new StringBuilder(oldString);

                while (sb.Length < length)
                {
                    sb.Append("\0");
                }

                return sb.ToString();

            }
            else
            {
                newString = oldString;
            }

            return newString;
        }
    }
}
