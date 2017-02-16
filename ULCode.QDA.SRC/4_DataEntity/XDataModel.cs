namespace ULCode.QDA
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    public abstract class XDataModel : XModel
    {
        public List<XDataModel> ChildModels;
        public DataTable DTCache;
        //public object[] KeyValues=null;
        public XDataEntity ParentEntity;
        public XDataModel()
        {
        }
        public XDataModel(XDataEntity parentEntity)
        {
            this.ParentEntity = parentEntity;
        }
        public XDataModel(XDataEntity parentEntity, DataRow drCache)
        {
            this.ParentEntity = parentEntity;
            LoadFromDataRow(drCache);
        }
        public XDataModel(XDataEntity parentEntity, params object[] keyValues)
        {
            this.ParentEntity = parentEntity;
            LoadFromDB(false, keyValues);
        }
        public XDataModel(XDataEntity parentEntity, DataTable dtCache, params object[] keyValues)
        {
            this.ParentEntity = parentEntity;
            LoadFromDataTable(dtCache, keyValues);
        }
        ~XDataModel()
        {
            if (ChildModels != null)
                ChildModels.Clear();
        }

        //基本操作
        public int Delete()
        {
            int iR = this.ParentEntity.DeleteOne(this);
            if (iR > 0)
            {
                if (DTCache != null)
                {
                    DataRow[] drs = this.DTCache.Select(ParentEntity.GetKeyConditionString(false, this));
                    foreach (DataRow dr in drs)
                    {
                        dr.Delete();
                    }
                }
            }
            return iR;
        }
        public int Insert()
        {
            return this.ParentEntity.Insert(this);
        }
        public int Insert(bool returnIdentityID)
        {
            int iR = this.ParentEntity.Insert(this, returnIdentityID);
            if (returnIdentityID)
            {
                this.DFields[ParentEntity.KeyField].set(iR); 
                return iR;
            }
            else
            {
                return iR;
            }
        }
        public int Update()
        {
            //return this.ParentEntity.SaveToDb(this);
            return this.ParentEntity.UpdateOne(this);
        }

        //从缓存中DTCache中
        public DataRow GetDataRowFromDTCache()
        {   
            DataRow[] drs = this.DTCache.Select(ParentEntity.GetKeyConditionString(false, this));
            if (drs.Length > 0)
                return drs[0];
            else
                return null;
        }
        public DataRow GetDataRowFromDTCache(string sort)
        {
            DataRow[] drs = this.DTCache.Select(ParentEntity.GetKeyConditionString(false, this),sort);
            if (drs.Length > 0)
                return drs[0];
            else
                return null;
        }

        //装载
        public int Load(params object[] keyValues)
        {
            return this.LoadFromDB(true, keyValues);
        }
        public int ReLoad()
        {
            return this.LoadFromDB(true, null);
        }
        public int LoadFromDataRow(DataRow dr)
        {
            if (dr == null)
            {
                return 0;
            }
            this.ParentEntity.LoadFromDataRow(this, dr);
            if (DTCache == null)
                DTCache = dr.Table;
            return 1;
        }
        public int LoadFromDataTable(DataTable dt, params object[] keyValues)
        {
            if (dt == null)
            {
                dt = DTCache;
            }
            else
            {
                DTCache = dt;
            }
            string sCon = keyValues == null ?
                ParentEntity.GetKeyConditionString(false, this) : ParentEntity.GetKeyConditionString(false, keyValues);
            DataRow[] drs = dt.Select(sCon);
            if (drs.Length > 0)
            {
                this.ParentEntity.LoadFromDataRow(this, drs[0]);
                return 1;
            }
            return 0;
        }
        public int LoadFromDB(bool IsUpdateCache, params object[] keyValues)
        {
            int iR = this.ParentEntity.LoadFromDB(this, keyValues);
            if (iR > 0 && IsUpdateCache && this.DTCache != null)
            {
                this.ParentEntity.SaveToDataTable(this, this.DTCache);
            }
            return iR;
        }

        //保存
        public int Save()
        {
            if (this.SaveToDataDb() > 0)
            {
                return this.SaveToDataTable(null);
            }
            return 0;
        }
        public int SaveToDataDb()
        {
            return this.ParentEntity.SaveToDb(this);
        }
        public int SaveToDataRow(DataRow dr)
        {
            this.ParentEntity.SaveToDataRow(this, dr);
            if (this.DTCache == null)
            {
                this.DTCache = dr.Table;
            }
            return 1;
        }
        public int SaveToDataTable(DataTable dt)
        {
            if (dt == null) dt = DTCache;
            if (dt != null)
            {
                this.ParentEntity.SaveToDataTable(this, dt);
                if (this.DTCache == null)
                {
                    this.DTCache = dt;
                }
            }
            return 1;
        }

        //SaveChilds
        public void SaveChilds()
        {
            this.SaveChilds(this.ChildModels);
        }
        public void SaveChilds(List<XDataModel> childModels)
        {
            if (ChildModels == null) return;
            foreach (XDataModel xdm in childModels)
            {
                xdm.Save();
            }
        }

        //专门为TreeView控件所设置
        public List<TreeNode> AttachedNodes = new List<TreeNode>();
        public void AddNodeInto(TreeNode tnParent, ContextMenuStrip cms, params object[] para)
        {
            TreeNode tnNew = new TreeNode();
            this.RefreshNode(tnNew, para);
            if (!tnParent.Nodes.ContainsKey(tnNew.Name))
            {
                tnNew.Tag = this;
                if (cms != null)
                    tnNew.ContextMenuStrip = cms;
                this.AttachedNodes.Add(tnNew);
                tnParent.Nodes.Add(tnNew);
            }
        }
        public void AttachNode(TreeNode tn, ContextMenuStrip cms, params object[] para)
        {
            tn.Tag = this;
            if (cms != null)
                tn.ContextMenuStrip = cms;
            this.AttachedNodes.Add(tn);
            this.RefreshNode(tn, para);
        }
        public virtual void RefreshNode(TreeNode tn, params object[] para)
        {
            //tn.Name = String.Format("XXX_{0}", this.id);
            tn.Text = "notitle";
            //tn.ImageKey = tn.SelectedImageKey = "";            
        }
        public void RefreshAllNodes()
        {
            foreach (TreeNode tn in this.AttachedNodes)
            {
                this.RefreshNode(tn, tn.TreeView.Name);
            }
        }
        public void RemoveAllNodes()
        {
            for (int i = this.AttachedNodes.Count - 1; i >= 0; i--)
            {
                TreeNode tn = this.AttachedNodes[i];
                tn.Remove();
            }
            this.AttachedNodes.Clear();
        }
        //专门为ListView控件所设置
        public List<ListViewItem> AttachedItems = new List<ListViewItem>();
        public void AddItemInto(ListView lvParent, params object[] para)
        {
            ListViewItem tnNew = new ListViewItem();
            this.RefreshItem(tnNew, para);
            if (!lvParent.Items.ContainsKey(tnNew.Name))
            {
                tnNew.Tag = this;
                this.AttachedItems.Add(tnNew);
                lvParent.Items.Add(tnNew);
            }
        }
        public void AttachItem(ListViewItem tn, params object[] para)
        {
            tn.Tag = this;
            this.AttachedItems.Add(tn);
            this.RefreshItem(tn, para);
        }
        public virtual void RefreshItem(ListViewItem tn, params object[] para)
        {
            //tn.Name = String.Format("XXX_{0}", this.id);
            tn.Text = "notitle";
            //tn.ImageKey = tn.SelectedImageKey = "";            
        }
        public virtual void RefreshAllItems()
        {
            foreach (ListViewItem tn in this.AttachedItems)
            {
                this.RefreshItem(tn, tn.ListView.Name);
            }
        }
        public void RemoveAllItems()
        {
            for (int i = this.AttachedItems.Count - 1; i >= 0; i--)
            {
                ListViewItem tn = this.AttachedItems[i];
                tn.Remove();
            }
            this.AttachedItems.Clear();
        }

    }
}

