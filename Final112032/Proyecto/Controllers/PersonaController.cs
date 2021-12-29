using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comandos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto.Models;
using Resultados;

namespace APIproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : ControllerBase 
    {

        private readonly prog3Context db = new prog3Context();
        private readonly ResultadoAPI resultado = new ResultadoAPI();

        private readonly ILogger<PersonaController> _logger; 

        public PersonaController(ILogger<PersonaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("ObtenerPersonas")]
        public ActionResult<ResultadoAPI> Get()
        {
            var resultado = new ResultadoAPI();
            resultado.ok = true;
            resultado.Return = db.Personas.ToList();
            return resultado;
        }

        [HttpGet]
        [Route("ObtenerExequiel")]
        public ActionResult<ResultadoAPI> ObtenerExequiel()
        {
            string nombre= "Exequiel";
            var per = db.Personas.FirstOrDefault(c => c.Nombre == nombre);                

            var resultado = new ResultadoAPI();
            resultado.ok = true;
            resultado.Return = per;           

            return resultado;
        }

        [HttpGet]
        [Route("Alumno/GetAlumnoId/{idAlumno}")]

        public ActionResult<ResultadoAPI> getByID(int idAlumno)
        {
            var resultado = new ResultadoAPI();
            try
            {
                var alumno = db.Alumnos.Where(c => c.AlumnoId == idAlumno).FirstOrDefault();
                resultado.Ok = true;
                resultado.Return = alumno;

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Alumno no encontrado en la base de datos - " + ex.Message;

                return resultado;
            }
        }


        // CREATE   
        [HttpPost]
        [Route("Alumno/CrearAlumno")]
        public ActionResult<ResultadoAPI> PostAlumno([FromBody] ComandoCrearAlumno comando)
        {
            var resultado = new ResultadoAPI();
            // ejemplo validacion en back
            if (comando.Nombre.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese nombre";
                return resultado;
            }
            if (comando.Apellido.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese Apellido";
                return resultado;
            }

            if (comando.Curso.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese Curso";
                return resultado;
            }
            //funcion CREATE basica
            var alumno = new Alumno();
            alumno.Nombre = comando.Nombre;
            alumno.Apellido = comando.Apellido;
            alumno.Curso = comando.Curso;
            alumno.SexoId = comando.SexoId;

            db.Alumnos.Add(alumno);
            db.SaveChanges();

            resultado.Ok = true;
            resultado.Return = db.Alumnos.ToList();

            return resultado;
        }

        [HttpPut] 
        [Route("UpdatePersona")]
        public ActionResult<ResultadoAPI> UpdatePersona([FromBody]comandoUpdatePersona comando)
        {
            var resultado=new ResultadoAPI();

            var per=db.Personas.Where(c=>c.Id==comando.Id).FirstOrDefault();
            if(per != null)
            {
                per.Nombre=comando.Nombre;
                per.Apellido=comando.Apellido;
                per.Edad=comando.Edad;
                per.Telefono=comando.Telefono;
                per.Sexo=comando.Sexo;
                per.Casado=comando.Casado;

                db.Personas.Update(per);
                db.SaveChanges();
            }

            resultado.ok=true;
            resultado.Return=db.Personas.ToList();

            return resultado;
        }

          [HttpDelete]
        [Route("Alumno/DeleteAlumno/{idAlumno}")]
        public ActionResult<ResultadoAPI> deleteById(int idAlumno)
        {
            var resultado = new ResultadoAPI();
            var alumno = db.Alumnos.Where(c => c.AlumnoId == idAlumno).FirstOrDefault();
            db.Alumnos.Remove(alumno);
            db.SaveChanges();

            resultado.Ok = true;
            resultado.Return = db.Alumnos.ToList();
            return resultado;
        }

     }
 }
