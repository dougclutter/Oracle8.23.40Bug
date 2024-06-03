using BugCode.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddUserSecrets(typeof(Program).Assembly)
    .Build();

var connectionString = configuration.GetConnectionString("MyConnection")
    ?? "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.123.123.123)(PORT=1521))(CONNECT_DATA=(UR=A)(SERVICE_NAME=myServiceName)));User Id=myId;Password=myPassword";

var dbContextOptionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
dbContextOptionsBuilder.UseOracle(connectionString, options =>
{

    //IMPORTANT UPDATE: 6/3/2024 - Problem can be resolved by adding this line:
    //options.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19);
});
var dbContext = new MyDbContext(dbContextOptionsBuilder.Options);

var onlyWithDetails = true;
var filterName = onlyWithDetails ? "with" : "without";

/*
 * This query works with Oracle.EntityFrameworkCore 8.21.121.
 * This query fails with Oracle.EntityFrameworkCore 8.23.40; it returns an ORA-00904: "FALSE": invalid identifier.
 */

Console.WriteLine($"Finding all invoices {filterName} details:");
var invoices = dbContext.Invoices
    .Where(i => (i.InvoiceDetails.Count != 0) == onlyWithDetails)
    .Select(i => $"Invoice ID: {i.Id} Name: {i.Name}");

foreach (var invoice in invoices)
{
    Console.WriteLine(invoice);
}