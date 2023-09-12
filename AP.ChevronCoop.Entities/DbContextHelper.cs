using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;


namespace AP.ChevronCoop.Entities
{
    public static partial class DbContextHelper
    {

        public static long GetNextSequenceValue(this DbContext context, string name, string schema = null)
        {
            //var sqlGenerator = context.GetService<IUpdateSqlGenerator>();
            //var sql = sqlGenerator.GenerateNextSequenceValueOperation(name, schema ?? context.Model.GetDefaultSchema());
            //var rawCommandBuilder = context.GetService<IRawSqlCommandBuilder>();
            //var command = rawCommandBuilder.Build(sql);
            //var connection = context.GetService<IRelationalConnection>();
            ////var logger = context.GetService<IDiagnosticsLogger<DbLoggerCategory.Database.Command>>();
            ////var parameters = new RelationalCommandParameterObject(connection, null, null, context, logger);
            //var parameters = new RelationalCommandParameterObject(connection, null, null, context, null);
            //var result = command.ExecuteScalar(parameters);
            //return Convert.ToInt64(result, CultureInfo.InvariantCulture);

            //var command = context.Database.GetDbConnection().CreateCommand();


            //command.CommandType = System.Data.CommandType.Text;
            //command.CommandText = $"SELECT NEXT VALUE FOR [{schema}].[{name}]";

            //context.Database.OpenConnection();

            //try
            //{
            //    var result = command.ExecuteScalar();

            //    return (long)result;
            //}
            //finally
            //{
            //    context.Database.CloseConnection();
            //}

            var result = context.Database.SqlQuery<long>($"SELECT NEXT VALUE FOR [{schema}].[{name}]");
            return result.FirstOrDefault();
        }



        public static async Task<long> GetNextSequenceValueAsync(this DbContext context, string name, string schema = null)
        {
            //var sqlGenerator = context.GetService<IUpdateSqlGenerator>();
            //var sql = sqlGenerator.GenerateNextSequenceValueOperation(name, schema ?? context.Model.GetDefaultSchema());
            //var rawCommandBuilder = context.GetService<IRawSqlCommandBuilder>();
            //var command = rawCommandBuilder.Build(sql);
            //var connection = context.GetService<IRelationalConnection>();
            ////var logger = context.GetService<IDiagnosticsLogger<DbLoggerCategory.Database.Command>>();
            ////var parameters = new RelationalCommandParameterObject(connection, null, null, context, logger);
            //var parameters = new RelationalCommandParameterObject(connection, null, null, context, null);
            ////var result = command.ExecuteScalar(parameters);
            //var result = await command.ExecuteScalarAsync(parameters);
            //return Convert.ToInt64(result, CultureInfo.InvariantCulture);



            //var command = context.Database.GetDbConnection().CreateCommand();


            //command.CommandType = System.Data.CommandType.Text;
            //command.CommandText = $"SELECT NEXT VALUE FOR [{schema}].[{name}]";

            //context.Database.OpenConnection();

            //try
            //{
            //    var result = await command.ExecuteScalarAsync();

            //    return (long)result;
            //}
            //finally
            //{
            //    context.Database.CloseConnection();

            //}


            var result = await context.Database.SqlQuery<long>($"SELECT NEXT VALUE FOR [{schema}].[{name}]").FirstOrDefaultAsync();
            return result;
        }
    }

}