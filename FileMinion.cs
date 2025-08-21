using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeaboyTimesheet
{
    public class FileMinion
    {
        string DocumentsPath;
        string DefaultLogFolderPath;
        string DefaultLogFilePath;

        DateTime Now;

        SpreadsheetDocument Logfile;
        Workbook Workbook;
        Sheet CurrentSheet;

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
            DefaultLogFilePath = Path.Combine(DefaultLogFolderPath, "TeaboyTimesheet.xlsx");

            Now = DateTime.Now;

            if (!Directory.Exists(DefaultLogFolderPath))
            {
                Directory.CreateDirectory(DefaultLogFolderPath);
            }
            if (!File.Exists(DefaultLogFilePath))
            {
                CreateNewLogFile();
            }
            else
            {
                OpenLogFile();
            }
            
        }

        private void CreateNewLogFile()
        {            
            SpreadsheetDocument newFile = SpreadsheetDocument.Create(DefaultLogFilePath, SpreadsheetDocumentType.Workbook);
            WorkbookPart WBPart = newFile.AddWorkbookPart();
            WBPart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = WBPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            Sheets sheets = WBPart.Workbook.AppendChild(new Sheets());

            /* Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet() { Id = WBPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = Now.ToString("MMMM yyyy") };
            sheets.Append(sheet);*/

            

            Logfile = newFile;
            Workbook = WBPart.Workbook;

            CreateNewSheet();

            newFile.Dispose();

            OpenLogFile();

        }

        private void OpenLogFile()
        {
            Logfile = SpreadsheetDocument.Open(DefaultLogFilePath, true);

            Workbook = Logfile.WorkbookPart.Workbook;

            Sheet monthlySheetSearch = null;

            monthlySheetSearch = (Sheet)Workbook.Sheets.ChildElements.Where(x => x.GetAttribute("name","").Value == Now.ToString("MMMM yyyy")).FirstOrDefault();



            if (monthlySheetSearch is null)
            {
                CurrentSheet = CreateNewSheet();
            }
            else
            {
                CurrentSheet = monthlySheetSearch;
            }

            WorksheetPart WSPart = Logfile.WorkbookPart.WorksheetParts.First();
            WSPart.Worksheet.Save();
            Workbook.Save();
            Logfile.Save();


        }

        private Sheet CreateNewSheet()
        {
            WorkbookPart WBPart = Logfile.WorkbookPart;
            WorksheetPart worksheetPart = Logfile.WorkbookPart.WorksheetParts.First();
            Sheets sheets = WBPart.Workbook.Sheets;
            Sheet sheet = new Sheet() { Id = WBPart.GetIdOfPart(worksheetPart), SheetId = Convert.ToUInt32(sheets.Count()), Name = Now.ToString("MMMM yyyy") };
            sheets.Append(sheet);

            Columns lstColumns = new Columns();
            lstColumns.Append(new Column() { Min = 1, Max = 1, Width = 25, CustomWidth = true });
            lstColumns.Append(new Column() { Min = 2, Max = 2, Width = 12, CustomWidth = true });
            lstColumns.Append(new Column() { Min = 3, Max = 3, Width = 12, CustomWidth = true });
            worksheetPart.Worksheet.InsertAt(lstColumns, 0);


            AddNewEntry("Client", "Hours Logged", "Date");

            return sheet;
        }

        public void AddNewEntry(string clientName, decimal hoursLogged, DateTime logTime)
        {

            WorksheetPart worksheetPart = Logfile.WorkbookPart.WorksheetParts.First();
            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            UInt32? rowIndex = sheetData.Elements<Row>().Last().RowIndex;

            if (rowIndex == null)
            {
                rowIndex = 0;
            }

            Row newRow = new Row() { RowIndex = rowIndex + 1 };

            Cell cell1 = new Cell() { CellReference = "A" + newRow.RowIndex.ToString() };
            cell1.CellValue = new CellValue(clientName);
            cell1.DataType = new EnumValue<CellValues>(CellValues.String);
            newRow.Append(cell1);

            Cell cell2 = new Cell() { CellReference = "B" + newRow.RowIndex.ToString() };
            cell2.CellValue = new CellValue(hoursLogged);
            cell2.DataType = new EnumValue<CellValues>(CellValues.Number);
            newRow.Append(cell2);

            Cell cell3 = new Cell() { CellReference = "C" + newRow.RowIndex.ToString() };
            cell3.CellValue = new CellValue(logTime.ToString("MM/dd/yyyy"));
            cell3.DataType = new EnumValue<CellValues>(CellValues.String);
            newRow.Append(cell3);

            sheetData.Append(newRow);

            worksheetPart.Worksheet.Save();
            Workbook.Save();
            Logfile.Save();

        }

        private void AddNewEntry(string entry1, string entry2, string entry3)
        {

            WorksheetPart worksheetPart = Logfile.WorkbookPart.WorksheetParts.First();
            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            Row lastRow = null;
            lastRow = sheetData.Elements<Row>().LastOrDefault();

            UInt32 rowIndex = 1;

            if (lastRow is null)
            {
            }
            else
            {
                rowIndex = lastRow.RowIndex + 1;
            }

                Row newRow = new Row() { RowIndex = rowIndex };

            Cell cell1 = new Cell() { CellReference = "A" + newRow.RowIndex.ToString() };
            cell1.CellValue = new CellValue(entry1);
            cell1.DataType = new EnumValue<CellValues>(CellValues.String);
            newRow.Append(cell1);

            Cell cell2 = new Cell() { CellReference = "B" + newRow.RowIndex.ToString() };
            cell2.CellValue = new CellValue(entry2);
            cell2.DataType = new EnumValue<CellValues>(CellValues.String);
            newRow.Append(cell2);

            Cell cell3 = new Cell() { CellReference = "C" + newRow.RowIndex.ToString() };
            cell3.CellValue = new CellValue(entry3);
            cell3.DataType = new EnumValue<CellValues>(CellValues.String);
            newRow.Append(cell3);

            sheetData.Append(newRow);

            worksheetPart.Worksheet.Save();
            Workbook.Save();
            Logfile.Save();

        }

        public void FreshLog()
        {
            Logfile.Dispose();

            System.IO.File.Move(DefaultLogFilePath, Path.Combine(DefaultLogFolderPath, "TeaboyTimesheet_Old_" + DateTime.Now.ToString("MM-dd-yy") + ".xlsx"));

            CreateNewLogFile();
        }
    }
}

