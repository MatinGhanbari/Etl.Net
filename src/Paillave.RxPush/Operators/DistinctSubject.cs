﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paillave.RxPush.Core;

namespace Paillave.RxPush.Operators
{
    public class DistinctSubject<T> : FilterSubjectBase<T>
    {
        private LambdaEqualityComparer<T> _comparer;
        private IList<T> _passedItems = new List<T>();
        private object _syncValue = new object();
        public DistinctSubject(IPushObservable<T> observable, Func<T, T, bool> comparer) : base(observable)
        {
            lock (_syncValue)
            {
                _comparer = new LambdaEqualityComparer<T>(comparer);
            }
        }

        protected override bool AcceptsValue(T value)
        {
            lock (_syncValue)
            {
                if (!_passedItems.Contains(value, _comparer))
                {
                    _passedItems.Add(value);
                    return true;
                }
                return false;
            }
        }
    }
    public static partial class ObservableExtensions
    {
        public static IPushObservable<T> Distinct<T>(this IPushObservable<T> observable, Func<T, T, bool> comparer)
        {
            return new DistinctSubject<T>(observable, comparer);
        }
        public static IPushObservable<T> Distinct<T>(this IPushObservable<T> observable) where T : IEquatable<T>
        {
            return new DistinctSubject<T>(observable, (l, r) => l.Equals(r));
        }
    }
}