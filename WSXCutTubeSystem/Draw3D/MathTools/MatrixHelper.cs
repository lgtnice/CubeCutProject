using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.Draw3D.MathTools
{
    public class MatrixHelper
    {
        public static float[] Multi4x4with4x1(float[] four, float[] one)
        {
            float[] result = new float[4];
            for (int i = 0; i < one.Length; i++)
            {
                result[i] = one[0] * four[i] + one[1] * four[i + 4] + one[2] * four[i + 8] + one[3] * four[i + 12];
            }
            return result;
        }

        public static float[] Multi4x4with4x1ForMove(float[] four, float[] one)
        {
            float[] result = new float[4];
            for (int i = 0; i < one.Length; i++)
            {
                result[i] = one[0] * four[i] + one[1] * four[i + 4] + one[2] * four[i + 8] - one[3] * four[i + 12];
            }
            return result;
        }

        public static PointF Multi4x4with4x1ForNoMove(float[] four, float[] one)
        {
            float[] result = new float[4];
            for (int i = 0; i < one.Length; i++)
            {
                result[i] = one[0] * four[i] + one[1] * four[i + 4] + one[2] * four[i + 8];
            }
            return new PointF(result[0], result[1]);
        }

        public static PointF Multi4x4with4x1JustMove(float[] four, float[] one)
        {
            PointF point = new PointF();
            point.X = one[0] - four[12];
            point.Y = one[1] - four[13];
            return point;
        }

        public static float[] Multi1x4with4x1(float[] one, float[] four)
        {
            float[] result = new float[4];
            for (int i = 0; i < one.Length;i++)
            {
                result[i] = one[0] * four[i*4] + one[1] * four[i*4+1] + one[2] * four[i * 4 + 2] + one[3] * four[i * 4 + 3];
            }
            return result;
        }

        public static float[][] ConvertOneToTwoDimension(float[] matrix)
        {
            int len = (int)Math.Sqrt(matrix.Length);
            float[][] result = new float[len][];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new float[len];
            }
            for (int i = 0; i < matrix.Length; )
            {
                for (int j = 0; j < len; j++)
                {
                    result[i/len][j] = matrix[i++];
                }
            }
            return result;
        }

        public static float[] ConvertTwoDimensionToOne(float[][] matrix)
        {
            float[] result = new float[16];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[i*4+j] = matrix[i][j];
                }
            }
            return result;
        }
        public static float[][] InverseMatrix(float[][] matrix)
        {
            //matrix必须为非空
            if (matrix == null || matrix.Length == 0)
            {
                return new float[][] { };
            }
            //matrix 必须为方阵
            int len = matrix.Length;
            for (int counter = 0; counter < matrix.Length; counter++)
            {
                if (matrix[counter].Length != len)
                {
                    throw new Exception("matrix 必须为方阵");
                }
            }
            //计算矩阵行列式的值
            float dDeterminant = Determinant(matrix);
            if (Math.Abs(dDeterminant) <= 1E-6)
            {
                throw new Exception("矩阵不可逆");
            }
            //制作一个伴随矩阵大小的矩阵
            float[][] result = AdjoinMatrix(matrix);
            //矩阵的每项除以矩阵行列式的值，即为所求
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    result[i][j] = result[i][j] / dDeterminant;
                }
            }
            return result;
        }

        private static float Determinant(float[][] matrix)
        {
            if (matrix.Length == 0)
            {
                return 0;
            }
            else if (matrix.Length == 1)
            {
                return matrix[0][0];
            }
            else if (matrix.Length == 2)
            {
                return matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];
            }
            //对第一行使用“加边法”递归计算行列式的值
            float dSum = 0, dSign = 1;
            for (int i = 0; i < matrix.Length; i++)
            {
                float[][] matrixTemp = new float[matrix.Length - 1][];
                for (int count = 0; count < matrix.Length - 1; count++)
                {
                    matrixTemp[count] = new float[matrix.Length - 1];
                }
                for (int j = 0; j < matrixTemp.Length; j++)
                {
                    for (int k = 0; k < matrixTemp.Length; k++)
                    {
                        matrixTemp[j][k] = matrix[j + 1][k >= i ? k + 1 : k];
                    }
                }
                dSum += (matrix[0][i] * dSign * Determinant(matrixTemp));
                dSign = dSign * -1;
            }
            return dSum;
        }

        private static float[][] AdjoinMatrix(float[][] matrix)
        {
            //制作一个伴随矩阵大小的矩阵
            float[][] result = new float[matrix.Length][];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new float[matrix[i].Length];
            }
            //生成伴随矩阵
            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < result.Length; j++)
                {
                    //存储代数余子式的矩阵（行、列数都比原矩阵少1）
                    float[][] temp = new float[result.Length - 1][];
                    for (int k = 0; k < result.Length - 1; k++)
                    {
                        temp[k] = new float[result[k].Length - 1];
                    }
                    //生成代数余子式
                    for (int x = 0; x < temp.Length; x++)
                    {
                        for (int y = 0; y < temp.Length; y++)
                        {
                            temp[x][y] = matrix[x < i ? x : x + 1][y < j ? y : y + 1];
                        }
                    }
                    result[j][i] = ((i + j) % 2 == 0 ? 1 : -1) * Determinant(temp);
                }
            }
            return result;
        }

    }
}
