
using System;
using System.Data;
using QOnT.classes;

namespace QOnT.control
{
  public class ContactEmailDetails
  {

    private string _FirstName, _LastName, _EmailAddress;
    private string _altFirstName, _altLastName, _altEmailAddress;

    public ContactEmailDetails()
    {
      _FirstName = _LastName = _EmailAddress = "";
      _altFirstName = _altLastName = _altEmailAddress = "";
    }

    public string FirstName { get { return _FirstName; } set { _FirstName = value; } }
    public string LastName { get { return _LastName; } set { _LastName = value; } }
    public string EmailAddress { get { return _EmailAddress; } set { _EmailAddress = value; } }
    public string altFirstName { get { return _altFirstName; } set { _altFirstName = value; } }
    public string altLastName { get { return _altLastName; } set { _altLastName = value; } }
    public string altEmailAddress { get { return _altEmailAddress; } set { _altEmailAddress = value; } }

    const string CONST_SQLGETCONTACTEMAILDETAILS = "SELECT ContactFirstName, ContactLastName, ContactAltFirstName, ContactAltLastName, EmailAddress, AltEmailAddress, CustomerID " +
                                               " FROM CustomersTbl WHERE (CustomerID = ?)";

    public ContactEmailDetails GetContactsEmailDetails(long pContactID)
    {
      ContactEmailDetails _contactEmailDetails = new ContactEmailDetails();
      TrackerDb _TDB = new TrackerDb();
      _TDB.AddWhereParams(pContactID, DbType.Int64, "@CustomerID");
      IDataReader _drEmailDetails = _TDB.ExecuteSQLGetDataReader(CONST_SQLGETCONTACTEMAILDETAILS);
      if (_drEmailDetails != null)
      {
        if (_drEmailDetails.Read())
        {
          _contactEmailDetails.FirstName = (_drEmailDetails["ContactFirstName"] == DBNull.Value) ? string.Empty : _drEmailDetails["ContactFirstName"].ToString();
          _contactEmailDetails.LastName = (_drEmailDetails["ContactLastName"] == DBNull.Value) ? string.Empty : _drEmailDetails["ContactLastName"].ToString();
          _contactEmailDetails.EmailAddress = (_drEmailDetails["EmailAddress"] == DBNull.Value) ? string.Empty : _drEmailDetails["EmailAddress"].ToString();
          _contactEmailDetails.altFirstName = (_drEmailDetails["ContactAltFirstName"] == DBNull.Value) ? string.Empty : _drEmailDetails["ContactAltFirstName"].ToString();
          _contactEmailDetails.altLastName = (_drEmailDetails["ContactAltLastName"] == DBNull.Value) ? string.Empty : _drEmailDetails["ContactAltLastName"].ToString();
          _contactEmailDetails.altEmailAddress = (_drEmailDetails["AltEmailAddress"] == DBNull.Value) ? string.Empty : _drEmailDetails["AltEmailAddress"].ToString();
        }
        _drEmailDetails.Close();
      }
      _TDB.Close();
      return _contactEmailDetails;
    }

    public ContactEmailDetails fuckyou()
    { return new ContactEmailDetails { FirstName = "fuck you" }; }
  }

}