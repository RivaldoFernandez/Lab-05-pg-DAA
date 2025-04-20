using Lab_05_Roman_Qquelcca.DTOs;
using Lab_05_Roman_Qquelcca.Models;
using Lab_05_Roman_Qquelcca.Repository.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Lab_05_Roman_Qquelcca.Controllers;
[ApiController]
[Route("api/qquelcca/[controller]")]
public class EvaluacionController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EvaluacionController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var evaluaciones = await _unitOfWork.GetRepository<Evaluacione>().GetAllAsync();

        var evaluacionDtos = evaluaciones.Select(e => new EvaluacioneDtoGet()
        {
            IdEvaluacion = e.IdEvaluacion,
            IdEstudiante = e.IdEstudiante,
            IdCurso = e.IdCurso,
            Calificacion = e.Calificacion,
            Fecha = e.Fecha
        });

        return Ok(evaluacionDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var evaluacion = await _unitOfWork.GetRepository<Evaluacione>().GetByIdAsync(id);
        if (evaluacion == null)
            return NotFound();

        var dto = new EvaluacioneDtoGet
        {
            IdEvaluacion = evaluacion.IdEvaluacion,
            IdEstudiante = evaluacion.IdEstudiante,
            IdCurso = evaluacion.IdCurso,
            Calificacion = evaluacion.Calificacion,
            Fecha = evaluacion.Fecha
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EvaluacioneDto dto)
    {
        var evaluacion = new Evaluacione
        {
            IdEstudiante = dto.IdEstudiante,
            IdCurso = dto.IdCurso,
            Calificacion = dto.Calificacion,
            Fecha = dto.Fecha
        };

        await _unitOfWork.GetRepository<Evaluacione>().InsertAsync(evaluacion);
        await _unitOfWork.Complete();

        return CreatedAtAction(nameof(GetById), new { id = evaluacion.IdEvaluacion }, evaluacion);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EvaluacioneDto dto)
    {
        var existing = await _unitOfWork.GetRepository<Evaluacione>().GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.IdEstudiante = dto.IdEstudiante;
        existing.IdCurso = dto.IdCurso;
        existing.Calificacion = dto.Calificacion;
        existing.Fecha = dto.Fecha;

        _unitOfWork.GetRepository<Evaluacione>().Update(existing);
        await _unitOfWork.Complete();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var evaluacion = await _unitOfWork.GetRepository<Evaluacione>().GetByIdAsync(id);
        if (evaluacion == null)
            return NotFound();

        _unitOfWork.GetRepository<Evaluacione>().Delete(evaluacion);
        await _unitOfWork.Complete();

        return NoContent();
    }
}
