using System;
using System.ComponentModel;
using System.Reflection;

namespace JDC.Common.Extensions
{
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
            MemberInfo[] memberInfo = enumerationValue.GetType()
                .GetMember(enumerationValue.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            // If we don't have a description attribute, just return the ToString enumeration
            return enumerationValue.ToString();
        }
    }
}
