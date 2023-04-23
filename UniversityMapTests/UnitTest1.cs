using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniversityMap.Controllers;
using UniversityMap.Data;
using UniversityMap.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UniversityMapTests
{
    [TestClass]
    public class UnitTest1
    {

        public SqliteConnection ConnectionFactory() => new SqliteConnection("DataSource=:memory:");

        public DbContextOptions<UniversityMapContext> DbOptionsFactory(SqliteConnection connection) =>
            new DbContextOptionsBuilder<UniversityMapContext>()
        .UseSqlite(connection)
        .Options;

        public void RunWithDatabase(
            Action<UniversityMapContext> arrange,
            Func<UniversityMapContext, IActionResult> act,
            Action<IActionResult> assert)
        {
            var connection = ConnectionFactory();
            connection.Open();

            try
            {
                var options = DbOptionsFactory(connection);

                using (var context = new UniversityMapContext(options))
                {
                    context.Database.EnsureCreated();
                    // Arrange
                    arrange?.Invoke(context);
                }

                using (var context = new UniversityMapContext(options))
                {
                    // Act (and pass result into assert)
                    var result = act.Invoke(context);
                    // Assert
                    assert.Invoke(result);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task RunWithDatabaseAsync(
        Func<UniversityMapContext, Task> arrange,
        Func<UniversityMapContext, Task<IActionResult>> act,
        Action<IActionResult> assert)
        {
            var connection = ConnectionFactory();
            await connection.OpenAsync();

            try
            {
                var options = DbOptionsFactory(connection);

                using (var context = new UniversityMapContext(options))
                {
                    await context.Database.EnsureCreatedAsync();
                    if (arrange != null) await arrange.Invoke(context);
                }

                using (var context = new UniversityMapContext(options))
                {
                    var result = await act.Invoke(context);

                    assert.Invoke(result);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [TestMethod]
        public void MainPage()
        {
            HomeController homeController = new HomeController();

            List<ViewResult> results = new List<ViewResult>();

            results.Add(homeController.Index() as ViewResult);
            results.Add(homeController.Privacy() as ViewResult);

            results.ForEach(Assert.IsNotNull);
        }

        [TestMethod]
        public async Task MapsCRUDPages()
        {
            await RunWithDatabaseAsync(
                arrange: async context =>
                {
                    await context.Maps.AddRangeAsync(
                        new Map()
                        {
                            Id = 1,
                            Floor = 1,
                            JQueryScript = "1",
                            Name = "1",
                            SvgMap = "1"
                        },
                        new Map()
                        {
                            Id = 2,
                            Floor = 2,
                            JQueryScript = "1",
                            Name = "1",
                            SvgMap = "1"
                        }
                        );
                    await context.SaveChangesAsync();
                },
                act: context => new MapsCRUDController(context).Index(),
                assert: result => Assert.IsNotNull(result as ViewResult)
                );
            
            await RunWithDatabaseAsync(
                arrange: async context =>
                {
                    await context.Maps.AddRangeAsync(
                        new Map()
                        {
                            Id = 1,
                            Floor = 1,
                            JQueryScript = "1",
                            Name = "1",
                            SvgMap = "1"
                        },
                        new Map()
                        {
                            Id = 2,
                            Floor = 2,
                            JQueryScript = "1",
                            Name = "1",
                            SvgMap = "1"
                        }
                        );
                    await context.SaveChangesAsync();
                },
                act: context => new MapsCRUDController(context).Edit(id: 1),
                assert: result => Assert.IsNotNull(result as ViewResult)
                );
            await RunWithDatabaseAsync(
                arrange: async context =>
                {
                    await context.Maps.AddRangeAsync(
                        new Map()
                        {
                            Id = 1,
                            Floor = 1,
                            JQueryScript = "1",
                            Name = "1",
                            SvgMap = "1"
                        },
                        new Map()
                        {
                            Id = 2,
                            Floor = 2,
                            JQueryScript = "1",
                            Name = "1",
                            SvgMap = "1"
                        }
                        );
                    await context.SaveChangesAsync();
                },
                act: context => new MapsCRUDController(context).Create(
                    new Map()
                    {
                        Id = 3,
                        Floor = 2,
                        JQueryScript = "1",
                        Name = "1",
                        SvgMap = "1"
                    }),
                assert: result => Assert.IsNotNull(result)
                );
        }
    }
}