using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winforms
{
    public static class Matrices
    {

        static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        [ThreadStatic]
        static Random random;

        public static double[,] InicializarMatriz(int filas, int columnas)
        {
            if (random == null)
            {
                var buffer = new byte[4];
                rng.GetBytes(buffer);
                random = new Random(BitConverter.ToInt32(buffer, 0));
            }

            double[,] matriz = new double[filas, columnas];

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    matriz[i, j] = random.Next(100);
                }
            }

            return matriz;
        }

        public static double[,] SumarMatricesSecuencial(double[,] matA, double[,] matB)
        {
            int matARows = matA.GetLength(0);
            int matACols = matA.GetLength(1);
            int matBRows = matB.GetLength(0);
            int matBCols = matB.GetLength(1);

            if (matARows != matBRows || matACols != matBCols)
            {
                throw new ApplicationException("Las matrices deben tener las mismas dimensiones");
            }

            double[,] result = new double[matARows, matACols];

            for (int i = 0; i < matARows; i++)
            {
                for (int j = 0; j < matBCols; j++)
                {
                    result[i, j] = matA[i, j] + matB[i, j];
                }
            }

            return result;
        }

        public static void MultiplicarMatricesSecuencial(double[,] matA, double[,] matB,
                                                double[,] result)
        {
            int matACols = matA.GetLength(1);
            int matBCols = matB.GetLength(1);
            int matARows = matA.GetLength(0);

            for (int i = 0; i < matARows; i++)
            {
                for (int j = 0; j < matBCols; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < matACols; k++)
                    {
                        temp += matA[i, k] * matB[k, j];
                    }
                    result[i, j] += temp;
                }
            }
        }

        public static void MultiplicarMatricesParalelo(double[,] matA, double[,] matB,
                                               double[,] result,
                                               CancellationToken token = default,
                                               int maximoGradoParalelismo = -1)
        {
            int matACols = matA.GetLength(1);
            int matBCols = matB.GetLength(1);
            int matARows = matA.GetLength(0);

            Parallel.For(0, matARows,
                new ParallelOptions()
                {
                    CancellationToken = token,
                    MaxDegreeOfParallelism = maximoGradoParalelismo
                },
                i =>
            {
                for (int j = 0; j < matBCols; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < matACols; k++)
                    {
                        temp += matA[i, k] * matB[k, j];
                    }
                    result[i, j] += temp;
                }
            });
        }
    }
}
