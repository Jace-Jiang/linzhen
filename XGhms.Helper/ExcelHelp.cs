using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace XGhms.Helper
{
    public abstract class ExcelHelp
    {
        #region 读取excel文件，返回dataset

        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static DataSet importExcelToDataSet(string FilePath)
        {
            string strConn;

            //当 IMEX=0 时为“汇出模式”，这个模式开启的 Excel 档案只能用来做“写入”用途。
            //当 IMEX=1 时为“汇入模式”，这个模式开启的 Excel 档案只能用来做“读取”用途。
            //当 IMEX=2 时为“连結模式”，这个模式开启的 Excel 档案可同时支援“读取”与“写入”用途。
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + FilePath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";  //HDR=Yes，这代表第一行是标题，不做为数据使用
            string strConn2007 = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + FilePath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";  //HDR=Yes，这代表第一行是标题，不做为数据使用

            OleDbConnection conn = new OleDbConnection(strConn2007);
            OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn2007);
            DataSet myDataSet = new DataSet();
            try
            {
                myCommand.Fill(myDataSet);
            }
            catch
            {
                try
                {
                    conn = new OleDbConnection(strConn);
                    myCommand = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn);
                    myCommand.Fill(myDataSet);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            return myDataSet;
        }
        #endregion
    }
}
