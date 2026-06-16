using Bewoning.Api.Exceptions;
using Bewoning.Api.Options;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using static Dapper.SqlMapper;

namespace Bewoning.Data.Repositories.Postgres;
public abstract class PostgresRepoBase(IOptions<DatabaseOptions> databaseOptions)
{
    protected readonly IOptions<DatabaseOptions> _databaseOptions = databaseOptions;

    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(_databaseOptions.Value.ConnectionString);
    }

    protected static async Task<IEnumerable<TResponseType?>> ExecuteDapperQuery<TResponseType>((string, DynamicParameters) whereStringAndParams, string baseQuery, Func<string, DynamicParameters, Task<IEnumerable<TResponseType>>> executeQueryFunc)
    {
        var query = string.Format(baseQuery, whereStringAndParams.Item1);

        return await executeQueryFunc(query, whereStringAndParams.Item2);
    }

    protected async Task<List<TDataObject>> GetDataViaDapper<TDataObject>(string queryBase, DynamicParameters dynamicParameters, string whereCondition)
    {
        var query = string.Format(queryBase, whereCondition);

        return (await DapperQueryAsync<TDataObject>(query, dynamicParameters)).ToList();
    }

    protected Task<IEnumerable<TDataObject>> DapperQueryAsync<TDataObject>(string? query, DynamicParameters? dynamicParameters = null)
    {
        return DapperQueryAsync<TDataObject>(GetConnection(), query, dynamicParameters);
    }

    protected Task<IEnumerable<TDataObject>> DapperQueryAsync<TDataObject>(string? query, Type[] types, Func<object[], TDataObject> map, object? param = null, IDbTransaction? transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
    {
        return DapperQueryAsync(GetConnection(), query, types, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
    }

    private static Task<IEnumerable<TDataObject>> DapperQueryAsync<TDataObject>(NpgsqlConnection connection, string? query, Type[] types, Func<object[], TDataObject> map, object? param = null, IDbTransaction? transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
    {
        try
        {
            return connection.QueryAsync(query, types, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }
        catch (NpgsqlException npgEx)
        {
            throw new ServiceUnavailableException(npgEx.Message, npgEx);
        }
    }

    private static Task<IEnumerable<TDataObject>> DapperQueryAsync<TDataObject>(NpgsqlConnection connection, string? query, DynamicParameters? dynamicParameters = null)
    {
        try
        {
            if (dynamicParameters != null)
            {
                return connection.QueryAsync<TDataObject>(query, dynamicParameters);
            }

            return connection.QueryAsync<TDataObject>(query);
        }
        catch (NpgsqlException npgEx)
        {
            throw new ServiceUnavailableException(npgEx.Message, npgEx);
        }
    }

    protected static async Task OpenConnectionAndLog(NpgsqlConnection connection)
    {
        try
        {
            await connection.OpenAsync();
        }
        catch (NpgsqlException npgEx)
        {
            throw new ServiceUnavailableException(npgEx.Message, npgEx);
        }
    }
}