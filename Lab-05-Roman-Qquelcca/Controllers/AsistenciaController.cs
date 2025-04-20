using Lab_05_Roman_Qquelcca.Repository.Unit;

namespace Lab_05_Roman_Qquelcca.Controllers;


using Lab_05_Roman_Qquelcca.DTOs;
using Lab_05_Roman_Qquelcca.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/qquelcca/[controller]")]
public class AsistenciaController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AsistenciaController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var asistencias = await _unitOfWork.GetRepository<Asistencia>().GetAllAsync();

        var asistenciaDtos = asistencias.Select(a => new AsistenciaDtoGet
        {
            IdAsistencia = a.IdAsistencia,
            IdEstudiante = a.IdEstudiante,
            IdCurso = a.IdCurso,
            Fecha = a.Fecha,
            Estado = a.Estado
        });

        return Ok(asistenciaDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var asistencia = await _unitOfWork.GetRepository<Asistencia>().GetByIdAsync(id);
        if (asistencia == null)
            return NotFound();

        var dto = new AsistenciaDtoGet
        {
            IdAsistencia = asistencia.IdAsistencia,
            IdEstudiante = asistencia.IdEstudiante,
            IdCurso = asistencia.IdCurso,
            Fecha = asistencia.Fecha,
            Estado = asistencia.Estado
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AsistenciaDto dto)
    {
        var asistencia = new Asistencia
        {
            IdEstudiante = dto.IdEstudiante,
            IdCurso = dto.IdCurso,
            Fecha = dto.Fecha,
            Estado = dto.Estado
        };

        await _unitOfWork.GetRepository<Asistencia>().InsertAsync(asistencia);
        await _unitOfWork.Complete();

        return CreatedAtAction(nameof(GetById), new { id = asistencia.IdAsistencia }, asistencia);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AsistenciaDto dto)
    {
        var existing = await _unitOfWork.GetRepository<Asistencia>().GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.IdEstudiante = dto.IdEstudiante;
        existing.IdCurso = dto.IdCurso;
        existing.Fecha = dto.Fecha;
        existing.Estado = dto.Estado;

        _unitOfWork.GetRepository<Asistencia>().Update(existing);
        await _unitOfWork.Complete();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var asistencia = await _unitOfWork.GetRepository<Asistencia>().GetByIdAsync(id);
        if (asistencia == null)
            return NotFound();

        _unitOfWork.GetRepository<Asistencia>().Delete(asistencia);
        await _unitOfWork.Complete();

        return NoContent();
    }
}
