﻿namespace DeltaWare.SDK.Core.Comparison
{
    public class ObjectComparer : IObjectComparer
    {
        public bool Compare(object valueA, object valueB)
        {
            return valueA.Equals(valueB);
        }
    }
}