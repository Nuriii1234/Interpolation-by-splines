using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpolation_by_splines
{
    internal class Interpolation_by_splines
    {
        void PrintVector(double[] Vector)
        {
            for (int i = 0; i < Vector.Length; i++)
            {
                Console.Write(Vector[i] + "\t");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        //--------------------------------------------------------------------------------------------------------
        void PrintArray(double[,] Array)
        {
            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    Console.Write(Array[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        //--------------------------------------------------------------------------------------------------------
        void PrintVector_F(double[] vector_X, double[] vector_A, double[] vector_B, double[] vector_C, double[] vector_D)
        {
            for(int i = 0;i < vector_A.Length;i++)
            {
                Console.WriteLine("f(x) = " + vector_A[i] + " + " + vector_B[i] + "(x - " + vector_X[i] + ") + " + vector_C[i] + "(x - " + vector_X[i] + ")^2 + " + vector_D[i] + "(x - " + vector_X[i] + ")^3");
            }
        }
        double[] Original_vector_X()
        {
            double[] vector_X = { 0.1, 0.5, 0.9, 1.3, 1.7 };
            return vector_X;
        }
        //--------------------------------------------------------------------------------------------------------
        double[] Original_vector_F()
        {
            double[] vector_F = { -2.2026, -0.19315, 0.79464, 1.5624, 2.2306 };
            return vector_F;
        }
        //--------------------------------------------------------------------------------------------------------
        double[] Search_H (double[] vector_X) 
        {
            double[] vector_H = new double[vector_X.Length - 1];
            for (int i = 0; i < vector_X.Length - 1; i++)
            {
                vector_H[i] = vector_X[i + 1] - vector_X[i];
            }
            return vector_H;
        }
        //--------------------------------------------------------------------------------------------------------
        double[] Search_A (double[] vector_F) 
        {
            double[] vecrtor_A = new double[vector_F.Length - 1];
            for(int i = 0;  i < vecrtor_A.Length; i++)
            {
                vecrtor_A[i] = vector_F[i];
            }
            return vecrtor_A;
        }
        //--------------------------------------------------------------------------------------------------------
        double[] Search_B(double[] vector_F, double[] vector_H, double[] vector_C)
        {
            double[] vector_B = new double[vector_F.Length - 1];
            for (int i = 0; i < vector_B.Length; i++)
            {
                vector_B[i] = (vector_F[i + 1] - vector_F[i]) / vector_H[i] - (vector_H[i] * (vector_C[i + 1] + 2 * vector_C[i]) / 3);
            }
            return vector_B;
        }
        //--------------------------------------------------------------------------------------------------------
        double[,] Search_C(double [] vector_X, double [] vector_F, double[] vector_H) 
        {
            double[,] Matrix_C = new double[3,4];
            Matrix_C[0, 0] = 2 * (vector_H[0] + vector_H[1]);
            Matrix_C[0, 1] = vector_H[1];
            Matrix_C[0, 2] = 0;
            Matrix_C[0, 3] = 3 * ((vector_F[2] - vector_F[1]) / vector_H[1] - (vector_F[1] - vector_F[0]) / vector_H[0]);
            Matrix_C[1, 0] = vector_H[1];
            Matrix_C[1, 1] = 2 * (vector_H[1] + vector_H[2]);
            Matrix_C[1, 2] = vector_H[2];
            Matrix_C[1, 3] = 3 * ((vector_F[3] - vector_F[2]) / vector_H[2] - (vector_F[2] - vector_F[1]) / vector_H[1]);
            Matrix_C[2, 0] = 0;
            Matrix_C[2, 1] = vector_H[2];
            Matrix_C[2, 2] = 2 * (vector_H[2] + vector_H[3]);
            Matrix_C[2, 3] = 3 * ((vector_F[4] - vector_F[3]) / vector_H[3] - (vector_F[3] - vector_F[2]) / vector_H[2]);
            return Matrix_C;
        }
        //--------------------------------------------------------------------------------------------------------
        double[] Search_D(double[] vector_H, double[] vector_C)
        {
            double[] vecrtor_D = new double[vector_C.Length - 1];
            for(int i = 0; i < vecrtor_D.Length; i++)
            {
                vecrtor_D[i] = (vector_C[i + 1] - vector_C[i]) / (3 * vector_H[i]);
            }
            return vecrtor_D;
        }
        //--------------------------------------------------------------------------------------------------------
        double[,] Straight_running_Jordan_Gauss(double[,] Arr)
        {
            for (int i = 0; i < Arr.GetLength(0); i++)
            {
                double b = Arr[i, i];
                for (int j = i; j < Arr.GetLength(1); j++)
                {
                    Arr[i, j] /= b;
                }
                for (int k = i + 1; k < Arr.GetLength(0); k++)
                {
                    double d = Arr[k, i];
                    for (int l = i; l < Arr.GetLength(1); l++)
                    {
                        Arr[k, l] -= d * Arr[i, l];
                    }
                }
            }
            return Arr;
        }
        //--------------------------------------------------------------------------------------------------------
        double[,] Reverse_running_Jordan_Gauss(double[,] Arr)
        {
            for (int i = Arr.GetLength(0) - 1; i >= 1; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    double d = Arr[j, i];
                    for (int k = 0; k < Arr.GetLength(1); k++)
                    {
                        Arr[j, k] -= d * Arr[i, k];
                    }
                }
            }
            return Arr;

        }
        //--------------------------------------------------------------------------------------------------------
        void Method()
        {
            var vector_X = Original_vector_X();
            Console.WriteLine("Print vector X");
            PrintVector(vector_X);
            var vector_F = Original_vector_F();
            Console.WriteLine("Print vector F");
            PrintVector(vector_F);
            var vector_A = Search_A(vector_F);
            Console.WriteLine("Print vector A");
            PrintVector(vector_A);
            var vector_H = Search_H(vector_X);
            Console.WriteLine("Print vector H");
            PrintVector(vector_H);
            var Matrix_C = Search_C(vector_X, vector_F, vector_H);
            Console.WriteLine("Print Matrix C");
            PrintArray(Matrix_C);
            Matrix_C = Straight_running_Jordan_Gauss(Matrix_C);
            Matrix_C = Reverse_running_Jordan_Gauss(Matrix_C);
            Console.WriteLine("Print Matrix C");
            PrintArray(Matrix_C);
            double[] vector_C = new double[5];
            for (int i = 1; i < Matrix_C.GetLength(1); i++)
            {
                vector_C[i] = Matrix_C[i - 1, Matrix_C.GetLength(1) - 1];
            }
            vector_C[0] = 0;
            vector_C[4] = 0;
            Console.WriteLine("Print vector C");
            PrintVector(vector_C);
            var vector_B = Search_B(vector_F, vector_H, vector_C);
            Console.WriteLine("Print vector B");
            PrintVector(vector_B);
            var vector_D = Search_D(vector_H, vector_C);
            Console.WriteLine("Print vector D");
            PrintVector(vector_D);
            PrintVector_F(vector_X, vector_A, vector_B, vector_C, vector_D);
        }
        //--------------------------------------------------------------------------------------------------------
        public void Start()
        {
            Method();
        }
    }
}
