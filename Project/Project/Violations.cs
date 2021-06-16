using System;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Globalization;
using System.Text.RegularExpressions;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.UserDefined,
IsByteOrdered = true, MaxByteSize = 1000, ValidationMethodName = "ValidateViolation")]
public struct Violation : INullable, IBinarySerialize
{
    private string _description;
    private bool _isPaid;
    private float _toPay;
    private DateTime _date;
    private bool m_Null;

    public Violation(string description, DateTime date, float toPay, bool isPaid)
    {
        _description = description;
        _isPaid = isPaid;
        _toPay = toPay;
        _date = date;
        m_Null = false;
    }

    public override string ToString()
    {
        if (this.IsNull)
            return "NULL";
        else
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_description + "; Czas: " + _date.ToString()
            + "; Do zapłaty: " + _toPay + "; Opłacone: ");
            if (_isPaid == true)
            {
                sb.Append("Tak");
            }
            else
            {
                sb.Append("Nie");
            }
            return sb.ToString();
        }
    }

    public bool IsNull
    {
        get
        {
            return m_Null;
        }
    }

    public static Violation Null
    {
        get
        {
            Violation v = new Violation();
            v.m_Null = true;
            return v;
        }
    }

    public static Violation Parse(SqlString s)
    {
        string value = s.Value;

        if (s.IsNull || value.Trim() == "")
        {
            return Null;
        }

        string[] parameters = s.Value.Split(",".ToCharArray());

        Violation v = new Violation();

        v.Description = parameters[0].Trim();
        if(parameters[3].Trim().Equals("Tak"))
        {
            v.IsPaid = true;
        } else {
            v.IsPaid = false;
        }

        v._toPay = float.Parse(parameters[2], System.Globalization.CultureInfo.InvariantCulture);
        v.Date = Convert.ToDateTime(parameters[1].Trim());

        if (!v.ValidateViolation())
        {
            throw new ArgumentException("Invalid input sequence.");
        }

        return v;
    }

    public float surcharge(int perc)
    {
        if (perc < 0 || perc > 100)
        {
            throw new ArgumentException("Invalid percent value.");
        }
        return _toPay + _toPay * perc / 100;
    }

    public string Description
    {
        get
        {
            return this._description;
        }

        set
        {
            _description = value;
        }
    }

    public bool IsPaid
    {
        get
        {
            return this._isPaid;
        }

        set
        {
            _isPaid = value;
        }
    }

    public float ToPay
    {
        get
        {
            return this._toPay;
        }

        set
        {
            float temp = _toPay;
            _toPay = value;
            if (!ValidateViolation())
            {
                _toPay = temp;
                throw new ArgumentException("Invalid value.");
            }
        }
    }

    public DateTime Date
    {
        get
        {
            return this._date;
        }

        set
        {
            _date = value;
        }
    }

    private bool ValidateViolation()
    {
        return _toPay > 0;
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(@_description);
        w.Write(_isPaid);
        w.Write(_toPay);
        w.Write(_date.Ticks);
    }

    public void Read(System.IO.BinaryReader r)
    {
        this._description = r.ReadString();
        this._isPaid = r.ReadBoolean();
        this._toPay = r.ReadSingle();
        this._date = new DateTime(r.ReadInt64());
    }
}