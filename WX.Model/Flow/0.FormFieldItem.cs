using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Flow
{
    public enum FormFieldDataType
    {
        Item = 1,
        Data = 2
    }
    public class FormField
    {
        private const string ItemSplitter = "{$|}";
        public string Id;
        public string Text; 
        public string Value;
        public string Type;
        public FormField()
        {
            ;
        }
        public FormField(string id, string text)
        {
            this.Id = id;
            this.Text = text;
        }
        public FormField(string data, FormFieldDataType dataType)
        {
            if (String.IsNullOrEmpty(data)) return;
            string[] datas = data.Split(new String[] { ItemSplitter }, StringSplitOptions.None);
            if (datas.Length != 3) return;
            if (dataType == FormFieldDataType.Data)
            {
                this.Id = datas[0];
                this.Text = datas[1];
                this.Value = datas[2];
            }
            else //if(dataType== FormFieldDataType.Item)
            {
                this.Id = datas[0];
                this.Text = datas[1];
                this.Type = datas[2];
            }
        }
        public string GetSavedData(FormFieldDataType dataType)
        {
            if (dataType == FormFieldDataType.Data)
                return String.Format("{1}{0}{2}{0}{3}", ItemSplitter, this.Id, this.Text, this.Value);
            else //if(dataType== FormFieldDataType.Item)
                return String.Format("{1}{0}{2}{0}{3}", ItemSplitter, this.Id, this.Text, this.Type);
        }
    }
    public class FormFieldCollection:IEnumerable
    {
        private const string GroupSplitter = "{$||}";
        private List<FormField> _FORM_FIELDS = new List<FormField>();
        
        public FormFieldCollection()
        {
            ;
        }
        public FormFieldCollection(String data, FormFieldDataType dataType)
        {
            if (String.IsNullOrEmpty(data)) return;
            string[] datas = data.Split(new String[] { GroupSplitter }, StringSplitOptions.None);
            foreach (String s in datas)
            {
                this._FORM_FIELDS.Add(new FormField(s, dataType));
            }
        }
        public int GetFormFieldCount()
        {
            return this._FORM_FIELDS.Count;
        }
        public FormField this[int index]
        {
            get { return this._FORM_FIELDS[index]; }
        }
        public FormField this[string id]
        {
            get
            {
                return this._FORM_FIELDS.Find(delegate(FormField f_dele) { return f_dele.Id == id; });
            }
        }
        public FormFieldCollection GetCommonFields()
        {
            FormFieldCollection ffc = new FormFieldCollection();
            foreach (FormField ff in this._FORM_FIELDS) 
            {
                if (String.IsNullOrEmpty(ff.Type))
                    ffc.Add(ff);
            }
            return ffc;
        }
        public FormFieldCollection GetSysFields()
        {
            FormFieldCollection ffc = new FormFieldCollection();
            foreach (FormField ff in this._FORM_FIELDS)
            {
                if (!String.IsNullOrEmpty(ff.Type))
                    ffc.Add(ff);
            }
            return ffc;
        }
        public void Add(FormField formField)
        {
            this._FORM_FIELDS.Add(formField);
        }
        public void Remove(FormField formField)
        {
            this._FORM_FIELDS.Remove(formField);
        }
        public void RemoveAt(int index)
        {
            this._FORM_FIELDS.RemoveAt(index);
        }
        public bool Contains(string Id)
        {
            return this._FORM_FIELDS.Find(delegate(FormField ff_dele) { return Id == ff_dele.Id; }) != null;
        }
        public FormField FindItemByTitle(string Title)
        {
            return this._FORM_FIELDS.Find(delegate(FormField ff_dele) { return Title == ff_dele.Text; });
        }
        public void RemoveById(string id)
        {
            FormField ff = this._FORM_FIELDS.Find(delegate(FormField f_dele) { return f_dele.Id == id; });
            if (ff != null)
                this._FORM_FIELDS.Remove(ff);
        }
        public void Clear()
        {
            this._FORM_FIELDS.Clear();
        }
        public string GetSavedDatas(FormFieldDataType dataType)
        {
            StringBuilder sb = new StringBuilder();
            foreach (FormField ff in this._FORM_FIELDS)
            {
                if (sb.Length > 0) sb.Append(GroupSplitter);
                sb.Append(ff.GetSavedData(dataType));
            }
            return sb.ToString();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new FormFieldCollection_Enumerator(this);
        }
        private class FormFieldCollection_Enumerator : IEnumerator
        {
            private int position = -1;
            private FormFieldCollection t;

            public FormFieldCollection_Enumerator(FormFieldCollection t)
            {
                this.t = t;
            }

            public bool MoveNext()
            {
                if (position < t._FORM_FIELDS.Count - 1)
                {
                    position++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    try
                    {
                        return t._FORM_FIELDS[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}
