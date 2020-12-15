using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Hospital.Services
{
    public enum SortMethod
    {
        Standart,
        StandartTurnOver,
        DateTop,
        DateDown,
        Free,
        Booked
    }
    
    public static class SortMethods
    {
        public static string GetString(SortMethod method)
        {
            switch (method)
            {
                case SortMethod.Standart:
                    return "Standart";
                case SortMethod.StandartTurnOver:
                    return "StandartTurnOver";
                case SortMethod.DateTop:
                    return "DateTop";
                case SortMethod.DateDown:
                    return "DateDown";
                case SortMethod.Free:
                    return "Free";
                case SortMethod.Booked:
                    return "Booked";
                default:
                    return "Standart";
            }
        }

        public static SortMethod GetMethod(string method)
        {
            switch (method)
            {
                case "Standart":
                    return SortMethod.Standart;
                case "StandartTurnOver":
                    return SortMethod.StandartTurnOver;
                case "DateTop":
                    return SortMethod.DateTop;
                case "DateDown":
                    return SortMethod.DateDown;
                case "Free":
                    return SortMethod.Free;
                case "Booked":
                    return SortMethod.Booked;
                default:
                    return SortMethod.Standart;
            }
        }
    }
}