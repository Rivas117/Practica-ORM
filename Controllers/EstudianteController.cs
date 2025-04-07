using Microsoft.AspNetCore.Mvc;
using Practica_ORM.interfaces;
using Practica_ORM.Entities;

namespace Practica_ORM.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudianteController : ControllerBase
{
    private readonly IEstudianteService _estudianteService;

    public EstudianteController(IEstudianteService estudianteService)
    {
        _estudianteService = estudianteService;
    }

    // GET: api/Estudiante
    [HttpGet]
    public ActionResult<IEnumerable<Estudiante>> GetEstudiantes()
    {
        var estudiantes = _estudianteService.ObtenerTodosConDocumento();
        return Ok(estudiantes);
    }

    // GET: api/Estudiante/5
    [HttpGet("{id}")]
    public ActionResult<Estudiante> GetEstudiante(int id)
    {
        var estudiante = _estudianteService.ObtenerEstudiantePorId(id);

        if (estudiante == null)
            return NotFound();

        return Ok(estudiante);
    }

    // POST: api/Estudiante
    [HttpPost]
    public ActionResult PostEstudiante(Estudiante estudiante)
    {
        _estudianteService.AgregarEstudiante(estudiante);
        return Ok("Estudiante creado");
    }

    // PUT: api/Estudiante/5
    [HttpPut("{id}")]
    public ActionResult PutEstudiante(int id, Estudiante estudiante)
    {
        estudiante.Id = id;
        _estudianteService.ActualizarEstudiante(estudiante);
        return Ok("Estudiante actualizado");
    }

    // DELETE: api/Estudiante/5
    [HttpDelete("{id}")]
    public ActionResult DeleteEstudiante(int id)
    {
        _estudianteService.EliminarEstudiante(id);
        return Ok("Estudiante eliminado");
    }
}