using System.IO;
using VTL = NVelocity.App.Velocity;

namespace Brevity.Velocity
{
	/// <summary>
	/// Extension methods for Velocity support.
	/// </summary>
	public static class VelocityExtensions
	{
		/// <summary>
		/// Creates a StringTemplate of the string and sets an attribute for the template.
		/// </summary>
		/// <param name="templateText">The template.</param>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <returns>The template that can have further properties set.</returns>
		public static VelocityContext Set(this string templateText, string name, object value)
		{
			return new VelocityContext(templateText).Set(name, value);
		}

		public sealed class VelocityContext
		{
			private readonly string _templateText;
			private readonly NVelocity.VelocityContext _context;

			private static bool _isInitialized = false;
			private static readonly object SpinLock = new object();

			internal VelocityContext(string templateText)
			{
				_templateText = templateText;
				_context = new NVelocity.VelocityContext();
			}

			/// <summary>
			/// Add a value to the context.
			/// </summary>
			public VelocityContext Set(string name, object value)
			{
				_context.Put(name, value);
				return this;
			}

			/// <summary>
			/// Renders the template and returns it as a string. 
			/// </summary>
			/// <returns>The rendered template.</returns>
			public string Render()
			{
				var output = new StringWriter();

				EnsureVelocityInit();

				VTL.Evaluate(_context, output, string.Empty, _templateText);

				return output.GetStringBuilder().ToString();
			}

			/// <summary>
			/// Renders the template to the given textwriter.
			/// </summary>
			public void Render(TextWriter textWriter)
			{
				EnsureVelocityInit();

				VTL.Evaluate(_context, textWriter, string.Empty, _templateText);
			}

			private static void EnsureVelocityInit()
			{
				lock (SpinLock)
				{
					if(!_isInitialized)
					{
						VTL.Init();
						_isInitialized = true;
					}
				}
			}

			/// <summary>
			/// Enables implicit conversion to string.
			/// </summary>
			public static implicit operator string(VelocityContext context)
			{
				return context.Render();
			}
		}
	}
}