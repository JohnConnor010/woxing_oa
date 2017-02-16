namespace ULCode
{
    using System;
    using System.Data;
    using System.IO;
    using System.Text;
    /// <summary>
    /// TextFile是一个IO类。
    /// 简化IO过程。
    /// 接口有：
    /// 1.Save,直接保存文本内容.
    /// 2.Text,直接获取安全字符串.
    /// 3.ToDataSet与ToDataTable,输出成数据源可用的数据格式
    /// 4.Write直接写到控件
    /// </summary>
    public class TextFile
    {
        public string FileName;
        public string EncodingName="GB2312";

        public TextFile(string fileName)
        {
            this.FileName = fileName;
        }
        public TextFile(string fileName, string encodingName)
        {
            this.FileName = fileName;
            this.EncodingName = encodingName;
        }
        public bool Save(string sText) { return Save(sText, false); }
        public bool Save(string sText,bool append)
        {
            bool result = false;
            StreamWriter sw = null;
            Encoding code = Encoding.GetEncoding(EncodingName);            
            try
            {
                bool blnExists = File.Exists(this.FileName);
                if (!blnExists)
                {
                    sw = File.CreateText(this.FileName);
                }
                else
                {
                    sw = new StreamWriter(this.FileName, append, code);
                }
                sw.Write(sText);
                sw.Flush();
                sw.Close();
                sw.Dispose();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
                sw = null;
            }
                return result;
        }

        public string Text()
        {
            StreamReader sr = null;
            Encoding code = Encoding.GetEncoding(EncodingName);
            string str = null;
            if (!File.Exists(this.FileName))
                return null;
            try
            {
                sr = new StreamReader(this.FileName, code);
                str = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
            return str;
        }
        public string[] GetLines()
        {
            String s = Text();
            if (String.IsNullOrEmpty(s))
                return null;
            else
                return Text().Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
        public DataSet ToDataSet()
        {
            DataTable dt = this.ToDataTable();
            if (dt == null)
            {
                return null;
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }

        public DataTable ToDataTable()
        {
            string sText = this.Text();
            if (sText == null)
            {
                return null;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Text");
            dt.Rows.Add(new object[] { sText });
            return dt;
        }
    }
}

