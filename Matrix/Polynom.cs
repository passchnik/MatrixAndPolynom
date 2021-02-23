using System;
namespace MatrixAndPolynom
{
    public class Polynom 
    {
        public double [] Coefficients { get; private set; }
        public int Length;

        public Polynom(params double [] Coefficients)
        {
            if (Coefficients == null)
            {
                throw new PolynomException("Null");
            }
            this.Coefficients = Coefficients;
            Length = Coefficients.Length;
        }

        

        public static Polynom operator +(Polynom FirstPolynom, Polynom SecondPolynom)
        {
            double[] result = (FirstPolynom.Length > SecondPolynom.Length) ?
                FirstPolynom.Coefficients: SecondPolynom.Coefficients;

            double[] polynomCoefficientsMinLength = (FirstPolynom.Length < SecondPolynom.Length) ?
                FirstPolynom.Coefficients : SecondPolynom.Coefficients;


            for (int i = 0; i < polynomCoefficientsMinLength.Length; i++)
            {
                result[i] += polynomCoefficientsMinLength[i];
            }

            return new Polynom(result);
        }

        public static Polynom operator -(Polynom FirstPolynom, Polynom SecondPolynom)
        {
      
            double[] result = (FirstPolynom.Length > SecondPolynom.Length) ?
                FirstPolynom.Coefficients : SecondPolynom.Coefficients;

            if (FirstPolynom.Length > SecondPolynom.Length)
            {
                for (int i = 0; i < SecondPolynom.Length; i++)
                {
                    result[i] -= SecondPolynom.Coefficients[i];
                }
            }
            else
            {
                for (int i = 0; i < FirstPolynom.Length; i++)
                {
                    result [i] = FirstPolynom.Coefficients[i] - result[i];
                }
            }

            return new Polynom(result);
        }


        public static Polynom operator *(Polynom FirstPolynom, Polynom SecondPolynom)
        {
            int size = FirstPolynom.Length + SecondPolynom.Length - 1;

            double[] result = new double[size];

            // for adding new value
            for (int i = 0; i < size; i++)
                result[i] = 0;

            for (int i = 0; i < FirstPolynom.Length; i++)
            {
                for (int j = 0; j < SecondPolynom.Length; j++)
                {
                    if (FirstPolynom.Coefficients[i] != 0d &&
                        SecondPolynom.Coefficients[j] != 0d)
                    {
                        int power = i + j;
                        double coefficient =
                            FirstPolynom.Coefficients[i] * SecondPolynom.Coefficients[j];

                        result[power] += coefficient;
                    }
                }
            }

            return new Polynom(result);
        }

        public static Polynom operator *(Polynom FirstPolynom, double multiplier)
        {
            double[] result = FirstPolynom.Coefficients;

            for (int i = 0; i < FirstPolynom.Length; i++)
            {
                result[i] *= multiplier;
            }

            return new Polynom(result) ;
        }


        public void ChangePower(int currentPower, int newPower)
        {
            if (currentPower < 0 || newPower < 0)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
            if (Length - 1 > newPower)
            {
                Coefficients[newPower] += Coefficients[currentPower];
                Coefficients[currentPower] = 0;
            }
            else
            {
                double[] result = new double[newPower + 1];

                for (int i = 0; i < Length; i++)
                    result[i] = Coefficients[i];

                for (int i = Length; i < result.Length; i++)
                    result[i] = 0;

                result[newPower] += result[currentPower];
                result[currentPower] = 0;

                Coefficients = result;
            }
        }

        public double this[int i]
        {
            get
            {
                if (i >= Length)
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
                return Coefficients[i];
            }
            set
            {

                if (i < 0)
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }

                if (i > Length-1)
                {
                    double[] result = new double[i + 1];

                    for (int j = 0; j < Length; j++)
                        result[j] = Coefficients[j];

                    for (int j = Length; j < result.Length; j++)
                        result[j] = 0;

                    result[i] = value;

                    Coefficients = result;
                    Length = result.Length;
                }
                else
                {
                    Coefficients[i] = value;
                }
                
            }
        }


    }
}
