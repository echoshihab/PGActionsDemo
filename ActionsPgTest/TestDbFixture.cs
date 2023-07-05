// Reference Implementation - https://learn.microsoft.com/en-us/ef/core/testing/testing-with-the-database

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ActionsPg;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ActionsPg;

namespace COSM.PatientPortal.Tests;

[ExcludeFromCodeCoverage]
public class TestDatabaseFixture
{
    private const string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=test;IncludeErrorDetail=true;";
    private const string AdministrativeConnString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres;";


    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public TestDatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = this.CreateContext())
                {
                    context.Database.EnsureDeleted();
                    var test = context.Database.GenerateCreateScript();



                    using (NpgsqlConnection connection = new NpgsqlConnection(AdministrativeConnString))
                    {
                        var dbCreateCommand = new NpgsqlCommand(@"CREATE DATABASE test WITH OWNER = postgres", connection);
                        connection.Open();
                        dbCreateCommand.ExecuteNonQuery();
                        connection.Close();
                    }


                    Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                    var filePath = Path.Combine("Sql", "testDb.sql");
                    context.Database.ExecuteSqlRaw(File.ReadAllText(filePath));

                    this.SeedTestData(context);
                }

                _databaseInitialized = true;
            }
        }
    }

    public TestDataContext CreateContext()
    {
        return new TestDataContext(
            new DbContextOptionsBuilder<TestDataContext>()
                .UseNpgsql(ConnectionString)
                .Options);
    }

    private void SeedTestData(TestDataContext context)
    {
        context.AddRange(
            new List<TestData>
            {
               new(){ Name="TestData1" },
               new(){ Name="TestData2" }
            });

        context.SaveChanges();
    }

}