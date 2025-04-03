/// <summary>
/// El estudiante tiene relacion 1:1 con documento
/// y tiene relacion M:M con Clase que se conecta junto a EstudianteClase
/// </summary>

namespace Practica_ORM.Entities
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }

        public Documento Documento { get; set; } // 1:1
        public List<EstudianteClase> EstudianteClases { get; set; }//M:M
    }
}
