using System.Data;
using System.Data.SqlClient;

namespace CVSpider.Code
{ 
    public class DAL
    {
        internal static void CreateEmail(string email)
        {
            using (SqlConnection con = DbUtilsDal.OpenConnection(DbUtilsDal.MainDB))
            {
                DbUtilsDal.ExecuteNonQuery(con, "dbo.CreateEmail",
                           new string[] { "@Email" },
                           new SqlDbType[] { SqlDbType.VarChar },
                           new object[] { email });
            }
        }

        internal static EmailRow GetEmail(string email)
        {
            DataRow row = null;
            using (SqlConnection con = DbUtilsDal.OpenConnection(DbUtilsDal.MainDB))
            {
                DbUtilsDal.ExecuteNonQuery(con, "dbo.GetEmail",
                           new string[] { "@Email" },
                           new SqlDbType[] { SqlDbType.VarChar },
                           new object[] { email });
            }
            return new EmailRow(row);
        }
    }
}