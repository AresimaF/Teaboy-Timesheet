using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaboyTimesheet
{
    public class FileMinion
    {
        string DocumentsPath;
        string DefaultLogFolderPath;
        string DefaultLogFilePath;

        SpreadsheetDocument Logfile;

        //Workbook = File/Document
        //Worksheet = Specific Table
        //Parts = ???

        /*
            TODO:

        Create a worksheet INSIDE the file on initial creation
        Do a check for last worksheet month, create worksheets for each month
        Append to worksheet a new row with current input info

        */


        public FileMinion()
        {
            DocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            DefaultLogFolderPath = Path.Combine(DocumentsPath, "Teaboy Timesheet");

            if (!Directory.Exists(DefaultLogFolderPath))
            {
                Directory.CreateDirectory(DefaultLogFolderPath);
            }
            if (!File.Exists(DefaultLogFilePath))
            {
                CreateNewLogFile();
            }

            OpenLogFile();
            
        }

        private void CreateNewLogFile()
        {
            SpreadsheetDocument.Create(DefaultLogFilePath, SpreadsheetDocumentType.Workbook);
        }

        private void OpenLogFile()
        {
            Logfile = SpreadsheetDocument.Open(DefaultLogFilePath, true);
        }

        private void AddNewEntry(string clientName, decimal hoursLogged, DateTime logTime)
        {
            SheetData sheetData = Logfile.Worksheet.GetFirstChild<SheetData>();
            Row lastRow = sheetData.Elements<Row>().LastOrDefault();

            if (lastRow != null)
            {
                sheetData.InsertAfter(new Row() { RowIndex = (lastRow.RowIndex + 1) }, lastRow);
            }
            else
            {
                sheetData.Insert(new Row() { RowIndex = 0 });
            }
        }
    }
    }
}
