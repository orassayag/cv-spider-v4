using System;
using System.Linq;
using System.Collections.Generic;

namespace CVSpider.Code
{
    public class Cities
    {
        /*Description:
        */
        public Cities() { }

        /*Description:
        */
        public static string GetRandomCity()
        {
            Random R = new Random();
            List<string> Cities = CitiesList();
            return Cities.ElementAt(R.Next(1, Cities.Count));
        }

        /*Description:
        */
        public static List<string> CitiesList()
        {
            return new List<string>()
            {
                "עמק-חפר",
                "עמק חפר",
                "כפר-סבא",
                "כפר סבא",
                "רעננה",
                "ראש העין",
                "ראש-העין",
                "רמות השבים",
                "רמות-השבים",
                "הרצליה",
                "רמת השרון",
                "רמת-השרון",
                "פתח תקווה",
                "פתח-תקווה",
                "הוד השרון",
                "הוד-השרון",
                "פרדסיה",
                "נתניה",
                "אבן-יהודה",
                "אבן יהודה",
                "צופית",
                "כפר יונה",
                "כפר-יונה",
                "צורן",
                "קדימה",
                "כוכב יאיר",
                "כוכב-יאיר",
                "צור יגאל",
                "צור יגאל",
                @"כפ""ס",
                @"פ""ת",
            };
        }
    }
}