using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPAdressen
{
    public class Helper
    {
      public static string CleanNumber(string s)
        {
            if (s.Length > 0)
            {
                if (s.Substring(0, 1) == "0")
                {
                    s = s.Substring(1);
                    s = CleanNumber(s);
                }
            }

            return s;
        }
      public static string AddZeros(string s)
      {
          if (s.Length < 3)
          {
              if (s.Substring(0, 1) != "0")
              {
                  s = "0" + s;
                  s = AddZeros(s);
              }
          }

          return s;
      }


     public static string ParseIP(String s)
      {

          Byte b = new byte();

          if (!Byte.TryParse(s, out b))
          {
              if (s.Length > 0)
              {
                  s = ParseIP(s.Substring(s.Length - 1));
              }

          }
          return s;
      }

        public static bool HatXNachfolger(List<int> l, int v, int n) {

            for (int i = 0; i < n; i++)
            {                
                if (!l.Contains(( v + i - 1 )))
                { 
                    return false;
                }
            }
            return true;



        }
    }
}