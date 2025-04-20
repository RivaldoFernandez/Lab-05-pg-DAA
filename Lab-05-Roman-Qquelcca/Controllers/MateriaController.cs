using Lab_05_Roman_Qquelcca.DTOs;
using Lab_05_Roman_Qquelcca.Models;
using Lab_05_Roman_Qquelcca.Repository.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Lab_05_Roman_Qquelcca.Controllers;


[ApiController]
[Route("api/qquelcca/[controller]")]
public class MateriasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MateriasController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var materias = await _unitOfWork.GetRepository<Materia>().GetAllAsync();

        var materiaDtos = materias.Select(m => new MateriaDtoGet()
        {
            IdMateria = m.IdMateria,
            IdCurso = m.IdCurso,
            Nombre = m.Nombre,
            Descripcion = m.Descripcion
        });

        return Ok(materiaDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var materia = await _unitOfWork.GetRepository<Materia>().GetByIdAsync(id);
        if (materia == null)
            return NotFound();

        var dto = new MateriaDtoGet
        {
            IdMateria = materia.IdMateria,
            IdCurso = materia.IdCurso,
            Nombre = materia.Nombre,
            Descripcion = materia.Descripcion
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MateriaDto dto)
    {
        var materia = new Materia
        {
            IdCurso = dto.IdCurso,
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion
        };

        await _unitOfWork.GetRepository<Materia>().InsertAsync(materia);
        await _unitOfWork.Complete();

        return CreatedAtAction(nameof(GetById), new { id = materia.IdMateria }, materia);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MateriaDto dto)
    {
        var existing = await _unitOfWork.GetRepository<Materia>().GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.IdCurso = dto.IdCurso;
        existing.Nombre = dto.Nombre;
        existing.Descripcion = dto.Descripcion;

        _unitOfWork.GetRepository<Materia>().Update(existing);
        await _unitOfWork.Complete();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var materia = await _unitOfWork.GetRepository<Materia>().GetByIdAsync(id);
        if (materia == null)
            return NotFound();

        _unitOfWork.GetRepository<Materia>().Delete(materia);
        await _unitOfWork.Complete();

        return NoContent();
    }
}
