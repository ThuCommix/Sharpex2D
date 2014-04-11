using System;
using System.Collections.Generic;
using System.Linq;
using Sharpex2D.Framework.Common.TypeParsers.Types;
using Sharpex2D.Framework.Implementation;

namespace Sharpex2D.Framework.Common.TypeParsers
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
