using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace TESTINGFACT
{
    public partial class Ventanas : System.Web.UI.Page
    {
        //SqlConnection con = new SqlConnection("Data Source=OLVERA\\SQLEXPRESS;Initial Catalog=bdqmanager;Integrated Security=False; User Id=test; Password=cuatri12");

        //SqlDataAdapter ad = new SqlDataAdapter();
        //SqlDataAdapter adr = new SqlDataAdapter();
        //DataTable dt = new DataTable();

        int interup = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetInitialRow();
            }
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string)));
            dt.Columns.Add(new DataColumn("Column5", typeof(string)));
            dt.Columns.Add(new DataColumn("Column6", typeof(string)));
            dt.Columns.Add(new DataColumn("Column7", typeof(string)));
            dt.Columns.Add(new DataColumn("Column8", typeof(string)));
            dt.Columns.Add(new DataColumn("Column9", typeof(string)));
            dt.Columns.Add(new DataColumn("Column10", typeof(string)));

            dr = dt.NewRow();

            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dr["Column4"] = string.Empty;
            dr["Column5"] = string.Empty;
            dr["Column6"] = string.Empty;
            dr["Column7"] = string.Empty;
            dr["Column8"] = string.Empty;
            dr["Column9"] = string.Empty;
            dr["Column10"] = string.Empty;

            dt.Rows.Add(dr);
            //dr = dt.NewRow();

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            Gridview1.DataSource = dt;
            Gridview1.DataBind();
        }
        private void AddNewRowToGrid()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[0].FindControl("TextBox1");
                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox2");
                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox3");
                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox4");
                        TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox5");
                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("TextBox6");
                        TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("TextBox7");
                        TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("TextBox8");
                        TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("TextBox9");
                        TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("TextBox10");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Column1"] = box0.Text;
                        dtCurrentTable.Rows[i - 1]["Column2"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["Column3"] = box2.Text;
                        dtCurrentTable.Rows[i - 1]["Column4"] = box3.Text;
                        dtCurrentTable.Rows[i - 1]["Column5"] = box4.Text;
                        dtCurrentTable.Rows[i - 1]["Column6"] = box5.Text;
                        dtCurrentTable.Rows[i - 1]["Column7"] = box6.Text;
                        dtCurrentTable.Rows[i - 1]["Column8"] = box7.Text;
                        dtCurrentTable.Rows[i - 1]["Column9"] = box8.Text;
                        dtCurrentTable.Rows[i - 1]["Column10"] = box9.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    Gridview1.DataSource = dtCurrentTable;
                    Gridview1.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }
        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[0].FindControl("TextBox1");
                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox2");
                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox3");
                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox4");
                        TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox5");
                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("TextBox6");
                        TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("TextBox7");
                        TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("TextBox8");
                        TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("TextBox9");
                        TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("TextBox10");

                        box0.Text = dt.Rows[i]["Column1"].ToString();
                        box1.Text = dt.Rows[i]["Column2"].ToString();
                        box2.Text = dt.Rows[i]["Column3"].ToString();
                        box3.Text = dt.Rows[i]["Column4"].ToString();
                        box4.Text = dt.Rows[i]["Column5"].ToString();
                        box5.Text = dt.Rows[i]["Column6"].ToString();
                        box6.Text = dt.Rows[i]["Column7"].ToString();
                        box7.Text = dt.Rows[i]["Column8"].ToString();
                        box8.Text = dt.Rows[i]["Column9"].ToString();
                        box9.Text = dt.Rows[i]["Column10"].ToString();

                        rowIndex++;
                    }
                }
            }
        }
        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            string sg1 = "";
            string sg2 = "";
            string sg3 = "";
            string sg4 = "";
            string sg5 = "";
            string sg6 = "";
            string sg7 = "";
            string sg8 = "";
            string sg9 = "";
            string sg10 = "";

            for (int w = 0; w < Gridview1.Rows.Count; w++)
            {
                sg1 = ((TextBox)Gridview1.Rows[w].FindControl("TextBox1")).Text.Trim();
                sg2 = ((TextBox)Gridview1.Rows[w].FindControl("TextBox2")).Text.Trim();
                sg3 = ((TextBox)Gridview1.Rows[w].FindControl("TextBox3")).Text.Trim();
                sg4 = ((TextBox)Gridview1.Rows[w].FindControl("TextBox4")).Text.Trim();
                sg5 = ((TextBox)Gridview1.Rows[w].FindControl("TextBox5")).Text.Trim();
                sg6 = ((TextBox)Gridview1.Rows[w].FindControl("TextBox6")).Text.Trim();
                sg7 = ((TextBox)Gridview1.Rows[w].FindControl("TextBox7")).Text.Trim();
                sg8 = ((TextBox)Gridview1.Rows[w].FindControl("TextBox8")).Text.Trim();
                sg9 = ((TextBox)Gridview1.Rows[w].FindControl("TextBox9")).Text.Trim();
                sg10 = ((TextBox)Gridview1.Rows[w].FindControl("TextBox10")).Text.Trim();

                ((TextBox)Gridview1.Rows[w].FindControl("TextBox1")).Text = "";

                ((TextBox)Gridview1.Rows[w].FindControl("TextBox2")).Text = "";

                ((TextBox)Gridview1.Rows[w].FindControl("TextBox3")).Text = "";
                ((TextBox)Gridview1.Rows[w].FindControl("TextBox4")).Text = "";
                ((TextBox)Gridview1.Rows[w].FindControl("TextBox5")).Text = "";
                ((TextBox)Gridview1.Rows[w].FindControl("TextBox6")).Text = "";
                ((TextBox)Gridview1.Rows[w].FindControl("TextBox7")).Text = "";
                ((TextBox)Gridview1.Rows[w].FindControl("TextBox8")).Text = "";
                ((TextBox)Gridview1.Rows[w].FindControl("TextBox9")).Text = "";
                ((TextBox)Gridview1.Rows[w].FindControl("TextBox10")).Text = "";

                sg1 = "";
                sg2 = "";
                sg3 = "";
                sg4 = "";
                sg5 = "";
                sg6 = "";
                sg7 = "";
                sg8 = "";
                sg9 = "";
                sg10 = "";
            }

        }
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        protected void ImageButton18_Click(object sender, ImageClickEventArgs e)
        {
            /// INSERTAR EN LA TABLA /////
            /*ad.InsertCommand = new SqlCommand("insert into InvMats values(@CODIGO,@CANTIDAD,@CLASE,@DESCRIPCION,@UNIDAD,@PRECIO,@MINIMO,@MAXIMO,@ANAQUEL,@IVA,@ISH,@IEPS,@APLICA)", con);
              ad.InsertCommand.Parameters.Add("@CODIGO", SqlDbType.VarChar).Value = txtcodigo.Text;
              ad.InsertCommand.Parameters.Add("@CANTIDAD", SqlDbType.VarChar).Value = "0";
              ad.InsertCommand.Parameters.Add("@CLASE", SqlDbType.VarChar).Value = txtclass.Text;
              ad.InsertCommand.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar).Value = txtdescripcion.Text;
              ad.InsertCommand.Parameters.Add("@UNIDAD", SqlDbType.VarChar).Value = txtunidad.Text;
              ad.InsertCommand.Parameters.Add("@PRECIO", SqlDbType.Decimal).Value = Convert.ToDecimal(txtprecio.Text);
              ad.InsertCommand.Parameters.Add("@MINIMO", SqlDbType.Decimal).Value = Convert.ToDecimal(txtmin.Text);
              ad.InsertCommand.Parameters.Add("@MAXIMO", SqlDbType.Decimal).Value = Convert.ToDecimal(txtmax.Text);
              ad.InsertCommand.Parameters.Add("@ANAQUEL", SqlDbType.VarChar).Value = txtanaquel.Text;
              ad.InsertCommand.Parameters.Add("@IVA", SqlDbType.VarChar).Value = rbiva.Text;
              ad.InsertCommand.Parameters.Add("@ISH", SqlDbType.VarChar).Value = rbish.Text;
              ad.InsertCommand.Parameters.Add("@IEPS", SqlDbType.VarChar).Value = txttest2.Text;
              ad.InsertCommand.Parameters.Add("@APLICA", SqlDbType.VarChar).Value = mystring;

              con.Open();
              ad.InsertCommand.ExecuteNonQuery();*/

            /// VER TABLA /////////
            /*
            ad.SelectCommand = new SqlCommand($"select * from {txtver.Text}", con);

            dt.Columns.Clear();
            dt.Clear();
            ad.Fill(dt);

            dataGridView1.DataSource = dt;
            con.Open();
            ad.SelectCommand.ExecuteNonQuery();
            con.Close();
            */

            /// CREAR TABLA /////////
            /*
            string createString = "CREATE TABLE InvMats (CODIGO NVARCHAR(25), CANTIDAD NVARCHAR(3), CLASE NVARCHAR(30), DESCRIPCION NVARCHAR(200), UNIDAD NVARCHAR(15), PRECIO DECIMAL(10,2), MINIMO DECIMAL(10,2), MAXIMO DECIMAL(10,2), ANAQUEL VARCHAR(25), IVA VARCHAR(5), ISH VARCHAR(5), IEPS VARCHAR(6), APLICA VARCHAR(30))"; //YOUR SQL COMMAND TO CREATE A TABLE

            SqlCommand create = new SqlCommand(createString, con);

            con.Open();
            create.ExecuteNonQuery();
            con.Close();
            */

            /// UPDATE TABLA ////////
            /*
              ad.UpdateCommand = new SqlCommand($"UPDATE InvMats SET CLASE = '" + txtclass.Text + "', DESCRIPCION = '" + txtdescripcion.Text + "', UNIDAD = '" + txtunidad .Text + "', PRECIO = " + Convert.ToDecimal(txtprecio.Text) + ", MINIMO = " + Convert.ToDecimal(txtmin.Text) + ", MAXIMO = " + Convert.ToDecimal(txtmax.Text) + ", ANAQUEL = '" + txtanaquel.Text + "', IVA = '" + rbiva.Text + "', ISH = '" + rbish.Text + "', IEPS = '" + txttest2.Text + "', APLICA='"+mystring+"' Where (CODIGO = '" + txtcodigo.Text + "')", con);
              con.Open();
              ad.UpdateCommand.ExecuteNonQuery();
              con.Close();
            */

            /// PASAR ROWS DE GRIDVIEW7 A GRIDVIEW1
            /*
              int icountgrid = Gridview1.Rows.Count;

                    for (int i = 0; i < icountgrid; i++)
                    {
                        if (String.IsNullOrWhiteSpace(((TextBox)Gridview1.Rows[i].FindControl("TextBox1")).Text) && interup == 0)
                        {
                            int index = Convert.ToInt32(GridView9.SelectedIndex);
                            string scod = System.Net.WebUtility.HtmlDecode(GridView9.Rows[index].Cells[1].Text);

                            string scant = System.Net.WebUtility.HtmlDecode(GridView9.Rows[index].Cells[2].Text);
                            string sunidad = System.Net.WebUtility.HtmlDecode(GridView9.Rows[index].Cells[3].Text);
                            string sdesc = System.Net.WebUtility.HtmlDecode(GridView9.Rows[index].Cells[4].Text);
                            string sprecio = System.Net.WebUtility.HtmlDecode(GridView9.Rows[index].Cells[5].Text);
                            string siva = System.Net.WebUtility.HtmlDecode(GridView9.Rows[index].Cells[6].Text);
                            string sish = System.Net.WebUtility.HtmlDecode(GridView9.Rows[index].Cells[7].Text);
                            string sieps = System.Net.WebUtility.HtmlDecode(GridView9.Rows[index].Cells[8].Text);

                            ((TextBox)Gridview1.Rows[i].FindControl("TextBox1")).Text = lbreq.Text;
                            ((TextBox)Gridview1.Rows[i].FindControl("TextBox2")).Text = scod;
                            ((TextBox)Gridview1.Rows[i].FindControl("TextBox3")).Text = scant;
                            ((TextBox)Gridview1.Rows[i].FindControl("TextBox4")).Text = scant;
                            ((TextBox)Gridview1.Rows[i].FindControl("TextBox5")).Text = sunidad;
                            ((TextBox)Gridview1.Rows[i].FindControl("TextBox6")).Text = sdesc;
                            ((TextBox)Gridview1.Rows[i].FindControl("TextBox7")).Text = sprecio;
                            ((TextBox)Gridview1.Rows[i].FindControl("TextBox9")).Text = siva;
                            ((TextBox)Gridview1.Rows[i].FindControl("TextBox10")).Text = sish;
                            ((TextBox)Gridview1.Rows[i].FindControl("TextBox11")).Text = sieps;

                            interup = 1;

                            if ((Gridview1.Rows.Count - i) == 1)
                            {
                                AddNewRowToGrid();
                            }
                        }
                    }

                    interup = 0;
            */
        }

        protected void ImageButton14_Click(object sender, ImageClickEventArgs e)
{

}

protected void ImageButton20_Click(object sender, ImageClickEventArgs e)
{

}

protected void GridView7_SelectedIndexChanged(object sender, EventArgs e)
{

}
}
}
 