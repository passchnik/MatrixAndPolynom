using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MatrixAndPolynom
{
    [Serializable]
    public class Matrix 
    {
        public int[,] Array { get; private set; }
        public int Height { get; private set; }
        public int Length { get; private set; }



        public Matrix(int[,] Array)
        {
                        
            this.Array = Array;
            Height = Array.GetLength(0);
            Length = Array.GetLength(1);
        }

        public Matrix()
        {
        }


        public static Matrix operator +(Matrix FirstMatrix, Matrix SecodMatrix)
        {
            if (FirstMatrix.Array == null || SecodMatrix.Array == null)
            {
                
                throw new MatrixException("You cannot add by an empty matrix. First, fill it out.");
            }
            else
            {
                if ((FirstMatrix.Length == SecodMatrix.Length) &&
                (FirstMatrix.Height == SecodMatrix.Height))
                {
                    Matrix newMatrix = new Matrix();
                    newMatrix.Height = FirstMatrix.Height;
                    newMatrix.Length = FirstMatrix.Length;
                    newMatrix.Array = Sum(FirstMatrix.Array, SecodMatrix.Array);
                    return newMatrix;
                }
                else
                {
                    throw new MatrixException("You cannot sum matrices with different sizes.");
                }
            }

        }

        private static int[,] Sum(int[,] FirstMatrix, int[,] SecodMatrix)
        {
            int[,] temp = new int[FirstMatrix.GetLength(0), FirstMatrix.GetLength(1)];

            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    temp[i, j] = FirstMatrix[i, j] + SecodMatrix[i, j];
                }
            }
            return temp;

        }



        public static Matrix operator +(Matrix matrix, int addition)
        {
            if (matrix.Array == null)
            {
                throw new MatrixException("You cannot add by an empty matrix. First, fill it out.");
            }
            else
            {
                Matrix newMatrix = new Matrix(matrix.Array);

                for (int i = 0; i < newMatrix.Height; i++)
                {
                    for (int j = 0; j < newMatrix.Length; j++)
                    {
                        newMatrix.Array[i, j] += addition;
                    }
                }
                return newMatrix;
            }

        }



        public static Matrix operator -(Matrix FirstMatrix, Matrix SecodMatrix)
        {
            if (FirstMatrix.Array == null || SecodMatrix.Array == null)
            {
                throw new MatrixException("You cannot subtract by an empty matrix. First, fill it out.");
            }
            else
            {
                if ((FirstMatrix.Length == SecodMatrix.Length) &&
                (FirstMatrix.Height == SecodMatrix.Height))
                {
                    Matrix newMatrix = new Matrix();
                    newMatrix.Height = FirstMatrix.Height;
                    newMatrix.Length = FirstMatrix.Length;
                    newMatrix.Array = Subtraction(FirstMatrix.Array, SecodMatrix.Array);
                    return newMatrix;

                }
                else
                {
                    throw new MatrixException("You cannot subtract matrices with different sizes.");
                }
            }


        }

        private static int[,] Subtraction(int[,] FirstMatrix, int[,] SecodMatrix)
        {
            int[,] temp = new int[FirstMatrix.GetLength(0), FirstMatrix.GetLength(1)];

            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    temp[i, j] = FirstMatrix[i, j] - SecodMatrix[i, j];
                }
            }
            return temp;

        }


        public static Matrix operator -(Matrix matrix, int negative)
        {
            if (matrix.Array == null)
            {
                throw new MatrixException("You cannot subtract by an empty matrix. First, fill it out.");
            }
            else
            {
                Matrix newMatrix = new Matrix(matrix.Array);

                for (int i = 0; i < newMatrix.Height; i++)
                {
                    for (int j = 0; j < newMatrix.Length; j++)
                    {
                        newMatrix.Array[i, j] -= negative;
                    }
                }
                return newMatrix;
            }

        }

        public static Matrix operator *(Matrix FirstMatrix, Matrix SecodMatrix)
        {
            if (FirstMatrix.Array == null || SecodMatrix.Array == null)
            {
                throw new MatrixException("You cannot multiply by an empty matrix. First, fill it out.");
            }
            else
            {
                if (FirstMatrix.Length == SecodMatrix.Height)
                {
                    Matrix newMatrix = new Matrix();
                    newMatrix.Height = FirstMatrix.Height;
                    newMatrix.Length = SecodMatrix.Length;
                    newMatrix.Array = MultiplicationMatricesCalculate(FirstMatrix.Array, SecodMatrix.Array);
                    return newMatrix;
                }
                else if (FirstMatrix.Height == SecodMatrix.Length)
                {
                    Matrix newMatrix = new Matrix();
                    newMatrix.Height = SecodMatrix.Height;
                    newMatrix.Length = FirstMatrix.Length;
                    newMatrix.Array = MultiplicationMatricesCalculate(SecodMatrix.Array, FirstMatrix.Array);
                    return newMatrix;
                }
                else
                {
                    throw new MatrixException("Матрицы не могут быть перемножены так как высота одной не равнa ширине другой");
                }
            }



        }

        public static Matrix operator *(Matrix matrix, int multiplier)
        {
            if (matrix.Array == null)
            {
                throw new MatrixException("You cannot multiply by an empty matrix. First, fill it out.");
            }
            else
            {
                return Multiplication(matrix, multiplier);  
            }

        }

        public static Matrix operator *(int multiplier, Matrix matrix)
        {
            if (matrix.Array == null)
            {
                throw new MatrixException("You cannot multiply by an empty matrix. First, fill it out.");
            }
            else
            {
                return Multiplication(matrix, multiplier);
            }
        }

        private static Matrix Multiplication(Matrix matrix, int multiplier)
        {
            Matrix newMatrix = new Matrix(matrix.Array);

            for (int i = 0; i < newMatrix.Height; i++)
            {
                for (int j = 0; j < newMatrix.Length; j++)
                {
                    newMatrix.Array[i, j] *= multiplier;
                }
            }
            return newMatrix;
        }



        private static int[,] MultiplicationMatricesCalculate(int[,] Array1, int[,] Array2)
        {
            int[,] temp = new int[Array1.GetLength(0), Array2.GetLength(1)];

            //заполнение конечной матрицы
            for (int i = 0; i < Array1.GetLength(0); i++)
            {

                //подсчет элеменtа матрицы
                for (int k = 0; k < Array2.GetLength(1); k++)
                {
                    for (int l = 0; l < Array2.GetLength(0); l++)
                    {
                        temp[i, k] += Array1[i, l] * Array2[l, k];
                    }

                }
            }
            return temp;
        }

        public void SaveBine(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                
                formatter.Serialize(fs, this);

                Console.WriteLine("The object has serialized");
            }

        }


        public void OpenBine(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                Matrix result  = (Matrix)formatter.Deserialize(fs);

                Array = result.Array;
                Height = result.Height;
                Length = result.Length;
                
            }
        }


        public Matrix Clone()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (Matrix)formatter.Deserialize(ms);
            }
            
        }

        

        public static bool operator ==(Matrix FirstMatrix, Matrix SecodMatrix)
        {
            if ((FirstMatrix.Length != SecodMatrix.Length )&&
                (FirstMatrix.Height != SecodMatrix.Height))
            {
                throw new Exception("Matrices have different sizes");
            }

            return (FirstMatrix.Array == SecodMatrix.Array) ? true : false;
            

        }

        public static bool operator !=(Matrix FirstMatrix, Matrix SecodMatrix)
        {
            if ((FirstMatrix.Length != SecodMatrix.Length) &&
                (FirstMatrix.Height != SecodMatrix.Height))
            {
                throw new Exception("Matrices have different sizes"); 
            }

            return (FirstMatrix.Array == SecodMatrix.Array) ? false : true;
        }


        public int this[int i, int j]
        {

                
            get
            {
                if (i>Height || j>Length)
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
                return Array[i, j];
            }
            set
            {
                if (i > Height || j > Length)
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
                Array[i, j] = value;
            }
        }




    }

    
}
