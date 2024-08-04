using OfficeOpenXml;

namespace StaffManagement.Portal.Excel.ExportExcel
{
    public abstract class Exporter
    {
        public abstract Task<ExcelWorksheet> ExportStaffAsync(StaffExportParam param);
    }
}