using Microsoft.AspNetCore.Mvc;


// 資料庫連接，模型
using EmployeeAPI.Data;
using EmployeeAPI.Models;

using SQLitePCL;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Collections.Immutable;

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

        // 建構式
        // 把在 Program.cs 註冊好的 AppDbContext 傳進 controller
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }


        // 取得全部員工資料
        // Get /api/employee

        // async ... await     ToListAsync()
        // 回傳型別 ActionResult<IEnumerable<Employee>>
        // IEnumerable<Employee> 是「一組員工資料集合」

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }





        // 取得單個員工資料
        // Get /api/employee/{id}

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            // FindAsync 是用主鍵去找單筆資料
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }



        // 新增員工
        // Post /api/employee

        // 接收前端送來的 Employee 資料
        // 把它加入 _context.Employees.Add(...)
        // SaveChangesAsync() 寫進資料庫


        [HttpPost]

        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {


            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();


            // return OK(employee) 也能執行

            // 新增成功 =>  CreatedAtAction(...) 回傳 201 Created

            // CreatedAtAction(actionName, routeValues, value)
            // CreatedAtAction(目標Action名稱, 路由參數, 回傳資料)

            // actionName => 拿「GetEmployee 這個方法的名字」當字串使用
            // routeValues =>  指定 action 需要的路由參數
            // value => 放進 response body 裡的內容 (新增成功的那筆資料本身 )

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);

        }





        // 編輯資料
        // PUT /api/employee/{id}
        // 要考量前端輸入欄位

        // 前端送出要更新的 id
        // 前端送出新資料內容
        // updateEmployee => 從 request body 拿到

        // 修改完要 await _context.SaveChangesAsync();


        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> EditEmployee(int id, Employee updateEmployee)
        {
            // 用 id 找到資料
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }


            // 舊資料被新的覆蓋掉
            employee.Name = updateEmployee.Name;
            employee.Department = updateEmployee.Department;
            employee.Telephone = updateEmployee.Telephone;
            employee.Email = updateEmployee.Email;

            await _context.SaveChangesAsync();



            return Ok(employee);
        }




        // 刪除
        // DELETE /api/employee/{id}


        [HttpDelete("{id}")]

        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            // 根據 id 來刪除對應資料
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }


            // 資料庫刪除
            // 會改變資料庫內容的動作，通常都要 SaveChangesAsync()，像是新增、編輯、刪除


            // SaveChangesAsync() 是 DbContext 的方法，不是 DbSet 的方法


            // DbContext 是資料庫操作的總入口
            // DbSet 是某一張資料表的操作入口

            // _context 是 AppDbContext
            // .Employees 是裡面的 DbSet<Employee>


            // 先從 DbSet 標記刪除，再由 DbContext 寫回資料庫
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();


            return Ok(employee);

        }

    }
}
