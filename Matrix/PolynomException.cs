using System;
namespace MatrixAndPolynom
{
    public class PolynomException : Exception
    {
        public PolynomException() : base() { }

        public PolynomException(String str) : base (str) { }
    }
}
