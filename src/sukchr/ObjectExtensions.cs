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
		
		/// <summary>
		/// Returns true if the object is null.
		/// </summary>
		public static bool IsNull(this object @object)
		{
			return @object == null;
		}
		
		/// <summary>
		/// Returns true if the object is not null.
		/// </summary>
		public static bool IsNotNull(this object @object)
		{
			return !IsNull(@object);
		}
		
		/// <summary>
		/// Invokes the given action if the object is null.
		/// </summary>
		/// <returns>The object.</returns>
		public static T IsNull<T>(this T @object, Action action) where T : class
		{
			if(action == null) throw new ArgumentNullException("action");
			if(@object.IsNull()) action();
			return @object;
		}
		
		/// <summary>
		/// Invokes the given action if the object is not null.
		/// </summary>
		/// <returns>The object.</returns>
		public static T IsNotNull<T>(this T @object, Action action) where T : class
		{
			if(action == null) throw new ArgumentNullException("action");
			if(@object.IsNotNull()) action();
			return @object;
		}
		
		/// <summary>
		/// Throws an <see cref="ArgumentNullException"/> if the object is null.
		/// </summary>
		public static T Require<T>(this T @object, string parameterName) where T : class
		{
			if(@object.IsNull()) throw new ArgumentNullException(parameterName);
			return @object;
		}
    }
}