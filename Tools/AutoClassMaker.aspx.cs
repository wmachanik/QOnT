using System;
using System.Collections.Generic;
using System.IO;
using System.Data.OleDb;
using System.Configuration;
using System.Data;

namespace QOnT.test
{
  public partial class AutoClassMaker : System.Web.UI.Page
  {
    const string CONST_CONSTRING = "Tracker08ConnectionString";
    const string CONST_NAMESPACE = "TrackerDotNet.control";
    const string SPC = "        "; 
    private Dictionary<OleDbType, dbTypesDef> _ColDBTypes;
    StreamWriter _ColsStream;


    private OleDbConnection OpenTrackerOleDBConnection()
    {
      OleDbConnection pConn = null;
      string _connectionString;

      if (ConfigurationManager.ConnectionStrings[CONST_CONSTRING] == null ||
          ConfigurationManager.ConnectionStrings[CONST_CONSTRING].ConnectionString.Trim() == "")
      {
        throw new Exception("A connection string named " + CONST_CONSTRING + " with a valid connection string " +
                            "must exist in the <connectionStrings> configuration section for the application.");
      }
      _connectionString = ConfigurationManager.ConnectionStrings[CONST_CONSTRING].ConnectionString;
      pConn = new OleDbConnection(_connectionString);

      return pConn;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        // load the drop down list with table names
        OleDbConnection _TableNamesConn = OpenTrackerOleDBConnection();
        if (_TableNamesConn != null)
        {
          try
          {
            _TableNamesConn.Open();

            DataTable _TableNamesSchema = _TableNamesConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            ddlTables.DataSource = _TableNamesSchema;
            ddlTables.DataTextField = "TABLE_NAME";
            ddlTables.DataBind();
          }
          catch (Exception ex)
          {
            throw new Exception("Error: " + ex.Message);
          }
          finally
          {
            _TableNamesConn.Close();
          }
        }

      }

    }

    protected void ddlTables_SelectedIndexChanged(object sender, EventArgs e)
    {
      // set the name of the class file
      tbxORMClassFileName.Text = ddlTables.SelectedValue + ".cs";
    }
      
    public struct dbTypesDef
    {
      public string typeName;  
      public string typeNil;
      public string typeConvert;
    }

    protected OleDbType GetOleDBType(string pRowDBType)
    {
      return (OleDbType)Int32.Parse(pRowDBType);
    }
    protected DbType GetDBType(string pRowDBType)
    {
      DbType _DbType = (DbType)Int32.Parse(pRowDBType);
      if (_DbType.ToString().Equals("130"))
        _DbType = DbType.String;
      return _DbType;
    }

    private Dictionary<OleDbType, dbTypesDef> GetColDBTypes()
    {
      Dictionary<OleDbType, dbTypesDef> _ColDBTypes = new Dictionary<OleDbType, dbTypesDef>();
      _ColDBTypes.Add(OleDbType.Binary, new dbTypesDef { typeName = "bool", typeNil = "false", typeConvert = "Convert.ToBoolean" });
      _ColDBTypes.Add(OleDbType.Boolean, new dbTypesDef { typeName = "bool", typeNil = "false", typeConvert = "Convert.ToBoolean" });
      _ColDBTypes.Add(OleDbType.BigInt, new dbTypesDef { typeName = "long", typeNil = "0", typeConvert = "Convert.ToInt64" });
      _ColDBTypes.Add(OleDbType.UnsignedBigInt, new dbTypesDef { typeName = "long", typeNil = "0", typeConvert = "Convert.ToInt64" });
      _ColDBTypes.Add(OleDbType.UnsignedTinyInt, new dbTypesDef { typeName = "byte", typeNil = "0", typeConvert = "Convert.ToByte" });
      _ColDBTypes.Add(OleDbType.TinyInt, new dbTypesDef { typeName = "int", typeNil = "0", typeConvert = "Convert.ToInt16" });
      _ColDBTypes.Add(OleDbType.Integer, new dbTypesDef { typeName = "int", typeNil = "0", typeConvert = "Convert.ToInt32" });
      _ColDBTypes.Add(OleDbType.Currency, new dbTypesDef { typeName = "double", typeNil = "0.0", typeConvert = "Convert.ToDouble" });
      _ColDBTypes.Add(OleDbType.Date, new dbTypesDef { typeName = "DateTime", typeNil = "System.DateTime.Now", typeConvert = "Convert.ToDateTime" });
      _ColDBTypes.Add(OleDbType.DBDate, new dbTypesDef { typeName = "DateTime", typeNil = "System.DateTime.Now", typeConvert = "Convert.ToDateTime" });
      _ColDBTypes.Add(OleDbType.DBTime, new dbTypesDef { typeName = "DateTime", typeNil = "System.DateTime.Now", typeConvert = "Convert.ToDateTime" });
      _ColDBTypes.Add(OleDbType.Double, new dbTypesDef { typeName = "double", typeNil = "0.0", typeConvert = "Convert.ToDouble" });
      _ColDBTypes.Add(OleDbType.Guid, new dbTypesDef { typeName = "long", typeNil = "0", typeConvert = "Convert.ToInt64" });
      _ColDBTypes.Add(OleDbType.Char, new dbTypesDef { typeName = "string", typeNil = "string.Empty", typeConvert = "" });
      _ColDBTypes.Add(OleDbType.WChar, new dbTypesDef { typeName = "string", typeNil = "string.Empty", typeConvert = "" });
      _ColDBTypes.Add(OleDbType.VarChar, new dbTypesDef { typeName = "string", typeNil = "string.Empty", typeConvert = "" });
      _ColDBTypes.Add(OleDbType.LongVarChar, new dbTypesDef { typeName = "string", typeNil = "string.Empty", typeConvert = "" });
      _ColDBTypes.Add(OleDbType.LongVarWChar, new dbTypesDef { typeName = "string", typeNil = "string.Empty", typeConvert = "" });
      _ColDBTypes.Add(OleDbType.Single, new dbTypesDef { typeName = "double", typeNil = "0.0", typeConvert = "Convert.ToDouble" });
      _ColDBTypes.Add(OleDbType.SmallInt, new dbTypesDef { typeName = "int16", typeNil = "0", typeConvert = "Convert.ToInt16" });
      _ColDBTypes.Add(OleDbType.Numeric, new dbTypesDef { typeName = "double", typeNil = "0.0", typeConvert = "Convert.ToDouble" });
      _ColDBTypes.Add(OleDbType.Decimal, new dbTypesDef { typeName = "double", typeNil = "0.0", typeConvert = "Convert.ToDouble" });
      _ColDBTypes.Add(OleDbType.IUnknown, new dbTypesDef { typeName = "var", typeNil = "null", typeConvert = "" });
      _ColDBTypes.Add(OleDbType.Empty, new dbTypesDef { typeName = "var", typeNil = "null", typeConvert = "" });

      return _ColDBTypes;
    }

    private string GetSELECTstring(DataRow[] pRows)
    {
      string _select = "    const string CONST_SQL_SELECT = \"SELECT ";
      // add each line
      string _selectRows = "";
      int i = 0;
      foreach (DataRow _row in pRows)
      {
        _selectRows += _row["COLUMN_NAME"].ToString() + ", ";
        i++;
        if (i == 8)
        {
          _selectRows += "\" + " + Environment.NewLine + "                            \" ";
          i = 0;
        }
      }

      _selectRows = _selectRows.Remove(_selectRows.Length -2,2);
      _select += _selectRows + " FROM " + ddlTables.SelectedValue + "\";";

      return _select;
    }
    private string GetINSERTstring(DataRow[] pRows)
    {
      string _insert = "    const string CONST_SQL_INSERT = \"INSERT INTO " + ddlTables.SelectedValue+ " (";
      // add each line
      string _insertFields = "";
      string _insertValues = "";
      int i = 0;
      foreach (DataRow _row in pRows)
      {
        // skip the ID field which should be the first field
        if (i == 0)
        {
          // do nothing for insert it should be an autoinc
        }
        else
        {
          _insertFields += _row["COLUMN_NAME"].ToString() + ", ";
          _insertValues += "?, ";
          if (i % 8 == 0)
          {
            _insertFields += "\" + " + Environment.NewLine + "                            \" ";
          }
        }
        i++;
      }

      _insertFields = _insertFields.Remove(_insertFields.Length - 2, 2)+")";
      _insertValues = _insertValues.Remove(_insertValues.Length - 2, 2);
      _insert += _insertFields +  "\" + " + Environment.NewLine + "                          \" VALUES ( " + _insertValues + ")\";   //id field not inserted";

      return _insert;
    }
    private string GetUPDATEstring(DataRow[] pRows)
    {
      string _update = "    const string CONST_SQL_UPDATE = \"UPDATE " + ddlTables.SelectedValue+ " SET ";
      // add each line
      string _updateFields = "";
      string _updateWhere = "";
      int i = 0;
      foreach (DataRow _row in pRows)
      {
        if (i == 0)
        {
          // this is the where clause field
          _updateWhere = " WHERE (" + _row["COLUMN_NAME"].ToString() + " = ?)";
        }
        else
        {
          _updateFields += _row["COLUMN_NAME"].ToString() + " = ?, ";
          if (i % 8 == 0)
          {
            _updateFields += "\" + " + Environment.NewLine + "                            \" ";
          }
        }
        i++;
      }

      _updateFields = _updateFields.Remove(_updateFields.Length - 2, 2);
      _update += _updateFields + "\" + " + Environment.NewLine + "                           \"" + _updateWhere + "\";";

      return _update;
    }
    private string GetDELETEstring(DataRow[] pRows)
    {
      string _delete = "    const string CONST_SQL_DELETE = \"DELETE FROM " + ddlTables.SelectedValue;

      _delete += " WHERE (" + pRows[0]["COLUMN_NAME"].ToString() + " = ?)\";";

      return _delete;
    }
    private void WriteGetAllProc(DataRow[] pRows)
    {
      _ColsStream.WriteLine("    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]");
      _ColsStream.WriteLine("    public List<" + ddlTables.SelectedValue + "> GetAll(string SortBy)");
      _ColsStream.WriteLine("    {");
      _ColsStream.WriteLine("      List<" + ddlTables.SelectedValue + "> _DataItems = new List<" + ddlTables.SelectedValue + ">();");
      _ColsStream.WriteLine("      TrackerDb _TDB = new TrackerDb();");
      _ColsStream.WriteLine("      string _sqlCmd = CONST_SQL_SELECT;");
      _ColsStream.WriteLine("      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += \" ORDER BY \" + SortBy;     // Add order by string");
      _ColsStream.WriteLine("      // params would go here if need");
      _ColsStream.WriteLine("      IDataReader _DataReader = _TDB.ExecuteSQLGetDataReader(_sqlCmd);");
      _ColsStream.WriteLine("      if (_DataReader != null)");
      _ColsStream.WriteLine("      {");
      _ColsStream.WriteLine("        while (_DataReader.Read())");
      _ColsStream.WriteLine("        {");
      _ColsStream.WriteLine("          " + ddlTables.SelectedValue + " _DataItem = new " + ddlTables.SelectedValue + "();");
      _ColsStream.WriteLine();
      _ColsStream.WriteLine("           #region StoreThisDataItem");
      // for each item assign the value
      foreach (DataRow _row in pRows)
      {
        OleDbType _thisType = GetOleDBType(_row["DATA_TYPE"].ToString());
        _ColsStream.Write("          _DataItem." + _row["COLUMN_NAME"].ToString() + " = (_DataReader[\"" + _row["COLUMN_NAME"].ToString() + "\"");
        _ColsStream.Write("] == DBNull.Value) ? " + _ColDBTypes[_thisType].typeNil + " : ");
        if (String.IsNullOrEmpty(_ColDBTypes[_thisType].typeConvert))
          _ColsStream.WriteLine("_DataReader[\"" + _row["COLUMN_NAME"].ToString() + "\"].ToString();");
        else
          _ColsStream.WriteLine(_ColDBTypes[_thisType].typeConvert + "(_DataReader[\"" + _row["COLUMN_NAME"].ToString() + "\"]);");
      }
      _ColsStream.WriteLine("          #endregion");
      _ColsStream.WriteLine("          _DataItems.Add(_DataItem);");
      //// ---- change Items _DataItems
      _ColsStream.WriteLine("        }");
      _ColsStream.WriteLine("        _DataReader.Close();");
      _ColsStream.WriteLine("      }");
      _ColsStream.WriteLine("      _TDB.Close();");
      _ColsStream.WriteLine("      return _DataItems;");
      _ColsStream.WriteLine("    }");
    }
    private void WriteInsertProc(DataRow[] pRows)
    {
      _ColsStream.WriteLine("    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert, true)]");
      _ColsStream.WriteLine("    public string Insert(" + ddlTables.SelectedValue +" p" +ddlTables.SelectedValue+")");
      _ColsStream.WriteLine("    {");
      _ColsStream.WriteLine("      string _result = string.Empty;");
      _ColsStream.WriteLine("      TrackerDb _TDB = new TrackerDb();");
      _ColsStream.WriteLine();
      _ColsStream.WriteLine("      #region InsertParameters");
      // for each item write the row
      int i = 0;
      foreach (DataRow _row in pRows)
      {
        if (i > 0)
        {
          _ColsStream.WriteLine("      _TDB.AddParams(p" + ddlTables.SelectedValue + "." + _row["COLUMN_NAME"].ToString() + ", DbType." + GetDBType(_row["DATA_TYPE"].ToString()) + ", \"@" + _row["COLUMN_NAME"].ToString() + "\");");
        }
        i++;
      }
      _ColsStream.WriteLine("      #endregion");
      _ColsStream.WriteLine("      // Now we have the parameters excute the SQL");
      _ColsStream.WriteLine("      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_INSERT);");
      //// ---- change Items _DataItems
      _ColsStream.WriteLine("      _TDB.Close();");
      _ColsStream.WriteLine("      return _result;");
      _ColsStream.WriteLine("    }");
      _ColsStream.WriteLine();
    }
    private void WriteUpdateProc(DataRow[] pRows)
    {
      _ColsStream.WriteLine("    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]");
      _ColsStream.WriteLine("    public string Update(" + ddlTables.SelectedValue +" p" +ddlTables.SelectedValue+")");
      _ColsStream.WriteLine("    { return Update(p" +ddlTables.SelectedValue+", 0); }");
      _ColsStream.WriteLine("    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, false)]");
      _ColsStream.WriteLine("    public string Update(" + ddlTables.SelectedValue +" p" +ddlTables.SelectedValue+", int pOrignal_"+pRows[0]["COLUMN_NAME"].ToString()+")");
      _ColsStream.WriteLine("    {");
      _ColsStream.WriteLine("      string _result = string.Empty;");
      _ColsStream.WriteLine("      TrackerDb _TDB = new TrackerDb();");
      _ColsStream.WriteLine();
      _ColsStream.WriteLine("      #region UpdateParameters");
      // for each item write the row
      int i = 0;
      foreach (DataRow _row in pRows)
      {
        if (i == 0)
        {
          _ColsStream.WriteLine("      if (pOrignal_"+pRows[0]["COLUMN_NAME"].ToString()+" > 0)");
          _ColsStream.WriteLine("        _TDB.AddWhereParams(pOrignal_"+pRows[0]["COLUMN_NAME"].ToString()+", DbType.Int32);  // check this line it assumes id field is int32");
          _ColsStream.WriteLine("      else");
          _ColsStream.WriteLine("        _TDB.AddWhereParams(p" + ddlTables.SelectedValue + "." + _row["COLUMN_NAME"].ToString() + ", DbType." + GetDBType(_row["DATA_TYPE"].ToString()) + ", \"@" + _row["COLUMN_NAME"].ToString() + "\");");
          _ColsStream.WriteLine("");
        }
        else
          _ColsStream.WriteLine("      _TDB.AddParams(p" + ddlTables.SelectedValue + "." + _row["COLUMN_NAME"].ToString() + ", DbType." + GetDBType(_row["DATA_TYPE"].ToString()) + ", \"@" + _row["COLUMN_NAME"].ToString() + "\" );");
        i++;
      }
      _ColsStream.WriteLine("      #endregion");
      _ColsStream.WriteLine("      // Now we have the parameters excute the SQL");
      _ColsStream.WriteLine("      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_UPDATE);");
      //// ---- change Items _DataItems
      _ColsStream.WriteLine("      _TDB.Close();");
      _ColsStream.WriteLine("      return _result;");
      _ColsStream.WriteLine("    }");
      _ColsStream.WriteLine();
    }
    private void WriteDeleteProc(DataRow[] pRows)
    {
      _ColsStream.WriteLine("    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, true)]");
      _ColsStream.WriteLine("    public string Delete(" + ddlTables.SelectedValue +" p" +ddlTables.SelectedValue+")");
      _ColsStream.WriteLine("    { return Delete(p" +ddlTables.SelectedValue+ "."+ pRows[0]["COLUMN_NAME"].ToString() + "); }");
      _ColsStream.WriteLine("    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete, false)]");
      _ColsStream.WriteLine("    public string Delete(int p" + pRows[0]["COLUMN_NAME"].ToString() + ")");
      _ColsStream.WriteLine("    {");
      _ColsStream.WriteLine("      string _result = string.Empty;");
      _ColsStream.WriteLine("      TrackerDb _TDB = new TrackerDb();");
      _ColsStream.WriteLine();
      _ColsStream.WriteLine("      _TDB.AddWhereParams(p" + pRows[0]["COLUMN_NAME"].ToString() + ", DbType.Int32, \"@"+ pRows[0]["COLUMN_NAME"].ToString()+"\");");  // check this line it assumes id field is int32");

      _ColsStream.WriteLine("      _result = _TDB.ExecuteNonQuerySQL(CONST_SQL_DELETE);");
      //// ---- change Items _DataItems
      _ColsStream.WriteLine("      _TDB.Close();");
      _ColsStream.WriteLine("      return _result;");
      _ColsStream.WriteLine("    }");
      _ColsStream.WriteLine();
    }

    protected DataRow[] GetDBRowDefinitions()
    {
      DataRow[] _rows = null;
      OleDbConnection _ColsConn = OpenTrackerOleDBConnection();
      try
      {
        _ColsConn.Open();
        DataTable _ColsSchema = _ColsConn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null , ddlTables.SelectedValue, null });
        // sort the schema ordinally so that the column names are in the same order as they appear in the database
        _rows = _ColsSchema.Select(null, "ORDINAL_POSITION", DataViewRowState.CurrentRows);
      }
      catch (Exception ex)
      {
        throw new Exception("Error: " + ex.Message);
      }
      finally
      {
        _ColsConn.Close();
      }
      return _rows;
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
      
      // create a Dictionary of types using the common DBType, add more if required
      //Dictionary<OleDbType, dbTypesDef>
      _ColDBTypes = GetColDBTypes();   // allocate the values to the private variable

      // open a file
      string _fName = "c:\\temp\\" + tbxORMClassFileName.Text  ;
      _ColsStream = new StreamWriter(_fName, false);  // create new file
      // first Write Header information
      _ColsStream.WriteLine("/// --- auto generated class for table: " + ddlTables.SelectedValue);
      _ColsStream.WriteLine("using System;   // for DateTime variables");
      _ColsStream.WriteLine("using System.Collections.Generic;   // for data stuff");
 //     _ColsStream.WriteLine("using System.Data.OleDb;");
      _ColsStream.WriteLine("using System.Data;                  // for IDataReader and DbType");
      _ColsStream.WriteLine("using QOnT.classes;        // TrackerDot classes used for DB access");

      _ColsStream.WriteLine();
      _ColsStream.WriteLine("namespace " + CONST_NAMESPACE);     // modify this if you gonna store in different location
      _ColsStream.WriteLine("{");

      _ColsStream.WriteLine("  public class " + ddlTables.SelectedValue );
      _ColsStream.WriteLine("  {");
      _ColsStream.WriteLine("    #region InternalVariableDeclarations");

      // ExportTableColumns to file
      DataRow[] _rows = GetDBRowDefinitions();
      foreach (DataRow _row in _rows)
      {
        // for do the private definitions
        OleDbType _thisType = GetOleDBType(_row["DATA_TYPE"].ToString());
        _ColsStream.Write("    private ");
        if (_ColDBTypes.ContainsKey(_thisType))
        {
          _ColsStream.Write(_ColDBTypes[_thisType].typeName);
        }
        else
          _ColsStream.Write(_thisType.ToString());

        _ColsStream.WriteLine(" _" + _row["COLUMN_NAME"].ToString() + ";");
      }
      // now define the class initializer
      _ColsStream.WriteLine("    #endregion");
      _ColsStream.WriteLine();
      _ColsStream.WriteLine("    // class definition");
      _ColsStream.WriteLine("    public " + ddlTables.SelectedValue + "()");
      _ColsStream.WriteLine("    {");
      // now do the intialization class;
      foreach (DataRow _row in _rows)
      {
        // for do the private definitions
        OleDbType _thisType = GetOleDBType(_row["DATA_TYPE"].ToString());

        if (_ColDBTypes.ContainsKey(_thisType))
          _ColsStream.WriteLine(String.Format("      _{0} = {1};", _row["COLUMN_NAME"].ToString(), _ColDBTypes[_thisType].typeNil));
        else
          _ColsStream.WriteLine(String.Format("      _{0} = {1};", _row["COLUMN_NAME"].ToString(), "1"));
      }
      _ColsStream.WriteLine("    }");
      // now each get and set
      _ColsStream.WriteLine("    #region PublicVariableDeclarations");
      foreach (DataRow _row in _rows)
      {
        // for do the private definitions
        OleDbType _thisType = GetOleDBType(_row["DATA_TYPE"].ToString());
        _ColsStream.Write("    public ");
        if (_ColDBTypes.ContainsKey(_thisType))
        {
          _ColsStream.Write(_ColDBTypes[_thisType].typeName);
        }
        else
          _ColsStream.Write(_thisType.ToString());

        _ColsStream.Write(" "+_row["COLUMN_NAME"].ToString());
        _ColsStream.Write(" { get { return _");
        _ColsStream.Write(_row["COLUMN_NAME"].ToString());
        _ColsStream.Write(";}  set { _");
        _ColsStream.Write(_row["COLUMN_NAME"].ToString());
        _ColsStream.WriteLine(" = value;} }");
      }
      _ColsStream.WriteLine("    #endregion");
      _ColsStream.WriteLine();
      // close class  - decided to do everything in the one class.
      //_ColsStream.WriteLine("  }");
      //// now a list all class with constants defining the SQL
      //_ColsStream.WriteLine("  public class " + ddlTables.SelectedValue + "DAL");
      //_ColsStream.WriteLine("  {");
      _ColsStream.WriteLine("    #region ConstantDeclarations");
//        _ColsStream.WriteLine("    const string CONST_CONSTRING = \"" + CONST_CONSTRING + "\";");

      _ColsStream.WriteLine(GetSELECTstring(_rows));
      _ColsStream.WriteLine(GetINSERTstring(_rows));
      _ColsStream.WriteLine(GetUPDATEstring(_rows));
      _ColsStream.WriteLine(GetDELETEstring(_rows));

        
      _ColsStream.WriteLine("    #endregion");
      _ColsStream.WriteLine();

      WriteGetAllProc(_rows);
      WriteInsertProc(_rows);
      WriteUpdateProc(_rows);
      WriteDeleteProc(_rows);

        _ColsStream.WriteLine("  }");
      _ColsStream.WriteLine("}");
      _ColsStream.Close();

    }

    protected void WriteTextBoxTemplate(string pFieldName, string pIndnt)
    {
      _ColsStream.WriteLine(pIndnt + "<asp:TemplateField ConvertEmptyStringToNull=\"False\" ");
      _ColsStream.WriteLine(pIndnt + "  HeaderText=\"" + pFieldName + "\" SortExpression=\"" + pFieldName + "\">");
      _ColsStream.WriteLine(pIndnt + "  <EditItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "    <asp:TextBox ID=\"" + pFieldName + "TextBox\" runat=\"server\" Text='<%# Bind(\"" + pFieldName + "\") %>' Width=\"10em\" />");
      _ColsStream.WriteLine(pIndnt + "  </EditItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "  <ItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "    <asp:Label ID=\"" + pFieldName + "Label\" runat=\"server\" Text='<%# Bind(\"" + pFieldName + "\") %>' />");
      _ColsStream.WriteLine(pIndnt + " </ItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "  <FooterTemplate>");
      _ColsStream.WriteLine(pIndnt + "    <asp:TextBox ID=\"" + pFieldName + "TextBox\" runat=\"server\"  Width=\"10em\" />");
      _ColsStream.WriteLine(pIndnt + "  </FooterTemplate>");
      _ColsStream.WriteLine(pIndnt + "</asp:TemplateField>");
    }
    protected void WriteCheckBoxTemplate(string pFieldName, string pIndnt)
    {
      _ColsStream.WriteLine(pIndnt + "<asp:TemplateField ConvertEmptyStringToNull=\"False\" ");
      _ColsStream.WriteLine(pIndnt + "  HeaderText=\"" + pFieldName + "\" SortExpression=\"" + pFieldName + "\">");
      _ColsStream.WriteLine(pIndnt + "  <EditItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "    <asp:CheckBox ID=\"" + pFieldName + "CheckBox\" runat=\"server\" Text=\"Yes\" Checked='<%# Bind(\"" + pFieldName + "\") %>' />");
      _ColsStream.WriteLine(pIndnt + "  </EditItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "  <ItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "    <asp:CheckBox ID=\"" + pFieldName + "CheckBox\" runat=\"server\" Text=\"Yes\" Checked='<%# Bind(\"" + pFieldName + "\") %>' />");
      _ColsStream.WriteLine(pIndnt + " </ItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "  <FooterTemplate>");
      _ColsStream.WriteLine(pIndnt + "    <asp:CheckBox ID=\"" + pFieldName + "CheckBox\" runat=\"server\" Text=\"Yes\" Checked='<%# Bind(\"" + pFieldName + "\") %>' />");
      _ColsStream.WriteLine(pIndnt + "  </FooterTemplate>");
      _ColsStream.WriteLine(pIndnt + "</asp:TemplateField>");
    }

    protected void WriteTemplateFields(DataRow[] pRows, string pIndnt)
    {
      _ColsStream.WriteLine(pIndnt + "<asp:TemplateField ShowHeader=\"False\">");
      _ColsStream.WriteLine(pIndnt + "  <EditItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "    <asp:Button ID=\"UpdateButton\" runat=\"server\" CausesValidation=\"True\" CommandName=\"Update\" Text=\"Update\" />");
      _ColsStream.WriteLine(pIndnt + "    &nbsp;<asp:Button ID=\"CancelButton\" runat=\"server\" CausesValidation=\"False\" CommandName=\"Cancel\" Text=\"Cancel\" />");
      _ColsStream.WriteLine(pIndnt + "  </EditItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "  <ItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "    <asp:Button ID=\"EditButton\" runat=\"server\" CausesValidation=\"False\" CommandName=\"Edit\" Text=\"Edit\" />");
      _ColsStream.WriteLine(pIndnt + "    &nbsp;<asp:Button ID=\"DeleteButton\" runat=\"server\" CausesValidation=\"False\" CommandName=\"Delete\" Text=\"Delete\" />");
      _ColsStream.WriteLine(pIndnt + "  </ItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "  <FooterTemplate>");
      _ColsStream.WriteLine(pIndnt + "    <asp:Button ID=\"AddButton\" runat=\"server\" CausesValidation=\"False\" CommandName=\"Add\" Text=\"Add\" />");
      _ColsStream.WriteLine(pIndnt + "  </FooterTemplate>");
      _ColsStream.WriteLine(pIndnt + "</asp:TemplateField>");

      string _FieldName = string.Empty;
      OleDbType _thisType = OleDbType.Empty;
      int i = 0;
      foreach (DataRow _row in pRows)
      {
        if (i == 0)
        {
          // do nothing
        }
        else
        {
          _FieldName = _row["COLUMN_NAME"].ToString();
          _thisType = GetOleDBType(_row["DATA_TYPE"].ToString());

          if (_thisType.Equals(OleDbType.Boolean))
            WriteCheckBoxTemplate(_FieldName, pIndnt);
          else
            WriteTextBoxTemplate(_FieldName, pIndnt);
        } //else
        i++;
      } // foreach
      // now add id field
      string _IdFieldName = pRows[0]["COLUMN_NAME"].ToString();
      _ColsStream.WriteLine(pIndnt + "<asp:TemplateField ConvertEmptyStringToNull=\"False\" ");
      _ColsStream.WriteLine(pIndnt + "  HeaderText=\"" + _IdFieldName + "\" SortExpression=\"" + _IdFieldName + "\">");
      _ColsStream.WriteLine(pIndnt + "  <EditItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "    <asp:Literal ID=\"" + _IdFieldName + "Literal\" runat=\"server\" Text='<%# Bind(\"" + _IdFieldName + "\") %>' />");
      _ColsStream.WriteLine(pIndnt + "  </EditItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "  <ItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "    <asp:Literal ID=\"" + _IdFieldName + "Literal\" runat=\"server\" Text='<%# Bind(\"" + _IdFieldName + "\") %>' />");
      _ColsStream.WriteLine(pIndnt + "  </ItemTemplate>");
      _ColsStream.WriteLine(pIndnt + "  <FooterTemplate>");
      _ColsStream.WriteLine(pIndnt + "    <asp:Literal ID=\"" + _IdFieldName + "Literal\" runat=\"server\" Text='<%# Bind(\"" + _IdFieldName + "\") %>' />");
      _ColsStream.WriteLine(pIndnt + "  </FooterTemplate>");
      _ColsStream.WriteLine(pIndnt + "</asp:TemplateField>");
 
    }
    protected void WriteEmptyTemplate(DataRow[] pRows, string pIndnt)
    {
      _ColsStream.WriteLine(pIndnt+"<EmptyDataTemplate>");
      string _FieldName = string.Empty;
      OleDbType _thisType = OleDbType.Empty;

      int i = 0;
      foreach (DataRow _row in pRows)
      {
        if (i == 0)
        {
          // do nothing
        }
        else
        {
          _FieldName = _row["COLUMN_NAME"].ToString();
          _thisType = GetOleDBType(_row["DATA_TYPE"].ToString());
          _ColsStream.Write(pIndnt+"  &nbsp;&nbsp;" + _FieldName + ":&nbsp;");
          if (_thisType.Equals(OleDbType.Boolean))
            _ColsStream.WriteLine("<asp:CheckBox ID=\"" + _FieldName + "CheckBox\" runat=\"server\" Text=\"Yes\" />");
          else
            _ColsStream.WriteLine("<asp:TextBox ID=\"" + _FieldName + "TextBox\" runat=\"server\" Width=\"10em\" />");
        } //else
          i++;
      } // foreach

    _ColsStream.WriteLine(pIndnt+"</EmptyDataTemplate>");

    }
    protected void WriteObjectDataSource(DataRow[] pRows, string pFormName, string pIndnt)
    {
      _ColsStream.WriteLine(pIndnt + "<asp:ObjectDataSource ID=\"ods"+pFormName+"\" runat=\"server\" TypeName=\"TrackerDotNet.control."+pFormName+"\"");
      _ColsStream.WriteLine(pIndnt + "  DataObjectTypeName=\"TrackerDotNet.control."+pFormName+"\" SelectMethod=\"GetAll\" SortParameterName=\"SortBy\"");
      _ColsStream.WriteLine(pIndnt + "  UpdateMethod=\"Update\" OldValuesParameterFormatString=\"original_{0}\" InsertMethod=\"Insert\" DeleteMethod=\"Delete\">");
      _ColsStream.WriteLine(pIndnt + "  <DeleteParameters>");
      _ColsStream.WriteLine(pIndnt + "    <asp:Parameter Name=\"p" + pRows[0]["COLUMN_NAME"].ToString() +"\" Type=\"Int32\" />");
      _ColsStream.WriteLine(pIndnt + "  </DeleteParameters>");
      _ColsStream.WriteLine(pIndnt + "  <SelectParameters>");
      _ColsStream.WriteLine(pIndnt + "    <asp:Parameter DefaultValue=\""+ pRows[1]["COLUMN_NAME"].ToString()+"\" Name=\"SortBy\" Type=\"String\" />");
      _ColsStream.WriteLine(pIndnt + "  </SelectParameters>");
      _ColsStream.WriteLine(pIndnt + "  <UpdateParameters>");
      _ColsStream.WriteLine(pIndnt + "    <asp:Parameter Name=\""+pFormName+"\" Type=\"Object\" DbType=\"Object\" />");
      _ColsStream.WriteLine(pIndnt + "    <asp:Parameter Name=\"pOrignal_" + pRows[0]["COLUMN_NAME"].ToString() + "\" Type=\"Int32\" />");
      _ColsStream.WriteLine(pIndnt + "  </UpdateParameters>");
      _ColsStream.WriteLine(pIndnt + "</asp:ObjectDataSource>");

    }

    protected void WriteRowLogic(DataRow[] pRows, string pFormName, string pIndnt)
    {
      OleDbType _thisType = OleDbType.Empty;
      int _origIndntLen = pIndnt.Length;

      string _FieldName = pRows[1]["COLUMN_NAME"].ToString();
      _ColsStream.WriteLine(pIndnt+"TextBox _" + _FieldName +"TextBox = (TextBox)_row.FindControl(\"" + _FieldName + "TextBox\");");
      _ColsStream.WriteLine(pIndnt+"if ((_" + _FieldName + "TextBox != null) && (!String.IsNullOrEmpty(_" + _FieldName + "TextBox.Text)))");
      _ColsStream.WriteLine(pIndnt+"{");
      pIndnt += "  ";
      _ColsStream.WriteLine(pIndnt + "control." + pFormName + "Tbl _" + pFormName + " = new control." + pFormName + "Tbl();");
      _FieldName = pRows[0]["COLUMN_NAME"].ToString();
      _ColsStream.WriteLine(pIndnt + "Literal _" + _FieldName + "Literal = (Literal)_row.FindControl(\"" + _FieldName + "Literal\");");
      _ColsStream.WriteLine(pIndnt + "_" + pFormName + "." + _FieldName + " = (_" + _FieldName + "Literal != null) ? Convert.ToInt32(_" + _FieldName + "Literal.Text) : 0;");
      _ColsStream.WriteLine(pIndnt + "if (e.CommandName.Equals(\"Delete\"))");
      _ColsStream.WriteLine(pIndnt + "{");
      _ColsStream.WriteLine(pIndnt + "  _" + pFormName + ".Delete(_" + pFormName + "." + _FieldName + ");");
      _ColsStream.WriteLine(pIndnt + "}");
      _ColsStream.WriteLine(pIndnt + "else");
      _ColsStream.WriteLine(pIndnt + "{");
      pIndnt += "  ";
      for (int i = 2; i < pRows.Length; i++)
			{
        _FieldName = pRows[i]["COLUMN_NAME"].ToString();
        _thisType = GetOleDBType(pRows[i]["DATA_TYPE"].ToString());
        if (_thisType.Equals(OleDbType.Boolean))
          _ColsStream.WriteLine(pIndnt + "CheckBox _" + _FieldName + " = (CheckBox)_row.FindControl(\"" + _FieldName + "CheckBox\");");
        else
          _ColsStream.WriteLine(pIndnt+"TextBox _" + _FieldName +"TextBox = (TextBox)_row.FindControl(\"" + _FieldName + "TextBox\");");
        i++;
			}

      int j = 0;
      foreach (DataRow _row in pRows)
      {
        _FieldName = _row["COLUMN_NAME"].ToString();
        if (j == 0)
        {
          // done already
        }
        else
        {
          _thisType = GetOleDBType(_row["DATA_TYPE"].ToString());
          if (_thisType.Equals(OleDbType.Boolean))
            _ColsStream.WriteLine(pIndnt + "_" + pFormName + "." + _FieldName + " = (_" + _FieldName + "CheckBox != null) ? _" + _FieldName + "CheckBox.Checked : false;");
          else
            _ColsStream.WriteLine(pIndnt + "_" + pFormName + "." + _FieldName + " = (_" + _FieldName + "TextBox != null) ? _" + _FieldName + "TextBox.Text : string.Empty");
        }
      }
      _FieldName = pRows[0]["COLUMN_NAME"].ToString();  // id field set it to this since we reuse this
      
      _ColsStream.WriteLine(pIndnt+"if (e.CommandName.Equals(\"Add\") || e.CommandName.Equals(\"Insert\"))");
      _ColsStream.WriteLine(pIndnt+"{");
      _ColsStream.WriteLine(pIndnt+"  _" + pFormName + ".Insert(_" + pFormName + ");");
      _ColsStream.WriteLine(pIndnt+"}");
      _ColsStream.WriteLine(pIndnt+"else if (e.CommandName.Equals(\"Update\"))");
      _ColsStream.WriteLine(pIndnt+"{");
      _ColsStream.WriteLine(pIndnt+"  _" + pFormName + ".Update(_" + pFormName + ", _" + pFormName + "." + _FieldName + ");");
      _ColsStream.WriteLine(pIndnt+"}");

      _ColsStream.WriteLine(pIndnt.Remove(1,2)+"}");

      pIndnt = pIndnt.Remove(1, pIndnt.Length - _origIndntLen); // take indent back to the beginning
      _ColsStream.WriteLine(pIndnt+"}");
    }
    protected void WriteView(bool pIsGridView)
    {
      string _ViewType = (pIsGridView) ? "GridView" : "DetailsView";
      string _ViewTypePrefix = (pIsGridView) ? "gv" : "dv";

      _ColDBTypes = GetColDBTypes();   // allocate the values to the private variable

      // open a file
      string _fName = "c:\\temp\\" + tbxORMClassFileName.Text;
      if (_fName.Contains(".cs"))
      {
        _fName = _fName.Replace(".cs", ".aspx");
      }
      _ColsStream = new StreamWriter(_fName, false);  // create new file
      string _TableName = ddlTables.SelectedValue;
      string _FormName = (_TableName.Contains("Tbl")) ? _TableName.Remove(_TableName.IndexOf("Tbl"), 3) : _TableName;
      DataRow[] _rows = GetDBRowDefinitions();

      _ColsStream.WriteLine("// copy and past the code below into the area you want to place the " + _ViewType + "");

      _ColsStream.WriteLine(SPC + "<asp:UpdateProgress runat=\"server\" ID=\"" + _ViewTypePrefix + _FormName + "UpdateProgress AssociatedUpdatePanelID=\"" + _ViewTypePrefix + "" + _TableName + "Panel\" >");
      _ColsStream.WriteLine(SPC + "  <ProgressTemplate>");
      _ColsStream.WriteLine(SPC + "     Please Wait&nbsp;<img src=\"../images/animi/QuaffeeProgress.gif\" alt=\"Please Wait...\" />&nbsp;...");
      _ColsStream.WriteLine(SPC + "  </ProgressTemplate>");
      _ColsStream.WriteLine(SPC + "</asp:UpdateProgress>");
      _ColsStream.WriteLine(SPC + "<asp:UpdatePanel ID=\"" + _ViewTypePrefix + _FormName + "UpdatePanel\" runat=\"server\" ChildrenAsTriggers=\"true\">");
      _ColsStream.WriteLine(SPC + "  <ContentTemplate>");
      _ColsStream.WriteLine(SPC + "    <asp:" + _ViewType + " ID=\"" + _ViewTypePrefix + _FormName + "\" runat=\"server\" AllowSorting=\"True\" DataSourceID=\"ods" + _FormName + "\" DataKeyNames=\"" + _rows[0]["COLUMN_NAME"].ToString() + "\"");
      _ColsStream.WriteLine(SPC + "       CssClass=\"TblWhite\" AutoGenerateColumns=\"False\" ShowFooter=\"True\"");
      _ColsStream.WriteLine(SPC + "       OnRowCommand=\"" + _ViewTypePrefix + _FormName + "_RowCommand\">");
      
      if (pIsGridView)
        _ColsStream.WriteLine(SPC + "    <Columns>");
      else
        _ColsStream.WriteLine(SPC + "    <Fields>");

      WriteTemplateFields(_rows, SPC + "      ");
      if (pIsGridView)
        _ColsStream.WriteLine(SPC + "    </Columns>");
      else
        _ColsStream.WriteLine(SPC + "    </Fields>");

      WriteEmptyTemplate(_rows, SPC + "    ");

      _ColsStream.WriteLine(SPC + "    </asp:" + _ViewType + ">");
      _ColsStream.WriteLine(SPC + "  </ContentTemplate>");
      _ColsStream.WriteLine(SPC + "</asp:UpdatePanel>");

      WriteObjectDataSource(_rows, _FormName, SPC);

      _ColsStream.Close();

      if (_fName.Contains(".aspx"))
      {
        _fName = _fName.Replace(".aspx", ".aspx.cs");
      }

      _ColsStream = new StreamWriter(_fName, false);  // create new file

      _ColsStream.WriteLine("    protected void " + _ViewTypePrefix + _FormName + "_RowCommand(object sender, " + _ViewType + "CommandEventArgs e)");
      _ColsStream.WriteLine("    {");
      _ColsStream.WriteLine("      " + _ViewType + "Row _row = (" + _ViewType + "Row)((Control)e.CommandSource).NamingContainer;");
      _ColsStream.WriteLine("      if (_row != null)");
      _ColsStream.WriteLine("      {");
      WriteRowLogic(_rows, _FormName, "        ");
      _ColsStream.WriteLine("        " + _ViewTypePrefix + _FormName + ".DataBind();");
      _ColsStream.WriteLine("      }");
      _ColsStream.WriteLine("    }");

      _ColsStream.Close();
    }
    protected void btnCreateGV_Click(object sender, EventArgs e)
    {
      WriteView(true);
    }
    protected void btnCreateDV_Click(object sender, EventArgs e)
    {
      WriteView(false);
    }
  }
}

/*
       _ColsStream.WriteLine("    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]");
        _ColsStream.WriteLine("    public List<" + ddlTables.SelectedValue + "> GetAll(string SortBy)");
        _ColsStream.WriteLine("    {");
        _ColsStream.WriteLine("      List<" + ddlTables.SelectedValue + "> _DataItems = new List<" + ddlTables.SelectedValue +">();");
        _ColsStream.WriteLine("      TrackerDb _TDB = new TrackerDb();");
        _ColsStream.WriteLine("      string _sqlCmd = CONST_SQL_SELECT;");
        _ColsStream.WriteLine("      if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += \" ORDER BY \" + SortBy;     // Add order by string");
        _ColsStream.WriteLine("      // params would go here if need");
        _ColsStream.WriteLine("      IDataReader _DataReader = _TDB.ReturnDataReader(_sqlCmd);");
        _ColsStream.WriteLine();
        _ColsStream.WriteLine("      while (_DataReader.Read())");
        _ColsStream.WriteLine("      {");
        _ColsStream.WriteLine("        " + ddlTables.SelectedValue + " _DataItem = new " + ddlTables.SelectedValue + "();");
        _ColsStream.WriteLine();
        // for each item assign the value
        foreach (DataRow _row in _rows)
        {
          OleDbType _thisType = GetOleDBType(_row["DATA_TYPE"].ToString());
          _ColsStream.Write("        _DataItem." + _row["COLUMN_NAME"].ToString() + " = (_DataReader[\"" + _row["COLUMN_NAME"].ToString() + "\"");
          _ColsStream.Write("] == DBNull.Value) ? " + _ColDBTypes[_thisType].typeNil + " : ");
          if (String.IsNullOrEmpty(_ColDBTypes[_thisType].typeConvert))
            _ColsStream.WriteLine("_DataReader[\"" + _row["COLUMN_NAME"].ToString() + "\"].ToString();");
          else
            _ColsStream.WriteLine(_ColDBTypes[_thisType].typeConvert  + "(_DataReader[\"" + _row["COLUMN_NAME"].ToString() + "\"]);");
        }
        _ColsStream.WriteLine("        _DataItems.Add(_DataItem);");
          //// ---- change Items _DataItems
        _ColsStream.WriteLine("      }");
        _ColsStream.WriteLine("      _DataReader.Close();");
        _ColsStream.WriteLine("      _TDB.Close();");
        _ColsStream.WriteLine("      return _DataItems;");
        _ColsStream.WriteLine("    }");
 
 
*/

/*

        _ColsStream.WriteLine("      string _connectionStr = ConfigurationManager.ConnectionStrings[CONST_CONSTRING].ConnectionString;");
        _ColsStream.WriteLine();
        _ColsStream.WriteLine("      using (OleDbConnection _conn = new OleDbConnection(_connectionStr))");
        _ColsStream.WriteLine("      {");
        _ColsStream.WriteLine("        if (!String.IsNullOrEmpty(SortBy)) _sqlCmd += \" ORDER BY \" + SortBy;     // Add order by string");
        _ColsStream.WriteLine("        OleDbCommand _cmd = new OleDbCommand(_sqlCmd, _conn);                    // run the query we have built");
        _ColsStream.WriteLine("        _conn.Open();");
        _ColsStream.WriteLine("        OleDbDataReader _DataReader = _cmd.ExecuteReader();");


*/