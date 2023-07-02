using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Repository
{
    /// <summary>
    /// class triển khai các phương thức của thực thể employee truy vấn cơ sở dữ liệu
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created By: BNTIEN (17/06/2023)
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Hàm tạo
        /// </summary>
        /// <param name="configuration"></param>
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        #region Method riêng (Employee)
        /// <summary>
        /// Lấy mã nhân viên lớn nhất trong hệ thống
        /// </summary>
        /// <returns>Mã nhân viên</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<string?> GetByCodeMaxAsync()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    var res = await connection.QueryFirstOrDefaultAsync<string>("Proc_Employee_GetCodeMax", commandType: CommandType.StoredProcedure);
                    return !string.IsNullOrEmpty(res) ? res.ToString() : "";
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Tìm kiếm và phân trang trên giao diện
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách nhân viên theo tìm kiếm, phân trang</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<FilterEmployee?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    textSearch = textSearch ?? string.Empty;
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@PageSize", pageSize);
                    parameters.Add("@PageNumber", pageNumber);
                    parameters.Add("@TextSearch", textSearch);
                    parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    var result = await connection.QueryAsync<Employee>("Proc_Employee_GetFilter", parameters, commandType: CommandType.StoredProcedure);
                    var totalRecord = parameters.Get<int>("@TotalRecord");

                    var currentPageRecords = 0;
                    if (pageNumber < Math.Ceiling((decimal)totalRecord / pageSize))
                    {
                        currentPageRecords = pageSize;
                    }
                    else if (pageNumber == Math.Ceiling((decimal)totalRecord / pageSize))
                    {
                        currentPageRecords = totalRecord - (pageNumber - 1) * pageSize;
                    }

                    return new FilterEmployee
                    {
                        TotalPage = (int)Math.Ceiling((decimal)totalRecord / pageSize),
                        TotalRecord = totalRecord,
                        CurrentPage = pageNumber,
                        CurrentPageRecords = currentPageRecords,
                        Data = result.ToList()
                    };
                }
                catch
                {
                    return new FilterEmployee 
                    {
                        TotalPage = 0,
                        TotalRecord = 0,
                        CurrentPage = 0,
                        CurrentPageRecords = 0,
                        Data = new List<Employee>()
                    };
                }
            }
        }
        #endregion
    }
}
