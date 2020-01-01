using System;

namespace Net.FreeORM.Math.Matrices
{
    public class Matrix
    {
        public static double[,] Transpose(double[,] double_array)
        {
            try
            {
                int row = double_array.GetLength(0);
                int col = double_array.GetLength(1);
                double[,] retArray = new double[col, row];
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        retArray[j, i] = double_array[i, j];
                    }
                }
                return retArray;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static double[,] Multiply(double[,] array1, double[,] array2)
        {
            try
            {
                if (array1.GetLength(1) == array2.GetLength(0))
                {
                    int U1 = array1.GetLength(0);
                    int U2 = array2.GetLength(1);
                    double[,] retArray = new double[U1, U2];

                    for (int i = 0; i < U1; i++)
                    {
                        for (int j = 0; j < U2; j++)
                        {
                            double total = 0.0d;
                            for (int k = 0; k < array1.GetLength(1); k++)
                            {
                                total += array1[i, k] * array2[k, j];
                            }
                            retArray[i, j] = total;
                        }
                    }

                    return retArray;
                }
                else
                    throw new InvalidOperationException(
                        "columnsize of first matrix must be equals to row count of second matrix");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}