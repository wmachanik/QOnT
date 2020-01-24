/// --- auto generated class for table: UsedItemGroupTbl
using System;   // for DateTime variables
using System.Collections.Generic;   // for data stuff
using System.Data;                  // for IDataReader and DbType
using QOnT.classes;        // TrackerDot classes used for DB access

namespace QOnT.control
{
  public class UsedItemGroupTbl
  {
    #region InternalVariableDeclarations
    private int _UsedItemGroupID;
    private long _ContactID;
    private int _GroupItemTypeID;
    private int _LastItemTypeID;
    private int _LastItemTypeSortPos;
    private DateTime _LastItemDateChanged;
    private string _Notes;
    #endregion

    // class definition
    public UsedItemGroupTbl()
    {
      _UsedItemGroupID = TrackerDb.CONST_INVALIDID;
      _ContactID = TrackerDb.CONST_INVALIDID;
      _GroupItemTypeID = TrackerDb.CONST_INVALIDID;
      _LastItemTypeID = TrackerDb.CONST_INVALIDID;
      _LastItemTypeSortPos = 0;
      _LastItemDateChanged = System.DateTime.Now.Date;
      _Notes = string.Empty;
    }
    #region PublicVariableDeclarations
    public int UsedItemGroupID { get { return _UsedItemGroupID;}  set { _UsedItemGroupID = value;} }
    public long ContactID { get { return _ContactID;}  set { _ContactID = value;} }
    public int GroupItemTypeID { get { return _GroupItemTypeID;}  set { _GroupItemTypeID = value;} }
    public int LastItemTypeID { get { return _LastItemTypeID;}  set { _LastItemTypeID = value;} }
    public int LastItemTypeSortPos { get { return _LastItemTypeSortPos;}  set { _LastItemTypeSortPos = value;} }
    public DateTime LastItemDateChanged { get { return _LastItemDateChanged;}  set { _LastItemDateChanged = value;} }
    public string Notes { get { return _Notes;}  set { _Notes = value;} }
    #endregion

    #region ConstantDeclarations

    const string CONST_SQL_SELECT = "SELECT UsedItemGroupID, ContactID, GroupItemTypeID, LastItemTypeID, LastItemTypeSortPos, LastItemDateChanged, Notes FROM UsedItemGroupTbl";
    const string CONST_SQL_INSERT = "INSERT INTO UsedItemGroupTbl (ContactID, GroupItemTypeID, LastItemTypeID, LastItemTypeSortPos, LastItemDateChanged, Notes)" + 
                          " VALUES ( ?, ?, ?, ?, ?, ?)";   //id field not inserted
    const string CONST_SQL_UPDATE = "UPDATE UsedItemGroupTbl SET ContactID = ?, GroupItemTypeID = ?, LastItemTypeID = ?, LastItemTypeSortPos = ?, LastItemDateChanged = ?, Notes = ?" + 
                           " WHERE (UsedItemGroupID = ?)";
    const string CONST_SQL_DELETE = "DELETE FROM UsedItemGroupTbl WHERE (UsedItemGroupID = ?)";
    const string CONST_SQL_GETGROUPITEMTYPED = "SELECT UsedItemGroupID, LastItemTypeID, LastItemTypeSortPos, LastItemDateChanged, Notes FROM UsedItemGroupTbl WHERE(ContactID = ?) AND (GroupItemTypeID = ?)";
    const string CONST_SELECTLASTUSEDITEMID = "SELECT UsedItemGroupID, GroupItemTypeID, LastItemTypeSortPos, Notes FROM UsedItemGroupTbl WHERE ContactID = ? AND LastItemTypeID = ? AND LastItemDateChanged = ?";
    #endregion

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
    public List<UsedItemGroupTbl> GetAll(string SortBy)
    {
      List<UsedItemGroupTbl> _DataItems = new List<UsedItemGroupTbl>();
      TrackerDb _TDB = new TrackerDb();
      string _sqlCmd = CONST_SQL_SELECT;
      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += " ORDER BY " + SortBy;     // Add order by string
      // params would go here if need
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);
      if (_DataReader != null)
      {
        while (_DataReader.Read())
        {
          UsedItemGroupTbl _DataItem = new UsedItemGroupTbl();

           #region StoreThisDataItem
          _DataItem.UsedItemGroupID = (_DataReader["UsedItemGroupID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["UsedItemGroupID"]);
          _DataItem.ContactID = (_DataReader["ContactID"] == DBNull.Value) ? 0 : Convert.ToInt64(_DataReader["ContactID"]);
          _DataItem.GroupItemTypeID = (_DataReader["GroupItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["GroupItemTypeID"]);
          _DataItem.LastItemTypeID = (_DataReader["LastItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["LastItemTypeID"]);
          _DataItem.LastItemTypeSortPos = (_DataReader["LastItemTypeSortPos"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["LastItemTypeSortPos"]);
          _DataItem.LastItemDateChanged = (_DataReader["LastItemDateChanged"] == DBNull.Value) ? System.DateTime.Now : Convert.ToDateTime(_DataReader["LastItemDateChanged"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
          #endregion
          _DataItems.Add(_DataItem);
        }
        _DataReader.Close();
      }
      _TDB.Close();
      return _DataItems;
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public string Insert(UsedItemGroupTbl pUsedItemGroupTbl)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      #region InsertParameters
      _TDB.AddParams(pUsedItemGroupTbl.ContactID, DbType.Int64, "@ContactID");
      _TDB.AddParams(pUsedItemGroupTbl.GroupItemTypeID, DbType.Int32, "@GroupItemTypeID");
      _TDB.AddParams(pUsedItemGroupTbl.LastItemTypeID, DbType.Int32, "@LastItemTypeID");
      _TDB.AddParams(pUsedItemGroupTbl.LastItemTypeSortPos, DbType.Int32, "@LastItemTypeSortPos");
      _TDB.AddParams(pUsedItemGroupTbl.LastItemDateChanged, DbType.Date, "@LastItemDateChanged");
      _TDB.AddParams(pUsedItemGroupTbl.Notes, DbType.String, "@Notes");
      #endregion
      // Now we have the parameters excute the SQL
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_INSERT);
      _TDB.Close();
      return _result;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
    public string Update(UsedItemGroupTbl pUsedItemGroupTbl)
    { return Update(pUsedItemGroupTbl, 0); }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, false)]
    public string Update(UsedItemGroupTbl pUsedItemGroupTbl, int pOrignal_UsedItemGroupID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      #region UpdateParameters
      if (pOrignal_UsedItemGroupID > 0)
        _TDB.AddWhereParams(pOrignal_UsedItemGroupID, DbType.Int32);  // check this line it assumes id field is int32
      else
        _TDB.AddWhereParams(pUsedItemGroupTbl.UsedItemGroupID, DbType.Int32, "@UsedItemGroupID");

      _TDB.AddParams(pUsedItemGroupTbl.ContactID, DbType.Int64, "@ContactID");
      _TDB.AddParams(pUsedItemGroupTbl.GroupItemTypeID, DbType.Int32, "@GroupItemTypeID");
      _TDB.AddParams(pUsedItemGroupTbl.LastItemTypeID, DbType.Int32, "@LastItemTypeID");
      _TDB.AddParams(pUsedItemGroupTbl.LastItemTypeSortPos, DbType.Int32, "@LastItemTypeSortPos");
      _TDB.AddParams(pUsedItemGroupTbl.LastItemDateChanged, DbType.Date, "@LastItemDateChanged");
      _TDB.AddParams(pUsedItemGroupTbl.Notes, DbType.String, "@Notes");
      #endregion
      // Now we have the parameters excute the SQL
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_UPDATE);   // UPDATE UsedItemGroupTbl SET ContactID = ?, GroupItemTypeID = ?, LastItemTypeID = ?, LastItemTypeSortPos = ?, LastItemDateChanged = ?, Notes = ?  WHERE (UsedItemGroupID = ?)";
      _TDB.Close();
      return _result;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public string Delete(UsedItemGroupTbl pUsedItemGroupTbl)
    { return Delete(pUsedItemGroupTbl.UsedItemGroupID); }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public string Delete(int pUsedItemGroupID)
    {
      string _result = string.Empty;
      TrackerDb _TDB = new TrackerDb();

      _TDB.AddWhereParams(pUsedItemGroupID, DbType.Int32, "@UsedItemGroupID");
      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_DELETE);
      _TDB.Close();
      return _result;
    }
    /// <summary>
    /// Check to see if this contact has ever taken an item from this group before - if so return it otherwise return -1
    /// </summary>
    /// <param name="pContactID">The ID of the contact</param>
    /// <param name="pGroupItemTypeID">The Group ID requesed</param>
    /// <returns>True if it </returns>
    public UsedItemGroupTbl ContactLastGroupItem(long pContactID, int pGroupItemTypeID)
    {
      UsedItemGroupTbl _DataItem = new UsedItemGroupTbl();

      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pContactID, DbType.Int64, "@ContactID");
      _TDB.AddWhereParams(pGroupItemTypeID, DbType.Int32, "@GroupItemTypeID");

      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SQL_GETGROUPITEMTYPED);   //SELECT UsedItemGroupID, LastItemTypeID, LastItemTypeSortPos, LastItemDateChanged, Notes FROM UsedItemGroupTbl WHERE(ContactID = ?) AND (GroupItemTypeID = ?)";

      if (_DataReader != null)
      {
        if (_DataReader.Read())
        {
          _DataItem.UsedItemGroupID = (_DataReader["UsedItemGroupID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["UsedItemGroupID"]);
          _DataItem.ContactID = pContactID;
          _DataItem.GroupItemTypeID = pGroupItemTypeID;
          _DataItem.LastItemTypeID = (_DataReader["LastItemTypeID"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["LastItemTypeID"]);
          _DataItem.LastItemTypeSortPos = (_DataReader["LastItemTypeSortPos"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["LastItemTypeSortPos"]);
          _DataItem.LastItemDateChanged = (_DataReader["LastItemDateChanged"] == DBNull.Value) ? System.DateTime.Now : Convert.ToDateTime(_DataReader["LastItemDateChanged"]);
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
        }
        _DataReader.Close();
      }
      _TDB.Close();

      return _DataItem;
    }

    /// <summary>
    /// Get NextGroup Item for the Client and Item Group passed:
    /// Uses: ContactID, ItemGroupID and returns ItemTypeID
    /// •	If the contactID+ItemGroupID exists then:
    ///     o	Get the Last Item used in the UsedItemGroup.
    ///     o	Retrieve the next item as per the LastSortItem number so select Item where SortItem#>LastSortItem, get ready to return it.
    /// •	Else get the first item in the ItemGroup and set ItemTypeToUse
    /// •	Save to the table that this item is now the LastItem, and save sortpos, including the date it was changed.
    /// •	Return the ItemTypeID.
    /// </summary>
    /// <param name="pContactID">The Contact / Client ID for this group</param>
    /// <param name="pGroupItemTypeID">The Item Group that the Item is from</param>
    /// <returns>ItemID to use</returns>
    public ItemGroupTbl GetNextGroupItem(long pContactID, int pGroupItemTypeID, DateTime pDeliveryDate)
    {
      ItemGroupTbl _IGT = new ItemGroupTbl();
      UsedItemGroupTbl _LastItem = new UsedItemGroupTbl();
      _LastItem = ContactLastGroupItem(pContactID, pGroupItemTypeID);

      if (_LastItem.UsedItemGroupID == TrackerDb.CONST_INVALIDID)
      {
        _IGT = _IGT.GetFirstGroupItemType(pGroupItemTypeID);
        // save it for next time
        _LastItem.ContactID = pContactID;
        _LastItem.GroupItemTypeID = pGroupItemTypeID;
        _LastItem.LastItemTypeID = _IGT.ItemTypeID;
        _LastItem.LastItemTypeSortPos = _IGT.ItemTypeSortPos;
        _LastItem.LastItemDateChanged = pDeliveryDate;
        Insert(_LastItem);
        //does not seem to be updating - or finding next.
      }
      else
      {
        _IGT = _IGT.GetNextGroupItemType(pGroupItemTypeID,_LastItem.LastItemTypeSortPos);
        // save it for next time
        _LastItem.LastItemTypeID = _IGT.ItemTypeID;
        _LastItem.LastItemTypeSortPos = _IGT.ItemTypeSortPos;
        _LastItem.LastItemDateChanged = pDeliveryDate;
        Update(_LastItem);
        //does not seem to be updating - or finding next.
      }

      return _IGT;     
    }
    /// <summary>
    /// Update an item if it was a group item
    /// </summary>
    /// <param name="pContactID">contact/companyid</param>
    /// <param name="pItemTypeID">ItemTypeID</param>
    /// <param name="pOldDeliveryDate">PrevDeliverDate</param>
    /// <param name="pNewDeliveryDate">NewDeliveryDate</param>
    /// <returns></returns>
    public bool UpdateIfGroupItem(long pContactID, int pItemTypeID, DateTime pOldDeliveryDate, DateTime pNewDeliveryDate)
    {
      bool _result = false;
      if (!pNewDeliveryDate.Equals(pOldDeliveryDate))
      {
        UsedItemGroupTbl _DataItem = new UsedItemGroupTbl();
        _DataItem = GetLastUsedItemID(pContactID, pItemTypeID, pOldDeliveryDate);

        if (_DataItem.UsedItemGroupID != TrackerDb.CONST_INVALIDID)
        {
          _DataItem.LastItemDateChanged = pNewDeliveryDate;
          _result = Update(_DataItem) == string.Empty;
        }
      }
      return _result;
    }
    public UsedItemGroupTbl GetLastUsedItemID(long pContactID, int pItemID, DateTime pDeliveryDate)
    {
      UsedItemGroupTbl _DataItem = new UsedItemGroupTbl();
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pContactID, DbType.Int64, "@ContactID");
      _TDB.AddWhereParams(pItemID, DbType.Int32, "@LastItemTypeID");
      _TDB.AddWhereParams(pDeliveryDate, DbType.Date, "@LastItemDateChanged ");

      //"SELECT UsedItemGroupID, GroupItemTypeID, LastItemTypeSortPos, Notes FROM UsedItemGroupTbl WHERE ContactID = ? AND LastItemTypeID = ? AND LastItemDateChanged = ?";
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SELECTLASTUSEDITEMID);  

      if (_DataReader != null)
      {
        if (_DataReader.Read())
        {
          _DataItem.UsedItemGroupID = (_DataReader["UsedItemGroupID"] == DBNull.Value) ? TrackerDb.CONST_INVALIDID : Convert.ToInt32(_DataReader["UsedItemGroupID"]);
          _DataItem.ContactID = pContactID;
          _DataItem.GroupItemTypeID = (_DataReader["GroupItemTypeID"] == DBNull.Value) ? TrackerDb.CONST_INVALIDID : Convert.ToInt32(_DataReader["GroupItemTypeID"]);
          _DataItem.LastItemTypeID = pItemID;
          _DataItem.LastItemTypeSortPos = (_DataReader["LastItemTypeSortPos"] == DBNull.Value) ? 0 : Convert.ToInt32(_DataReader["LastItemTypeSortPos"]);
          _DataItem.LastItemDateChanged = pDeliveryDate;
          _DataItem.Notes = (_DataReader["Notes"] == DBNull.Value) ? string.Empty : _DataReader["Notes"].ToString();
        }
        _DataReader.Close();
      }
      _TDB.Close();

      return _DataItem;
    }
    public int ChangeItemIDToGroupIfItWas(long pContactID, int pItemID, DateTime pDeliveryDate)
    {

      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pContactID, DbType.Int64, "@ContactID");
      _TDB.AddWhereParams(pItemID, DbType.Int32, "@LastItemTypeID");
      _TDB.AddWhereParams(pDeliveryDate, DbType.Date, "@LastItemDateChanged ");

      //"SELECT UsedItemGroupID, GroupItemTypeID, LastItemTypeSortPos, Notes FROM UsedItemGroupTbl WHERE ContactID = ? AND LastItemTypeID = ? AND LastItemDateChanged = ?";
      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(CONST_SELECTLASTUSEDITEMID);

      if (_DataReader != null)
      {
        if (_DataReader.Read())
        {
          pItemID = (_DataReader["GroupItemTypeID"] == DBNull.Value) ? TrackerDb.CONST_INVALIDID : Convert.ToInt32(_DataReader["GroupItemTypeID"]);
        }
        _DataReader.Close();
      }
      _TDB.Close();

      return pItemID;
    }
  }
}
