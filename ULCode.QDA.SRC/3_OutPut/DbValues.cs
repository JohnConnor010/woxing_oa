namespace ULCode.QDA
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class DbValues
    {
        public List<DbValue> oValues;

        public DbValues(object[] oArr)
        {
            this.oValues = null;
            this.oValues = new List<DbValue>();
            for (int i = 0; i < oArr.Length; i++)
            {
                this.oValues.Add(new DbValue(oArr[i]));
            }
        }

        public DbValues(List<object> oArr)
        {
            this.oValues = null;
            this.oValues = new List<DbValue>();
            for (int i = 0; i < oArr.Count; i++)
            {
                this.oValues.Add(new DbValue(oArr[i]));
            }
        }
        public DbValues(DataTable dt)
        {
            this.oValues = null;
            this.oValues = new List<DbValue>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        this.oValues.Add(new DbValue(dt.Rows[i][j]));
                    }
                }
            }
        }

        public string Join(string sSplitter)
        {
            string sList = string.Empty;
            for (int i = 0; i < this.oValues.Count; i++)
            {
                sList = sList + ((i == 0) ? string.Empty : sSplitter) + Convert.ToString(this.oValues[i].ToStr());
            }
            return sList;
        }

        public DbValue[] ToArray()
        {
            return this.oValues.ToArray();
        }

        public List<DbValue> ToListObject()
        {
            return this.oValues;
        }
    }
}

