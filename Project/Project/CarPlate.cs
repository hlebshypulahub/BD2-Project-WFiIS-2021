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
IsByteOrdered = true, MaxByteSize = 14, ValidationMethodName = "ValidateCarPlate")]
public struct CarPlate : INullable, IBinarySerialize
{
    private string _number;
    private bool m_Null;

    public CarPlate(string number)
    {
        CarPlate plate = new CarPlate("WW12345");
        plate.Number = number;
        if (!plate.ValidateCarPlate())
        {
            throw new ArgumentException("Invalid car plate number.");
        }

        _number = number;
        m_Null = false;
    }

    public override string ToString()
    {
        if (this.IsNull)
        {
            return "NULL";
        }
        else
        {
            return _number;
        }
    }

    public bool IsNull
    {
        get
        {
            return m_Null;
        }
    }

    public static CarPlate Null
    {
        get
        {
            CarPlate plate = new CarPlate();
            plate.m_Null = true;
            return plate;
        }
    }

    public static CarPlate Parse(SqlString s)
    {
        string value = s.Value;
        if (s.IsNull || value.Trim() == "")
        {
            return Null;
        }

        CarPlate plate = new CarPlate();
        plate._number = value.Trim();

        if (!plate.ValidateCarPlate())
        {
            throw new ArgumentException("Invalid car plate number.");
        }

        return plate;
    }

    public string Number
    {
        get
        {
            return this._number;
        }

        set
        {
            string temp = _number;
            _number = value;
            if (!ValidateCarPlate())
            {
                _number = temp;
                throw new ArgumentException("Invalid car plate number.");
            }
        }
    }

    public string Province
    {
        get
        {
            switch (_number.Substring(0, 1))
            {
                case "B":
                    return "Podlaskie";
                case "C":
                    return "Kujawsko-pomorskie";
                case "D":
                    return "Dolnośląskie";
                case "E":
                    return "Łódzkie";
                case "F":
                    return "Lubuskie";
                case "G":
                    return "Pomorskie";
                case "K":
                    return "Małopolskie";
                case "L":
                    return "Lubelskie";
                case "N":
                    return "Warmińsko-mazurskie";
                case "O":
                    return "Opolskie";
                case "P":
                    return "Wielkopolskie";
                case "R":
                    return "Podkarpackie";
                case "S":
                    return "Śląskie";
                case "T":
                    return "Świętokrzyskie";
                case "W":
                    return "Mazowieckie";
                case "Z":
                    return "Zachodniopomorskie";
                default:
                    return "No option found";
            }
        }
    }

    public bool fromSameProvince(CarPlate plate)
    {
        return this.Province.Equals(plate.Province);
    }

    private bool ValidateCarPlate()
    {
        if (String.IsNullOrEmpty(this.ToString()) || this.ToString().Length > 7)
        {
            return false;
        }

        return Regex.IsMatch(this.ToString(), @"^[A-Z]{1,2}[A-Z0-9]{1,5}$");
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(@_number);
    }

    public void Read(System.IO.BinaryReader r)
    {
        string stringValue;

        stringValue = r.ReadString();

        this._number = stringValue;
    }
}