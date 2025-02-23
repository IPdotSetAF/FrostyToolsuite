﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace BulkExporterPlugin.Enums
{
    public static class EnumHelper
    {
        public static string Description(this Enum value)
        {
            var attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Any())
                return (attributes.First() as DescriptionAttribute).Description;

            // If no description is found, the least we can do is replace underscores with spaces
            return value.ToString().Replace("_", " ");
        }

        public static IEnumerable<ValueDescription> GetAllValuesAndDescriptions(Type t)
        {
            if (!t.IsEnum)
                throw new ArgumentException($"{nameof(t)} must be an enum type");

            return Enum.GetValues(t).Cast<Enum>().Select((e) => new ValueDescription() { Value = e, Description = e.Description() }).ToList();;
        }

        public class ValueDescription
        {
            public object Value { get; set; }
            public object Description { get; set; }
        }
    }
}
