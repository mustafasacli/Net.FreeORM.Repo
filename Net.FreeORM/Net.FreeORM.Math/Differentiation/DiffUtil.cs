using System;

namespace Net.FreeORM.Math.Differentiation
{
    public class DiffUtil
    {

        public static double[] DifferentiationArray(double[] diffArray)
        {
            try
            {
                switch (diffArray.Length)
                {
                    case 1:
                        return new double[] { 0.0d };
                    default:
                        double[] arr = new double[diffArray.Length - 1];
                        for (int i = 0; i < arr.Length; i++)
                        {
                            arr[i] = (i + 1) * diffArray[i + 1];
                        }

                        return arr;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static double[] IntegralArray(double[] integArray)
        {
            try
            {
                switch (integArray.Length)
                {
                    case 0:
                        return new double[] { 0.0 };
                    default:
                        double[] Integrali = new double[integArray.Length + 1];
                        Integrali[0] = 0.0;
                        if (integArray.Length > 0)
                        {
                            for (int i = 0; i < integArray.Length; i++)
                                Integrali[i + 1] = integArray[i] / (i + 1);
                        }

                        return Integrali;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static double[] MultiplyTwoOneDimensionArray(double[] d1, double[] d2)
        {
            try
            {
                int dim = d1.Length + d2.Length - 1;
                double[] retArray = new double[dim];

                for (int i = 0; i < d1.Length; i++)
                {
                    for (int j = 0; j < d2.Length; j++)
                    {
                        retArray[i + j] += d1[i] * d2[j];
                    }
                }

                return retArray;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static double[] SumOfTwoOneDimensionArray(double[] d1, double[] d2)
        {
            try
            {
                int max, min;
                bool state = false;
                if (d1.Length > d2.Length)
                {
                    max = d1.Length;
                    min = d2.Length;
                    state = true;
                }
                else
                {
                    max = d2.Length;
                    min = d1.Length;
                }

                double[] retArray = new double[max];
                for (int i = 0; i < min; i++)
                {
                    retArray[i] = d1[i] + d2[i];
                }

                if (state)
                {
                    for (int i = min; i < max; i++)
                    {
                        retArray[i] = d1[i];
                    }
                }
                else
                {
                    for (int i = min; i < max; i++)
                    {
                        retArray[i] = d2[i];
                    }
                }

                return retArray;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public double[] DivideArrayWithAnDouble(double[] d, double divider)
        {
            try
            {
                double[] d2 = d;
                for (int i = 0; i < d2.Length; i++)
                {
                    d2[i] /= divider;
                }
                return d2;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}