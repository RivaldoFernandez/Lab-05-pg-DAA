namespace Lab_05_Roman_Qquelcca.DTOs;

public class ProfesoreDtoGet
{
    public int IdProfesor { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Especialidad { get; set; }

    public string? Correo { get; set; }

}