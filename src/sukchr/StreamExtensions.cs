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
            var reader = new BinaryReader(stream);
            var binary = new byte[stream.Length] ;
            reader.Read(binary, 0, (int)stream.Length); //TODO: will fail if stream is too large for byte array
            stream.Dispose();
            return Convert.ToBase64String(binary);
        }
    }
}
