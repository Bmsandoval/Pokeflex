using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public static class UdfMigrations
    {
        public static void MigrateUdfs(PokeflexContext dbContext)
        {
            dbContext.Database.OpenConnection();
            try
            {
                HashSet<string> existingFunctions = new();
                
                var connection = dbContext.Database.GetDbConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
select obj.name as function_name, mod.definition
from sys.objects obj
    join sys.sql_modules mod
        on mod.object_id = obj.object_id
where obj.type in ('FN', 'TF', 'IF')
order by function_name;";

                    var table = new DataTable();
                    using (var reader = cmd.ExecuteReader())
                    {
                        table.Load(reader);
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        existingFunctions.Add((string) row["function_name"]);
                    }
                }

                foreach (var (functionName, functionDefinition) in udfs)
                {
                    if (existingFunctions.Contains(functionName))
                        dbContext.Database.ExecuteSqlRaw($"drop function [{functionName}]");
                    dbContext.Database.ExecuteSqlRaw(functionDefinition);
                }
            }
            finally
            {
                dbContext.Database.CloseConnection();
            }
        }
        
        private static Dictionary<string, string> udfs =>
            new()
            { // [Id], [ApiSource], [GroupId], [Name], [Number]
                // MAKE RANGE
                {"MakeRange", @"
CREATE FUNCTION MakeRange (@Min int, @Max int)
RETURNS TABLE AS
 RETURN (
SELECT TOP((@Max-@Min)+1) CAST((ROW_NUMBER() OVER (ORDER BY number))-1+@Min AS INT) AS Number, CAST(0 AS INT) AS Id, NULL AS ApiSource, CAST(0 AS INT) AS GroupId, NULL AS Name
FROM [master]..spt_values
)"},
                // SELECT FLEXMON
                {"SelectFlexmon", @"
CREATE FUNCTION SelectFlexmon (@GroupId int, @Number int)
RETURNS TABLE AS
RETURN (
    SELECT * 
    FROM [Pokemons] AS [p]
    WHERE ([p].[GroupId] = @GroupId) AND ([p].[Number] = @Number)
)"}
                
            };
    }
}
// If Exists (Select * from sys.objects where name ='SelectFlexmon'  and type =N'FN')
//     drop function [SelectFlexmon]
// EXEC('
// CREATE FUNCTION SelectFlexmon (groupId int, number int) RETURNS Pokemons
// AS BEGIN
//  RETURN SELECT * 
//  FROM [Pokemons] AS [p]
//  WHERE ([p].[GroupId] = groupId) AND ([p].[Number] = number)
// END
// ')

// If Exists (Select * from sys.objects where name ='SelectFlexmon'  and type =N'FN')
//     drop function [SelectFlexmon]
// GO
// CREATE FUNCTION SelectFlexmon (groupId int, number int) RETURNS Pokemons
// AS BEGIN
//  RETURN SELECT * 
//  FROM [Pokemons] AS [p]
//  WHERE ([p].[GroupId] = groupId) AND ([p].[Number] = number)
// END
// GO

// CREATE FUNCTION SelectFlexmon (@groupId int, @number int)
// RETURNS Pokemons AS BEGIN
// DECLARE @result AS Pokemons
// SELECT @result= * 
// FROM [Pokemons] AS [p]
// WHERE ([p].[GroupId] = @p0) AND ([p].[Number] = @p1)
// RETURN @result

// CREATE FUNCTION SelectFlexmon (@GroupId int, @Number int)
// RETURNS Pokemons AS
// RETURN (
//     SELECT * 
//     FROM [Pokemons] AS [p]
//     WHERE ([p].[GroupId] = @GroupId) AND ([p].[Number] = @Number)
// )
