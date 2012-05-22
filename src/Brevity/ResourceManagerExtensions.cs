using System.Globalization;
using System.Resources;

namespace Brevity
{
    /// <summary>
    /// Provides extension methods for <see cref="ResourceManager"/>.
    /// </summary>
    public static class ResourceManagerExtensions
    {
        /// <summary>
        /// Returns the value of the specified string resource, formatted with the given arguments, using the current UI culture. 
        /// </summary>
        /// <param name="resourceManager"></param>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <returns></returns>
         public static string GetString(this ResourceManager resourceManager, string name, params object[] args)
        {
            return GetString(resourceManager, name, CultureInfo.CurrentUICulture, args);
        }

        /// <summary>
        /// Returns the value of the specified string resource, formatted with the given arguments, using the specified culture. 
        /// </summary>
        /// <param name="resourceManager"></param>
        /// <param name="name"></param>
        /// <param name="culture"> </param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetString(this ResourceManager resourceManager, string name, CultureInfo culture, params object[] args)
         {
             var text = resourceManager.GetString(name, culture);
             return string.IsNullOrEmpty(text) ? text : text.FormatWith(args);
         }
    }
}