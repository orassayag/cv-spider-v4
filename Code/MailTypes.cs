using System;
using System.Linq;
using System.Net.Mail;
using System.Collections.Generic;

namespace CVSpider.Code
{
    public class MailTypes
    {
        /*Description:
        */
        public MailTypes() { }

        /*Description:
        */
        public static string GetRandomMailType()
        {
            Random R = new Random();
            List<string> MailTypes = MailTypesList();
            return MailTypes.ElementAt(R.Next(1, MailTypes.Count));
        }

        /*Description:
        */
        public static List<string> MailTypesList()
        {
            return new List<string>()
            {
                "מייל",
                "אי-מייל",
                @"דוא""ל",
                "דואר אלקטרוני",
                "Email",
                "e-mail",
                "דואל",
                "דואר-אלקטרוני"
            };
        }

        /*Description:
        */
        public string ClearEmail(string mail)
        {
            mail = mail.Replace("/", "");
            mail = mail.Replace("\\", "");
            mail = mail.Replace(".co", ".co.il");
            mail = mail.Replace("!", "");
            mail = mail.Replace("'", "");
            mail = mail.Replace("\"", "");
            mail = mail.Replace("?", "");
            mail = mail.Replace(".coil", ".co.il");
            mail = mail.Replace(".ilm", ".il");
            mail = mail.Replace("%", "");
            mail = mail.Replace("|", "");
            mail = mail.Replace("org.i", "org.il");
            mail = mail.Replace("con", "com");
            mail = mail.Replace(".co.ili", ".co.il");
            mail = mail.Replace(".njet", ".net");
            mail = mail.Replace(".net.i", ".net.il");
            mail = mail.Replace(".met", ".net");
            mail = mail.Replace(".co.oil", ".co.il");
            mail = mail.Replace(".ill", ".il");
            mail = mail.Replace(".co.i", ".co.il");
            mail = mail.Replace(".walla.c", ".walla.com");
            mail = mail.Replace(".com2", ".com");
            mail = mail.Replace("@.", "@");
            mail = mail.Replace(".co.ill", ".co.il");
            mail = mail.Replace(".walla.co", ".walla.co.il");
            mail = mail.Replace("gmail.comm", "gmail.com");
            mail = mail.Replace("gmail.com.il", "gmail.com");
            mail = mail.Replace(".org.ill", ".org.il");
            mail = mail.Replace(".gov.i", ".gov.il");
            mail = mail.Replace(".walla.cil", ".walla.co.il");
            mail = mail.Replace("gmail.co", "gmail.com");
            mail = mail.Replace("mailto%20", "");
            mail = mail.Replace("mailto:", "");
            mail = mail.Replace("mailto", "");
            mail = mail.Replace("%20", "");
            mail = mail.Replace(".muni.i", "muni.il");
            mail = mail.Replace("^", "");
            mail = mail.Replace(".netl", ".net");
            mail = mail.Replace(".co.il1", ".co.il");
            mail = mail.Replace(".comcom", ".com");
            mail = mail.Replace(".comm", ".com");
            mail = mail.Replace(".co.ill", ".co.il");
            mail = mail.Replace(".ill", ".il");
            mail = mail.Replace(".co.il.il", ".co.il");
            mail = mail.Replace(".co.il.l", ".co.il");
            mail = mail.Replace(".com.il", ".com");
            mail = mail.Replace(".comn", ".com");
            mail = mail.Replace(".co.il.i", ".co.il");
            mail = mail.Replace(".co.ilcom", ".com");
            mail = mail.Replace(".co.ill", ".co.il");
            if (mail.Contains('='))
            {
                mail = mail.Split('=')[1];
            }
            if (mail.Contains(".."))
            {
                mail = string.Join(".", mail.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries));
            }
            if (mail.Contains("@."))
            {
                mail = mail.Replace("@.", "@");
            }
            return mail;
        }

        /*Description:
        */
        public bool ValidateMail(string mail)
        {
            if (string.IsNullOrEmpty(mail))
            {
                return false;
            }

            if (!mail.Contains("@"))
            {
                return false;
            }

            if (mail.Contains(".jpg"))
            {
                return false;
            }

            if (mail.Contains(".png"))
            {
                return false;
            }

            string[] splitter = mail.Split('@');
            foreach (string m in splitter)
            {
                if (!string.IsNullOrEmpty(m))
                {
                    if (m.Trim().Length <= 2)
                    {
                        return false;
                    }
                }
            }

            try
            {
                mail = mail.Trim();
                MailAddress m = new MailAddress(mail);
                if (mail.Contains("-"))
                {
                    if (mail.Contains("@-"))
                    {
                        return false;
                    }

                    if (mail.Contains("-."))
                    {
                        int i = mail.IndexOf("-.");
                        int d = mail.IndexOf("@");
                        if (i > d)
                        {
                            return false;
                        }
                    }
                }

                if (mail.Contains(".."))
                {
                    return false;
                }
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }
    }
}