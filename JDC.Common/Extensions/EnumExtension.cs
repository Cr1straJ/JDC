using System;
using System.ComponentModel;

namespace JDC.Common.Extensions
{
    /// <summary>
    /// Contains extension methods for <see cref="Enum"/>.
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// An object extension method that gets description attribute.
        /// </summary>
        /// <param name="enumerationValue">The value to act on.</param>
        /// <returns>The description attribute.</returns>
        public static string GetDescription(this Enum enumerationValue)
        {
            if (enumerationValue is null)
            {
                throw new ArgumentNullException(nameof(enumerationValue));
            }

            // Searching for the DescriptionAttribute attribute for the enumeration
            var memberInfo = enumerationValue.GetType()
                .GetMember(enumerationValue.ToString());

            if (memberInfo is not null && memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes is not null && attributes.Length > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            // If we don't have a description attribute, just return the ToString enumeration
            return enumerationValue.ToString();
        }
    }
}
