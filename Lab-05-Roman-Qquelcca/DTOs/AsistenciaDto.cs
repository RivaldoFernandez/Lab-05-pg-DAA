namespace Lab_05_Roman_Qquelcca.DTOs;

public class AsistenciaDto
{
    public int? IdEstudiante { get; set; }
    public int? IdCurso { get; set; }
    public DateOnly? Fecha { get; set; }
    public string? Estado { get; set; }
}