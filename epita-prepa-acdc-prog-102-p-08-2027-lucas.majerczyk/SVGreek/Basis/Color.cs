using System;
using System.Collections.Generic;
using System.Linq;

namespace SVGreek;

public class Color
{
    public readonly byte R;
    public readonly byte G;
    public readonly byte B;
    public readonly byte A;

    public Color(byte r, byte g, byte b, byte a = 255)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public Color(string data)
    {
        bool notFound = false;
        bool toDouble = data.Length == 3 || data.Length == 4;
        int temp = 0;
        for (int i = 0; i < data.Length; i ++)
        {
            char c = data[i];
            if ((c >= 48 && c < 58) || (c > 64 && c < 71) || (c > 96 && c < 103) )
            {
                int number = toInt(c);
                switch (i)
                {
                    case 1:
                        if (toDouble)
                        {
                            R = (byte)(number * number);
                        }
                        else
                        {
                            temp = number;
                        }

                        break;
                    case 2:
                        if (toDouble)
                        {
                            G = (byte)(number * number);
                        }
                        else
                        {
                            R = (byte)(number * temp);
                        }

                        break;
                    case 3:
                        if (toDouble)
                        {
                            B = (byte)(number * number);
                        }
                        else
                        {
                            temp = number;
                        }

                        break;
                    case 4:
                        if (toDouble)
                        {
                            if (data.Length == 4)
                            {
                                A = (byte)(number * number);
                            }
                            else
                            {
                                A = 255;
                            }
                        }
                        else
                        {
                            G = (byte)(number * temp);
                        }

                        break;
                    case 5:
                        temp = number;
                        break;
                    case 6:
                        B = (byte)(number * temp);
                        if (data.Length == 6)
                        {
                            A = 255;
                        }

                        break;
                    case 7:
                        temp = number;
                        break;
                    case 8:
                        A = (byte)(number * temp);
                        break;
                    default:
                        notFound = true;
                        break;
                }
            }
            else
            {
                if (c != 35)
                {
                    notFound = true;
                }
            }

            if (notFound)
            {
                i = data.Length;
            }
        }

        if (notFound)
        {
            if (Colors.ContainsKey(data))
            {
                Color color = Colors[data];
                R = color.R;
                G = color.G;
                B = color.B;
                A = color.A;
            }
            else
            {
                throw new InvalidArgumentException();
            }
        }
    }

    public Color()
    {
        R = 0;
        G = 0;
        B = 0;
        A = 0;
    }
    public int toInt(char c)
    {
        if (c >= 48 && c < 58)
        {
            return c - 48;
        }
        return c - 86;
    }

    public static readonly Dictionary<string, Color> Colors = new Dictionary<string, Color>()
    {
        ["black"] = new Color(0, 0, 0),
        ["white"] = new Color(255, 255, 255),
        ["red"] = new Color(255, 0, 0),
        ["green"] = new Color(0, 255, 0),
        ["blue"] = new Color(0, 0, 255),
        ["yellow"] = new Color(255, 255, 0),
        ["cyan"] = new Color(0, 255, 255),
        ["magenta"] = new Color(255, 0, 255),
        ["none"] = new Color(0, 0, 0, 0)
    };

    public override bool Equals(object obj)
    {
        Color color = (Color)obj;
        if (R == color.R && G == color.G && B == color.B && A == color.A)
        {
            return true;
        }
        return false;
    }

    public static bool operator ==(Color? color1, Color? color2)
    {
        if (color1 is null)
        {
            if (color2 is null)
            {
                return true;
            }

            if (color2.A == 0 && color2.R == 0 && color2.G == 0 && color2.B == 0)
            {
                return true;
            }

            return false;
        }

        if (color2 is null)
        {
            return color2 == color1;
        }
        
        if (color1.A == color2.A && color1.R == color2.R && color1.G == color2.G && color1.B == color2.B)
        {
            return true;
        }

        return false;
    }

    public static bool operator !=(Color? color1, Color? color2)
    {
        return !(color1 == color2);
    }

    public override string ToString()
    {
        if (Colors.ContainsValue(this))
        {
            return Colors.First(x => x.Value == this).Key;
        }

        string answer = intToString(R) + intToString(G) + intToString(B);
        if (A != 255)
        {
            answer += intToString(A);
        }
        
        return answer;
    }

    public string intToString(int n)
    {
        var (first,second) = Math.DivRem(n, 16);
        return findChar(first) + findChar(second);
    }

    public string findChar(int n)
    {
        switch (n)
        {
            case 10:
                return "A";
            case 11:
                return "B";
            case 12:
                return "C";
            case 13:
                return "D";
            case 14:
                return "E";
            case 15:
                return "F";
            default:
                if (n > 15)
                {
                    throw new ArgumentException("the byte of the color is > to 255");
                }
                return n.ToString();
        }
    }
}