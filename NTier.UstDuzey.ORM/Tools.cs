using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NTier.UstDuzey.ORM
{
    public class Tools
    {
        private static SqlConnection baglanti;

        public static SqlConnection Baglanti
        {
            get
            {
                if (baglanti == null)
                {
                    baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalBaglanti"].ConnectionString);
                }
                return baglanti;
            }
            set { baglanti = value; }
        }

        public static bool ExecuteNonQuery(SqlCommand cmd)
        {
            try
            {
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                int etk = cmd.ExecuteNonQuery();
                return etk > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
        }

        public static bool InsertUpdate<T>(string procType, T entity)
        {
            Type TipGetir = typeof(T);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Tools.Baglanti;
            cmd.CommandText = string.Format("prc_{0}_{1}", TipGetir.Name, procType);
            cmd.CommandType = CommandType.StoredProcedure;
            
            PropertyInfo[] propertys = TipGetir.GetProperties();

            foreach (PropertyInfo pi in propertys)
            {
                string prmAdi = "@" + pi.Name;
                object deger = pi.GetValue(entity);
                cmd.Parameters.AddWithValue(prmAdi, deger);
            }

            return Tools.ExecuteNonQuery(cmd);
        }
    }
}
