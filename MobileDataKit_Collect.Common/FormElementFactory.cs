using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit_Collect.Common
{
  public  class FormElementFactory
    {

        private  int ExcelHandle = 1;

        private  List<FormElement> CreateElements( IWorksheet worksheet)
        {

            var elements = new List<FormElement>();
            while (ExcelHandle < worksheet.Rows.Length)
            {



                var formelement = new FormElement();
                formelement.ID = Guid.NewGuid().ToString();
                formelement.Type = worksheet.Rows[ExcelHandle].Cells[0].Value2.ToString();
                if(formelement.Type == "end_group")
                {
                    ExcelHandle = ExcelHandle + 1;
                    return elements;
                }
                formelement.Name = worksheet.Rows[ExcelHandle].Cells[1].Value2.ToString();
                formelement.Label = worksheet.Rows[ExcelHandle].Cells[2].Value2.ToString();
                elements.Add(formelement);
                ExcelHandle = ExcelHandle + 1;
                if (formelement.Type == "begin_group")
                {
                    formelement.Type = "Group";
                    foreach (var t in CreateElements(worksheet))
                    {
                        t.ParentFormElement = formelement;

                        formelement.Fields.Add(t);
                    }
                        
                }
                   
                


            }

            return elements;
        }
        public  Project Create(byte[] file)
        {

            var proj= new Project {ID =Guid.NewGuid().ToString() };

            using (var mem=new System.IO.MemoryStream(file))
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
               
                //Instantiate the Excel application object
                IApplication application = excelEngine.Excel;

                application.Workbooks.Open(mem);
                //Assigns default application version
                application.DefaultVersion = ExcelVersion.Excel2016;
                application.UseFastRecordParsing = true;
               
                //A new workbook is created equivalent to creating a new workbook in Excel
                //Create a workbook with 1 worksheet
                IWorkbook workbook = application.Workbooks[0];

                
                //Access first worksheet from the workbook
                IWorksheet worksheet = workbook.Worksheets[0];
               
                ExcelHandle = 1;

                foreach (var el in CreateElements(worksheet))
                {

                    el.Project = proj;
                    proj.FormElements.Add(el);
                }
                  

                //Adding text to a cell
                worksheet.Range["A1"].Text = "Hello World";

                //Saving the workbook to disk in XLSX format
               // workbook.SaveAs("Sample.xlsx");
            }

            return proj;


        }
    }
}
