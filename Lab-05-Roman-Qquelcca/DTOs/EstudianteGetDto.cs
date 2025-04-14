namespace Lab_05_Roman_Qquelcca.DTOs;

public class EstudianteGetDto
{
    public int IdEstudiante { get; set; }
    public string Nombre { get; set; } = string.Empty;  // Inicializa con un valor vacío.
    public int Edad { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
}
