using OfficeOpenXml;
using StaffManagement.Portal.Commons;
using StaffManagement.Portal.Models.Staff;

namespace StaffManagement.Portal.Excel.ExportExcel
{
    public class StaffExportParam
    {
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public ExcelPackage ExcelPackage { get; set; } = new ExcelPackage();
        public PaginatedResponseModel<StaffModel> StaffModel { get; set; } = new();
    }
}
