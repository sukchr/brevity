using System;
using System.IO;
using System.Linq;
using System.Text;

namespace sukchr
{
    public static class StringExtensions
    {
        /// <summary>
        /// Saves the string to the given path.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        public static void Save(this string text, string path)
        {
            if (path == null) throw new ArgumentNullException("path");
            var writer = new StreamWriter(path);
            writer.Write(text);
        }

        /// <summary>
        /// Removes occurences of the given string.
        /// </summary>
        /// <param name="value">The string to remove from.</param>
        /// <param name="remove">The value to remove.</param>
        /// <returns></returns>
        public static string Remove(this string value, params string[] remove)
        {
            if (remove == null) throw new ArgumentNullException("remove");
            if (remove.Length == 0) throw new ArgumentException("remove cannot be empty array");
            return remove.Aggregate(value, (current, t) => current.Replace(t, string.Empty));
        }

        /// <summary>
        /// Opens a stream from the given path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Stream Open(this string path)
        {
            return File.OpenRead(path); //need to dispose?
        }

        /// <summary>
        /// Repeats the string the given number of times.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public static string Repeat(this string value, int times)
        {
            if (times < 1) return string.Empty;
            if (times == 1) return value;

            var stringBuilder = new StringBuilder(value.Length);
            for (var i = 0; i < times; i++) stringBuilder.Append(value);
            return stringBuilder.ToString();
        }

        private const string MaskChar = "*";

        /// <summary>
        /// Masks everything but the first <see cref="visible"/> chars.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        public static string Mask(this string value, int visible)
        {
            if (value.Length < visible) return value;
            return value.Substring(0, visible) + MaskChar.Repeat(value.Length - visible);
        }
    }
}