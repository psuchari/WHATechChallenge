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
        List<Int32> RiskyCustomers = new List<Int32>();

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
            GridViewSettledBets.DataSource = SummariseSettledBets(dataTable); ;
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
                                    datatableCsv.Columns.Add(rowValues[j].Trim(), typeof(Int32)); 
                                }
                            }
                            else // data rows
                            {
                                DataRow dr = datatableCsv.NewRow();
                                for (int k = 0; k < rowValues.Count(); k++)
                                {
                                    dr[k] = int.Parse(rowValues[k]);
                                }
                                datatableCsv.Rows.Add(dr);
                            }                            
                        }
                    }
                }
            }
            return datatableCsv;
        }

        private DataTable SummariseSettledBets(DataTable settledBets)
        {
            DataTable dataTable = new DataTable();

            DataColumn column;
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "Customer";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(Double);
            column.ColumnName = "AverageStake";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "BetCount";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "WinCount";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(Double);
            column.ColumnName = "WinPercentage";
            dataTable.Columns.Add(column);

            return settledBets.AsEnumerable()
                .GroupBy(r => r.Field<int>("Customer"))
                .Select(g => 
                {
                    DataRow row = dataTable.NewRow();
                    row["Customer"] = g.Key;
                    row["AverageStake"] = g.Average(x => x.Field<int>("Stake"));
                    row["BetCount"] = g.Count();
                    row["WinCount"] = g.Count(x => x.Field<int>("Win") > 0);
                    row["WinPercentage"] = (double)row.Field<Int32>("WinCount") / row.Field<Int32>("BetCount") * 100;

                    return row;
                }).CopyToDataTable();

        }

        protected void GridViewSettledBets_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "WinPercentage")) > 60)
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                    RiskyCustomers.Add(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Customer")));
                }
            }
        }

        protected void GridViewUnsettledBets_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Int32 customer = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Customer"));
                if (IsCustomerRisky(customer))
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }

                Int32 stake = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Stake"));
                Double averageStake = GetCustomerAverageStake(customer);
                if (IsStakeHighlyUnusual(stake, averageStake))
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }
                else if (IsStakeUnusual(stake, averageStake))
                {
                    e.Row.ForeColor = System.Drawing.Color.Orange;
                }
            }
        }

        private bool IsCustomerRisky(Int32 customer)
        {
            return (RiskyCustomers.Count > 0 && RiskyCustomers.Exists(x => x == customer));
        }

        private Double GetCustomerAverageStake(Int32 customer)
        {
            return ((DataTable)GridViewSettledBets.DataSource).AsEnumerable()
                .Where<DataRow>(r => r.Field<Int32>("Customer") == customer)
                .Select(r => r.Field<Double>("AverageStake")).SingleOrDefault();
        }

        private bool IsStakeUnusual(Int32 stake, Double averageStake)
        {
            return (stake > averageStake * 10);
        }

        private bool IsStakeHighlyUnusual(Int32 stake, Double averageStake)
        {
            return (stake > averageStake * 30);
        }
    }
}