namespace Lab_05_Roman_Qquelcca.DTOs;

public class CursoDtoGet
{
    public int IdCurso { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int Creditos { get; set; }
}