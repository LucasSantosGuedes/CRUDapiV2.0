using System.ComponentModel.DataAnnotations; // Adicione este using

public class Video
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Url { get; set; }

    public Video()
    {
        Titulo = string.Empty; // Inicialize com um valor padrão
        Url = string.Empty; // Inicialize com um valor padrão
    }
}

