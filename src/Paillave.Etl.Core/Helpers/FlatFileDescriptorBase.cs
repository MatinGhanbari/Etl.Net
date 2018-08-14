﻿using System.Collections.Generic;
using System;
using Paillave.Etl.Core.MapperFactories;

namespace Paillave.Etl.Core.Helpers
{
    public abstract class FlatFileDescriptorBase<T> where T : new()
    {
        internal int LinesToIgnore { get; private set; } = 0;

        internal Func<string, IList<string>> LineSplitter { get; private set; } = Mappers.CsvLineSplitter();
        internal Func<IList<string>, string> LineJoiner { get; private set; } = Mappers.CsvLineJoiner();

        public void IsFieldDelimited(char fieldDelimiter = ';', char textDelimiter = '"')
        {
            LineSplitter = Mappers.CsvLineSplitter(fieldDelimiter, textDelimiter);
            LineJoiner = Mappers.CsvLineJoiner(fieldDelimiter, textDelimiter);
        }
        public void IgnoreFirstLines(int linesToIgnore)
        {
            LinesToIgnore = linesToIgnore;
        }
    }
}