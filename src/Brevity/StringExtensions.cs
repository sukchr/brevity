using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Antlr.StringTemplate;
using log4net;
using log4net.Core;
using Newtonsoft.Json;

namespace Brevity
{
    public static class StringExtensions
    {
        private static int _indentLevel = 0;
        private static readonly Indentation DisposableIndent = new Indentation();

        public class Indentation : IDisposable
        {
            internal Indentation() { }
            public void Dispose()
            {
                _indentLevel--;
            }
        }

        public static Indentation Indent()
        {
            _indentLevel++;
            return DisposableIndent;
        }

        /// <summary>
        /// Saves the string to the given path.
        /// </summary>
        /// <param name="text">The text to save.</param>
        /// <param name="path">The path to save the text to.</param>
        /// <param name="args">Optional arguments to the path.</param>
        /// <returns>The saved text.</returns>
        public static string Save(this string text, string path, params string[] args)
        {
            if (path == null) throw new ArgumentNullException("path", "You must specify a path to save to.");
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
            if (string.IsNullOrEmpty(value)) return value;
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
            if (path == null) throw new ArgumentNullException("path", "You must specify a path to open.");
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
            if (string.IsNullOrEmpty(value)) return value;
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
            if (string.IsNullOrEmpty(value)) return value;
            if (value.Length < visible) return value;
            return value.Substring(0, visible) + MaskChar.Repeat(value.Length - visible);
        }

        /// <summary>
        /// Masks half the string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Mask(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Mask(value.Length / 2);
        }

        /// <summary>
        /// Converts the JSON string back into an object of the given type.
        /// </summary>
        /// <typeparam name="TDeserializedType">The type of object to deserialize into.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns></returns>
        public static TDeserializedType FromJson<TDeserializedType>(this string json)
        {
            if (json == null) throw new ArgumentNullException("json", "You must specify a JSON string to deserialize.");
            return JsonConvert.DeserializeObject<TDeserializedType>(json);
        }

        /// <summary>
        /// Does a HTTP GET.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>The response.</returns>
        public static string Get(this string url)
        {
            if (url == null) throw new ArgumentNullException("url", "You must specify a URL to perform HTTP GET on.");
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
            if (url == null) throw new ArgumentNullException("url", "You must specify a URL to perform HTTP POST on.");
            using (var client = new WebClient()) return client.UploadString(url, "POST", upload);
        }

        /// <summary>
        /// Writes the given value to the console (with newline).
        /// </summary>
        /// <returns>The written value.</returns>
        public static string Write(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            var preserved = value;
            if (_indentLevel > 0) value = "\t".Repeat(_indentLevel) + value.Replace("\n", "\n" + "\t".Repeat(_indentLevel));
            Console.WriteLine(value);
            return preserved;
        }

        /// <summary>
        /// Writes the given value to the console (with newline).
        /// </summary>
        /// <returns>The written value.</returns>
        public static string Write(this string value, params object [] args)
        {
            if (string.IsNullOrEmpty(value)) return value;
            var formattedValue = string.Format(value, args);
            formattedValue.Write();
            return formattedValue;
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
        /// <returns>The truncated string.</returns>
        public static string Truncate(this string value, int length)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (length < TruncateIndicator.Length) throw new ArgumentException(string.Format("length must be at least one larger than the length of the truncate indicator which is {0}.", TruncateIndicator.Length), "length");
            if (value.Length <= length) return value;
            return value.Substring(0, length - TruncateIndicator.Length) + TruncateIndicator;
        }

        /// <summary>
        /// Converts a string to a date.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>A DateTime representing the given string. <see cref="DateTime.MinValue"/> is returned if the value is not a valid date.</returns>
        public static DateTime ToDateTime(this string value)
        {
            DateTime result;
            return DateTime.TryParse(value, null, DateTimeStyles.AllowWhiteSpaces, out result) ? result : DateTime.MinValue;
        }

        /// <summary>
        /// Ensures that the string ends with the given text.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ensure">The text to ensure at the end of the string.</param>
        /// <returns></returns>
        public static string EnsureTrailing(this string value, string ensure)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (ensure == null) throw new ArgumentNullException("ensure", "You must specify the trailing text to ensure.");
            if (value.EndsWith(ensure)) return value;
            return value + ensure;
        }

        /// <summary>
        /// Opens an embedded resource with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Stream OpenEmbeddedResource(this string name)
        {
            if (name == null) throw new ArgumentNullException("name", "You must specify a path to open.");
            return Assembly.GetCallingAssembly().GetManifestResourceStream(name);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the string is null or empty.
        /// </summary>
        public static string Require(this string @value, string parameterName)
        {
            if (@value.IsNull()) throw new ArgumentNullException(parameterName);
            if (@value == string.Empty) throw new ArgumentException(parameterName);
            return @value;
        }

        /// <summary>
        /// Formats the string.
        /// </summary>
        public static string FormatWith(this string @value, params object[] args)
        {
            return string.IsNullOrEmpty(@value) ? @value : string.Format(@value, args);
        }

        /// <summary>
        /// Returns true if the string matches any of the given arguments.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool IsEither(this string @value, params string[] args)
        {
            if (args == null || args.Length == 0) throw new ArgumentException("you must specify at least one string to match", "args");
            return args.Any(arg => @value == arg);
        }

        /// <summary>
        /// Returns true if the string matches none of the given arguments.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool IsNeither(this string @value, params string[] args)
        {
            return !@value.IsEither(args);
        }

        /// <summary>
        /// Returns a concatenated string with all the values in the array. 
        /// The values are separated by the given separator.
        /// </summary>
        /// <param name="value">The values to join.</param>
        /// <param name="separator">The string to insert between the joined values.</param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> @value, string separator)
        {
            return value == null ? null : string.Join(separator, @value.Where(s => !string.IsNullOrEmpty(s)).ToArray());
        }

        /// <summary>
        /// Returns a concatenated string with all the values in the array. 
        /// The values are separated by the default separator: ", ".
        /// </summary>
        /// <param name="value">The values to join.</param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> value)
        {
            return value.Join(", ");
        }

        /// <summary>
        /// Returns the given value surrounded by the given argument.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="wrap"></param>
        /// <returns></returns>
        public static string WrapWith(this string @value, string wrap)
        {
            if (value == null) return null;
            if (wrap == null) throw new ArgumentNullException("wrap");
            return string.Format("{0}{1}{0}", wrap, value);
        }

        /// <summary>
        /// Wraps all the values in the given enumerable with the given wrap.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="wrap"></param>
        /// <returns></returns>
        public static IEnumerable<string> WrapWith(this IEnumerable<string> source, string wrap)
        {
            var result = new List<string>();
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext()) result.Add(enumerator.Current.WrapWith(wrap));
            return result;
        }

        /// <summary>
        /// True if the given string matches the regex pattern. 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatch(this string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// Creates a StringTemplate of the string and sets an attribute for the template.
        /// </summary>
        /// <param name="input">The template.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <returns>The template that can have further properties set.</returns>
        public static Template Set(this string input, string name, string value)
        {
            return new Template(new StringTemplate(input)).Set(name, value);
        }

        /// <summary>
        /// Wraps a StringTemplate to provide fluent setting of attributes.
        /// </summary>
        public sealed class Template
        {
            private readonly StringTemplate _template;

            internal Template(StringTemplate template)
            {
                _template = template;
            }

            /// <summary>
            /// Sets an attribute.
            /// </summary>
            /// <param name="name">The name of the attribute.</param>
            /// <param name="value">The value of the attribute.</param>
            /// <returns>The template that can have further properties set.</returns>
            public Template Set(string name, string value)
            {
                _template.SetAttribute(name, value);
                return this;
            }

            /// <summary>
            /// Renders the template. Invoke this after having set all your attributes.
            /// </summary>
            /// <returns>The rendered template.</returns>
            public string Render()
            {
                return _template.ToString();
            }

            /// <summary>
            /// Enables implicit conversion from template to string.
            /// </summary>
            /// <param name="template"></param>
            /// <returns></returns>
            public static implicit operator string(Template template)
            {
                return template._template.ToString();
            }
        }

        /// <summary>
        /// Computes the MD5 hash of the string. 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ComputeHash(this string input)
        {
            return ComputeHash(input, MD5.Create());
        }

        /// <summary>
        /// Uses the given <see cref="HashAlgorithm"/> to compute a hash of the string. 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public static string ComputeHash(this string input, HashAlgorithm algorithm)
        {
            var binary = Encoding.UTF8.GetBytes(input);
            var hash = algorithm.ComputeHash(binary);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
        }

        public static string LogInfo(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), Level.Info);
        }

        public static string LogInfo(this string message)
        {
            return Log(message, Level.Info);
        }

        public static string LogDebug(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), Level.Debug);
        }

        public static string LogDebug(this string message)
        {
            return Log(message, Level.Debug);
        }

        public static string LogWarn(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), Level.Warn);
        }

        public static string LogWarn(this string message)
        {
            return Log(message, Level.Warn);
        }

        public static string LogError(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), Level.Error);
        }

        public static string LogError(this string message)
        {
            return Log(message, Level.Error);
        }

        public static string LogFatal(this string message, params string[] args)
        {
            return Log(message.FormatWith(args), Level.Fatal);
        }

        public static string LogFatal(this string message)
        {
            return Log(message, Level.Fatal);
        }

        private static string Log(this string message, Level level)
        {
            var type = new StackTrace().GetCallingType(typeof(StringExtensions));
            var log = LogManager.GetLogger(type.Name);
            log.Logger.Log(type, level, message, null);
            return message;
        }

        ///<summary>
        /// Uses the default encoding to get the bytes for the given string.
        ///</summary>
        ///<param name="text">The text to get binary from.</param>
        ///<returns></returns>
        public static byte[] ToBinary(this string text)
        {
            return ToBinary(text, Encoding.Default);
        }

        ///<summary>
        /// Uses the given encoding to get the bytes for the given string.
        ///</summary>
        ///<param name="text">The text to get binary from.</param>
        ///<param name="encoding">The encoding to use.</param>
        ///<returns></returns>
        public static byte[] ToBinary(this string text, Encoding encoding)
        {
            return string.IsNullOrEmpty(text) ? null : encoding.GetBytes(text);
        }

        /// <summary>
        /// Loads the string into an XML document.
        /// </summary>
        /// <param name="xmlString">The string that contains the XML.</param>
        /// <returns></returns>
        public static XmlDocument ToXml(this string xmlString)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            return xmlDocument;
        }
    }
}