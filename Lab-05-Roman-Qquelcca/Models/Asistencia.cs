using System;
using System.Collections.Generic;

namespace Lab_05_Roman_Qquelcca.Models;

public partial class Asistencia
{
    public int IdAsistencia { get; set; }

    public int? IdEstudiante { get; set; }

    public int? IdCurso { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Estado { get; set; }

    public virtual Curso? IdCursoNavigation { get; set; }

    public virtual Estudiante? IdEstudianteNavigation { get; set; }
}
