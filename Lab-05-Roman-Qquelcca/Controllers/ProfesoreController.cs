using Lab_05_Roman_Qquelcca.DTOs;
using Lab_05_Roman_Qquelcca.Models;
using Lab_05_Roman_Qquelcca.Repository.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Lab_05_Roman_Qquelcca.Controllers;

[ApiController]
[Route("api/qquelcca/[controller]")]
public class ProfesoreController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProfesoreController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var profesores = await _unitOfWork.GetRepository<Profesore>().GetAllAsync();

        var result = profesores.Select(p => new ProfesoreDtoGet
        {
            IdProfesor = p.IdProfesor,
            Nombre = p.Nombre,
            Especialidad = p.Especialidad,
            Correo = p.Correo
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var profesor = await _unitOfWork.GetRepository<Profesore>().GetByIdAsync(id);
        if (profesor == null)
            return NotFound();

        var dto = new ProfesoreDtoGet
        {
            IdProfesor = profesor.IdProfesor,
            Nombre = profesor.Nombre,
            Especialidad = profesor.Especialidad,
            Correo = profesor.Correo
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProfesoreDto dto)
    {
        var profesor = new Profesore
        {
            IdProfesor = dto.IdProfesor,
            Nombre = dto.Nombre,
            Especialidad = dto.Especialidad,
            Correo = dto.Correo
        };

        await _unitOfWork.GetRepository<Profesore>().InsertAsync(profesor);
        await _unitOfWork.Complete();

        var result = new ProfesoreDtoGet
        {
            IdProfesor = profesor.IdProfesor,
            Nombre = profesor.Nombre,
            Especialidad = profesor.Especialidad,
            Correo = profesor.Correo
        };

        return CreatedAtAction(nameof(GetById), new { id = profesor.IdProfesor }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProfesoreDto dto)
    {
        var existing = await _unitOfWork.GetRepository<Profesore>().GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.Nombre = dto.Nombre;
        existing.Especialidad = dto.Especialidad;
        existing.Correo = dto.Correo;

        _unitOfWork.GetRepository<Profesore>().Update(existing);
        await _unitOfWork.Complete();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var profesor = await _unitOfWork.GetRepository<Profesore>().GetByIdAsync(id);
        if (profesor == null)
            return NotFound();

        _unitOfWork.GetRepository<Profesore>().Delete(profesor);
        await _unitOfWork.Complete();

        return NoContent();
    }
}
