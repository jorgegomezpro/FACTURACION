using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TESTINGFACT.CLASES;

namespace TESTINGFACT.TRANSACCIONES
{
    public class CONEXION
    {
        private SqlConnection cnn;
        private bool Conectar()
        {
            try
            {
                CONFIGURACIONES c = new CONFIGURACIONES();
                cnn = new SqlConnection(c.CadenaDeConexionPrincipal);
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
    }
}