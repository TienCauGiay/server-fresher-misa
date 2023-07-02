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
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
        }

        #region Method riêng (Department)
        /// <summary>
        /// Tìm kiếm phòng ban theo tên
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<IEnumerable<Department>?> GetByName(string? textSearch)
        {
            textSearch = textSearch ?? string.Empty;
            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TextSearch", textSearch);

                    var res = await connection.QueryAsync<Department>("Proc_Department_GetByName", parameters, commandType: CommandType.StoredProcedure);
                    return res;
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
