
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace StaffManagement.Portal.Excel.ExportExcel
{
    public class ExcelExporter : Exporter
    {
        public override Task<ExcelWorksheet> ExportStaffAsync(StaffExportParam param)
        {
            try
            {
                var scheduleSheet = param.ExcelPackage.Workbook.Worksheets.Add("Staff");
                var headerStyle = param.ExcelPackage.Workbook.Styles.CreateNamedStyle("HeaderStyle");
                headerStyle.Style.Font.Bold = true;
                headerStyle.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                headerStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerStyle.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gainsboro);

                if (scheduleSheet != null)
                {
                    scheduleSheet.Cells["A2"].Value = "Staff Id";
                    scheduleSheet.Cells["A2"].StyleName = "HeaderStyle";

                    scheduleSheet.Cells["B2"].Value = "Full Name";
                    scheduleSheet.Cells["B2"].StyleName = "HeaderStyle";

                    scheduleSheet.Cells["C2"].Value = "Gender";
                    scheduleSheet.Cells["C2"].StyleName = "HeaderStyle";

                    scheduleSheet.Cells["D2"].Value = "Birthday";
                    scheduleSheet.Cells["D2"].StyleName = "HeaderStyle";

                    var rowIndex = 3;

                    foreach (var dealer in param.StaffModel.Data!)
                    {
                        scheduleSheet.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        scheduleSheet.Cells["A" + rowIndex].Value = dealer.StaffId;
                        scheduleSheet.Cells["B" + rowIndex].Value = dealer.FullName;
                        scheduleSheet.Cells["C" + rowIndex].Value = dealer.Gender == 1 ? "Male" : "Female";
                        scheduleSheet.Cells["D" + rowIndex].Style.Numberformat.Format = "yyyy-MM-dd";
                        scheduleSheet.Cells["D" + rowIndex].Value = dealer.Birthday;
                        rowIndex++;
                    }
                    scheduleSheet.Cells[scheduleSheet.Dimension.Address].AutoFitColumns();
                    return Task.FromResult(scheduleSheet);
                }
                throw new Exception("Worksheet can't null");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
