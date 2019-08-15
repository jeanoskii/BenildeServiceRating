using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ThreeTier
{
    namespace DataAccess
    {
        static class DAL
        {
            public static string ConnectionString = "SERVER=ARCIA-PC\\ARCIA;DATABASE=BenildeServiceRating;UID=sa;PWD=benilde";
            public static DataTable GetData(string sql)
            {
                SqlConnection con = new SqlConnection(DAL.ConnectionString);
                con.Open();
                SqlCommand com = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return dt;
            }

            public static void Execute(string sql)
            {
                SqlConnection con = new SqlConnection(DAL.ConnectionString);
                con.Open();
                SqlCommand com = new SqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
