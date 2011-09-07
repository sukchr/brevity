using System;
using System.IO;
using System.Xml;

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

        /// <summary>
        /// Returns the XML content of the stream.
        /// </summary>
        /// <param name="xmlStream">A stream that contains XML data.</param>
        /// <returns></returns>
        public static XmlDocument ToXml(this Stream xmlStream)
        {
            if (xmlStream == null) return null;
            var xml = new XmlDocument();
            xml.Load(xmlStream);
            return xml;
        }

        /// <summary>
        /// Write the stream to the given path. If the stream is seekable, the stream is repositioned at the beginning. 
        /// The stream is not disposed. 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="path">The path to save the stream to.</param>
        /// <returns>The stream.</returns>
        public static Stream Save(this Stream stream, string path)
        {
            if (path == null) throw new ArgumentNullException("path");
            var buffer = new byte[4 * 1024];
            using (var output = File.Create(path))
            {
                int len;
                while ((len = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    output.Write(buffer, 0, len);
                }
            }

            if(stream.CanSeek) stream.Position = 0;
            return stream;
        }
    }
}
