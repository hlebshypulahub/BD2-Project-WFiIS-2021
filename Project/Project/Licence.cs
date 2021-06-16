using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Globalization;
using System.Text.RegularExpressions;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.UserDefined,
IsByteOrdered = true, MaxByteSize = 100, ValidationMethodName = "ValidateLicence")]
public struct Licence : INullable, IBinarySerialize
{
    private string _category;
    private DateTime _date;
    private bool m_Null;

    public Licence(string category, DateTime date)
    {
        _category = category;
        _date = date;
        m_Null = false;
    }

    public override string ToString()
    {
        if (this.IsNull)
            return "NULL";
        else
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Kategoria: ");
            builder.Append(_category);
            builder.Append("; Data wydania: ");
            builder.Append(_date.ToString("d"));
            return builder.ToString();
        }
    }

    public bool IsNull
    {
        get
        {
            return m_Null;
        }
    }

    public static Licence Null
    {
        get
        {
            Licence l = new Licence();
            l.m_Null = true;
            return l;
        }
    }

    public static Licence Parse(SqlString s)
    {
        string value = s.Value;

        if (s.IsNull || value.Trim() == "")
        {
            return Null;
        }

        Licence l = new Licence();
        string[] parameters = s.Value.Split(",".ToCharArray());
        l._category = parameters[0].Trim();
        l._date = Convert.ToDateTime(parameters[1].Trim());

        if(!l.ValidateLicence())
        {
            throw new ArgumentException("Invalid input values.");
        }

        return l;
    }

    public string Category
    {
        get
        {
            return this._category;
        }

        set 
        {
            string temp = _category;
            _category = value;
                if(!ValidateLicence())
            {
                _category = temp;
                throw new ArgumentException("Invalid input values.");
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

    private bool ValidateLicence()
    {
        if (String.IsNullOrEmpty(this.ToString()))
        {
            return false;
        }

        if (_category.Length > 7)
        {
            throw new ArgumentException("Invalid category.");
        }

        return true;
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(@_category);
        w.Write(_date.Ticks);
    }

    public void Read(System.IO.BinaryReader r)
    {
        this._category = r.ReadString();

        this._date = new DateTime(r.ReadInt64());
    }
}