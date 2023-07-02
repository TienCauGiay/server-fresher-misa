using MISA.WebFresher042023.Core.ExcelEntities.Employees;
using MISA.WebFresher042023.Core.Interfaces.Excels;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Excel
{
    /// <summary>
    /// Triển khai các phương thức nhập/xuất của thực thể employee
    /// </summary>
    /// Created By: BNTIEN (01/07/2023)
    public class EmployeeExcel : IEmployeeExcel
    {
        #region Methos
        /// <summary>
        /// Hàm nhập dữ liệu từ excel
        /// </summary>
        /// <param name="importExcels"></param>
        /// <returns>Số bản ghi insert thành công</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Created By: BNTIEN (01/07/2023)
        public int ImportExcel(List<EmployeeImportExcel> importExcels)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Hàm xuất dữ liệu ra excel
        /// </summary>
        /// <param name="exportExcels"></param>
        /// <returns>Số bản ghi xuất ra thành công</returns>
        /// Created By: BNTIEN (01/07/2023)
        public int ExportExcel(List<EmployeeExportExcel> exportExcels)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Hoặc LicenseContext.Commercial
            using (var package = new ExcelPackage())
            {
                try
                {
                    var worksheet = package.Workbook.Worksheets.Add("DANH SÁCH NHÂN VIÊN");

                    worksheet.Cells["A1:K1"].Merge = true;
                    worksheet.Cells["A1:K1"].Value = "DANH SÁCH NHÂN VIÊN";
                    worksheet.Cells["A1:K1"].Style.Font.Bold = true;
                    worksheet.Cells["A1:K1"].Style.Font.Size = 24;
                    worksheet.Cells["A1:K1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Định dạng tiêu đề
                    worksheet.Cells["A3:K3"].Style.Font.Bold = true;
                    worksheet.Cells["A3"].Value = "STT";
                    worksheet.Cells["B3"].Value = "Mã nhân viên";
                    worksheet.Cells["C3"].Value = "Họ tên";
                    worksheet.Cells["D3"].Value = "Giới tính";
                    worksheet.Cells["E3"].Value = "Ngày sinh";
                    worksheet.Cells["E3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["F3"].Value = "Số CMND";
                    worksheet.Cells["G3"].Value = "Chức danh";
                    worksheet.Cells["H3"].Value = "Tên đơn vị";
                    worksheet.Cells["I3"].Value = "Số tài khoản";
                    worksheet.Cells["J3"].Value = "Tên Ngân hàng";
                    worksheet.Cells["K3"].Value = "Chi Nhánh";

                    // Ghi dữ liệu
                    for (int i = 0; i < exportExcels.Count; i++)
                    {
                        var employee = exportExcels[i];
                        int rowIndex = i + 4; // Vị trí dòng bắt đầu từ dòng thứ 4

                        worksheet.Cells["A" + rowIndex].Value = i + 1; // Số thứ tự
                        worksheet.Cells["B" + rowIndex].Value = employee.EmployeeCode;
                        worksheet.Cells["C" + rowIndex].Value = employee.FullName;
                        worksheet.Cells["D" + rowIndex].Value = employee.GenderName;
                        worksheet.Cells["E" + rowIndex].Value = employee.DateOfBirth;
                        worksheet.Cells["F" + rowIndex].Value = employee.IdentityNumber;
                        worksheet.Cells["G" + rowIndex].Value = employee.PositionName;
                        worksheet.Cells["H" + rowIndex].Value = employee.DepartmentName;
                        worksheet.Cells["I" + rowIndex].Value = employee.BankAccount;
                        worksheet.Cells["J" + rowIndex].Value = employee.BankName;
                        worksheet.Cells["K" + rowIndex].Value = employee.BankBranch;

                        // Định dạng số thứ tự và căn giữa các ô dữ liệu
                        worksheet.Cells["A" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A" + rowIndex].Style.Numberformat.Format = "0";
                        worksheet.Cells["E" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["E" + rowIndex].Style.Numberformat.Format = "dd/MM/yyyy";
                    }
                    var dataRange = worksheet.Cells["A3:K" + (exportExcels.Count + 3)];
                    dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    // Đặt chiều rộng cột tự động hiển thị đủ nội dung
                    worksheet.Cells["A:K"].AutoFitColumns();

                    var fileName = "Danh_sach_nhan_vien.xlsx";
                    var filePath = Path.Combine("D:", fileName);
                    var fileExists = File.Exists(filePath);
                    var index = 1;

                    while (fileExists)
                    {
                        index++;
                        var newFileName = $"Danh_sach_nhan_vien({index}).xlsx";
                        filePath = Path.Combine("D:", newFileName);
                        fileExists = File.Exists(filePath);
                    }
                    package.SaveAs(new FileInfo(filePath));
                    return exportExcels.Count;
                }
                catch
                {
                    return 0;
                }
            }
        } 
        #endregion
    }
}
