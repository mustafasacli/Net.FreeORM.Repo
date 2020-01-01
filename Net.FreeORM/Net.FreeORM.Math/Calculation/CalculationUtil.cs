using System;

namespace Net.FreeORM.Math.Calculation
{
    public class CalculationUtil
    {
        public static ulong Factorial(ulong x)
        {
            try
            {
                if (x < 0)
                    throw new InvalidOperationException("number can not less than zero.");

                switch (x)
                {
                    case 0:
                    case 1:
                        return 1L;

                    default:
                        return x * Factorial(x - 1);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static ulong GetNthFibonacci(ulong n)
        {
            if (n < 0)
                throw new InvalidOperationException("number can not be less than zero.");

            switch (n)
            {
                case 0:
                    return 0L;

                case 1:
                    return 1L;

                default:
                    return GetNthFibonacci(n - 1) + GetNthFibonacci(n - 2);
            }
        }

        public static bool isPrime(long willBeExamined)
        {
            if (willBeExamined > 1L)
            {
                unsafe
                {
                    long* firstAdress = &willBeExamined;
                    bool willBeReturned = true;
                    bool* boolAdress = &willBeReturned;
                    long* adress;
                    for (long i = 2; i < (willBeExamined / 2 + 1); i++)
                    {
                        adress = &i;
                        *boolAdress &= *firstAdress % *adress != 0;
                        if ((*boolAdress) == false)
                            break;
                    }
                    return *boolAdress;
                }
            }
            else
                return false;//throw new InvalidOperationException("Number cannot be less than two.");
        }

        public static long GetHighestPrimeDivider(long willBeFound)
        {
            if (!isPrime(willBeFound))
            {
                unsafe
                {
                    long x = 1L;
                    long* intResult = &x;
                    long* firstAdress = &willBeFound;
                    for (long i = (*firstAdress / 2); i > 1; i--)
                    {
                        long* adresI = &i;

                        if (*firstAdress % *adresI == 0)
                        {
                            if (isPrime(*adresI))
                            {
                                *intResult = *adresI;
                                break;
                            }
                        }
                    }
                    return *intResult;
                }
            }
            else
                return willBeFound;
        }

        #region [ Convert the Numbers To Digits ]

        public static string GetIntegerBits(ulong sayi)
        {
            try
            {
                switch (sayi)
                {
                    case 0:
                        return "0";

                    case 1:
                        return "1";

                    default:
                        return String.Concat(GetIntegerBits(sayi / 2),
                        (sayi - (sayi / 2) * 2).ToString());
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #endregion [ Convert the Numbers To Digits ]

        public static int[] GetPascalTriangle(int pow)
        {
            if (pow < 0)
                throw new InvalidOperationException("Üs sıfırdan küçük olamaz...");
            else
            {
                switch (pow)
                {
                    case 0:
                        return new int[] { 1 };

                    case 1:
                        return new int[] { 1, 1 };

                    default:
                        int[] retArray = new int[pow + 1];
                        retArray[0] = 1; retArray[pow] = 1;
                        int[] tmpArray = GetPascalTriangle(pow - 1);
                        for (int i = 1; i < pow; i++)
                        {
                            retArray[i] = tmpArray[i] + tmpArray[i - 1];
                        }//end for
                        return retArray;
                }//end switch
            }//end else
        } //end getPascalTriangle

        public static ulong GetHighestCommonDivisor(ulong number1, ulong number2)
        {
            try
            {
                if (number1 < 1L || number2 < 1L)
                    throw new InvalidOperationException("one of numbers can not be less than one");

                if (number2 > number1)
                    return GetHighestCommonDivisor(number2, number1);
                else if (number2 == 1)
                    return 1;
                else if ((number1 % number2) == 0)
                    return number2;
                else
                    return GetHighestCommonDivisor(number2, (number1 % number2));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static ulong GetLowestMuliply(ulong number1, ulong number2)
        {
            return number1 * number2 / GetHighestCommonDivisor(number1, number2);
        }

        public static double SumOfGeometricArray(double coefficient, double baseNumber, long lastExponential)
        {
            if (baseNumber == 1.0)
            {
                return ((double)coefficient * (1 + lastExponential));
            }
            else
            {
                return ((double)coefficient * (1 - System.Math.Pow(baseNumber, (double)lastExponential)) / (1.0d - baseNumber));
            }
        }

        public static double[] GeometricArray(double coefficient, double baseNumber, long firstExponential, long lastExponential)
        {
            if (firstExponential > lastExponential)
            {
                return GeometricArray(coefficient, baseNumber, lastExponential, firstExponential);
            }
            else
            {
                long dim = lastExponential - firstExponential + 1;
                double[] dArray = new double[dim];

                for (long i = 0; i < dim; i++)
                {
                    dArray[i] = coefficient * ((double)System.Math.Pow(baseNumber, (double)i + firstExponential));
                }

                return dArray;
            }
        }

        public static double[] GeometricArray(double coefficient, double baseNumber, long exponential)
        {
            return GeometricArray(coefficient, baseNumber, 0L, exponential);
        }
    }
}