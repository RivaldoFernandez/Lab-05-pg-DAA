using AutoMapper;
using Lab_05_Roman_Qquelcca.DTOs;
using Lab_05_Roman_Qquelcca.Models;
using Lab_05_Roman_Qquelcca.Repository.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Lab_05_Roman_Qquelcca.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudianteController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EstudianteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET: api/Estudiante
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var estudiantes = await _unitOfWork.Repository<Estudiante>().GetAllAsync();
        
        // Mapea los estudiantes a EstudianteDto (sin las colecciones vacías)
        var estudianteDtos = estudiantes.Select(est => new EstudianteDto
        {
            IdEstudiante = est.IdEstudiante,
            Nombre = est.Nombre,
            Edad = est.Edad,
            Direccion = est.Direccion,
            Telefono = est.Telefono,
            Correo = est.Correo
        });

        return Ok(estudianteDtos);
    }

    // GET: api/Estudiante/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
        if (estudiante == null) return NotFound();

        // Mapea el estudiante a EstudianteGetDto (con colecciones si no están vacías)
        var estudianteGetDto = new EstudianteGetDto
        {
            IdEstudiante = estudiante.IdEstudiante,
            Nombre = estudiante.Nombre,
            Edad = estudiante.Edad,
            Direccion = estudiante.Direccion,
            Telefono = estudiante.Telefono,
            Correo = estudiante.Correo
            // Agregar otras propiedades si es necesario
        };

        return Ok(estudianteGetDto);
    }

    // POST: api/Estudiante
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EstudianteDto estudianteCreateDto)
    {
        // Mapea el EstudianteDto a Estudiante
        var estudiante = new Estudiante
        {
            Nombre = estudianteCreateDto.Nombre,
            Edad = estudianteCreateDto.Edad,
            Direccion = estudianteCreateDto.Direccion,
            Telefono = estudianteCreateDto.Telefono,
            Correo = estudianteCreateDto.Correo
            // Agregar otras propiedades si es necesario
        };

        await _unitOfWork.Repository<Estudiante>().InsertAsync(estudiante);
        await _unitOfWork.Complete();

        // Mapea el estudiante recién creado a EstudianteDto para la respuesta
        var estudianteDto = new EstudianteDto
        {
            IdEstudiante = estudiante.IdEstudiante,
            Nombre = estudiante.Nombre,
            Edad = estudiante.Edad,
            Direccion = estudiante.Direccion,
            Telefono = estudiante.Telefono,
            Correo = estudiante.Correo
            // Agregar otras propiedades si es necesario
        };

        return CreatedAtAction(nameof(GetById), new { id = estudiante.IdEstudiante }, estudianteDto);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EstudianteDto estudianteUpdateDto)
    {
        // Verifica que el modelo sea válido
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Obtén el estudiante existente por su ID
        var existing = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
        if (existing == null) return NotFound();

        // Mapea los campos de EstudianteDto a la entidad Estudiante existente
        existing.Nombre = estudianteUpdateDto.Nombre;
        existing.Edad = estudianteUpdateDto.Edad;
        existing.Direccion = estudianteUpdateDto.Direccion;
        existing.Telefono = estudianteUpdateDto.Telefono;
        existing.Correo = estudianteUpdateDto.Correo;

        // Si hay otros campos que deseas actualizar, agrégales aquí.

        // Aquí no es necesario acceder directamente al DbContext, el repositorio ya maneja el estado
        _unitOfWork.Repository<Estudiante>().Update(existing);

        // Guardar los cambios en la base de datos
        await _unitOfWork.Complete();

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
        if (estudiante == null) return NotFound();

        _unitOfWork.Repository<Estudiante>().Delete(estudiante);
        await _unitOfWork.Complete();

        return NoContent();
    }
}
