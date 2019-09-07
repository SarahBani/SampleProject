using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DomainModel.Entities
{
    public abstract class ValueObject
    {
        //protected abstract IEnumerable<object> GetEqualityComponents();
                
        //public override int GetHashCode()
        //{
        //    return GetEqualityComponents()
        //        .Aggregate(1, (current, obj) =>
        //        {
        //            return HashCode.Combine(current, obj);
        //        });
        //}

        //public static bool operator ==(ValueObject a, ValueObject b)
        //{
        //    if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
        //        return true;

        //    if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
        //        return false;

        //    return a.Equals(b);
        //}

        //public static bool operator !=(ValueObject a, ValueObject b)
        //{
        //    return !(a == b);
        //}

        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetAtomicValues();
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject other = (ValueObject)obj;

            //return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());

            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^
                    ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }

                if (thisValues.Current != null &&
                    !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }
        // Other utility methods
    }
}
