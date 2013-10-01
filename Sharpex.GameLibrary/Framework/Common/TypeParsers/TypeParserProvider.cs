using System;
using System.Collections.Generic;
using System.Linq;
using SharpexGL.Framework.Common.TypeParsers.Types;
using SharpexGL.Framework.Implementation;

namespace SharpexGL.Framework.Common.TypeParsers
{
    public class TypeParserProvider : IImplementation
    {
        /// <summary>
        /// Initializes a new TypeParserProvider class.
        /// </summary>
        public TypeParserProvider()
        {
            _parsers = new List<ITypeParser>
            {
                new BooleanParser(),
                new CharParser(),
                new CircleParser(),
                new NumericParser(),
                new RectangleParser(),
                new Vector2Parser()
            };
        }

        private readonly List<ITypeParser> _parsers;

        /// <summary>
        /// Gets a TypeParser for the given Type.
        /// </summary>
        /// <param name="type">The Type.</param>
        /// <returns>TypeParser</returns>
        public ITypeParser GetParser(Type type)
        {
            foreach (var parser in _parsers.Where(parser => parser.Type == type))
            {
                return parser;
            }

            throw new InvalidOperationException("The parser of type " + type.Name + " was not found.");
        }
    }
}
