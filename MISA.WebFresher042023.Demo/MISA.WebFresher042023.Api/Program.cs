using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MISA.WebFresher042023.Core.Exceptions;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.Services;
using MISA.WebFresher042023.Core.Middlewares;
using MISA.WebFresher042023.Core.Services;
using MISA.WebFresher042023.Infrastructure.Repository;
using System.Net;
using System.Runtime.Versioning;
using MISA.WebFresher042023.Core.Resources;
using MISA.WebFresher042023.Core.Interfaces.Excels;
using MISA.WebFresher042023.Infrastructure.Excel;
using MISA.WebFresher042023.Core.Interfaces.ExcelServices;
using MISA.WebFresher042023.Core.Excels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// validate dữ liệu 
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = actioncontext =>
    {
        var modelstate = actioncontext.ModelState;
        var errors = new Dictionary<string, string>();

        foreach (var entry in modelstate)
        {
            var key = entry.Key;
            var valueerrors = entry.Value.Errors.Select(error => error.ErrorMessage);
            var errormessage = string.Join(", ", valueerrors);

            errors[key] = errormessage;
        }

        return new BadRequestObjectResult(new BaseException
        {
            ErrorCode = (int)HttpStatusCode.BadRequest,
            DevMessage = ResourceVN.Validate_User_Input_Error,
            UserMessage = ResourceVN.Validate_User_Input_Error,
            TraceId = "",
            MoreInfo = "",
            Data = errors
        });
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Sử dụng thư viện AutoMapter để chuyển đổi giữa các đối tượng dữ liệu
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Policy
builder.Services.AddCors();

// Tiêm DI
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeExcel, EmployeeExcel>();
builder.Services.AddScoped<IEmployeeExcelSercive, EmployeeExcelService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.Run();
