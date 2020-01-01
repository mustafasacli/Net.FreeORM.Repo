using System;
using System.Collections.Generic;
using System.Text;

namespace Net.FreeORM.Math.Complexity
{
    public struct complex : IComplex
    {
        private double _real;
        private double _imaginer;
        private double _amplitude;
        private double _angle;

        public complex(double real, double imaginer)
        {
            _amplitude = 0.0d;
            _angle = 0.0d;
            _real = real;
            _imaginer = imaginer;
            _amplitude = computeAmplitude();
            _angle = computeAngle();
        }

        public complex(complex cmplx)
            : this(cmplx.Real, cmplx.Imaginer)
        { }

        public double Real
        {
            get { return _real; }
            set
            {
                _real = value;
                _amplitude = computeAmplitude();
                _angle = computeAngle();
            }
        }

        public double Imaginer
        {
            get { return _imaginer; }
            set
            {
                _imaginer = value;
                _amplitude = computeAmplitude();
                _angle = computeAngle();
            }
        }

        private double computeAmplitude()
        {
            return System.Math.Sqrt(_real * _real + _imaginer * _imaginer);
        }

        public double Angle
        {
            get { return _angle; }
        }

        private double computeAngle()
        {
            return System.Math.Atan2(_imaginer, _real);
        }


        public double Amplitude
        {
            get { return _amplitude; }
        }

        public static complex AmplitudeAndAngle(double amp, double ang)
        {
            return new complex(amp * System.Math.Cos(ang), amp * System.Math.Sin(ang));
        }

        public static complex operator *(complex c1, complex c2)
        {
            double re = c1.Real * c2.Real - c1.Imaginer * c2.Imaginer;
            double im = c1.Real * c2.Imaginer + c1.Imaginer * c2.Real;

            return new complex(re, im);
        }

        public static complex operator !(complex c1)
        {
            return new complex(c1.Real, -1.0d * c1.Imaginer);
        }

        public static complex operator /(complex c1, complex c2)
        {
            complex c3 = !c2;
            complex c4 = c1 * c3;
            c3 = c3 * c2;
            return new complex(c4.Real / c3.Real, c4.Imaginer / c3.Real);
        }

        public static complex operator ^(complex c1, complex c2)
        {
            c2.Real *= c1.Angle;
            c2.Imaginer *= c1.Angle;
            double eUs = System.Math.Log(c1.Amplitude);
            c2.Real *= eUs;
            c2.Imaginer *= eUs;
            double eAmp = System.Math.Pow(System.Math.E, c2.Real);
            double eAng = c2.Imaginer;

            return AmplitudeAndAngle(eAmp, eAng);
        }

        public static double operator ~(complex c1)
        {
            return c1.Amplitude;
        }

        public static bool operator ==(complex c1, complex c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(complex c1, complex c2)
        {
            return c1.Equals(c2) == false;
        }

        public override string ToString()
        {
            return _imaginer < 0 ?
                string.Format("{0:#.###} - j{1:#.###}", _real, -1 * _imaginer) :
                string.Format("{0:#.###} + j{1:#.###}", _real, _imaginer);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(typeof(complex), obj))
            {
                complex c = (complex)obj;
                return ((c.Real == _real) && (c.Imaginer == _imaginer));
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            if (_real < 0)
            {
                if (_imaginer < 0)
                    return 3;
                else
                    return 2;
            }
            else
            {
                if (_imaginer < 0)
                    return 4;
                else
                    return 1;
            }
        }

    }
}
