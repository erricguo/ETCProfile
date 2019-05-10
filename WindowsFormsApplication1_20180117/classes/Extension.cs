using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    public static class Extension
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            //若取不到屬性，則取名稱
            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static float Tofloat(this decimal f)
        {
            try
            {
                float a = -1;
                float.TryParse(f.ToString(), out a);
                return a;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static float Tofloat(this string s)
        {
            try
            {
                float a = -1;
                float.TryParse(s.ToString(), out a);
                return a;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static decimal ToDecimal(this float f)
        {
            try
            {
                decimal a = -1;
                decimal.TryParse(f.ToString(), out a);
                return a;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static decimal ToDecimal(this double f)
        {
            try
            {
                decimal a = -1;
                decimal.TryParse(f.ToString(), out a);
                return a;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static double ToDouble(this object s)
        {
            try
            {
                double a = -1;
                double.TryParse(s.ToString(), out a);
                return a;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static decimal ToDecimal(this object s)
        {
            try
            {
                decimal a = -1;
                decimal.TryParse(s.ToString(), out a);
                return a;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int ToInt(this object s)
        {
            try
            {
                int a = -1;
                int.TryParse(s.ToString(), out a);
                return a;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int ToInt(this string s)
        {
            try
            {
                return int.Parse(s);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string Right(this string s,int length)
        {
            
            try
            {
                return s.Substring(s.Length - length);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string Left(this string s, int length)
        {
            
            try
            {
                return s.Substring(0, length);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string LeftStart(this string s, int start)
        {
            
            try
            {
                return s.Substring(start, s.Length - start);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string Mid(this string s, int start, int length)
        {
            
            try
            {
                return s.Substring(start, length);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



    }
}
