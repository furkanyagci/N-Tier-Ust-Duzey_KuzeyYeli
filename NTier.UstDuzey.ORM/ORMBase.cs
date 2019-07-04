using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NTier.UstDuzey.ORM
{
    public class ORMBase<TT> : IORM<TT>
    {
        Type TipGetir
        {
            get
            {
                return typeof(TT);
            }
        }

        public DataTable Listele()
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Tools.Baglanti;
            cmd.CommandText = string.Format("prc_{0}_Select", TipGetir.Name);
            cmd.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }

        public bool Ekle(TT entity)
        {
            return Tools.InsertUpdate<TT>("Insert", entity);
        }

        public bool Guncelle(TT entity)
        {
            return Tools.InsertUpdate<TT>("Update", entity);
        }

        public bool Sil(int id)
        {
            TT ent = Activator.CreateInstance<TT>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = string.Format("prc_{0}_Delete",TipGetir.Name);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Tools.Baglanti;

            PropertyInfo primaryColumn = TipGetir.GetProperty("PrimaryColumn"); 

            string prmAdi = "@"+ primaryColumn.GetValue(ent);

            cmd.Parameters.AddWithValue(prmAdi, id);

            return Tools.ExecuteNonQuery(cmd);
        }
    }
}
