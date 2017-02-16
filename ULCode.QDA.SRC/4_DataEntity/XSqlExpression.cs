namespace ULCode.QDA.SqlStatement
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using ULCode.QDA;
    using System.Collections.Generic;

    //抽象子表达式
    public abstract class SubExpression
    {
        private string _Value;
        public string Value
        {
            get 
            {
                return _Value;
            }
            set { _Value = value; }
        }
        public SubExpression(string value)
        {
            Value = value; 
        }
    }

    //Order by
    public class OrderUnit:SubExpression
    {
        public OrderUnit(string value) : base(value) { ; }
        //********************************************************
        public static OrderExpression operator +(OrderUnit ou1, OrderUnit ou2)
        {
            return new OrderExpression(string.Format("{0},{1}", ou1.Value, ou2.Value)); 
        }
        public string OrderBy()
        {
            if (String.IsNullOrEmpty(Value))
                return String.Empty;
            else
                return string.Format(" Order by {0}", Value);
        }
    }
    public class OrderExpression:SubExpression
    {
        public string OrderBy()
        {
            if (String.IsNullOrEmpty(Value))
                return String.Empty;
            else
                return string.Format(" Order by {0}", Value); 
        }
        public OrderExpression(string value) : base(value) { ; }
        //********************************************************
        public static OrderExpression operator +(OrderExpression oe, OrderUnit ou)
        {
            return new OrderExpression(string.Format("{0},{1}", oe.Value, ou.Value));
        }
    }
    
    //ValueExpression
    public class ValueExpression : SubExpression
    {
        public ValueExpression(string value) : base(value) { ; }
        public string ValueG
        {
            get { return String.Format("({0})",Value); }
        }
        //********************************************************
        ///ValueExpression = ValueExpression + XDataField
        ///ValueExpression = ValueExpression - XDataField
        ///ValueExpression = ValueExpression * XDataField
        ///ValueExpression = ValueExpression / XDataField
        ///ValueExpression = ValueExpression % XDataField
        ///ValueExpression = ValueExpression & XDataField
        ///ValueExpression = ValueExpression | XDataField
        public static ValueExpression operator +(ValueExpression ve, XDataField xf)
        {
            return new ValueExpression(ve.ValueG + " + " + xf.NameStr());
        }
        public static ValueExpression operator -(ValueExpression ve, XDataField xf)
        {
            return new ValueExpression(ve.ValueG + " - " + xf.NameStr());
        }
        public static ValueExpression operator *(ValueExpression ve, XDataField xf)
        {
            return new ValueExpression(ve.ValueG + " * " + xf.NameStr());
        }
        public static ValueExpression operator /(ValueExpression ve, XDataField xf)
        {
            return new ValueExpression(ve.ValueG + " / " + xf.NameStr());
        }
        public static ValueExpression operator %(ValueExpression ve, XDataField xf)
        {
            return new ValueExpression(ve.ValueG + " % " + xf.NameStr());
        }
        public static ValueExpression operator &(ValueExpression ve, XDataField xf)
        {
            return new ValueExpression(ve.ValueG + " & " + xf.NameStr());
        }
        public static ValueExpression operator |(ValueExpression ve, XDataField xf)
        {
            return new ValueExpression(ve.ValueG + " | " + xf.NameStr());
        }
        ///ValueExpression = ValueExpression + object
        ///ValueExpression = ValueExpression - object
        ///ValueExpression = ValueExpression * object
        ///ValueExpression = ValueExpression / object
        ///ValueExpression = ValueExpression % object        
        ///ValueExpression = ValueExpression & object        
        ///ValueExpression = ValueExpression | object        
        public static ValueExpression operator +(ValueExpression ve, object value)
        {
            return new ValueExpression(ve.ValueG + " + " + XDataField.GetValueStr(value));
        }
        public static ValueExpression operator -(ValueExpression ve, object value)
        {
            return new ValueExpression(ve.ValueG + " - " + XDataField.GetValueStr(value));
        }
        public static ValueExpression operator *(ValueExpression ve, object value)
        {
            return new ValueExpression(ve.ValueG + " / " + XDataField.GetValueStr(value));
        }
        public static ValueExpression operator /(ValueExpression ve, object value)
        {
            return new ValueExpression(ve.ValueG + " / " + XDataField.GetValueStr(value));
        }
        public static ValueExpression operator %(ValueExpression ve, object value)
        {
            return new ValueExpression(ve.ValueG + " % " + XDataField.GetValueStr(value));
        }
        public static ValueExpression operator &(ValueExpression ve, object value)
        {
            return new ValueExpression(ve.ValueG + " & " + XDataField.GetValueStr(value));
        }
        public static ValueExpression operator |(ValueExpression ve, object value)
        {
            return new ValueExpression(ve.ValueG + " | " + XDataField.GetValueStr(value));
        }
        ///ValueExpression = ValueExpression + FieldsExpression
        ///ValueExpression = ValueExpression - FieldsExpression
        ///ValueExpression = ValueExpression * FieldsExpression
        ///ValueExpression = ValueExpression / FieldsExpression
        ///ValueExpression = ValueExpression % FieldsExpression        
        ///ValueExpression = ValueExpression & FieldsExpression        
        ///ValueExpression = ValueExpression | FieldsExpression        
        public static ValueExpression operator +(ValueExpression ve, FieldListExpression fe)
        {
            return new ValueExpression(ve.ValueG + " + " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator -(ValueExpression ve, FieldListExpression fe)
        {
            return new ValueExpression(ve.ValueG + " - " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator *(ValueExpression ve, FieldListExpression fe)
        {
            return new ValueExpression(ve.ValueG + " / " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator /(ValueExpression ve, FieldListExpression fe)
        {
            return new ValueExpression(ve.ValueG + " / " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator %(ValueExpression ve, FieldListExpression fe)
        {
            return new ValueExpression(ve.ValueG + " % " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator &(ValueExpression ve, FieldListExpression fe)
        {
            return new ValueExpression(ve.ValueG + " & " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator |(ValueExpression ve, FieldListExpression fe)
        {
            return new ValueExpression(ve.ValueG + " | " + fe.ValueExpressionStr());
        }
        ///ValueExpression = ValueExpression + ValueExpression 
        ///ValueExpression = ValueExpression - ValueExpression
        ///ValueExpression = ValueExpression * ValueExpression
        ///ValueExpression = ValueExpression / ValueExpression
        ///ValueExpression = ValueExpression % ValueExpression        
        ///ValueExpression = ValueExpression & ValueExpression        
        ///ValueExpression = ValueExpression | ValueExpression        
        public static ValueExpression operator -(ValueExpression ve1, ValueExpression ve2)
        {
            return new ValueExpression(ve1.ValueG + " - " + ve2.ValueG);
        }
        public static ValueExpression operator *(ValueExpression ve1, ValueExpression ve2)
        {
            return new ValueExpression(ve1.ValueG + " * " + ve2.ValueG);
        }
        public static ValueExpression operator /(ValueExpression ve1, ValueExpression ve2)
        {
            return new ValueExpression(ve1.ValueG + " / " + ve2.ValueG);
        }
        public static ValueExpression operator %(ValueExpression ve1, ValueExpression ve2)
        {
            return new ValueExpression(ve1.ValueG + " % " + ve2.ValueG);
        }
        public static ValueExpression operator &(ValueExpression ve1, ValueExpression ve2)
        {
            return new ValueExpression(ve1.ValueG + " & " + ve2.ValueG);
        }
        public static ValueExpression operator |(ValueExpression ve1, ValueExpression ve2)
        {
            return new ValueExpression(ve1.ValueG + " | " + ve2.ValueG);
        }
        /// ValueExpression == XDataField  ConditionUnit        
        /// ValueExpression > XDataField  ConditionUnit        
        /// ValueExpression >= XDataField  ConditionUnit        
        /// ValueExpression != XDataField  ConditionUnit        
        /// ValueExpression < XDataField  ConditionUnit        
        /// ValueExpression <= XDataField  ConditionUnit        
        public static ConditionUnit operator ==(ValueExpression ve, XDataField dataField)
        {
            return new ConditionUnit(ve.ValueG + " = " + dataField.NameStr());
        }
        public static ConditionUnit operator >(ValueExpression ve, XDataField dataField)
        {
            return new ConditionUnit(ve.ValueG + " > " + dataField.NameStr());
        }
        public static ConditionUnit operator >=(ValueExpression ve, XDataField dataField)
        {
            return new ConditionUnit(ve.ValueG + " >= " + dataField.NameStr());
        }
        public static ConditionUnit operator !=(ValueExpression ve, XDataField dataField)
        {
            return new ConditionUnit(ve.ValueG + " != " + dataField.NameStr());
        }
        public static ConditionUnit operator <(ValueExpression ve, XDataField dataField)
        {
            return new ConditionUnit(ve.ValueG + " < " + dataField.NameStr());
        }
        public static ConditionUnit operator <=(ValueExpression ve, XDataField dataField)
        {
            return new ConditionUnit(ve.ValueG + " <= " + dataField.NameStr());
        }
        /// ValueExpression == ValueExpression  ConditionUnit        
        /// ValueExpression > ValueExpression  ConditionUnit        
        /// ValueExpression >= ValueExpression  ConditionUnit        
        /// ValueExpression != ValueExpression  ConditionUnit        
        /// ValueExpression < ValueExpression  ConditionUnit        
        /// ValueExpression <= ValueExpression  ConditionUnit        
        public static ConditionUnit operator ==(ValueExpression ve1, ValueExpression ve2)
        {
            return new ConditionUnit(ve1.ValueG + " = " + ve2.Value);
        }
        public static ConditionUnit operator >(ValueExpression ve1, ValueExpression ve2)
        {
            return new ConditionUnit(ve1.ValueG + " > " + ve2.Value);
        }
        public static ConditionUnit operator >=(ValueExpression ve1, ValueExpression ve2)
        {
            return new ConditionUnit(ve1.ValueG + " >= " + ve2.Value);
        }
        public static ConditionUnit operator !=(ValueExpression ve1, ValueExpression ve2)
        {
            return new ConditionUnit(ve1.ValueG + " != " + ve2.Value);
        }
        public static ConditionUnit operator <(ValueExpression ve1, ValueExpression ve2)
        {
            return new ConditionUnit(ve1.ValueG + " < " + ve2.Value);
        }
        public static ConditionUnit operator <=(ValueExpression ve1, ValueExpression ve2)
        {
            return new ConditionUnit(ve1.ValueG + " <= " + ve2.Value);
        }
        /// ValueExpression == object  ConditionUnit        
        /// ValueExpression > object  ConditionUnit        
        /// ValueExpression >= object  ConditionUnit        
        /// ValueExpression != object  ConditionUnit        
        /// ValueExpression < object  ConditionUnit        
        /// ValueExpression <= object  ConditionUnit        
        public static ConditionUnit operator ==(ValueExpression ve, object value)
        {
            if (value == Convert.DBNull || value == null)
                return new ConditionUnit(ve.ValueG + " is Null ");
            else
                return new ConditionUnit(ve.ValueG + " = " + XDataField.GetValueStr(value));
        }
        public static ConditionUnit operator >(ValueExpression ve, object value)
        {
            if (value == Convert.DBNull || value == null)
            {
                throw new ApplicationException("Error");
            }
            return new ConditionUnit(ve.ValueG + " > " + XDataField.GetValueStr(value));
        }
        public static ConditionUnit operator >=(ValueExpression ve, object value)
        {
            if (value == Convert.DBNull || value == null)
            {
                throw new ApplicationException("Error");
            } 
            return new ConditionUnit(ve.ValueG + " >= " + XDataField.GetValueStr(value));
        }
        public static ConditionUnit operator !=(ValueExpression ve, object value)
        {
            if (value == Convert.DBNull || value == null)
            {
                throw new ApplicationException("Error");
            }
            return new ConditionUnit(ve.ValueG + " != " + XDataField.GetValueStr(value));
        }
        public static ConditionUnit operator <(ValueExpression ve, object value)
        {
            if (value == Convert.DBNull || value == null)
            {
                throw new ApplicationException("Error");
            }
            return new ConditionUnit(ve.ValueG + " < " + XDataField.GetValueStr(value));
        }
        public static ConditionUnit operator <=(ValueExpression ve, object value)
        {
            if (value == Convert.DBNull || value == null)
            {
                throw new ApplicationException("Error");
            }
            return new ConditionUnit(ve.ValueG + "<=" + XDataField.GetValueStr(value));
        }
        /// object == ValueExpression  ConditionUnit        
        /// object > ValueExpression  ConditionUnit        
        /// object >= ValueExpression  ConditionUnit        
        /// object != ValueExpression  ConditionUnit        
        /// object < ValueExpression  ConditionUnit        
        /// object <= ValueExpression  ConditionUnit        
        public static ConditionUnit operator ==(object value, ValueExpression ve)
        {
            if (value == Convert.DBNull || value == null)
                return new ConditionUnit(ve.ValueG + " is Null");
            else
                return new ConditionUnit(XDataField.GetValueStr(value) + " = " + ve.ValueG);
        }
        public static ConditionUnit operator >(object value, ValueExpression ve)
        {
            if (value == Convert.DBNull || value == null)
            {
                throw new ApplicationException("Error");
            }
            return new ConditionUnit(XDataField.GetValueStr(value) + " > " + ve.ValueG);
        }
        public static ConditionUnit operator >=(object value, ValueExpression ve)
        {
            if (value == Convert.DBNull || value == null)
            {
                throw new ApplicationException("Error");
            }
            return new ConditionUnit(XDataField.GetValueStr(value) + " >= " + ve.ValueG);
        }
        public static ConditionUnit operator !=(object value, ValueExpression ve)
        {
            if (value == Convert.DBNull || value == null)
            {
                throw new ApplicationException("Error");
            }
            return new ConditionUnit(XDataField.GetValueStr(value) + " != " + ve.ValueG);
        }
        public static ConditionUnit operator <(object value, ValueExpression ve)
        {
            if (value == Convert.DBNull || value == null)
            {
                throw new ApplicationException("Error");
            }
            return new ConditionUnit(XDataField.GetValueStr(value) + " < " + ve.ValueG);
        }
        public static ConditionUnit operator <=(object value, ValueExpression ve)
        {
            if (value == Convert.DBNull || value == null)
            {
                throw new ApplicationException("Error");
            }
            return new ConditionUnit(XDataField.GetValueStr(value) + " <= " + ve.ValueG);
        }
        //
        public override int GetHashCode()
        {
            return Value.Length * 100;
        }
        public override bool Equals(object obj)
        {
            ValueExpression x = (ValueExpression)obj;
            return x.Value == Value;
        }
    }

    //FieldListExpression
    public class FieldListExpression: SubExpression
    {
        public List<XDataField> Fields=null;
        public FieldListExpression(string value) : base(value) { ; }
        public void Add(XDataField xdf)
        {
            if (Fields == null)
                Fields = new List<XDataField>();
            Fields.Add(xdf);
        }
        public string OrderByString()
        {
            if (Fields == null) return String.Empty;
            StringBuilder sb = new StringBuilder();
            foreach(XDataField xdf in Fields)
            {
                if (sb.Length != 0) sb.Append(",");
                sb.Append(xdf.OrderStr());
            }
            string sOrder = sb.ToString();
            if (!String.IsNullOrEmpty(sOrder))
            {
                sOrder = " Order by " + sOrder;
            }
            return sOrder;
        }
        public string FieldsListString()
        {
            if (Fields == null) return String.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (XDataField xdf in Fields)
            {
                if (sb.Length != 0) sb.Append(",");
                sb.Append(xdf.NameStr());
            }
            string sOrder = sb.ToString();
            return sOrder;
        }
        public string ValueExpressionStr()
        {
            if (Fields == null) return String.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (XDataField xdf in Fields)
            {
                if (sb.Length != 0) sb.Append(" + ");
                sb.Append(xdf.NameStr());
            }
            return String.Format("({0})", sb.ToString());
        }
        ///***************************************************
        ///XDataField + XDataField = FieldsExpression
        ///FieldsExpression + XDataField = FieldsExpression
        ///XDataField + FieldsExpression = FieldsExpression
        public static FieldListExpression operator +(FieldListExpression fe, XDataField xf)  //+
        {
            fe.Add(xf);
            return fe;
        }
        public static FieldListExpression operator +(XDataField xf, FieldListExpression fe)  //+
        {
            fe.Add(xf);
            return fe;
        }
        public static FieldListExpression operator +(FieldListExpression fe1, FieldListExpression fe2)  //+
        {
            foreach (XDataField xde in fe2.Fields)
            {
                fe1.Add(xde); 
            }
            return fe1;
        }

        ///ValueExpression = FieldsExpression + xf(冲突删除)
        ///ValueExpression = FieldsExpression - xf
        ///ValueExpression = FieldsExpression * xf
        ///ValueExpression = FieldsExpression / xf
        ///ValueExpression = FieldsExpression % xf
        ///ValueExpression = FieldsExpression & xf
        ///ValueExpression = FieldsExpression | xf
        //public static ValueExpression operator +(FieldsExpression fe, XDataField xf)
        //{
        //    return new ValueExpression(fe.ValueExpressionStr() + " + " + xf.NameStr());
        //}
        public static ValueExpression operator -(FieldListExpression fe, XDataField xf)
        {
            return new ValueExpression(fe.ValueExpressionStr() + " - " + xf.NameStr());
        }
        public static ValueExpression operator *(FieldListExpression fe, XDataField xf)
        {
            return new ValueExpression(fe.ValueExpressionStr() + " * " + xf.NameStr());
        }
        public static ValueExpression operator /(FieldListExpression fe, XDataField xf)
        {
            return new ValueExpression(fe.ValueExpressionStr() + " / " + xf.NameStr());
        }
        public static ValueExpression operator %(FieldListExpression fe, XDataField xf)
        {
            return new ValueExpression(fe.ValueExpressionStr() + " % " + xf.NameStr());
        }
        public static ValueExpression operator &(FieldListExpression fe, XDataField xf)
        {
            return new ValueExpression(fe.ValueExpressionStr() + " & " + xf.NameStr());
        }
        public static ValueExpression operator |(FieldListExpression fe, XDataField xf)
        {
            return new ValueExpression(fe.ValueExpressionStr() + " | " + xf.NameStr());
        }

        ///ValueExpression = FieldsExpression + object
        ///ValueExpression = FieldsExpression - object
        ///ValueExpression = FieldsExpression * object
        ///ValueExpression = FieldsExpression / object
        ///ValueExpression = FieldsExpression % object
        ///ValueExpression = FieldsExpression & object
        ///ValueExpression = FieldsExpression | object
        public static ValueExpression operator +(FieldListExpression fe, object value)
        {
            if (value == Convert.DBNull || value == null)
            {
                return new ValueExpression(fe.ValueExpressionStr());
            }
            return new ValueExpression(fe.ValueExpressionStr() + " + " + XDataField.GetValueStr(value));
        }
        public static ValueExpression operator -(FieldListExpression fe, object value)
        {
            if (value == Convert.DBNull || value == null)
            {
                return new ValueExpression(fe.ValueExpressionStr());
            } 
            return new ValueExpression(fe.ValueExpressionStr() + " - " + XDataField.GetValueStr(value));
        }
        public static ValueExpression operator *(FieldListExpression fe, object value)
        {
            if (value == Convert.DBNull || value == null)
            {
                return new ValueExpression(fe.ValueExpressionStr());
            } 
            return new ValueExpression(fe.ValueExpressionStr() + " * " + XDataField.GetValueStr(value));
        }
        public static ValueExpression operator /(FieldListExpression fe, object value)
        {
            if (value == Convert.DBNull || value == null)
            {
                return new ValueExpression(fe.ValueExpressionStr());
            } 
            return new ValueExpression(fe.ValueExpressionStr() + " / " + XDataField.GetValueStr(value));
        }
        public static ValueExpression operator %(FieldListExpression fe, object value)
        {
            if (value == Convert.DBNull || value == null)
            {
                return new ValueExpression(fe.ValueExpressionStr());
            } 
            return new ValueExpression(fe.ValueExpressionStr() + " % " + XDataField.GetValueStr(value));
        }
        public static ValueExpression operator &(FieldListExpression fe, object value)
        {
            if (value == Convert.DBNull || value == null)
            {
                return new ValueExpression(fe.ValueExpressionStr());
            } 
            return new ValueExpression(fe.ValueExpressionStr() + " & " + XDataField.GetValueStr(value));
        }
        public static ValueExpression operator |(FieldListExpression fe, object value)
        {
            if (value == Convert.DBNull || value == null)
            {
                return new ValueExpression(fe.ValueExpressionStr());
            } 
            return new ValueExpression(fe.ValueExpressionStr() + " | " + XDataField.GetValueStr(value));
        }

        ///ValueExpression = object + FieldsExpression
        ///ValueExpression = object - FieldsExpression
        ///ValueExpression = object * FieldsExpression
        ///ValueExpression = object / FieldsExpression
        ///ValueExpression = object % FieldsExpression
        ///ValueExpression = object & FieldsExpression
        ///ValueExpression = object | FieldsExpression
        public static ValueExpression operator +(object value, FieldListExpression fe)
        {
            if (value == Convert.DBNull || value == null)
            {
                return new ValueExpression(fe.ValueExpressionStr());
            }
            return new ValueExpression(XDataField.GetValueStr(value) + " + " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator -(object value, FieldListExpression fe)
        {
            if (value == Convert.DBNull || value == null)
            {
                return new ValueExpression(fe.ValueExpressionStr());
            }
            return new ValueExpression(XDataField.GetValueStr(value) + " - " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator *(object value, FieldListExpression fe)
        {
            if (value == Convert.DBNull || value == null)
            {
                return new ValueExpression(fe.ValueExpressionStr());
            }
            return new ValueExpression(XDataField.GetValueStr(value) + " * " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator /(object value, FieldListExpression fe)
        {
            if (value == Convert.DBNull || value == null)
            {
                return new ValueExpression(fe.ValueExpressionStr());
            }
            return new ValueExpression(XDataField.GetValueStr(value) + " / " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator %(object value, FieldListExpression fe)
        {
            if (value == Convert.DBNull || value == null)
            {
                return new ValueExpression(fe.ValueExpressionStr());
            }
            return new ValueExpression(XDataField.GetValueStr(value) + " % " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator &(object value, FieldListExpression fe)
        {
            if (value == Convert.DBNull || value == null)
            {
                return new ValueExpression(fe.ValueExpressionStr());
            }
            return new ValueExpression(XDataField.GetValueStr(value) + " & " + fe.ValueExpressionStr());
        }
        public static ValueExpression operator |(object value, FieldListExpression fe)
        {
            if (value == Convert.DBNull || value == null)
            {
                return new ValueExpression(fe.ValueExpressionStr());
            }
            return new ValueExpression(XDataField.GetValueStr(value) + " | " + fe.ValueExpressionStr());
        }

        ///ValueExpression = FieldsExpression + FieldsExpression(冲突删除)
        ///ValueExpression = FieldsExpression - FieldsExpression
        ///ValueExpression = FieldsExpression * FieldsExpression
        ///ValueExpression = FieldsExpression / FieldsExpression
        ///ValueExpression = FieldsExpression % FieldsExpression
        ///ValueExpression = FieldsExpression & FieldsExpression
        ///ValueExpression = FieldsExpression | FieldsExpression
        //public static ValueExpression operator +(FieldsExpression fe, XDataField xf)
        //{
        //    return new ValueExpression(fe.ValueExpressionStr() + " + " + xf.NameStr());
        //}
        public static ValueExpression operator -(FieldListExpression fe1, FieldListExpression fe2)
        {
            return new ValueExpression(fe1.ValueExpressionStr() + " - " + fe2.ValueExpressionStr());
        }
        public static ValueExpression operator *(FieldListExpression fe1, FieldListExpression fe2)
        {
            return new ValueExpression(fe1.ValueExpressionStr() + " * " + fe2.ValueExpressionStr());
        }
        public static ValueExpression operator /(FieldListExpression fe1, FieldListExpression fe2)
        {
            return new ValueExpression(fe1.ValueExpressionStr() + " / " + fe2.ValueExpressionStr());
        }
        public static ValueExpression operator %(FieldListExpression fe1, FieldListExpression fe2)
        {
            return new ValueExpression(fe1.ValueExpressionStr() + " % " + fe2.ValueExpressionStr());
        }
        public static ValueExpression operator &(FieldListExpression fe1, FieldListExpression fe2)
        {
            return new ValueExpression(fe1.ValueExpressionStr() + " & " + fe2.ValueExpressionStr());
        }
        public static ValueExpression operator |(FieldListExpression fe1, FieldListExpression fe2)
        {
            return new ValueExpression(fe1.ValueExpressionStr() + " | " + fe2.ValueExpressionStr());
        }

        ///ValueExpression = FieldsExpression + ValueExpression
        ///ValueExpression = FieldsExpression - ValueExpression
        ///ValueExpression = FieldsExpression * ValueExpression
        ///ValueExpression = FieldsExpression / ValueExpression
        ///ValueExpression = FieldsExpression % ValueExpression
        ///ValueExpression = FieldsExpression & ValueExpression
        ///ValueExpression = FieldsExpression | ValueExpression
        public static ValueExpression operator +(FieldListExpression fe, ValueExpression ve)
        {
            return new ValueExpression(fe.ValueExpressionStr() + " + " + ve.ValueG);
        }
        public static ValueExpression operator -(FieldListExpression fe, ValueExpression ve)
        {
            return new ValueExpression(fe.ValueExpressionStr() + " - " + ve.ValueG);
        }
        public static ValueExpression operator *(FieldListExpression fe, ValueExpression ve)
        {
            return new ValueExpression(fe.ValueExpressionStr() + " * " + ve.ValueG);
        }
        public static ValueExpression operator /(FieldListExpression fe, ValueExpression ve)
        {
            return new ValueExpression(fe.ValueExpressionStr() + " / " + ve.ValueG);
        }
        public static ValueExpression operator %(FieldListExpression fe, ValueExpression ve)
        {
            return new ValueExpression(fe.ValueExpressionStr() + " % " + ve.ValueG);
        }
        public static ValueExpression operator &(FieldListExpression fe, ValueExpression ve)
        {
            return new ValueExpression(fe.ValueExpressionStr() + " & " + ve.ValueG);
        }
        public static ValueExpression operator |(FieldListExpression fe, ValueExpression ve)
        {
            return new ValueExpression(fe.ValueExpressionStr() + " | " + ve.ValueG);
        }
    }

    //ConditionUnit
    public class ConditionUnit : SubExpression
    {
        public ConditionUnit(string value) : base(value) { ; }
        //(当FieldName==object时)
        public string SetsStr()
        {
            return string.Format(" Set {0}", Value);
        }
        public string ConditionStr()
        {
            return string.Format(" where {0}", Value);
        }
        //******************************************************
        //!ConditionUnit     ConditionExpression
        ///ConditionUnit | ConditionUnit   ConditionExpression
        ///ConditionUnit & ConditionUnit   ConditionExpression
        public static ConditionExpression operator !(ConditionUnit cu)
        {
            return new ConditionExpression(String.Format("not({0})", cu.Value));
        }
        public static ConditionExpression operator |(ConditionUnit cu1, ConditionUnit cu2)
        {
            return new ConditionExpression(String.Format("({0}) or ({1})", cu1.Value, cu2.Value));
        }
        public static ConditionExpression operator &(ConditionUnit cu1, ConditionUnit cu2)
        {
            return new ConditionExpression(String.Format("({0}) and ({1})", cu1.Value, cu2.Value));
        }
        ///ConditionUnit | ConditionExpression ConditionExpression
        ///ConditionUnit & ConditionExpression ConditionExpression
        public static ConditionExpression operator |(ConditionUnit cu, ConditionExpression ce)
        {
            return new ConditionExpression(String.Format("({0}) or ({1})", cu.Value, ce.Value));
        }
        public static ConditionExpression operator &(ConditionUnit cu, ConditionExpression ce)
        {
            return new ConditionExpression(String.Format("({0}) and ({1})", cu.Value, ce.Value));
        }
        /// <summary>
        /// ConditionUnit + ConditionUnit = SetsExpression(当FieldName==object时)
        public static SetsExpression operator +(ConditionUnit cu1, ConditionUnit cu2)
        {
            return new SetsExpression(String.Format("{0},{1}", cu1.Value, cu2.Value));
        }

    }

    //ConditionExpression
    public class ConditionExpression : SubExpression
    {
        public ConditionExpression(string value) : base(value) { ; }
        public string ConditionStr()
        {
            return string.Format(" where {0}", Value);
        }
        ///!ConditionUnit   ConditionExpression
        public static ConditionExpression operator !(ConditionExpression cu)
        {
            return new ConditionExpression(String.Format("not({0})", cu.Value));
        }
        ///ConditionExpression | ConditionUnit   ConditionExpression
        ///ConditionExpression & ConditionUnit   ConditionExpression
        public static ConditionExpression operator |(ConditionExpression ce, ConditionUnit cu)
        {
            return new ConditionExpression(String.Format("({0}) or ({1})", ce.Value, cu.Value));
        }
        public static ConditionExpression operator &(ConditionExpression ce, ConditionUnit cu)
        {
            return new ConditionExpression(String.Format("({0}) and ({1})", ce.Value, cu.Value));
        }
        ///ConditionExpression | ConditionExpression   ConditionExpression
        ///ConditionExpression & ConditionExpression   ConditionExpression
        public static ConditionExpression operator |(ConditionExpression ce1, ConditionExpression ce2)
        {
            return new ConditionExpression(String.Format("({0}) or ({1})", ce1.Value, ce2.Value));
        }
        public static ConditionExpression operator &(ConditionExpression ce1, ConditionExpression ce2)
        {
            return new ConditionExpression(String.Format("({0}) and ({1})", ce1.Value, ce2.Value));
        }

    }
    
    //SetsUnit
    public class SetsUnit : SubExpression
    {
        public string SetsStr()
        {
            return string.Format(" Set {0}", Value);
        }
        public SetsUnit(string value) : base(value) { ; }
        //*********************************************************
        public static SetsExpression operator +(SetsUnit su1, SetsUnit su2)
        {
            return new SetsExpression(String.Format("{0}, {1}", su1.Value, su2.Value));
        }
        public static SetsExpression operator +(SetsUnit su, SetsExpression se)
        {
            return new SetsExpression(String.Format("{0}, {1}", su.Value, se.Value));
        }
    }
    
    //SetsExpression
    public class SetsExpression : SubExpression
    {
        public string SetsStr()
        {
            return string.Format(" Set {0}", Value);
        }
        public SetsExpression(string value) : base(value) { ; }
        //*********************************************************
        public static SetsExpression operator +(SetsExpression se, SetsUnit su)
        {
            return new SetsExpression(String.Format("{0}, {1}", se.Value, su.Value));
        }
        public static SetsExpression operator +(SetsExpression se1, SetsExpression se2)
        {
            return new SetsExpression(String.Format("{0}, {1}", se1.Value, se2.Value));
        }
    }
}

