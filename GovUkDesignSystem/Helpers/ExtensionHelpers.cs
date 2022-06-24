﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GovUkDesignSystem.Helpers
{
    public static class ExtensionHelpers
    {
        public static TAttributeType GetSingleCustomAttribute<TAttributeType>(this MemberInfo property)
            where TAttributeType : Attribute
        {
            return property.GetCustomAttributes(typeof(TAttributeType)).SingleOrDefault() as TAttributeType;
        }

        public static string ToTagAttributes(this IDictionary<string, string> attributesDictionary)
        {
            if (attributesDictionary == null)
            {
                return "";
            }

            var attributeStrings = attributesDictionary.Select(kv => kv.Value == null ? kv.Key : $"{kv.Key}=\"{kv.Value}\"");
            return string.Join(" ", attributeStrings);
        }
    }
}
