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
        return Ok(profesores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var profesor = await _unitOfWork.GetRepository<Profesore>().GetByIdAsync(id);
        if (profesor == null)
            return NotFound();

        return Ok(profesor);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Profesore profesor)
    {
        await _unitOfWork.GetRepository<Profesore>().InsertAsync(profesor);
        await _unitOfWork.Complete();

        return CreatedAtAction(nameof(GetById), new { id = profesor.IdProfesor }, profesor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Profesore profesor)
    {
        var existing = await _unitOfWork.GetRepository<Profesore>().GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.Nombre = profesor.Nombre;
        existing.Especialidad = profesor.Especialidad;
        existing.Correo = profesor.Correo;

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
