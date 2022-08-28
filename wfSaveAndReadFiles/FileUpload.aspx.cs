using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfSaveAndReadFiles
{
    public partial class FileUpload : System.Web.UI.Page
    {
        public BindingFlags BindFlags { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                //default path
                string path = MapPath("~/FilesReceived/");

                //check if the file has been added
                if (FileUpload1.HasFile)
                {
                    //get fileName
                    string fileName = FileUpload1.FileName;
                    string extension = Path.GetExtension(fileName);

                    //changing label text
                    lbChooseFile.Text = "No file selected.";

                    //Check whether Directory is available or not
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    //save the file to folder
                    FileUpload1.SaveAs(path+fileName);

                    lbMessage.Text = "File Uploaded Successfully.";
                    lbMessage.CssClass = "alert alert-primary";
                    lbMessage.Visible = true;

                    if (extension != ".xls" && extension != ".xlsx")
                    {
                        throw new Exception("The file is not excel type.");
                    }

                    //Read Excel File.
                    ReadExcelFile(path + fileName);
                } else
                {
                    lbMessage.Text = "No files added.";
                    lbMessage.CssClass = "alert alert-danger";
                    lbMessage.Visible = true;
                }
            }
            catch (Exception msg)
            {
                lbMessage.Text = msg.Message.ToString();
                lbMessage.CssClass = "alert alert-danger";
                lbMessage.Visible = true;
            }
        }

        protected void ReadExcelFile(string path)
        {
            //fetching ConnectionString from excel connection from web.config
            string Connection = ConfigurationManager.ConnectionStrings["excelconnection"].ConnectionString;
            Connection = String.Format(Connection, path);

            //instantiating connection
            OleDbConnection excelConnection = new OleDbConnection(Connection);

            try
            {
                //starting connection to excel
                excelConnection.Open();

                //getting the name of the worksheet tab dynamically
                DataTable exceldta = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string excelSheetname = exceldta.Rows[0]["TABLE_NAME"].ToString();

                //command that will fetch the data from the table
                OleDbCommand selectCommand = new OleDbCommand("Select * from [" + excelSheetname + "]", excelConnection);

                //reading the excel files and generating a data table.
                OleDbDataAdapter da = new OleDbDataAdapter(selectCommand);
                DataTable dt = new DataTable();
                //adding excel information in DataTable
                da.Fill(dt);

                //converting the DataTable to a Table (Titles)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TableRow line = new TableRow();

                    //assembling the table header
                    if ( i == 0)
                    {
                        TableHeaderRow lineTitle = new TableHeaderRow();

                        for (int xx = 0; xx < dt.Columns.Count; xx++)
                        {
                            TableHeaderCell columnTitle = new TableHeaderCell();

                            columnTitle.Text = dt.Columns[xx].ColumnName;

                            lineTitle.Cells.Add(columnTitle);
                        }

                        lineTitle.CssClass = "table-dark";
                        //table asp.net
                        Table1.Rows.Add(lineTitle);
                    }

                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        TableCell column = new TableCell();

                        column.Text = dt.Rows[i][c].ToString();

                        line.Cells.Add(column);
                    }

                    //table asp.net
                    Table1.Rows.Add(line);
                }


            }

            catch (Exception ex)
            {
                lbMessage.Text = ex.Message.ToString();
            }
            finally
            {
                //closing connection
                excelConnection.Close();
            }
        }

        protected void hlChooseFile_DataBinding(object sender, EventArgs e)
        {
        }
    }
}