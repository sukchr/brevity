using System.Linq;
using System.Reflection;

namespace Brevity
{
    public static class AssemblyExtension
    {
        /// <summary>
        /// Gets a custom attribute set on the assembly. Null if the given attribute hasn't been set. If there are multiple attributes
        /// of the given type, only the first is returned.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static TAttributeType GetCustomAttribute<TAttributeType>(this Assembly assembly) where TAttributeType : class
        {
            var attribute = assembly.GetCustomAttributes(typeof(TAttributeType), false).FirstOrDefault() as TAttributeType;
            return attribute;
        }
    }
}