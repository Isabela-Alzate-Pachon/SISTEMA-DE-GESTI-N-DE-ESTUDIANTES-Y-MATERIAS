using Models;

namespace Core
{
    public class ListaEstudiantes
    {
        private NodoEstudiante cabeza;
        private int contadorCodigo;

        public ListaEstudiantes()
        {
            cabeza = null;
            contadorCodigo = 1;
        }

        public int GenerarCodigo()
        {
            return contadorCodigo++;
        }

        public void Agregar(Estudiante estudiante)
        {
            NodoEstudiante nuevo = new NodoEstudiante(estudiante);

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                NodoEstudiante actual = cabeza;
                while (actual.Siguiente != null)
                    actual = actual.Siguiente;
                actual.Siguiente = nuevo;
            }

            Console.WriteLine($"\n Estudiante agregado con código: {estudiante.Codigo}");
        }

        public void Listar()
        {
            if (cabeza == null)
            {
                Console.WriteLine("\n  No hay estudiantes registrados.");
                return;
            }

            NodoEstudiante actual = cabeza;
            int i = 1;
            Console.WriteLine("\n--- ESTUDIANTES REGISTRADOS ---");
            while (actual != null)
            {
                Console.WriteLine($"{i}. [{actual.Dato.Codigo}] {actual.Dato.Nombre} {actual.Dato.Apellido} - {actual.Dato.Email}");
                actual = actual.Siguiente;
                i++;
            }
        }

        public NodoEstudiante Buscar(int codigo)
        {
            NodoEstudiante actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Codigo == codigo)
                    return actual;
                actual = actual.Siguiente;
            }
            return null;
        }

        public void Eliminar(int codigo)
        {
            if (cabeza == null)
            {
                Console.WriteLine("\n No hay estudiantes registrados.");
                return;
            }

            if (cabeza.Dato.Codigo == codigo)
            {
                cabeza = cabeza.Siguiente;
                Console.WriteLine("\n Estudiante eliminado correctamente.");
                return;
            }

            NodoEstudiante anterior = cabeza;
            NodoEstudiante actual = cabeza.Siguiente;

            while (actual != null)
            {
                if (actual.Dato.Codigo == codigo)
                {
                    anterior.Siguiente = actual.Siguiente;
                    Console.WriteLine("\n Estudiante eliminado correctamente.");
                    return;
                }
                anterior = actual;
                actual = actual.Siguiente;
            }

            Console.WriteLine("\n  Estudiante no encontrado.");
        }
    }
}