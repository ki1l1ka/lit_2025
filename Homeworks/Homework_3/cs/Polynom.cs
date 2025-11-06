using System;

class Polynomial
{
    private int degree;
    private double[] coeffs;

    public Polynomial()
    {
        degree = 0;
        coeffs = new double[1] { 0.0 };
    }

    public Polynomial(double[] new_coeffs)
    {
        degree = new_coeffs.Length - 1;
        coeffs = (double[])new_coeffs.Clone();
    }

    public int Degree
    {
        get { return degree; }
    }

    public double[] Coeffs
    {
        get { return (double[])coeffs.Clone(); }
    }

    public override string ToString()
    {
        string polynom = "1";
        for (int x = 1; x <= degree; x++)
        {
            if (coeffs[x] == 0){continue;}
            if (x == 1)
            {
                polynom = polynom + (coeffs[x] < 0? " - " + Math.Abs(coeffs[x]) + ^ + x : " + " coeffs[x] + ^ + x);
            }
            else
            {polynom = polynom + (coeffs[x] < 0? " - " + Math.Abs(coeffs[x]) + ^ + x : " + " coeffs[x] + ^ + x);}

        }
        return polynom;
    }
}
class Programm{
    static void Main()
    {
        double[] l = [2, 3, 5, 3, 5 - 34, 4, -4, 0, -1];
        Console.WriteLine("hi");
        Polynomial p = new Polynomial(l);
        Console.WriteLine(p.ToString());
    }
}