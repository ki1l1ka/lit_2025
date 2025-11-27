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
        for (int x = 1; x <= this.degree; x++)
        {
            if (this.coeffs == {0} | this.coeffs == {}) return "";
            if (this.coeffs[x] == 0){continue;}
            if (x == 1)
            {
                polynom = polynom + (this.coeffs[x] < 0? " - " + Math.Abs(this.coeffs[x]) + '^' + x : " + " this.coeffs[x] + '^' + x);
            }
            else
            {polynom = polynom + (this.coeffs[x] < 0? " - " + Math.Abs(this.coeffs[x]) + '^' + x : " + " this.coeffs[x] + '^' + x);}

        }
        return polynom;
    }
    public static Polynomial operator +(Polynomial obj1, Polynomial obj2)
    {
        int max = obj1.Length > obj2.Length? obj1.Length: obj2.Length;
        sum = new double[max];
        for (int i = 0; i < max; i++)
        {   
            double elem = 0;
            if (i < obj1.Length) {elem+= obj1[i];}
            if (i < obj2.Length) {elem+= obj2[i];}
            sum += elem;
        }
        return sum;
    }
    public static Polynomial operator * (Polynomial obj1, double k)
    {
        product = new double[obj1.Length-1];
        for (int i = 0; i < obj1.Length; i++)
        {
            product[i] = obj1[i] * k;
        }
        return product;
    }


}
class Programm{
    static void Main()
    {
        double[] l = {2, 3, 5, 3, 5 - 34, 4, -4, 0, -1};
        Console.WriteLine("hi");
        Polynomial p = new Polynomial(l);
        Console.WriteLine(p.ToString());
    }
}