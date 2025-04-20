namespace Lab_05_Roman_Qquelcca.DTOs;

public class EvaluacioneDtoGet
{
    public int IdEvaluacion { get; set; }
    public int? IdEstudiante { get; set; }
    public int? IdCurso { get; set; }
    public decimal? Calificacion { get; set; }
    public DateOnly? Fecha { get; set; }
}