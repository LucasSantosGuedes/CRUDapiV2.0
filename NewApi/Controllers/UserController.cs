using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Dapper;
using System.Collections.Generic;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public UserController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IEnumerable<User> Get()
    {
        using (var dbConnection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            dbConnection.Open();
            return dbConnection.Query<User>("SELECT * FROM User");
        }
    }

    [HttpGet("{id}")]
    public User Get(int id)
    {
        using (var dbConnection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            dbConnection.Open();
            return dbConnection.QueryFirstOrDefault<User>("SELECT * FROM User WHERE Id = @Id", new { Id = id });
        }
    }

   [HttpPost]
[Consumes("application/json")]
public IActionResult Post([FromBody] User user)
{
        if (user == null)
        {
            return BadRequest("Dados inválidos.");
        }

        using (var dbConnection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            dbConnection.Open();
            dbConnection.Execute("INSERT INTO User (Nome, Email) VALUES (@Nome, @Email)", user);
        }

        return Ok("Usuário criado com sucesso.");
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] User user)
    {
        if (user == null || user.Id != id)
        {
            return BadRequest("Dados inválidos.");
        }

        using (var dbConnection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            dbConnection.Open();
            dbConnection.Execute("UPDATE User SET Nome = @Nome, Email = @Email WHERE Id = @Id", user);
        }

        return Ok("Usuário atualizado com sucesso.");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using (var dbConnection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            dbConnection.Open();
            dbConnection.Execute("DELETE FROM User WHERE Id = @Id", new { Id = id });
        }

        return Ok("Usuário excluído com sucesso.");
    }
}