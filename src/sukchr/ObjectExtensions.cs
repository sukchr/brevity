using Newtonsoft.Json;

namespace sukchr
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns the object as a JSON string.
        /// </summary>
        public static string ToJson(this object @object, bool indent)
        {
            return JsonConvert.SerializeObject(@object, indent ? Formatting.Indented : Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        /// <summary>
        /// Returns the object as a JSON string with indentation.
        /// </summary>
        public static string ToJson(this object @object)
        {
            return ToJson(@object, true);
        }
    }
}