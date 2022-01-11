using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace JDC.BusinessLogic.Utilities.AutoMapper
{
    /// <summary>
    /// Provides method that mapped one object to another.
    /// </summary>
    /// <typeparam name="TOut">Destination type to create.</typeparam>
    public class CompiledMapper<TOut> : IMapper<TOut>
    {
        private readonly Type outType;
        private readonly ConstructorInfo outConstructor;
        private readonly Dictionary<string, PropertyInfo> outProperties;
        private readonly Dictionary<Type, Func<object, TOut>> converters;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompiledMapper{TOut}"/> class.
        /// </summary>
        public CompiledMapper()
        {
            this.outType = typeof(TOut);
            this.outConstructor = GetEmptyConstructor(this.outType);
            this.outProperties = this.outType.GetProperties().ToDictionary(p => p.Name);
            this.converters = new Dictionary<Type, Func<object, TOut>>();
        }

        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// The source type is inferred from the source object.
        /// </summary>
        /// <param name="source">Source object to map from.</param>
        /// <returns>Mapped destination object.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/> is null.
        /// </exception>
        public TOut Map(object source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var sourceType = source.GetType();
            if (this.converters.TryGetValue(sourceType, out var existsConverter))
            {
                return existsConverter(source);
            }

            var converter = this.BuildConverter(sourceType);
            this.converters.Add(sourceType, converter);

            return converter(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ConstructorInfo GetEmptyConstructor(Type type)
        {
            var available = type.GetTypeInfo().DeclaredConstructors;

            return available.FirstOrDefault(c => !c.IsStatic && c.GetParameters().Length == 0);
        }

        private Func<object, TOut> BuildConverter(Type sourceType)
        {
            var parameter = Expression.Parameter(typeof(object), "source");

            var sourceInstance = Expression.Variable(sourceType, "typedSource");
            var outInstance = Expression.Variable(this.outType, "outInstance");

            var expressions = new List<Expression>
            {
                Expression.Assign(sourceInstance, Expression.Convert(parameter, sourceType)),
                Expression.Assign(outInstance, Expression.New(this.outConstructor)),
            };

            var sourceProperties = sourceType.GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                if (!this.outProperties.TryGetValue(sourceProperty.Name, out var outProperty))
                {
                    continue;
                }

                var sourceValue = Expression.Property(sourceInstance, sourceProperty);
                var outValue = Expression.Property(outInstance, outProperty);

                expressions.Add(Expression.Assign(outValue, sourceValue));
            }

            expressions.Add(outInstance);
            var body = Expression.Block(new[] { sourceInstance, outInstance }, expressions);

            return Expression.Lambda<Func<object, TOut>>(body, parameter).Compile();
        }
    }
}
