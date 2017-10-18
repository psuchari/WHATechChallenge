using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AnalyseBets_Click(object sender, System.EventArgs e)
        {
            try
            {
                LoadCsvFiles();
            }
            catch (Exception ex)
            {
                LabelError.Text = ex.Message;
            }
        }

        private void LoadCsvFiles()
        {
            DataTable dataTable = new DataTable();
            dataTable = ReadCsvFile(SettledBetsFile);
            GridViewSettledBets.DataSource = dataTable;
            GridViewSettledBets.DataBind();
            dataTable = ReadCsvFile(UnsettledBetsFile);
            GridViewUnsettledBets.DataSource = dataTable;
            GridViewUnsettledBets.DataBind();
        }

        private DataTable ReadCsvFile(System.Web.UI.HtmlControls.HtmlInputFile inputFile)
        {

            DataTable datatableCsv = new DataTable();
            string fileAsText;
            if (inputFile.PostedFile != null & inputFile.PostedFile.ContentLength > 0)
            {
                using (StreamReader streamReader = new StreamReader(inputFile.PostedFile.InputStream))
                {
                    while (!streamReader.EndOfStream)
                    {
                        fileAsText = streamReader.ReadToEnd().ToString(); 
                        string[] rows = fileAsText.Split('\n'); 
                        for (int i = 0; i < rows.Count() - 1; i++)
                        {
                            string[] rowValues = rows[i].Split(','); 
                            
                            if (i == 0) // headers
                            {
                                for (int j = 0; j < rowValues.Count(); j++)
                                {
                                    datatableCsv.Columns.Add(rowValues[j]); 
                                }
                            }
                            else // data rows
                            {
                                DataRow dr = datatableCsv.NewRow();
                                for (int k = 0; k < rowValues.Count(); k++)
                                {
                                    dr[k] = rowValues[k].ToString();
                                }
                                datatableCsv.Rows.Add(dr);
                            }                            
                        }
                    }
                }
            }
            return datatableCsv;
        }


    }
}