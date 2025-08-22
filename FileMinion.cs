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

            // Add styles to the WorkbookPart
            WorkbookStylesPart StylePart = WBPart.AddNewPart<WorkbookStylesPart>();

            /* Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet() { Id = WBPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = Now.ToString("MMMM yyyy") };
            sheets.Append(sheet);*/

            WBPart.WorkbookStylesPart.Stylesheet = GenerateStyleSheet();
            Borders borders = WBPart.WorkbookStylesPart.Stylesheet.Elements<Borders>().First();
            borders.Append(GenerateBorder());


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
            lstColumns.Append(new Column() { Min = 2, Max = 2, Width = 15, CustomWidth = true });
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

        public Border GenerateBorder()
        {
            Border border2 = new Border();

            LeftBorder leftBorder2 = new LeftBorder() { Style = BorderStyleValues.Thin };
            Color color1 = new Color() { Indexed = (UInt32Value)64U };

            leftBorder2.Append(color1);

            RightBorder rightBorder2 = new RightBorder() { Style = BorderStyleValues.Thin };
            Color color2 = new Color() { Indexed = (UInt32Value)64U };

            rightBorder2.Append(color2);

            TopBorder topBorder2 = new TopBorder() { Style = BorderStyleValues.Thin };
            Color color3 = new Color() { Indexed = (UInt32Value)64U };

            topBorder2.Append(color3);

            BottomBorder bottomBorder2 = new BottomBorder() { Style = BorderStyleValues.Thin };
            Color color4 = new Color() { Indexed = (UInt32Value)64U };

            bottomBorder2.Append(color4);
            DiagonalBorder diagonalBorder2 = new DiagonalBorder();

            border2.Append(leftBorder2);
            border2.Append(rightBorder2);
            border2.Append(topBorder2);
            border2.Append(bottomBorder2);
            border2.Append(diagonalBorder2);

            return border2;
        }

        private Stylesheet GenerateStyleSheet()
        {

            var stylesheet = new Stylesheet();

            //tried adding attributes too
            /*
            var stylesheet = new Stylesheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            stylesheet.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            stylesheet.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");
           */

            var fonts = new Fonts() { Count = (UInt32Value)1U, KnownFonts = BooleanValue.FromBoolean(true) };
            var font = new Font
            {
                FontSize = new FontSize() { Val = 11D },
                FontName = new FontName() { Val = "Calibri" },
                Color = new Color() { Theme = (UInt32Value)1U },
                FontFamilyNumbering = new FontFamilyNumbering() { Val = 2 },
                FontScheme = new FontScheme() { Val = new EnumValue<FontSchemeValues>(FontSchemeValues.Minor) }
            };
            fonts.Append(font);

            var fills = new Fills() { Count = 1 };
            var fill = new Fill();
            fill.PatternFill = new PatternFill() { PatternType = new EnumValue<PatternValues>(PatternValues.None) };
            fills.Append(fill);

            var borders = new Borders() { Count = 1 };
            Border border2 = new Border();

            LeftBorder leftBorder2 = new LeftBorder() { Style = BorderStyleValues.Thin };
            Color color1 = new Color() { Indexed = (UInt32Value)64U };

            leftBorder2.Append(color1);

            RightBorder rightBorder2 = new RightBorder() { Style = BorderStyleValues.Thin };
            Color color2 = new Color() { Indexed = (UInt32Value)64U };

            rightBorder2.Append(color2);

            TopBorder topBorder2 = new TopBorder() { Style = BorderStyleValues.Thin };
            Color color3 = new Color() { Indexed = (UInt32Value)64U };

            topBorder2.Append(color3);

            BottomBorder bottomBorder2 = new BottomBorder() { Style = BorderStyleValues.Thin };
            Color color4 = new Color() { Indexed = (UInt32Value)64U };

            bottomBorder2.Append(color4);
            DiagonalBorder diagonalBorder2 = new DiagonalBorder();
            borders.Append(border2);

            var cellFormats = new CellFormats(
                    new CellFormat() { NumberFormatId = 0, FormatId = 0, FontId = 0, FillId = 0, BorderId = 0 }, // Index 0
                    new CellFormat() { NumberFormatId = 0, FormatId = 0, FontId = 1, FillId = 0, BorderId = 0, ApplyFont = true }, //Index 1 Bold
                    new CellFormat() { NumberFormatId = 0, FormatId = 0, FontId = 2, FillId = 0, BorderId = 0, ApplyFont = true }, //Index 2 Italics
                    new CellFormat() { NumberFormatId = 22, FormatId = 0, FontId = 0, BorderId = 0, FillId = 0, ApplyNumberFormat = BooleanValue.FromBoolean(true) } //Index 3 Date
                    );


            stylesheet.Append(fonts);
            stylesheet.Append(fills);
            stylesheet.Append(borders);
            stylesheet.Append(cellFormats);

            return stylesheet;
        }
    }
}

