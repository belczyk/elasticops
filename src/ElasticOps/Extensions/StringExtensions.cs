﻿using System.Linq;

namespace ElasticOps.Extensions
{
    public static class StringExtensions
    {
        public static string GetTextBeforePosition(this string text, int line, int column)
        {
            var lines = text.Split('\n');
            var caretLine = lines[line - 1];
            var leadingLines = line == 1 ? string.Empty : lines.Take(line - 1).Aggregate((c, n) => c + "\n" + n);
            var caretLinePrefix = caretLine.Substring(0, column - 1);

            if (string.IsNullOrEmpty(leadingLines))
                return caretLinePrefix;

            return leadingLines + "\n" + caretLinePrefix;
        }
    }
}