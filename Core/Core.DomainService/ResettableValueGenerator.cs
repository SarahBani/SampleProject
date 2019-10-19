using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Core.DomainService
{
    public class ResettableValueGenerator : ValueGenerator<int>
    {

        private int _current;

        public override bool GeneratesTemporaryValues => false;

        public override int Next(EntityEntry entry) => Interlocked.Increment(ref _current);

        public void Reset() => _current = 0;

    }
}
