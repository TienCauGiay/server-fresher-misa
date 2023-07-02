using AutoMapper;
using MISA.WebFresher042023.Core.DTO.Employees;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Exceptions;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.Services;
using MISA.WebFresher042023.Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.UnitTests.Core
{
    /// <summary>
    /// Class test các hàm trong employee service
    /// TextFixture: đánh dấu đây là class test
    /// </summary>
    /// Created By: BNTIEN (22/06/2023)
    [TestFixture]
    public class EmployeeServiceTests
    {
        /// <summary>
        /// Test hàm lấy tất cả danh sách nhân viên
        /// Trường hợp không có dữ liệu trả về
        /// </summary>
        /// <returns>null</returns>
        /// Created By: BNTIEN (22/06/2023)
        [Test]
        public async Task GetAllAsync_NullListEmployee_ReturnNull()
        {
            // Arrange
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            employeeRepository.GetAllAsync().ReturnsNull();

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            var actualResult = await employeeService.GetAllAsync();

            // Act & Assert
            Assert.That(actualResult, Is.Null);
            //Phải gọi dữ liệu từ database về r mới kiểm tra xem có null hay không
            await employeeRepository.Received(1).GetAllAsync();
            // Không cần map khi dữ liệu trả về là null
            mapper.Received(0).Map<List<EmployeeDto>>(null);
        }

        /// <summary>
        /// Test hàm lấy tất cả danh sách nhân viên
        /// Trường hợp có dữ liệu trả về
        /// </summary>
        /// <returns>danh sách EmployeeDto</returns>
        /// Created By: BNTIEN (22/06/2023)
        [Test]
        public async Task GetAllAsync_ValidListEmployee_ListEmployeeDto()
        {
            // Arrange
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            var listEmployee = new List<Employee>()
            {
                new Employee() {
                    EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145"),
                    EmployeeCode = "NV-0001",
                    FullName = "Test 1"
                },
                new Employee() {
                    EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6167"),
                    EmployeeCode = "NV-0002",
                    FullName = "Test 2"
                },
            };

            var listEmployeeDto = new List<EmployeeDto>()
            {
                new EmployeeDto() {
                    EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145"),
                    EmployeeCode = "NV-0001",
                    FullName = "Test 1"
                },
                new EmployeeDto() {
                    EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6167"),
                    EmployeeCode = "NV-0002",
                    FullName = "Test 2"
                },
            };

            employeeRepository.GetAllAsync().Returns(listEmployee);

            mapper.Map<List<EmployeeDto>>(Arg.Any<List<Employee>>()).Returns(listEmployeeDto);
            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            var actualResult = await employeeService.GetAllAsync();

            // Act & Assert
            Assert.That(actualResult, Is.EqualTo(listEmployeeDto));
            // Phải gọi dữ liệu từ database về r mới kiểm tra xem có null hay không
            await employeeRepository.Received(1).GetAllAsync();
            // Phải map dữ liệu trước khi trả về
            mapper.Received(1).Map<List<EmployeeDto>>(listEmployee);
        }

        /// <summary>
        /// Test hàm tìm kiếm nhân viên theo code
        /// Trường hợp không tìm thấy nhân viên thỏa mãn
        /// </summary>
        /// <returns>default EmployeeDto</returns>
        /// Created By: BNTIEN (22/06/2023)
        [Test]
        public async Task GetByCodeAsync_NullEmployee_DefaultEmployeeDto()
        {
            // Arrange
            var code = "NV-12345";
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            employeeRepository.GetByCodeAsync(code).ReturnsNull();

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            var actualResult = await employeeService.GetByCodeAsync(code);

            // Act & Assert
            Assert.That(actualResult, Is.EqualTo(default(EmployeeDto)));

            await employeeRepository.Received(1).GetByCodeAsync(code);

            mapper.Received(0).Map<EmployeeDto>(null);
        }

        /// <summary>
        /// Test hàm tìm kiếm nhân viên theo code
        /// Trường hợp tìm thấy nhân viên thỏa mãn
        /// </summary>
        /// <returns>EmployeeDto</returns>
        /// Created By: BNTIEN (22/06/2023)
        [Test]
        public async Task GetByCodeAsync_ValidEmployee_EmployeeDto()
        {
            // Arrange
            var code = "NV-12345";
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            var employee = new Employee()
            {
                EmployeeCode = code,
            };

            var employeeDto = new EmployeeDto()
            {
                EmployeeCode = code,
            };

            employeeRepository.GetByCodeAsync(code).Returns(employee);

            mapper.Map<EmployeeDto>(employee).Returns(employeeDto);

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            var actualResult = await employeeService.GetByCodeAsync(code);

            // Act & Assert
            Assert.That(actualResult, Is.EqualTo(employeeDto));

            await employeeRepository.Received(1).GetByCodeAsync(code);

            mapper.Received(1).Map<EmployeeDto>(employee);
        }

        /// <summary>
        /// Test hàm tìm kiếm nhân viên theo id
        /// Trường hợp không tìm thấy nhân viên thỏa mãn
        /// </summary>
        /// <returns>default EmployeeDto</returns>
        /// Created By: BNTIEN (22/06/2023)
        [Test]
        public async Task GetByIdAsync_NullEmployee_DefaultEmployeeDto()
        {
            // Arrange
            var id = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145");
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            employeeRepository.GetByIdAsync(id).ReturnsNull();

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            var actualResult = await employeeService.GetByIdAsync(id);

            // Act & Assert
            Assert.That(actualResult, Is.EqualTo(default(EmployeeDto)));

            await employeeRepository.Received(1).GetByIdAsync(id);

            mapper.Received(0).Map<EmployeeDto>(null);
        }

        /// <summary>
        /// Test hàm tìm kiếm nhân viên theo id
        /// Trường hợp tìm thấy nhân viên thỏa mãn
        /// </summary>
        /// <returns>EmployeeDto</returns>
        /// Created By: BNTIEN (22/06/2023)
        [Test]
        public async Task GetByIdAsync_ValidEmployee_EmployeeDto()
        {
            // Arrange
            var id = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145");
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            var employee = new Employee()
            {
                EmployeeId = id,
            };

            var employeeDto = new EmployeeDto()
            {
                EmployeeId = id,
            };

            employeeRepository.GetByIdAsync(id).Returns(employee);

            mapper.Map<EmployeeDto>(employee).Returns(employeeDto);

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            var actualResult = await employeeService.GetByIdAsync(id);

            // Act & Assert
            Assert.That(actualResult, Is.EqualTo(employeeDto));

            await employeeRepository.Received(1).GetByIdAsync(id);

            mapper.Received(1).Map<EmployeeDto>(employee);
        }

        /// <summary>
        /// Test hàm phân trang, tìm kiếm nhân viên
        /// Trường hợp không có dữ liệu thỏa mãn nội dung tìm kiếm
        /// </summary>
        /// <returns>null</returns>
        /// Created BY: BNTIEN (23/06/2023)
        [Test]
        public async Task GetFilterAsync_NullFilterEmployee_ReturnNull()
        {
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            int pageSize = 10;
            int pageNumber = 1;
            string textSearch = "abc";

            var filteredEmployee = new FilterEmployee()
            {
                Data = null,
            };

            employeeRepository.GetFilterAsync(pageSize, pageNumber, textSearch).Returns(filteredEmployee);

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            var actualResult = await employeeService.GetFilterAsync(pageSize, pageNumber, textSearch);

            // Act & Assert
            Assert.That(actualResult, Is.EqualTo(null));

            await employeeRepository.Received(1).GetFilterAsync(pageSize, pageNumber, textSearch);

            mapper.Received(0).Map<FilterEmployeeDto>(null);
        }

        /// <summary>
        /// Test hàm phân trang, tìm kiếm nhân viên
        /// Trường hợp có dữ liệu thỏa mãn nội dung tìm kiếm
        /// </summary>
        /// <returns>List FilterEmployeeDto</returns>
        /// Created BY: BNTIEN (23/06/2023)
        [Test]
        public async Task GetFilterAsync_ValidFilterEmployee_FilterEmployeeDto()
        {
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            int pageSize = 10;
            int pageNumber = 1;
            string textSearch = "abc";

            var filteredEmployee = new FilterEmployee()
            {
                Data = new List<Employee>()
                {
                    new Employee()
                    {
                        EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145"),
                        EmployeeCode = "NV-12345",
                        FullName= "Test1",
                    },
                    new Employee()
                    {
                        EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6132"),
                        EmployeeCode = "NV-56789",
                        FullName= "Test2",
                    },
                },
                TotalPage = 1,
                TotalRecord = 2,
                CurrentPage = 1,
                CurrentPageRecords = 2
            };
            var filteredEmployeeDto = new FilterEmployeeDto()
            {
                Data = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145"),
                        EmployeeCode = "NV-12345",
                        FullName= "Test1",
                    },
                    new EmployeeDto()
                    {
                        EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6132"),
                        EmployeeCode = "NV-56789",
                        FullName= "Test2",
                    },
                },
                TotalPage = 1,
                TotalRecord = 2,
                CurrentPage = 1,
                CurrentPageRecords = 2
            };

            employeeRepository.GetFilterAsync(pageSize, pageNumber, textSearch).Returns(filteredEmployee);

            mapper.Map<FilterEmployeeDto>(filteredEmployee).Returns(filteredEmployeeDto);

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            var actualResult = await employeeService.GetFilterAsync(pageSize, pageNumber, textSearch);

            // Act & Assert
            Assert.That(actualResult, Is.EqualTo(filteredEmployeeDto));

            await employeeRepository.Received(1).GetFilterAsync(pageSize, pageNumber, textSearch);

            mapper.Received(1).Map<FilterEmployeeDto>(filteredEmployee);
        }

        /// <summary>
        /// Test hàm thêm mới 1 nhân viên
        /// Trường hợp dữ liệu đầu vào không hợp lệ
        /// </summary>
        /// <returns>Throw ValidateException</returns>
        /// Created By: BNTIEN (22/06/2023)
        [Test]
        public async Task InsertAsync_InvalidEmployeeCreateDto_ThrowValidateException()
        {
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            // Employee nhập vào
            var employeeCreateDto = new EmployeeCreateDto()
            {
                EmployeeCode = "123",
            };
            // Employee sau khi mapper
            var employee = new Employee()
            {
                EmployeeCode = "123",
            };
            // Employee tìm theo code, nếu có thì có nghĩa đã tồn tại để throw exception
            var employeeCheckCode = new Employee()
            {
                EmployeeCode = "123"
            };

            mapper.Map<Employee>(employeeCreateDto).Returns(employee);
            employeeRepository.GetByCodeAsync(employee.EmployeeCode).Returns(employeeCheckCode);

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            Assert.ThrowsAsync<ValidateException>(async () =>
            {
                await employeeService.InsertAsync(employeeCreateDto);
            });

            await employeeRepository.Received(1).GetByCodeAsync(employee.EmployeeCode);

            await employeeRepository.Received(0).InsertAsync(employee);

            await departmentRepository.Received(1).GetByIdAsync(employee.DepartmentId);

            mapper.Received(1).Map<Employee>(employeeCreateDto);
        }

        /// <summary>
        /// Test hàm thêm mới 1 nhân viên
        /// Trường hợp dữ liệu đầu vào hợp lệ
        /// </summary>
        /// <returns>1 (Số hàng bị ảnh hưởng sau khi thêm)</returns>
        /// Created By: BNTIEN (22/06/2023)
        [Test]
        public async Task InsertAsync_ValidEmployeeCreateDto_Success()
        {
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            // Employee nhập vào
            var employeeCreateDto = new EmployeeCreateDto()
            {
                EmployeeCode = "NV-12345",
                FullName = "Test",
                DepartmentId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145"),
            };
            // Employee sau khi mapper
            var employee = new Employee()
            {
                EmployeeCode = "NV-12345",
                FullName = "Test",
                DepartmentId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145"),
            };

            var department = new Department()
            {
                DepartmentId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145"),
            };

            if(department.DepartmentId != employee.DepartmentId)
            {
                department = null;
            }

            mapper.Map<Employee>(employeeCreateDto).Returns(employee);
            employeeRepository.GetByCodeAsync(Arg.Any<string>()).ReturnsNull();
            departmentRepository.GetByIdAsync(employee.DepartmentId).Returns(department);

            employee.EmployeeId = Guid.NewGuid();
            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            employeeRepository.InsertAsync(employee).Returns(1);
            var actualResult = await employeeService.InsertAsync(employeeCreateDto);

            Assert.That(actualResult, Is.EqualTo(1));

            await employeeRepository.Received(1).GetByCodeAsync(employee.EmployeeCode);

            await employeeRepository.Received(1).InsertAsync(employee);

            await departmentRepository.Received(1).GetByIdAsync(employee.DepartmentId);

            mapper.Received(2).Map<Employee>(employeeCreateDto);
        }

        /// <summary>
        /// Test hàm cập nhật thông tin nhân viên
        /// Trường hợp id cần sửa không trùng khớp
        /// </summary>
        /// <returns>Throw ValidateException</returns>
        /// Created By: BNTIEN (22/06/2023)
        [Test]
        public async Task UpdateAsync_EmployeeNotMatch_ThrowValidateException()
        {
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            var employeeIdUpdate = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145");

            // Employee nhập vào
            var employeeUpdateDto = new EmployeeUpdateDto()
            {
               EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6147"),
            };
           
            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            Assert.ThrowsAsync<ValidateException>(async () =>
            {
                await employeeService.UpdateAsync(employeeUpdateDto, employeeIdUpdate);
            });

            await employeeRepository.Received(0).GetByCodeAsync(employeeUpdateDto.EmployeeCode);

            await departmentRepository.Received(0).GetByIdAsync(employeeUpdateDto.DepartmentId);

            mapper.Received(0).Map<Employee>(employeeIdUpdate);
        }

        /// <summary>
        /// Test hàm cập nhật thông tin nhân viên
        /// Trường hợp id đã trùng khớp nhưng dữ liệu đầu vào không hợp lệ
        /// </summary>
        /// <returns>Throw ValidateException</returns>
        /// Created By: BNTIEN (22/06/2023)
        [Test]
        public async Task UpdateAsync_InvalidEmployeeCreateDto_ThrowValidateException()
        {
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            var employeeIdUpdate = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6147");

            // Employee nhập vào
            var employeeUpdateDto = new EmployeeUpdateDto()
            {
                EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6147"),
                EmployeeCode = "123",
            };
            // Employee sau khi mapper
            var employee = new Employee()
            {
                EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6147"),
                FullName = "123",
                EmployeeCode = "123",
            };
            // Employee tìm theo code, nếu có thì có nghĩa đã tồn tại để throw exception
            var employeeCheckCode = new Employee()
            {
                EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6147"),
                FullName = "123",
                EmployeeCode = "123",
            };

            // Employee tìm theo code, nếu có thì có nghĩa đã tồn tại để throw exception
            var employeeCheckId = new Employee()
            {
                EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6148"),
                FullName = "123",
                EmployeeCode = "123",
            };

            employeeRepository.GetByIdAsync(employeeIdUpdate).Returns(employeeCheckId);

            mapper.Map<Employee>(employeeUpdateDto).Returns(employee);
            employeeRepository.GetByCodeAsync(employee.EmployeeCode).Returns(employeeCheckCode);

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            Assert.ThrowsAsync<ValidateException>(async () =>
            {
                await employeeService.UpdateAsync(employeeUpdateDto, employeeIdUpdate);
            });

            await employeeRepository.Received(1).GetByCodeAsync(employee.EmployeeCode);

            await employeeRepository.Received(0).UpdateAsync(employee, employeeIdUpdate);

            await departmentRepository.Received(1).GetByIdAsync(employee.DepartmentId);

            mapper.Received(1).Map<Employee>(employeeUpdateDto);
        }

        /// <summary>
        /// Test hàm cập nhật thông tin nhân viên
        /// Trường hợp dữ liệu đầu vào hợp lệ
        /// </summary>
        /// <returns>1 (Số hàng bị ảnh hưởng sau khi sửa)</returns>
        /// Created By: BNTIEN (22/06/2023)
        [Test]
        public async Task UpdateAsync_ValidEmployeeUpdateDto_Success()
        {
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            var employeeIdUpdate = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6150");

            // Employee nhập vào
            var employeeUpdateDto = new EmployeeUpdateDto()
            {
                EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6150"),
                EmployeeCode = "NV-12345",
                FullName = "Test",
                DepartmentId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145"),
            };
            // Employee sau khi mapper
            var employee = new Employee()
            {
                EmployeeId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6150"),
                EmployeeCode = "NV-12345",
                FullName = "Test",
                DepartmentId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145"),
            };

            var department = new Department()
            {
                DepartmentId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145"),
            };

            if (department.DepartmentId != employee.DepartmentId)
            {
                department = null;
            }

            mapper.Map<Employee>(employeeUpdateDto).Returns(employee);
            employeeRepository.GetByCodeAsync(Arg.Any<string>()).ReturnsNull();
            departmentRepository.GetByIdAsync(employee.DepartmentId).Returns(department);

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            employeeRepository.UpdateAsync(employee, employeeIdUpdate).Returns(1);
            var actualResult = await employeeService.UpdateAsync(employeeUpdateDto, employeeIdUpdate);

            Assert.That(actualResult, Is.EqualTo(1));

            await employeeRepository.Received(1).GetByCodeAsync(employee.EmployeeCode);

            await employeeRepository.Received(1).UpdateAsync(employee, employeeIdUpdate);

            await departmentRepository.Received(1).GetByIdAsync(employee.DepartmentId);

            mapper.Received(2).Map<Employee>(employeeUpdateDto);
        }

        /// <summary>
        /// Test hàm xóa 1 nhân viên theo id
        /// Trường hợp không tìm thấy nhân viên để xóa
        /// </summary>
        /// <returns>0</returns>
        /// Created By: BNTIEN (22/06/2023)
        [Test]
        public async Task DeleteAsync_NullEmployee_ReturnZero()
        {
            // Arrange
            var id = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145");

            var employeeRepository = Substitute.For<IEmployeeRepository>();
            employeeRepository.GetByIdAsync(id).ReturnsNull();
            employeeRepository.DeleteAsync(id).Returns(1);

            var departmentRepository = Substitute.For<IDepartmentRepository>();

            var mapper = Substitute.For<IMapper>();

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            var actualResult = await employeeService.DeleteAsync(id);

            // Act & Assert
            Assert.That(actualResult, Is.EqualTo(0));
            // K tìm thấy nhân viên để xóa thì không cần gọi vào hàm xóa
            await employeeRepository.Received(0).DeleteAsync(id);
        }

        /// <summary>
        /// Test hàm xóa 1 nhân viên theo id
        /// Trường hợp tìm thấy nhân viên và thực hiện xóa thành công
        /// </summary>
        /// <returns>1</returns>
        /// Created By: BNTIEN (22/06/2023)
        [Test]
        public async Task DeleteAsync_ValidEmployee_Success()
        {
            // Arrange
            var id = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6145");

            var employee = new Employee()
            {
                EmployeeId = id,
            };

            var employeeRepository = Substitute.For<IEmployeeRepository>();
            employeeRepository.GetByIdAsync(id).Returns(employee);
            employeeRepository.DeleteAsync(id).Returns(1);

            var departmentRepository = Substitute.For<IDepartmentRepository>();

            var mapper = Substitute.For<IMapper>();

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            var actualResult = await employeeService.DeleteAsync(id);

            // Act & Assert
            Assert.That(actualResult, Is.EqualTo(1));
            // Chỉ được gọi 1 lần vào hàm xóa
            await employeeRepository.Received(1).DeleteAsync(id);
        }
    }
}
