using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native,
IsByteOrdered = true, ValidationMethodName = "ValidateQuadraticFunction")]
public struct QuadraticFunction : INullable
{
    private double _a;
    private double _b;
    private double _c;
    private bool m_Null;

    public QuadraticFunction(double a, double b, double c)
    {
        _a = a;
        _b = b;
        _c = c;
        m_Null = false;
    }

    public override string ToString()
    {
        if (this.IsNull)
            return "NULL";
        else
        {
            return _a + "x^2" + " + " + _b + "x" + " + " + _c;
        }
    }

    public bool IsNull
    {
        get
        {
            return m_Null;
        }
    }

    public static QuadraticFunction Null
    {
        get
        {
            QuadraticFunction qf = new QuadraticFunction();
            qf.m_Null = true;
            return qf;
        }
    }

    public static QuadraticFunction Parse(SqlString s)
    {
        string value = s.Value;
        if (s.IsNull || value.Trim() == "")
        {
            return Null;
        }

        QuadraticFunction qf = new QuadraticFunction();
        string[] parameters = s.Value.Split(",".ToCharArray());

        if (parameters.Length != 3)
        {
            throw new ArgumentException("Invalid input values. Must have 3 values, but You typed " + parameters.Length);
        }

        qf._a = Double.Parse(parameters[0], System.Globalization.CultureInfo.InvariantCulture);
        qf._b = Double.Parse(parameters[1], System.Globalization.CultureInfo.InvariantCulture);
        qf._c = Double.Parse(parameters[2], System.Globalization.CultureInfo.InvariantCulture);

        if (!qf.ValidateQuadraticFunction())
        {
            throw new ArgumentException("Invalid input values.");
        }

        return qf;
    }

    public double A
    {
        get
        {
            return this._a;
        }

        set
        {
            _a = value;
        }
    }

    public double B
    {
        get
        {
            return this._b;
        }

        set
        {
            _b = value;
        }
    }

    public double C
    {
        get
        {
            return this._c;
        }

        set
        {
            _c = value;
        }
    }

    public string countMaxMin()
    {
        string output = "NULL";

        double secondPart = _c * 1.0 - (_b * _b / (4.0 * _a));

        if (_a > 0)
        {
            output = "Maxvalue = Infinity; Minvalue = " + secondPart;
        }
        else if (_a < 0)
        {
            output = "Maxvalue = " + secondPart + "; Minvalue = -Infinity";
        }

        return output;
    }

    public static bool isEquals(QuadraticFunction qf1, QuadraticFunction qf2)
    {
        return qf1.A == qf2.A && qf1.B == qf2.B && qf1.C == qf2.C;
    }

    private bool ValidateQuadraticFunction()
    {
        if (String.IsNullOrEmpty(this.ToString()))
        {
            return false;
        }

        string[] parameters = this.ToString().Split("+".ToCharArray());
        double a = Double.Parse(parameters[0].Trim().Split("x".ToCharArray())[0], System.Globalization.CultureInfo.InvariantCulture);
        double b = Double.Parse(parameters[1].Trim().Split("x".ToCharArray())[0], System.Globalization.CultureInfo.InvariantCulture);
        double c = Double.Parse(parameters[2].Trim(), System.Globalization.CultureInfo.InvariantCulture);

        if (a == 0)
        {
            throw new ArgumentException("a = 0. IT is not a quadratic function.");
        }

        return true;
    }
}