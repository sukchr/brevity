using System;
using System.IO;

namespace sukchr
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Converts the stream to a Base64 string.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ToBase64(this Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream", "You must specify the stream to convert to base64.");
            var reader = new BinaryReader(stream);
            var binary = new byte[stream.Length] ;
            reader.Read(binary, 0, (int)stream.Length); //TODO: will fail if stream is too large for byte array
            stream.Dispose();
            return Convert.ToBase64String(binary);
        }

        /// <summary>
        /// Returns the string content of the stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ToText(this Stream stream)
        {
            if (stream == null) return null;
            using(var reader = new StreamReader(stream)) return reader.ReadToEnd();
        }
    }
}
