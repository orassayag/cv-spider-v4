namespace CVSpider.Code
{
    public class BLL
    {
        public static void CreateEmail(string email)
        {
            DAL.CreateEmail(email);
        }

        public static EmailRow GetEmail(string email)
        {
            return DAL.GetEmail(email);
        }
    }
}