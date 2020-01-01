using System;

namespace Net.FreeORM.Math.Complexity
{
    public interface IComplex
    {
        Double Real { get; set; }

        Double Imaginer { get; set; }

        Double Angle { get; }

        Double Amplitude { get; }

    }
}