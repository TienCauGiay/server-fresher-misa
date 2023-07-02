using Dapper;
using Microsoft.Extensions.Configuration;
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
    /// class triển khai các phương thức chung truy vấn cơ sở dữ liệu
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created By: BNTIEN (17/06/2023)
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        /// <summary>
        /// Khai báo chuỗi kết nối với database
        /// </summary>
        protected readonly string connectionString;

        /// <summary>
        /// Kết nối tới database
        /// </summary>
        protected MySqlConnection connection;

        private string className = typeof(TEntity).Name;

        /// <summary>
        /// Hàm tạo, tiêm DI
        /// </summary>
        /// <param name="configuration"></param>
        public BaseRepository(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionString"];
        }

        #region Method chung
        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns>danh sách entities</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    var entities = await connection.QueryAsync<TEntity>($"Proc_{className}_GetAll", null, commandType: CommandType.StoredProcedure);
                    return entities;
                }
                catch 
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Lấy thông tin entities theo code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>entities theo code</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<TEntity?> GetByCodeAsync(string code)
        {
            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@code", code);

                    var entities = await connection.QueryFirstOrDefaultAsync<TEntity>($"Proc_{className}_GetByCode", parameters, commandType: CommandType.StoredProcedure);
                    return entities;
                }
                catch
                {
                    return default(TEntity);
                }
            }
        }

        /// <summary>
        /// Lấy thông tin entities theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>entities theo id</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", id);

                    var entities = await connection.QueryFirstOrDefaultAsync<TEntity>($"Proc_{className}_GetById", parameters, commandType: CommandType.StoredProcedure);
                    return entities;
                }
                catch { return default(TEntity); }
            }
        }

        /// <summary>
        /// Thêm mới 1 entities
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi thêm</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<int> InsertAsync(TEntity entity)
        {
            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    foreach (var prop in entity.GetType().GetProperties())
                    {
                        if (prop.Name.Contains($"{className}Id"))
                        {
                            parameters.Add($"@{className}Id", Guid.NewGuid());
                        }
                        else if (prop.Name.Contains("CreatedDate"))
                        {
                            parameters.Add($"@CreatedDate", DateTime.Now);
                        }
                        else
                        {
                            parameters.Add($"@{prop.Name}", prop.GetValue(entity));
                        }
                    }

                    var rowsAffected = await connection.ExecuteAsync($"Proc_{className}_Insert", parameters, commandType: CommandType.StoredProcedure);
                    return rowsAffected;
                }
                catch { return 0; }
            }
        }

        /// <summary>
        /// Cập nhật thông tin entities
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi sửa</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<int> UpdateAsync(TEntity entity, Guid id)
        {
            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    foreach (var prop in entity.GetType().GetProperties())
                    {
                        if (prop.Name.Contains("ModifiedDate"))
                        {
                            parameters.Add($"@ModifiedDate", DateTime.Now);
                        }
                        else
                        {
                            parameters.Add($"@{prop.Name}", prop.GetValue(entity));
                        }
                    }
                    var rowsAffected = await connection.ExecuteAsync($"Proc_{className}_Update", parameters, commandType: CommandType.StoredProcedure);
                    return rowsAffected;
                }
                catch { return 0; }
            }
        }

        /// <summary>
        /// Xóa thực thể theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi xóa</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<int> DeleteAsync(Guid id)
        {
            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", id);

                    var rowsAffected = await connection.ExecuteAsync($"Proc_{className}_Delete", parameters, commandType: CommandType.StoredProcedure);
                    return rowsAffected;
                }
                catch { return 0; }
            }
        }

        /// <summary>
        /// Xóa nhiều thực thể theo các id tương ứng
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>số hàng bị ảnh hưởng sau khi xóa</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            using(connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("ids", ids);
                string query = $"DELETE FROM {className} WHERE {className}Id IN @ids";
                var res = await connection.ExecuteAsync(query, parameters);
                return res;
            }
        }
        #endregion
    }
}
