using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDO.Application.Concrete;
using ToDO.Application.Interfaces;
using ToDO.Domain.Entities;

namespace ToDO.Controllers
{
    /// <summary>
    /// </summary>
    [Route("api/task")]
    public class TaskController : ApiController
    {
        private readonly ITaskService _taskService;

        /// <summary>
        /// </summary>
        public TaskController()
        {
            _taskService = new TaskService();
        }

        // GET api/task
        /// <summary>
        /// Lee toda las tareas en la base de datos y devuelve un Listado de tareas.
        /// </summary>
        /// /// <para>
        /// Here's how you could make a second paragraph in a description. <see cref="System.Console.WriteLine(System.String)"/> for information about output statements.
        /// </para>

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var tasks = _taskService.GetAllTasks();

            if (tasks == null) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tareas no encontradas");

            var modelEntities = tasks as List<Task> ?? tasks.ToList();

            return modelEntities.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, modelEntities)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tareas no encontradas");
        }

        // GET api/task/5
        /// <summary>Busca y devuelve una tarea para el id espeficicado</summary>
        /// <value>Entero</value>
        /// <param name="id"> Identificador unico de la tarea</param>
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var task = _taskService.GetTaskById(id);

            return task != null
                ? Request.CreateResponse(HttpStatusCode.OK, task)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No hay Tareas para este Id");
        }

        // POST api/task
        /// <summary>Crea una tarea y devuelve el id registrado</summary>
        /// <param name="m">Modelo datos Tarea</param>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Task m)
        {
            //return _taskService.CreateTask()
            return Request.CreateResponse(HttpStatusCode.OK, 0);
        }

        // PUT api/task/5
        /// <summary>Actualiza una tarea para el id espeficicado </summary>
        /// <param name="id">Identificador unico de la tarea</param>
        /// <param name="value">Modelo datos Tarea</param>
        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/task/5
        /// <summary>Elimina una tarea para el id espeficicado.</summary>
        /// <param name="id"> Identificador unico de la tarea</param>
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}