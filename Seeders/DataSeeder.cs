using Microsoft.VisualBasic;
using Practica_ORM.Context;
using Practica_ORM.Entities;


namespace Practica_ORM.Seeders
{
    public static class DataSeeder
    {

        /// <summary>
        /// // Este método será llamado desde Program.cs
        /// </summary>
        /// <param name="context"> El context es como decirle quiero añadir un nuevo Estudiante </param>
        public static void Seed(EscuelaDbContext context)
        {
            ///<summary>
            /// Verificamos si ya hay datos en la base
            /// Si ya hay, salimos para evitar duplicados
            /// </summary>
            if (context.Estudiantes.Any() || context.Clases.Any())
                return;

            var profesor1 = new Profesor
            {
                NombreProfesor = "Mario Bros",
                Clases = new List<Clase>()
            };


            // Clases
            var clase1 = new Clase
            {
                NombreClase = "Matematicas",
                Profesor = profesor1,
                EstudianteClases = new List<EstudianteClase>()
            };

            var clase2 = new Clase
            {
                NombreClase = "Historia",
                Profesor = profesor1,
                EstudianteClases = new List<EstudianteClase>()
            };


            // Estudiantes
            var estudiante1 = new Estudiante
            {
                Nombre = "Maria Jose",
                Edad = 22,
                EstudianteClases = new List<EstudianteClase>()
            };

            var estudiante2 = new Estudiante
            {
                Nombre = "Jose Maria",
                Edad = 22,
                EstudianteClases = new List<EstudianteClase>()
            };


            // Documentos
            var documento1 = new Documento
            {
                NumeroDocumento = "ID123456",
                Estudiante = estudiante1 // ← requerido
            };

            var documento2 = new Documento
            {
                NumeroDocumento = "ID654321",
                Estudiante = estudiante2
            };

            // Relacion 1:1 Con Documentos
            estudiante1.Documento = documento1;
            estudiante2.Documento = documento2;


            // EstudianteClase
            var escla1 = new EstudianteClase
            {
                Estudiante = estudiante1,
                Clase = clase1
            };

            var escla2 = new EstudianteClase
            {
                Estudiante = estudiante1,
                Clase = clase2
            };

            var escla3 = new EstudianteClase
            {
                Estudiante = estudiante2,
                Clase = clase2
            };

            // Relacion M:M en EstudianteClase = Estudiantes + Clases
            estudiante1.EstudianteClases.AddRange(new[] { escla1,escla2 });
            estudiante2.EstudianteClases.Add(escla3);

            clase1.EstudianteClases.Add(escla1);
            clase2.EstudianteClases.AddRange(new[] {escla2,escla3});

            // Bandeja que dice que es lo que queresmos guardar 
            context.AddRange(
                profesor1,
                clase1,clase2,
                estudiante1,estudiante2,
                documento1,documento2,
                escla1,escla2,escla3
            );

            /// Rapidamente le decimos que lo guarde
            /// Genera todas las consultas SQL necesarias
            /// Ejecuta la bamdeja de Rangos en el orden correcto
            context.SaveChanges();
        }
    }
}