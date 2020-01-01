using System;

namespace Net.FreeORM.Math.Geometry
{
    public class GeometryUtil
    {

        public static double GetAreaOfSquare(double side)
        {
            if ((side <= 0.0d) == true)
                throw new InvalidOperationException("side length can not less than or equals to zero. ");
            else
                return (side * side);
        }


        public static bool isTriangle(double side1, double side2, double side3)
        {
            return ((side1 + side2 - side3) * (side1 + side3 - side2) * (side2 + side3 - side1)) > 0;
        }

        public static double AreaOfATriangle(double side1, double side2, double side3)
        {
            if (isTriangle(side1, side2, side3) == true)
            {
                double s = (side1 + side1 + side3) * 0.5d;

                double qS = (s - side1) * (s - side2) * (s - side3) * s;

                return System.Math.Sqrt(qS);
            }
            else
                throw new InvalidOperationException("this is not a triangle.");
        }


        public static double AreaOfPentagonal(double side)
        {
            if ((side <= 0.0d) == true)
                throw new InvalidOperationException("side can not be less than or equals to zero.");
            else
            {
                double area = 1.25 * side * side;
                area *= System.Math.Sin(0.4 * System.Math.PI);
                area /= (1 - System.Math.Cos(0.4 * System.Math.PI));
                return area;
            }
        }

        public static double AreaOfHegzagonal(double side)
        {
            if ((side <= 0.0d) == true)
                throw new InvalidOperationException("side can not be less than or equals to zero.");
            else
            {
                double d = 1.5d * side * side;
                d *= System.Math.Sqrt(3);
                return d;
            }
        }
    }
}