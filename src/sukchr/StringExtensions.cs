using System;
using System.IO;
using System.Linq;

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
    }
}