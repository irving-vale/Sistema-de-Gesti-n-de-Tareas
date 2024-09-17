using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.Controllers
{
    public class TareasController : ApiController
    {

        private readonly TaskManagerContext db = new TaskManagerContext();

        // GET: api/Tareas
        public IEnumerable<Tarea> Get()
        {
            return db.Tareas.ToList();
        }

        // GET: api/Tareas/id
        public IHttpActionResult Get(int id)
        {
            var tarea = db.Tareas.Find(id);
            if (tarea == null)
            {
                return NotFound();
            }
            return Ok(tarea);
        }

        // POST: api/Tareas
        public IHttpActionResult Post([FromBody] Tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tareas.Add(tarea);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tarea.Id }, tarea);
        }

        // PUT: api/Tareas/id
        public IHttpActionResult Put(int id, [FromBody] Tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTarea = db.Tareas.Find(id);
            if (existingTarea == null)
            {
                return NotFound();
            }
            existingTarea.Titulo = tarea.Titulo;
            existingTarea.Descripcion = tarea.Descripcion;
            existingTarea.FechaCreacion = tarea.FechaCreacion;
            existingTarea.Estado = tarea.Estado;

            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Tareas/id
        public IHttpActionResult Delete(int id)
        {
            var tarea = db.Tareas.Find(id);
            if (tarea == null)
            {
                return NotFound();
            }
            db.Tareas.Remove(tarea);
            db.SaveChanges();
            return Ok(tarea);
        }

    }
}