using SistemaEstudiantes.Models;

namespace SistemaEstudiantes.Core
{
    public class ListaMaterias
    {
        private NodoMateria cabeza;

        public ListaMaterias()
        {
            cabeza = null;
        }

        // Buscar materia por nombre (para validar duplicados)
        public NodoMateria Buscar(string nombre)
        {
            NodoMateria actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Nombre.ToLower() == nombre.ToLower())
                    return actual;
                actual = actual.Siguiente;
            }
            return null;
        }

        // Agregar materia (evita duplicados)
        public void Agregar(Materia materia)
        {
            if (Buscar(materia.Nombre) != null)
            {
                Console.WriteLine("\n  Esta materia ya está registrada para este estudiante.");
                return;
            }

            NodoMateria nuevo = new NodoMateria(materia);

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                NodoMateria actual = cabeza;
                while (actual.Siguiente != null)
                    actual = actual.Siguiente;
                actual.Siguiente = nuevo;
            }

            Console.WriteLine("\n Materia agregada correctamente.");
        }

        // Listar todas las materias
        public void Listar()
        {
            if (cabeza == null)
            {
                Console.WriteLine("\n  Este estudiante no tiene materias registradas.");
                return;
            }

            NodoMateria actual = cabeza;
            int i = 1;
            Console.WriteLine("\n--- MATERIAS REGISTRADAS ---");
            while (actual != null)
            {
                Console.WriteLine($"{i}. {actual.Dato.Nombre} - Nota: {actual.Dato.Nota}");
                actual = actual.Siguiente;
                i++;
            }
        }

        // Modificar nota de una materia
        public void ModificarNota(string nombre, float nuevaNota)
        {
            NodoMateria nodo = Buscar(nombre);
            if (nodo == null)
            {
                Console.WriteLine("\n  Materia no encontrada.");
                return;
            }

            nodo.Dato.Nota = nuevaNota;
            Console.WriteLine("\n Nota actualizada correctamente.");
        }

        // Eliminar materia
        public void Eliminar(string nombre)
        {
            if (cabeza == null)
            {
                Console.WriteLine("\n  No hay materias registradas.");
                return;
            }

            if (cabeza.Dato.Nombre.ToLower() == nombre.ToLower())
            {
                cabeza = cabeza.Siguiente;
                Console.WriteLine("\n Materia eliminada correctamente.");
                return;
            }

            NodoMateria anterior = cabeza;
            NodoMateria actual = cabeza.Siguiente;

            while (actual != null)
            {
                if (actual.Dato.Nombre.ToLower() == nombre.ToLower())
                {
                    anterior.Siguiente = actual.Siguiente;
                    Console.WriteLine("\n Materia eliminada correctamente.");
                    return;
                }
                anterior = actual;
                actual = actual.Siguiente;
            }

            Console.WriteLine("\n  Materia no encontrada.");
        }
    }
}