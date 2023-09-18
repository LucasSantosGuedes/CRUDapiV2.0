using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using MySql.Data.MySqlClient;
public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new ArgumentException("A string de conexão não pode estar vazia ou nula.", nameof(connectionString));
    }
        services.AddScoped<IDbConnection>(_ => new MySqlConnection(connectionString));
        services.AddTransient(_ => new DapperContext(connectionString));
        services.AddScoped<DapperContext>();
        services.AddControllers();
    }
}

// Corrigido: declarar a classe como public
public class DapperContext
{
    
    public string ConnectionString { get; }
    public IDbConnection Connection { get; private set; }

    public DapperContext(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("A string de conexão não pode estar vazia ou nula.", nameof(connectionString));
        }

        ConnectionString = connectionString;
        Connection = new MySqlConnection(connectionString);
    }
}