using System;
using System.Collections.Generic;

namespace Lab_05_Roman_Qquelcca.Models;

public partial class Matricula
{
    public int IdMatricula { get; set; }

    public int? IdEstudiante { get; set; }

    public int? IdCurso { get; set; }

    public string? Semestre { get; set; }

    public virtual Curso? IdCursoNavigation { get; set; }

    public virtual Estudiante? IdEstudianteNavigation { get; set; }
}
