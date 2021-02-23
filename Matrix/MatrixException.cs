using System;
namespace MatrixAndPolynom
{
    public class MatrixException : Exception
    {
        public MatrixException() : base(){}

        public MatrixException(string str): base (str) { }
    }
}
