using Lab_05_Roman_Qquelcca.DTOs;
using Lab_05_Roman_Qquelcca.Models;
using Lab_05_Roman_Qquelcca.Repository.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Lab_05_Roman_Qquelcca.Controllers;


[ApiController]
[Route("api/qquelcca/[controller]")]
public class CursoController : ControllerBase
    
{
    private readonly IUnitOfWork _unitOfWork;

    public CursoController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cursos = await _unitOfWork.GetRepository<Curso>().GetAllAsync();

        var cursosDto = cursos.Select(c => new CursoDtoGet()
        {
            IdCurso = c.IdCurso,
            Nombre = c.Nombre,
            Descripcion = c.Descripcion,
            Creditos = c.Creditos
        });

        return Ok(cursosDto);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var curso = await _unitOfWork.GetRepository<Curso>().GetByIdAsync(id);
        if (curso == null)
            return NotFound();

        var cursoDto = new CursoDtoGet
        {
            IdCurso = curso.IdCurso,
            Nombre = curso.Nombre,
            Descripcion = curso.Descripcion,
            Creditos = curso.Creditos
        };

        return Ok(cursoDto);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CursoDto cursoDto)
    {
        var curso = new Curso
        {
            Nombre = cursoDto.Nombre,
            Descripcion = cursoDto.Descripcion,
            Creditos = cursoDto.Creditos
        };

        await _unitOfWork.GetRepository<Curso>().InsertAsync(curso);
        await _unitOfWork.Complete();

        return CreatedAtAction(nameof(GetById), new { id = curso.IdCurso }, new CursoDtoGet
        {
            IdCurso = curso.IdCurso,
            Nombre = curso.Nombre,
            Descripcion = curso.Descripcion,
            Creditos = curso.Creditos
        });
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CursoDto cursoDto)
    {
        var existing = await _unitOfWork.GetRepository<Curso>().GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.Nombre = cursoDto.Nombre;
        existing.Descripcion = cursoDto.Descripcion;
        existing.Creditos = cursoDto.Creditos;

        _unitOfWork.GetRepository<Curso>().Update(existing);
        await _unitOfWork.Complete();

        return NoContent();
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var curso = await _unitOfWork.GetRepository<Curso>().GetByIdAsync(id);
        if (curso == null)
            return NotFound();

        _unitOfWork.GetRepository<Curso>().Delete(curso);
        await _unitOfWork.Complete();

        return NoContent();
    }
}
