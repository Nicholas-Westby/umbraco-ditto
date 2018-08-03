﻿using System;
using System.Collections;
using System.Linq;

namespace Our.Umbraco.Ditto
{
    /// <summary>
    /// An enumerable Ditto processor that converts values to/from an enumerable based
    /// upon the properties target type
    /// NB: It won't try to cast the inner values, just convert an enumerable so this
    /// should ideally already have occurred.
    /// </summary>
    public class EnumerableConverterAttribute : DittoProcessorAttribute
    {
        /// <summary>
        /// Direction of the enumerable conversion
        /// </summary>
        public EnumerableConvertionDirection Direction { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerableConverterAttribute" /> class.
        /// </summary>
        public EnumerableConverterAttribute()
        {
            // Default to automatic
            Direction = EnumerableConvertionDirection.Automatic;
        }

        /// <summary>
        /// Processes the value.
        /// </summary>
        /// <returns>
        /// The <see cref="object" /> representing the processed value.
        /// </returns>
        public override object ProcessValue()
        {
            object result = this.Value;
            var propertyType = this.Context.PropertyInfo.PropertyType;
            var propertyIsEnumerableType = Direction == EnumerableConvertionDirection.Automatic
                ? propertyType.IsEnumerableType()
                    && (propertyType == typeof(string)) == false
                : Direction == EnumerableConvertionDirection.ToEnumerable;

            if (this.Value != null)
            {
                var valueType = this.Value.GetType();
                var valueIsEnumerableType = valueType.IsEnumerableType()
                    && (this.Value is string) == false;

                if (propertyIsEnumerableType)
                {
                    if (valueIsEnumerableType == false)
                    {
                        // Property is enumerable, but value isn't, so make enumerable
                        var arr = Array.CreateInstance(valueType, 1);
                        arr.SetValue(this.Value, 0);
                        result = arr;
                    }
                }
                else
                {
                    if (valueIsEnumerableType)
                    {
                        // Property is not enumerable, but value is, so grab first item
                        var enumerator = ((IEnumerable)this.Value).GetEnumerator();
                        result = enumerator.MoveNext() ? enumerator.Current : null;
                    }
                }
            }
            else
            {
                if (propertyIsEnumerableType)
                {
                    if (propertyType.IsInterface && propertyType.IsEnumerableOfKeyValueType() == false)
                    {
                        // Value is null, but property is enumerable interface, so return empty enumerable
                        result = EnumerableInvocations.Empty(propertyType.GenericTypeArguments.First());
                    }
                    else
                    {
                        // Concrete enumerables cannot be cast from Array so we create an instance and return it
                        // if we know it has an empty constructor.
                        var constructorParams = propertyType.GetConstructorParameters();
                        if (constructorParams != null && constructorParams.Length == 0)
                        {
                            // Internally this uses Activator.CreateInstance which is heavily optimized.
                            result = propertyType.GetInstance();
                        }
                    }
                }
            }

            return result;
        }
    }

    /// <summary>
    /// Determines the direction of the numerable conversion
    /// </summary>
    public enum EnumerableConvertionDirection
    {
        /// <summary>
        /// Automatically convert the value based on the target property type
        /// </summary>
        Automatic,

        /// <summary>
        /// Convert to enumerable
        /// </summary>
        ToEnumerable,

        /// <summary>
        /// Convert from enumerable
        /// </summary>
        FromEnumerable
    }
}