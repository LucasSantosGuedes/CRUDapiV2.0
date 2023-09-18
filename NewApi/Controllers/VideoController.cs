using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Dapper;
using System.Collections.Generic;
using System.Data;


    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public VideoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Video> Get()
        {
            using (var dbConnection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                dbConnection.Open();
                return dbConnection.Query<Video>("SELECT * FROM Video");
            }
        }

        [HttpGet("{id}")]
        public Video Get(int id)
        {
            using (var dbConnection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Video>("SELECT * FROM Video WHERE Id = @Id", new { Id = id });
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post([FromBody] Video video)
        {
            if (video == null)
            {
                return BadRequest("Dados inválidos.");
            }

            using (var dbConnection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO Video (Titulo, Url) VALUES (@Titulo, @Url)", video);
            }

            return Ok("Vídeo criado com sucesso.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Video video)
        {
            if (video == null || video.Id != id)
            {
                return BadRequest("Dados inválidos.");
            }

            using (var dbConnection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                dbConnection.Open();
                dbConnection.Execute("UPDATE Video SET Titulo = @Titulo, Url = @Url WHERE Id = @Id", video);
            }

            return Ok("Vídeo atualizado com sucesso.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var dbConnection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM Video WHERE Id = @Id", new { Id = id });
            }

            return Ok("Vídeo excluído com sucesso.");
        }
    }
