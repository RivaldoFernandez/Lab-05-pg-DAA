using Lab_05_Roman_Qquelcca.DTOs;
using Lab_05_Roman_Qquelcca.Models;
using Lab_05_Roman_Qquelcca.Repository.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Lab_05_Roman_Qquelcca.Controllers;


[ApiController]
[Route("api/qquelcca/[controller]")]
public class MatriculaController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MatriculaController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var matriculas = await _unitOfWork.GetRepository<Matricula>().GetAllAsync();

        var matriculaDtos = matriculas.Select(m => new MatriculaDtoGet()
        {
            IdMatricula = m.IdMatricula,
            IdEstudiante = m.IdEstudiante,
            IdCurso = m.IdCurso,
            Semestre = m.Semestre
        });

        return Ok(matriculaDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var matricula = await _unitOfWork.GetRepository<Matricula>().GetByIdAsync(id);
        if (matricula == null)
            return NotFound();

        var dto = new MatriculaDtoGet
        {
            IdMatricula = matricula.IdMatricula,
            IdEstudiante = matricula.IdEstudiante,
            IdCurso = matricula.IdCurso,
            Semestre = matricula.Semestre
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MatriculaDto dto)
    {
        var matricula = new Matricula
        {
            IdEstudiante = dto.IdEstudiante,
            IdCurso = dto.IdCurso,
            Semestre = dto.Semestre
        };

        await _unitOfWork.GetRepository<Matricula>().InsertAsync(matricula);
        await _unitOfWork.Complete();

        return CreatedAtAction(nameof(GetById), new { id = matricula.IdMatricula }, matricula);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MatriculaDto dto)
    {
        var existing = await _unitOfWork.GetRepository<Matricula>().GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.IdEstudiante = dto.IdEstudiante;
        existing.IdCurso = dto.IdCurso;
        existing.Semestre = dto.Semestre;

        _unitOfWork.GetRepository<Matricula>().Update(existing);
        await _unitOfWork.Complete();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var matricula = await _unitOfWork.GetRepository<Matricula>().GetByIdAsync(id);
        if (matricula == null)
            return NotFound();

        _unitOfWork.GetRepository<Matricula>().Delete(matricula);
        await _unitOfWork.Complete();

        return NoContent();
    }
}
