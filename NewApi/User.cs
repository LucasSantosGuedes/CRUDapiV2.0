using System.ComponentModel.DataAnnotations; // Adicione este using

public class User
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    public User()
    {
        Nome = string.Empty; // Inicialize com um valor padrão
        Email = string.Empty; // Inicialize com um valor padrão
    }
}

