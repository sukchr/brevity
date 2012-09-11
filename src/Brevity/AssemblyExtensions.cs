using System;
using System.Linq;
using System.Reflection;

namespace Brevity
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets a custom attribute set on the assembly. Null if the given attribute hasn't been set. If there are multiple attributes
        /// of the given type, only the first is returned.
        /// </summary>
		/// <param name="assembly">The assembly to find attributes in.</param>
		/// <typeparam name="TAttributeType">The type of attribute to find.</typeparam>
        /// <returns></returns>
        public static TAttributeType GetCustomAttribute<TAttributeType>(this Assembly assembly) where TAttributeType : class
        {
            var attribute = assembly.GetCustomAttributes(typeof(TAttributeType), false).FirstOrDefault() as TAttributeType;
            return attribute;
        }

    	/// <summary>
    	/// Gets a custom attribute set on the assembly. Null if the given attribute hasn't been set. If there are multiple attributes
    	/// of the given type, only the first is returned.
    	/// </summary>
    	/// <param name="assembly">The assembly to find attributes in.</param>
    	/// <param name="notFound">If the custom attribute is not found, this function will be called.</param>
    	/// <typeparam name="TAttributeType">The type of attribute to find.</typeparam>
    	/// <returns></returns>
    	public static TAttributeType GetCustomAttribute<TAttributeType>(this Assembly assembly, Func<TAttributeType> notFound) where TAttributeType : class
		{
			return GetCustomAttribute<TAttributeType>(assembly) ?? (notFound != null ? notFound() : null);
		}
    }
}