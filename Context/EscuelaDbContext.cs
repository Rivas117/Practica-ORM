using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; /// Asi podremos utilizar ORM
using Practica_ORM.Entities;

namespace Practica_ORM.Context /// El Context lo ponemos porque forma parte de esa carpeta 
{
    public class EscuelaDbContext: DbContext /// El DbContext Significa que vamos a utilizar lo del ORM que trae desde EntityFrameworkCore
    {
        /// <summary>
        /// El DbContextOptions es el que contiene configuraciones
        /// de a que Base De Datos nos vamos a conectar y demas
        /// </summary>
        /// <param name="options"> Es el que le dice que le pase estas opciones a DbContext</param>
        public EscuelaDbContext(DbContextOptions<EscuelaDbContext> options): base(options){
        }

        public DbSet<Clase> Clases { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<EstudianteClase> EstudianteClases { get; set; }
        public DbSet<Profesor> Profesores { get; set; }

        /// <summary>
        /// Configura la relación muchos a muchos (M:M) entre Estudiante y Clase
        /// utilizando una entidad intermedia explícita llamada EstudianteClase.
        /// Se establece una clave primaria compuesta en la tabla intermedia
        /// utilizando las claves foráneas de Estudiante y Clase.
        /// Esto permite que un estudiante esté inscrito en muchas clases
        /// y que una clase tenga muchos estudiantes.
        /// </summary>
        /// <param name="modelBuilder"> Es donde guardamos la config de la tabala intermedia </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); /// Llama la configuración base del framework

            modelBuilder.Entity<EstudianteClase>() /// Configura la entidad intermedia
                .HasKey(ec => new { ec.EstudianteId, ec.ClaseId }); /// Define clave primaria compuesta
        }
    }
}
