using SistemaEstudiantes.Models;

namespace SistemaEstudiantes.Core
{
    public class NodoEstudiante
    {
        public Estudiante Dato { get; set; }
        public NodoEstudiante Siguiente { get; set; }
        public ListaMaterias Materias { get; set; }

        public NodoEstudiante(Estudiante dato)
        {
            Dato = dato;
            Siguiente = null;
            Materias = new ListaMaterias();
        }
    }
}