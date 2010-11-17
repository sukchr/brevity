using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace sukchr
{
    public static class StringExtensions
    {
        /// <summary>
        /// Saves the string to the given path.
        /// </summary>
        /// <param name="text">The text to save.</param>
        /// <param name="path">The path to save the text to.</param>
        /// <param name="args">Optional arguments to the path.</param>
        /// <returns>The saved text.</returns>
        public static string Save(this string text, string path, params string[] args)
        {
            if (path == null) throw new ArgumentNullException("path");
            using (var writer = new StreamWriter(string.Format(path, args)))
            {
                writer.Write(text);
            }
            return text;
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
        /// Returns the text in the file at the given path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string OpenText(this string path)
        {
            using(var stream = File.OpenText(path)) return stream.ReadToEnd();
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

        /// <summary>
        /// Converts the JSON string back into an object of the given type.
        /// </summary>
        /// <typeparam name="TDeserializedType">The type of object to deserialize into.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns></returns>
        public static TDeserializedType FromJson<TDeserializedType>(this string json)
        {
            return JsonConvert.DeserializeObject<TDeserializedType>(json);
        }

        /// <summary>
        /// Does a HTTP GET.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>The response.</returns>
        public static string Get(this string url)
        {
            using (var client = new WebClient()) return client.DownloadString(url);
        }


        /// <summary>
        /// Does a HTTP POST.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="upload"></param>
        /// <returns>The response</returns>
        public static string Post(this string url, string upload)
        {
            using (var client = new WebClient()) return client.UploadString(url, "POST", upload);
        }

        /// <summary>
        /// Writes the given value to the console (with newline).
        /// </summary>
        /// <returns>The written value.</returns>
        public static string Write(this string value)
        {
            Console.WriteLine(value);
            return value;
        }

        /// <summary>
        /// Writes the given value to the console (with newline).
        /// </summary>
        /// <returns>The written value.</returns>
        public static string Write(this string value, params string[] args)
        {
            var write = string.Format(value, args);
            Console.WriteLine(write);
            return write;
        }

        /// <summary>
        /// Tries to convert the string  to an int. If the value is not a valid int, then 0 is returned.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this string value)
        {
            return ToInt(value, 0);
        }

        /// <summary>
        /// Tries to convert the string  to an int. If the value is not a valid int, then the default value is returned.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public static int ToInt(this string value, int @default)
        {
            int parsedValue;
            return int.TryParse(value, out parsedValue) ? parsedValue : @default;
        }

        /// <summary>
        /// The value used to indicate that the string was truncated.
        /// </summary>
        private const string TruncateIndicator = "...";

        /// <summary>
        /// Truncates the string. 
        /// </summary>
        /// <param name="value">The length of the truncated string.</param>
        /// <param name="length"></param>
        /// <returns>The trucated string.</returns>
        public static string Truncate(this string value, int length)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (length < TruncateIndicator.Length) throw new ArgumentException(string.Format("length must be at least one larger than the length of the truncate indicator which is {0}.", TruncateIndicator.Length), "length");
            if (value.Length <= length) return value;
            return value.Substring(0, length - TruncateIndicator.Length) + TruncateIndicator;
        }

        /// <summary>
        /// Converts a string to a date.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? ToDate(this string value)
        {
            DateTime result;
            return DateTime.TryParse(value, null, DateTimeStyles.AllowWhiteSpaces, out result) ? new DateTime?(result) : null;
        }
    }
}