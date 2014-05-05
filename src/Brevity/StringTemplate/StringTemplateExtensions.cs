using System;
using System.Collections.Generic;
using ST = Antlr4.StringTemplate;
using ST_Template = Antlr4.StringTemplate.Template;

namespace Brevity.StringTemplate
{
	/// <summary>
	/// Extension methods for StringTemplate support.
	/// </summary>
	public static class StringTemplateExtensions
	{
		/// <summary>
		/// Creates a StringTemplate of the string and sets an attribute for the template.
		/// </summary>
		/// <param name="input">The template.</param>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <returns>The template that can have further properties set.</returns>
		public static Template Set(this string input, string name, object value)
		{
			return new Template(input).Set(name, value);
		}

		/// <summary>
		/// Wraps a StringTemplate to provide fluent setting of attributes.
		/// </summary>
		public sealed class Template
		{
			private readonly List<Tuple<string, object>> _nameValues = new List<Tuple<string, object>>();
			/// <summary>
			/// Holds any renderers added via <see cref="RegisterRenderer{T}"/>.
			/// </summary>
			private readonly List<Tuple<Type, ST.IAttributeRenderer>> _renderers = new List<Tuple<Type, ST.IAttributeRenderer>>();
			/// <summary>
			/// Holds the template text. Used to create the StringTemplate inside <see cref="Render()"/>.
			/// </summary>
			private readonly string _template;

			internal Template(string template)
			{
				_template = template;
			}

			/// <summary>
			/// Sets an attribute.
			/// </summary>
			/// <param name="name">The name of the attribute.</param>
			/// <param name="value">The value of the attribute.</param>
			/// <returns>The template that can have further properties set.</returns>
			public Template Set(string name, object value)
			{
				_nameValues.Add(new Tuple<string, object>(name, value));
				return this;
			}

			/// <summary>
			/// Registers a renderer for the given type.
			/// </summary>
			/// <param name="attributeRenderer"></param>
			/// <typeparam name="T"></typeparam>
			/// <returns></returns>
			public Template RegisterRenderer<T>(ST.IAttributeRenderer attributeRenderer)
			{
				_renderers.Add(new Tuple<Type, ST.IAttributeRenderer>(typeof(T), attributeRenderer));
				return this;
			}

			/// <summary>
			/// Renders the template. Invoke this after having set all your attributes. Uses '$' as start and stop delimiters. 
			/// </summary>
			/// <returns>The rendered template.</returns>
			public string Render()
			{
				return Render(StringExtensions.Delimiter.Dollar);
			}

			/// <summary>
			/// Renders the template using the given delimiters. The default delmiter is '$'. 
			/// </summary>
			/// <returns></returns>
			public string Render(StringExtensions.Delimiter delimiter)
			{
				var delimiterChars = StringExtensions.GetDelimiterChars(delimiter);

				var template = new ST_Template(_template, delimiterChars.Item1, delimiterChars.Item2);
				foreach (var nameValue in _nameValues)
				{
					template.Add(nameValue.Item1, nameValue.Item2);
				}

				foreach (var renderer in _renderers)
					template.Group.RegisterRenderer(renderer.Item1, renderer.Item2);

				return template.Render();
			}

			/// <summary>
			/// Enables implicit conversion from template to string.
			/// </summary>
			/// <param name="template"></param>
			/// <returns></returns>
			public static implicit operator string(Template template)
			{
				return template.Render();
			}
		}
	}
}