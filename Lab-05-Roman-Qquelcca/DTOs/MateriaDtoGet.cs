namespace Lab_05_Roman_Qquelcca.DTOs;

public class MateriaDtoGet
{
    public int IdMateria { get; set; }
    public int? IdCurso { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
}