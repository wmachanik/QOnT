/// --- auto generated class for table: ItemGroupTbl
using System;   // for DateTime variables
using System.Collections.Generic;   // for data stuff
using System.Data;                  // for IDataReader and DbType
using QOnT.classes;        // TrackerDot classes used for DB access

namespace QOnT.control
{
  public class ItemGroupTbl
  {
    #region InternalVariableDeclarations
    private int _ItemGroupID;
    private int _GroupItemTypeID;
    private int _ItemTypeID;
    private int _ItemTypeSortPos;
    private bool _Enabled;
    private string _Notes;
    #endregion

    // class definition
    public ItemGroupTbl()
    {
      _ItemGroupID = 0;
      _GroupItemTypeID = 0;
      _ItemTypeID = 0;
      _ItemTypeSortPos = 0;
      _Enabled = false;
      _Notes = string.Empty;
    }
    #region PublicVariableDeclarations
    public int ItemGroupID { get { return _ItemGroupID; } set { _ItemGroupID = value; } }
    public int GroupItemTypeID { get { return _GroupItemTypeID; } set { _GroupItemTypeID = value; } }
    public int ItemTypeID { get { return _ItemTypeID; } set { _ItemTypeID = value; } }
    public int ItemTypeSortPos { get { return _ItemTypeSortPos; } set { _ItemTypeSortPos = value; } }
    public bool Enabled { get { return _Enabled; } set { _Enabled = value; } }
    public string Notes { get { return _Notes; } set { _Notes = value; } }
    #endregion

    #region ConstantDeclarations
    const string CONST_SQL_SELECT = "SELECT ItemGroupID, GroupItemTypeID, ItemTypeID, ItemTypeSortPos, Enabled, Notes FROM ItemGroupTbl";
    const string CONST_SQL_SELECTBYGOUPITEMID = "SELECT ItemGroupID, GroupItemTypeID, ItemTypeID, ItemTypeSortPos, Enabled, Notes FROM ItemGroupTbl WHERE GroupItemTypeID=?";
    const string CONST_SQL_INSERT = "INSERT INTO ItemGroupTbl (GroupItemTypeID, ItemTypeID, ItemTypeSortPos, Enabled, Notes)" +
                          " VALUES ( ?, ?, ?, ?, ?)";   //id field not inserted
    const string CONST_SQL_UPDATE = "UPDATE ItemGroupTbl SET GroupItemTypeID = ?, ItemTypeID = ?, ItemTypeSortPos = ?, Enabled = ?, Notes = ?" +
                           " WHERE (ItemGroupID = ?)";
    const string CONST_SQL_UPDATEITEMSORTPOS = "UPDATE ItemGroupTbl SET ItemTypeSortPos = ? WHERE (GroupItemTypeID = ?) AND (ItemTypeID = ?) ";
    const string CONST_SQL_DELETE = "DELETE FROM ItemGroupTbl WHERE (ItemGroupID = ?) ";
    const string CONST_SQL_DELETEITEMFROMGROUP = "DELETE FROM ItemGroupTbl WHERE (GroupItemTypeID = ?) AND (ItemTypeID = ?) ";

    const string CONST_SELECT_FIRSTINGROUP = "SELECT TOP 1 ItemGroupID, GroupItemTypeID, ItemTypeID, ItemTypeSortPos, Enabled, Notes FROM ItemGroupTbl WHERE (GroupItemTypeID = ?) ORDER BY ItemTypeSortPos";
    const string CONST_SELECT_PREVINGROUP = "SELECT TOP 1 ItemGroupID, GroupItemTypeID, ItemTypeID, ItemTypeSortPos, Enabled, Notes FROM ItemGroupTbl WHERE (GroupItemTypeID = ?) AND (ItemTypeSortPos < ?) ORDER BY ItemTypeSortPos DESC";
    const string CONST_SELECT_NEXTINGROUP = "SELECT TOP 1 ItemGroupID, GroupItemTypeID, ItemTypeID, ItemTypeSortPos, Enabled, Notes FROM ItemGroupTbl WHERE (GroupItemTypeID = ?) AND (ItemTypeSortPos > ?) ORDER BY ItemTypeSortPos";
    const string CONST_SELECT_LASTINGROUP = "SELECT TOP 1 ItemGroupID, GroupItemTypeID, ItemTypeID, ItemTypeSortPos, Enabled, Notes FROM ItemGroupTbl WHERE (GroupItemTypeID = ?) ORDER BY ItemTypeSortPos DESC";
    const string CONST_SELECT_SORTPOSINGROUP = "SELECT ItemTypeID FROM ItemGroupTbl WHERE (GroupItemTypeID = ?) AND (ItemTypeSortPos = ?)";
    #endregion

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<ItemGroupTbl> GetAll(string SortBy)
    {
      List<ItemGroupTbl> _DataItems = new List<ItemGroupTbl>();
      TrackerDb _TDB = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      _sqlCmd += " ORDER BY " + ((String.IsNullOrEmpty(SortBy)) ? "ItemTypeSortPos" : SortBy);     // Add order by string
      // params would go here if need
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          ItemGroupTbl _DataItem = new ItemGroupTbl();

          #region StoreThisDataItem
          _DataItem.ItemGroupID = (_DataReader["ItemGroupID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemGroupID"]);
          _DataItem.GroupItemTypeID = (_DataReader["GroupItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["GroupItemTypeID"]);
          _DataItem.ItemTypeID = (_DataReader["ItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeID"]);
          _DataItem.ItemTypeSortPos = (_DataReader["ItemTypeSortPos"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeSortPos"]);
          _DataItem.Enabled = (_DataReader["Enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["Enabled"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          #endregion
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TDB.Close();
      return _DataItems;
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<ItemGroupTbl> GetAllByGroupItemTypeID(int pGroupItemID, string SortBy)
    {
      List<ItemGroupTbl> _DataItems = new List<ItemGroupTbl>();
      if (!pGroupItemID.Equals(TrackerDb.CONST_INVALIDID))
      {
        TrackerDb _TDB = new TrackerDb();
        string _sqlCmd = CONST_SQL_SELECTBYGOUPITEMID; //        = "SELECT ItemGroupID, GroupItemTypeID, ItemTypeID, ItemTypeSortPos, Enabled, Notes FROM ItemGroupTbl WHERE GroupItemTypeID=?";
        _sqlCmd += " ORDER BY " + ((String.IsNullOrEmpty(SortBy)) ? "ItemTypeSortPos" : SortBy);     // Add order by string
        // params would go here if need
        _TDB.AddWhereParams(pGroupItemID, DbType.Int32, "@GroupItemTypeID");
        IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);
        if (_DataReader != null)
        {
          while (_DataReader.Read())
          {
            ItemGroupTbl _DataItem = new ItemGroupTbl();

            #region StoreThisDataItem
            _DataItem.ItemGroupID = (_DataReader["ItemGroupID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemGroupID"]);
            _DataItem.GroupItemTypeID = (_DataReader["GroupItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["GroupItemTypeID"]);
            _DataItem.ItemTypeID = (_DataReader["ItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeID"]);
            _DataItem.ItemTypeSortPos = (_DataReader["ItemTypeSortPos"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeSortPos"]);
            _DataItem.Enabled = (_DataReader["Enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["Enabled"]);
            _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
            #endregion
            _DataItems.Add(_DataItem);
          }
          _DataReader.Close();
        }
        _TDB.Close();
      }
      return _DataItems;
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public string InsertItemGroup(ItemGroupTbl pItemGroupTbl)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      #region InsertParameters
      _TDB.AddParams(pItemGroupTbl.GroupItemTypeID, DbType.Int32, "@GroupItemTypeID");
      _TDB.AddParams(pItemGroupTbl.ItemTypeID, DbType.Int32, "@ItemTypeID");
      _TDB.AddParams(pItemGroupTbl.ItemTypeSortPos, DbType.Int32, "@ItemTypeSortPos");
      _TDB.AddParams(pItemGroupTbl.Enabled, DbType.Boolean, "@Enabled");
      _TDB.AddParams(pItemGroupTbl.Notes, DbType.String, "@Notes");
      #endregion
      // Now we have the parameters excute the SQL
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_INSERT);
      _TDB.Close();
      return _result;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
    public string UpdateItemGroup(ItemGroupTbl pItemGroupTbl)
    { return UpdateItemGroup(pItemGroupTbl, 0); }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, false)]
    public string UpdateItemGroup(ItemGroupTbl pItemGroupTbl, int pOrignal_ItemGroupID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      #region UpdateParameters
      if (pOrignal_ItemGroupID > 0)
        _TDB.AddWhereParams(pOrignal_ItemGroupID, DbType.Int32);  // check this line it assumes id field is int32
      else
        _TDB.AddWhereParams(pItemGroupTbl.ItemGroupID, DbType.Int32, "@ItemGroupID");

      _TDB.AddParams(pItemGroupTbl.GroupItemTypeID, DbType.Int32, "@GroupItemTypeID");
      _TDB.AddParams(pItemGroupTbl.ItemTypeID, DbType.Int32, "@ItemTypeID");
      _TDB.AddParams(pItemGroupTbl.ItemTypeSortPos, DbType.Int32, "@ItemTypeSortPos");
      _TDB.AddParams(pItemGroupTbl.Enabled, DbType.Boolean, "@Enabled");
      _TDB.AddParams(pItemGroupTbl.Notes, DbType.String, "@Notes");
      #endregion
      // Now we have the parameters excute the SQL
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_UPDATE);
      _TDB.Close();
      return _result;
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public string DeleteItemGroup(ItemGroupTbl pItemGroupTbl)
    { return DeleteItemGroup(pItemGroupTbl.ItemGroupID); }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public string DeleteItemGroup(int pItemGroupID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      _TDB.AddWhereParams(pItemGroupID, DbType.Int32, "@ItemGroupID");
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_DELETE);
      _TDB.Close();
      return _result;
    }
    public string DeleteGroupItemFromGroup(int pGroupItemID, int pItemTypeID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      _TDB.AddWhereParams(pGroupItemID, DbType.Int32, "@GroupItemTypeID");
      _TDB.AddWhereParams(pItemTypeID, DbType.Int32, "@ItemTypeID");
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_DELETEITEMFROMGROUP);
      _TDB.Close();
      return _result;
    }
    public string UpdateItemsSortPos(int pSortPos, int pGroupItemID, int pItemTypeID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      _TDB.AddParams(pSortPos, DbType.Int32, "@SortPos");
      _TDB.AddWhereParams(pGroupItemID, DbType.Int32, "@GroupItemTypeID");
      _TDB.AddWhereParams(pItemTypeID, DbType.Int32, "@ItemTypeID");
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_UPDATEITEMSORTPOS);
      _TDB.Close();
      return _result;
    }
    /// <summary>
    /// Return the minimum of the sort order of the current GroupItem
    /// </summary>
    /// <param name="pGroupItemTypeID"></param>
    /// <returns></returns>
    public ItemGroupTbl GetFirstGroupItemType(int pGroupItemTypeID)
    {
      ItemGroupTbl _DataItem = new ItemGroupTbl();

      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pGroupItemTypeID, DbType.Int32, "GroupItemTypeID");

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SELECT_FIRSTINGROUP);

      if (_DataReader != null)
      {
        if (_DataReader.Read())
        {
          #region StoreThisDataItem
          _DataItem.ItemGroupID = (_DataReader["ItemGroupID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemGroupID"]);
          _DataItem.GroupItemTypeID = (_DataReader["GroupItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["GroupItemTypeID"]);
          _DataItem.ItemTypeID = (_DataReader["ItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeID"]);
          _DataItem.ItemTypeSortPos = (_DataReader["ItemTypeSortPos"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeSortPos"]);
          _DataItem.Enabled = (_DataReader["Enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["Enabled"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          #endregion
        }
        _DataReader.Close();
      }
      _TDB.Close();

      return _DataItem;
    }
    /// <summary>
    /// Get the item previous to this one
    /// </summary>
    /// <param name="pGroupItemTypeID">this group</param>
    /// <param name="pItemTypeSortPos">current sort pos</param>
    /// <returns>the groupiten previous</returns>
    public ItemGroupTbl GetPrevGroupItemType(int pGroupItemTypeID, int pItemTypeSortPos)
    {
      ItemGroupTbl _DataItem = new ItemGroupTbl();

      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pGroupItemTypeID, DbType.Int32, "GroupItemTypeID");
      _TDB.AddWhereParams(pItemTypeSortPos, DbType.Int32, "ItemTypeSortPos");

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SELECT_PREVINGROUP);

      if (_DataReader != null)
      {
        if (_DataReader.Read())
        {
          #region StoreThisDataItem
          _DataItem.ItemGroupID = (_DataReader["ItemGroupID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemGroupID"]);
          _DataItem.GroupItemTypeID = (_DataReader["GroupItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["GroupItemTypeID"]);
          _DataItem.ItemTypeID = (_DataReader["ItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeID"]);
          _DataItem.ItemTypeSortPos = (_DataReader["ItemTypeSortPos"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeSortPos"]);
          _DataItem.Enabled = (_DataReader["Enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["Enabled"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          #endregion
        }
        else
          _DataItem = GetLastGroupItem(pGroupItemTypeID); // nothing found so get the last one.

        _DataReader.Close();
      }
      _TDB.Close();

      return _DataItem;
    }
    public ItemGroupTbl GetNextGroupItemType(int pGroupItemTypeID, int pItemTypeSortPos)
    {
      ItemGroupTbl _DataItem = new ItemGroupTbl();

      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pGroupItemTypeID, DbType.Int32, "GroupItemTypeID");
      _TDB.AddWhereParams(pItemTypeSortPos, DbType.Int32, "ItemTypeSortPos");

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SELECT_NEXTINGROUP);

      if (_DataReader != null)
      {
        if (_DataReader.Read())
        {
          #region StoreThisDataItem
          _DataItem.ItemGroupID = (_DataReader["ItemGroupID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemGroupID"]);
          _DataItem.GroupItemTypeID = (_DataReader["GroupItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["GroupItemTypeID"]);
          _DataItem.ItemTypeID = (_DataReader["ItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeID"]);
          _DataItem.ItemTypeSortPos = (_DataReader["ItemTypeSortPos"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeSortPos"]);
          _DataItem.Enabled = (_DataReader["Enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["Enabled"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          #endregion
        }
        else
          _DataItem = GetFirstGroupItemType(pGroupItemTypeID);   // did not find we must be at the end of the group.

        _DataReader.Close();
      }

      _TDB.Close();

      return _DataItem;
    }
    /// <summary>
    /// Get the item that is in sort pos passed
    /// </summary>
    /// <param name="pGroupItemTypeID">The group id</param>
    /// <param name="pSortPos">the post selected</param>
    /// <returns>the item id</returns>
    public int GetSortPosItemInGroup(int pGroupItemTypeID, int pSortPos)
    {
      int _ItemTypeID = TrackerDb.CONST_INVALIDID;

      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pGroupItemTypeID, DbType.Int32, "GroupItemTypeID");
      _TDB.AddWhereParams(pSortPos, DbType.Int32, "ItemTypeSortPos");

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SELECT_SORTPOSINGROUP); // "SELECT ItemTypeID, ItemTypeSortPos FROM ItemGroupTbl WHERE (GroupItemTypeID = ?) AND (ItemTypeSortPos = ?)";
      if (_DataReader != null)
      {
        if (_DataReader.Read())
          _ItemTypeID = (_DataReader["ItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeID"]);

        _DataReader.Close();
      }
      _TDB.Close();

      return _ItemTypeID;
    }
    public ItemGroupTbl GetLastGroupItem(int pGroupItemTypeID)
    {
      ItemGroupTbl _DataItem = new ItemGroupTbl();

      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pGroupItemTypeID, DbType.Int32, "GroupItemTypeID");

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SELECT_LASTINGROUP);

      if (_DataReader != null)
      {
        if (_DataReader.Read())
        {
          #region StoreThisDataItem
          _DataItem.ItemGroupID = (_DataReader["ItemGroupID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemGroupID"]);
          _DataItem.GroupItemTypeID = (_DataReader["GroupItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["GroupItemTypeID"]);
          _DataItem.ItemTypeID = (_DataReader["ItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeID"]);
          _DataItem.ItemTypeSortPos = (_DataReader["ItemTypeSortPos"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["ItemTypeSortPos"]);
          _DataItem.Enabled = (_DataReader["Enabled"] == DBNull.Value) ? false : Convert.ToBoolean(_DataReader["Enabled"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          #endregion
        }
        _DataReader.Close();
      }
      _TDB.Close();

      return _DataItem;
    }
    public int GetLastGroupItemSortPos(int pGroupItemTypeID)
    {
      int _LastItemSortPos = TrackerDb.CONST_INVALIDID;

      ItemGroupTbl _DataItem = GetLastGroupItem(pGroupItemTypeID);

      if (_DataItem != null)
        _LastItemSortPos = _DataItem.ItemTypeSortPos;

      return _LastItemSortPos;
    }
    public int IncItemSortPos(ItemGroupTbl pItemGroupTbl)
    {
      pItemGroupTbl.ItemTypeSortPos = IncItemSortPos(pItemGroupTbl.GroupItemTypeID, pItemGroupTbl.ItemTypeID, pItemGroupTbl.ItemTypeSortPos);
      return pItemGroupTbl.ItemTypeSortPos;
    }
    public int IncItemSortPos(int pGroupItemTypeID, int pThisItemID, int pSortPos)
    {
      int _NewSortPos = pSortPos;
      int _LastItemSortPos = GetLastGroupItemSortPos(pGroupItemTypeID);
      if (pSortPos < _LastItemSortPos)
      {
        ItemGroupTbl _DataItem = GetNextGroupItemType(pGroupItemTypeID, pSortPos);
        if (_DataItem != null)
        {
          _NewSortPos = _DataItem.ItemTypeSortPos;
          _DataItem.ItemTypeSortPos--;  // increment the last one by one
          UpdateItemGroup(_DataItem);
        }
        UpdateItemsSortPos(_NewSortPos, pGroupItemTypeID, pThisItemID);

        //_NewSortPos++;
        //int _CurrItemInSortPosID = GetSortPosItemInGroup(pGroupItemTypeID, _NewSortPos);
        //// if an item excists in the current pos the swop it around otherwise place this item there
        //if (_CurrItemInSortPosID != TrackerDb.CONST_INVALIDID)
        //  UpdateItemsSortPos(pSortPos, pGroupItemTypeID, _CurrItemInSortPosID);

        //UpdateItemsSortPos(_NewSortPos, pGroupItemTypeID, pThisItemID);
      }
      return _NewSortPos;
    }
    public int DecItemSortPos(ItemGroupTbl pItemGroupTbl)
    {
      pItemGroupTbl.ItemTypeSortPos = DecItemSortPos(pItemGroupTbl.GroupItemTypeID, pItemGroupTbl.ItemTypeID, pItemGroupTbl.ItemTypeSortPos);
      return pItemGroupTbl.ItemTypeSortPos;
    }
    public int DecItemSortPos(int pGroupItemTypeID, int pThisItemID, int pSortPos)
    {
      int _NewSortPos = pSortPos;
      if (pSortPos > 1)
      {
        ItemGroupTbl _DataItem = GetPrevGroupItemType(pGroupItemTypeID, pSortPos);
        if (_DataItem != null)
        {
          _NewSortPos = _DataItem.ItemTypeSortPos;
          _DataItem.ItemTypeSortPos++;  // increment the last one by one
          UpdateItemGroup(_DataItem);
        }
        UpdateItemsSortPos(_NewSortPos, pGroupItemTypeID, pThisItemID);

//        int _CurrItemInSortPosID = GetSortPosItemInGroup(pGroupItemTypeID, _NewSortPos);
//        // if an item excists in the current pos the swop it around otherwise place this item there
//        if (_CurrItemInSortPosID != TrackerDb.CONST_INVALIDID)
//          UpdateItemsSortPos(pSortPos, pGroupItemTypeID, _CurrItemInSortPosID);

      }
      return _NewSortPos;
    }
  }
}
