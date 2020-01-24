using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace QOnT.classes
{
  public class LogFile
  {
    private StringBuilder _LogLines;
    private string _LogFileName;
    private bool _AppendFile;
    private bool _NewLine;

    public LogFile(string pLogFileName, bool pAppendFile)
    {
      _LogLines = new StringBuilder();
      _LogFileName = pLogFileName;
      _AppendFile = pAppendFile;
      _NewLine = true;
    }

    public StringBuilder LogLines { get { return _LogLines; } set { value = _LogLines; } }

    public void AddToLog(string pLine)
    {
      if (_NewLine)
      {
        _LogLines.AppendFormat("{0:d}, ", DateTime.Now.Date);
        _NewLine = false;
      }
      
      _LogLines.Append(pLine);
    }
    public void AddFormatStringToLog(string pFormatString, object pObj1)
    {
      AddToLog(String.Format(pFormatString, pObj1));
    }
    public void AddFormatStringToLog(string pFormatString, object pObj1, object pObj2)
    {
      AddToLog(String.Format(pFormatString, pObj1, pObj2));
    }
    public void AddFormatStringToLog(string pFormatString, object pObj1, object pObj2, object pObj3)
    {
      AddToLog(String.Format(pFormatString,  pObj1, pObj2, pObj3));
    }
    public void AddFormatStringToLog(string pFormatString, object pObj1, object pObj2, object pObj3, object pObj4)
    {
      AddToLog(String.Format(pFormatString,  pObj1, pObj2, pObj3, pObj4));
    }
    public void AddFormatStringToLog(string pFormatString, object pObj1, object pObj2, object pObj3, object pObj4, object pObj5)
    {
      AddToLog(String.Format(pFormatString,  pObj1, pObj2, pObj3, pObj4, pObj5));
    }
    public void AddLineToLog(string pLine)
    {
      if (_NewLine)
        _LogLines.AppendFormat("{0:d}, ", DateTime.Now.Date);

      _LogLines.Append(pLine);
      _LogLines.AppendLine();
      _NewLine = true;
    }
    public void AddLineFormatStringToLog(string pFormatString, object pObj1)
    {
      AddLineToLog(String.Format(pFormatString, pObj1));
    }
    public void AddLineFormatStringToLog(string pFormatString, object pObj1, object pObj2)
    {
      AddLineToLog(String.Format(pFormatString,  pObj1, pObj2));
    }
    public void AddLineFormatStringToLog(string pFormatString, object pObj1, object pObj2, object pObj3)
    {
      AddLineToLog(String.Format(pFormatString,  pObj1, pObj2, pObj3));
    }
    public void AddLineFormatStringToLog(string pFormatString, object pObj1, object pObj2, object pObj3, object pObj4)
    {
      AddLineToLog(String.Format(pFormatString,  pObj1, pObj2, pObj3, pObj4));
    }
    public void AddLineFormatStringToLog(string pFormatString, object pObj1, object pObj2, object pObj3, object pObj4, object pObj5)
    {
      AddLineToLog(String.Format(pFormatString,  pObj1, pObj2, pObj3, pObj4, pObj5));
    }
    /// <summary>
    /// Write the lines to a log file return string.empty if no error
    /// </summary>
    /// <returns>string.empty if no error otherwise returns the error</returns>
    public string WriteLinesToLogFile()
    {
      string _errString = string.Empty;

      try
      {
        using (StreamWriter _Write = new StreamWriter(_LogFileName, _AppendFile))
        {
          _Write.Write(_LogLines);
        }
        _LogLines.Clear(); // they are written so clear them
        _AppendFile = true; // if this in not the last time this will be called append the file next time
      }
      catch (Exception _ex)
      {
        _errString = _ex.Message;
      }

      return _errString;
    }

  }
}