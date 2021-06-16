﻿using System;
using System.Data;
using System.Globalization;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native,
IsByteOrdered = true, ValidationMethodName = "ValidateRGBColor")]
public struct RGBColor : INullable
{
    private int _r;
    private int _g;
    private int _b;
    private bool m_Null;

    public RGBColor(int r, int g, int b)
    {
        _r = r;
        _g = g;
        _b = b;
        m_Null = false;
    }

    public override string ToString()
    {
        if (this.IsNull)
            return "NULL";
        else
        {
            return "(" + _r + "," + _g + "," + _b + ")";
        }
    }

    public bool IsNull
    {
        get
        {
            return m_Null;
        }
    }

    public static RGBColor Null
    {
        get
        {
            RGBColor rgb = new RGBColor();
            rgb.m_Null = true;
            return rgb;
        }
    }

    public static RGBColor Parse(SqlString s)
    {
        string value = s.Value;
        if (s.IsNull || value.Trim() == "")
        {
            return Null;
        }

        RGBColor rgb = new RGBColor();
        string[] vals = s.Value.Split(",".ToCharArray());

        if (vals.Length != 3)
        {
            throw new ArgumentException("Invalid input values. Must have 3 values for Reg, Green and Blue");
        }

        vals[0] = vals[0].Trim();
        vals[1] = vals[1].Trim();
        vals[2] = vals[2].Trim();

        rgb._r = int.Parse(vals[0].Substring(1, vals[0].Length - 1).Trim(), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);
        rgb._g = int.Parse(vals[1].Trim(), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);
        rgb._b = int.Parse(vals[2].Substring(0, vals[2].Length - 1).Trim(), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);

        if (!rgb.ValidateRGBColor())
        {
            throw new ArgumentException("Invalid input values.");
        }

        return rgb;
    }

    public int R
    {
        get
        {
            return this._r;
        }

        set
        {
            _r = value;
        }
    }

    public int G
    {
        get
        {
            return this._g;
        }

        set
        {
            _g = value;
        }
    }

    public int B
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

    public static RGBColor add(RGBColor rgb1, RGBColor rgb2)
    {
        return new RGBColor(Math.Min(rgb1.R + rgb2.R, 255), Math.Min(rgb1.G + rgb2.G, 255),
        Math.Min(rgb1.B + rgb2.B, 255));
    }

    public static bool isEquals(RGBColor rgb1, RGBColor rgb2)
    {
        return rgb1.R == rgb2.R && rgb1.G == rgb2.G && rgb1.B == rgb2.B;
    }

    private bool ValidateRGBColor()
    {
        if (String.IsNullOrEmpty(this.ToString()))
        {
            return false;
        }

        if (_r < 0 || _r > 255)
        {
            throw new ArgumentException("R must be int range [0, 255]");
        }

        if (_g < 0 || _g > 255)
        {
            throw new ArgumentException("G must be int range [0, 255]");
        }

        if (_b < 0 || _b > 255)
        {
            throw new ArgumentException("B must be int range [0, 255]");
        }

        return true;
    }
}