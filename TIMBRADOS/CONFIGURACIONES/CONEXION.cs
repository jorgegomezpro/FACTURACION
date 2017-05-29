using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIMBRADOS.CONFIGURACIONES
{
    public class CONEXION
    {
        private SqlConnection cnn;
        private bool Conectar()
        {
            try
            {
                string cadenadeconexion = System.Configuration.ConfigurationManager.ConnectionStrings["GestionDetimbrados"].ConnectionString;
                //System.Configuration.ConfigurationManager.AppSettings["GestionDetimbrados"];
                cnn = new SqlConnection(cadenadeconexion);
                cnn.Open();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public Nullable<int> EjecutarSentencia(string ConsultaSQL)
        {
            int res = 0;
            if (Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(ConsultaSQL, cnn);
                    res = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    return null;
                }
                cnn.Close();
            }
            return res;
        }
        public object EjecutarEscalar(string ConsultaSQL)
        {
            object res = null;
            if (Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(ConsultaSQL, cnn);
                    res = cmd.ExecuteScalar();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                }
                cnn.Close();
                return res;
            }
            return null;
        }
        public DataTable EjecutarConsulta(string ConsultaSQL)
        {
            DataTable res = null;
            if (Conectar())
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(ConsultaSQL, cnn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    res = ds.Tables[0];
                    da.Dispose();
                }
                catch (Exception ex)
                {
                }
                cnn.Close();
                return res;
            }
            return res;
        }
        public DataTable EjecutarConsulta(string StoredProcedure, Dictionary<string, object> Parameters)
        {
            DataTable res = null;
            if (Conectar())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedure, cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (var item in Parameters)
                            cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        res = ds.Tables[0];
                        da.Dispose();
                    }
                }
                catch (Exception ex)
                {
                }
                cnn.Close();
                return res;
            }
            return res;
        }
    }
}
