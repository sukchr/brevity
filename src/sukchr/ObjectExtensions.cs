using System;
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
            if (@object == null) throw new ArgumentNullException("object", "You must specify the object to serialize to a JSON string.");
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