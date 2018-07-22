﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace LiteDB
{
    internal class DateTimeResolver : ITypeResolver
    {
        public string ResolveMethod(MethodInfo method)
        {
            switch (method.Name)
            {
                // instance methods
                case "AddYears": return "DATEADD('y', @0, #)";
                case "AddMonths": return "DATEADD('M', @0, #)";
                case "AddDays": return "DATEADD('d', @0, #)";
                case "AddHours": return "DATEADD('h', @0, #)";
                case "AddMinutes": return "DATEADD('m', @0, #)";
                case "AddSeconds": return "DATEADD('s', @0, #)";
                case "ToString": return "TO_STRING(#)";

                // static methods
                case "Parse": return "TO_DATETIME(@0)";
            };

            return null;
        }

        public string ResolveMember(MemberInfo member)
        {
            switch (member.Name)
            {
                // static properties
                case "Now": return "NOW()";
                case "UtcNow": return "NOW_UTC()";
                case "Today": return "TODAY()";

                // instance properties
                case "Year": return "YEAR(#)";
                case "Month": return "MONTH(#)";
                case "Day": return "DAY(#)";
                case "Hour": return "HOUR(#)";
                case "Minute": return "MINUTE(#)";
                case "Second": return "SECOND(#)";
            }

            return null;
        }

        public string ResolveCtor(ConstructorInfo ctor)
        {
            var pars = ctor.GetParameters();

            if (pars.Length == 3)
            {
                // int year, int month, int day
                if (pars[0].ParameterType == typeof(int) && pars[1].ParameterType == typeof(int) && pars[2].ParameterType == typeof(int))
                {
                    return "TO_DATETIME(@0, @1, @2)";
                }
            }

            return null;
        }
    }
}