using Microsoft.AspNetCore.Mvc;


// 資料庫連接，模型
using EmployeeAPI.Data;
using EmployeeAPI.Models;

using SQLitePCL;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers
{

    //  controller 的路由基底會是 api/employee

    // Web API 通常只需要處理 HTTP request / response，不需要 View 相關功能
    // Web API controller => 繼承的是 ControllerBase，沒有 View 支援
    // MVC Razor 語法 =>  才會繼承 Controller，有 View 支援


    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        // 建構式注入
        // 把 Program.cs 裡註冊好的 AppDbContext 注入進來
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }


        // 取得全部員工
        // /api/employee

        // async ... await    GetEmployeeAsync()   ToListAsync()
        // 回傳型別 ActionResult<IEnumerable<Employee>>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }




    }
}
