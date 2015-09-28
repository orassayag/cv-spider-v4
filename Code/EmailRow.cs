using System.Data;

namespace CVSpider.Code
{
    public class EmailRow
    {
        public long Id { get; set; }
        public string Email { get; set; }

        public EmailRow() { }

        public EmailRow(DataRow row)
        {
            if (row != null)
            {
                if (row["Id"] != null)
                {
                    if (!string.IsNullOrEmpty(row["id"].ToString()))
                    {
                        this.Id = (long)row["id"];
                    }
                }

                if (row["Email"] != null)
                {
                    if (!string.IsNullOrEmpty(row["Email"].ToString()))
                    {
                        Email = row["Email"].ToString();
                    }
                }
            }
        }
    }
}