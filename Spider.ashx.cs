using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CVSpider.Code;

namespace CVSpider
{
    public class Spider : IHttpHandler
    {
        /*Description:
        */
        public void ProcessRequest(HttpContext context)
        {
            string actionType = "search";
            int printFromYear = 2015;
            int printFromMonth = 9;
            int printFromDay = 28;
            int printFromHour = 22;
            int printFromMinute = 0;
            int printFromSeconds = 0;
            string mainPath = @"C:\Or\Web\CVSpider\CVSpider\CVSpider\CVSpider\Logs\";

            switch (actionType)
            {
                case "search":
                    SearchMails();
                    break;
                case "print":
                    string logPath = Path.Combine(mainPath, DateTime.Now.ToString("dd_MM_yyyy"), ".txt");
                    break;
            }
        }

        /*Description:
        */
        public void SearchMails()
        {
            List<string> urls = null;
            string city = Cities.GetRandomCity();
            string profession = Professions.GetRandomProfession();
            string mailType = MailTypes.GetRandomMailType();
            string querySearch = string.Format($"דרוש/ה+{profession}+ב{city}+{mailType}");

            for (int i = 10; i > 1; i--)
            {
                string query = string.Format("http://search.walla.co.il/?q={0}&type=text&page={1}", querySearch, i);
                string pageSource = TextUtils.GetPageSource(query);
                urls = TextUtils.GetUrls(pageSource);
            }

            foreach (string u in urls)
            {
                if (!string.IsNullOrEmpty(u))
                {
                    GetMails(u);
                }
            }
        }

        /*Description:
        */
        private void PrintMails()
        {
            using (FileStream fs = new FileStream(@"C:\Or\Web\CV\mails1.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    List<string> f = BLL.GetCVMails(id);
                    foreach (var m in f)
                    {
                        writer.Write(m + ", ");
                    }
                }
            }
        }

        /*Description:
        */
        private void GetMails(string source)
        {
            string htmlMatch = TextUtils.GetPageSource(source);
            if (string.IsNullOrEmpty(htmlMatch))
            {
                return;
            }

            Regex t = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            foreach (Match match in t.Matches(htmlMatch))
            {
                if (TextUtils.ValidateMail(match.Value))
                {
                    string email = match.Value.Trim().ToLower();
                    EmailRow emailRow = BLL.GetEmail(email);
                    if (emailRow == null)
                    {
                        CreateEmail(email);
                    }
                }
            }
        }

        private 

        /*Description:
        */
        private void CreateEmail(string email)
        {
            int maxRetries = 10;
            int retriesCount = 0;
            bool success = false;
            while (!success && retriesCount < maxRetries)
            {
                try
                {
                    retriesCount++;
                    BLL.CreateEmail(email);
                    success = true;
                }
                catch (Exception) { }
            }
        }

        /*Description:
        */
        public bool IsReusable { get { return false; } }
    }
}