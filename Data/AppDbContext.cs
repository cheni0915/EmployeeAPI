// Data：放和資料庫連線與 EF Core 有關的類別

// 建立 AppDbContext 並註冊連線字串 ( appsetting.json )

// DbContext 是 EF Core 操作資料庫的主要入口。
// 只要繼承它，這個類別就具備了和資料庫互動的能力，例如查資料、追蹤變更、儲存變更

// 而 DbSet<Employee> 代表一組 Employee 實體，會對應到資料表。

// 目前用的版本為 .NET 8 ，套件版本也得選 8.x.x 才可
// NuGet安裝套件 Microsoft.EntityFrameworkCore.SqlServer
// NuGet安裝套件 Microsoft.EntityFrameworkCore.Tools
// NuGet安裝套件 Microsoft.EntityFrameworkCore.Design


using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Models;
using Microsoft.Extensions.Options;



namespace EmployeeAPI.Data
{

    // 建立一個類別，名字叫 AppDbContext，讓它繼承 EF Core 的 DbContext。
    public class AppDbContext : DbContext
    {


        // 建立物件時要吃進一份設定，而這份設定裡會包含
        // 「我要連哪個資料庫、用哪種資料庫 provider」等資訊
        // options = 連線設定包
        // base(options) = 把設定包交給 EF Core 的核心去處理

        // !!!      options 通常在 Program.cs 裡註冊     !!!
        // !!!      要先在 appsettings.json 設定 connection string    !!!

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //  EF Core 要管理一張 Employees 資料表
        // 之後 Controller 可以透過 _context.Employees 查詢或新增資料
        public DbSet<Employee> Employees { get; set; }
    }
}
