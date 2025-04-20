

using Lab_05_Roman_Qquelcca.DTOs;
using Lab_05_Roman_Qquelcca.Models;
using Lab_05_Roman_Qquelcca.Repository.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Lab_05_Roman_Qquelcca.Controllers;


[ApiController]
[Route("api/qquelcca/[controller]")]
public class EstudianteController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EstudianteController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var estudiantes = await _unitOfWork.GetRepository<Estudiante>().GetAllAsync();

        var estudiantesDto = estudiantes.Select(e => new EstudianteGetDto()
        {
            IdEstudiante = e.IdEstudiante,
            Nombre = e.Nombre,
            Edad = e.Edad,
            Direccion = e.Direccion,
            Telefono = e.Telefono,
            Correo = e.Correo
        });

        return Ok(estudiantesDto);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var estudiante = await _unitOfWork.GetRepository<Estudiante>().GetByIdAsync(id);
        if (estudiante == null)
            return NotFound();

        var estudianteDto = new EstudianteGetDto
        {
            IdEstudiante = estudiante.IdEstudiante,
            Nombre = estudiante.Nombre,
            Edad = estudiante.Edad,
            Direccion = estudiante.Direccion,
            Telefono = estudiante.Telefono,
            Correo = estudiante.Correo
        };

        return Ok(estudianteDto);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EstudianteDto estudianteDto)
    {
        var estudiante = new Estudiante
        {
            Nombre = estudianteDto.Nombre,
            Edad = estudianteDto.Edad,
            Direccion = estudianteDto.Direccion,
            Telefono = estudianteDto.Telefono,
            Correo = estudianteDto.Correo
        };

        await _unitOfWork.GetRepository<Estudiante>().InsertAsync(estudiante);
        await _unitOfWork.Complete();

        return CreatedAtAction(nameof(GetById), new { id = estudiante.IdEstudiante }, new EstudianteGetDto
        {
            IdEstudiante = estudiante.IdEstudiante,
            Nombre = estudiante.Nombre,
            Edad = estudiante.Edad,
            Direccion = estudiante.Direccion,
            Telefono = estudiante.Telefono,
            Correo = estudiante.Correo
        });
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EstudianteDto estudianteDto)
    {
        var existing = await _unitOfWork.GetRepository<Estudiante>().GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.Nombre = estudianteDto.Nombre;
        existing.Edad = estudianteDto.Edad;
        existing.Direccion = estudianteDto.Direccion;
        existing.Telefono = estudianteDto.Telefono;
        existing.Correo = estudianteDto.Correo;

        _unitOfWork.GetRepository<Estudiante>().Update(existing);
        await _unitOfWork.Complete();

        return NoContent();
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var estudiante = await _unitOfWork.GetRepository<Estudiante>().GetByIdAsync(id);
        if (estudiante == null)
            return NotFound();

        _unitOfWork.GetRepository<Estudiante>().Delete(estudiante);
        await _unitOfWork.Complete();

        return NoContent();
    }
}
