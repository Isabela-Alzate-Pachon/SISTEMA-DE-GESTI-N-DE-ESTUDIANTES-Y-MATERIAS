using SistemaEstudiantes.Core;
using SistemaEstudiantes.Models;

namespace SistemaEstudiantes.UI
{
    public class Menu
    {
        private ListaEstudiantes listaEstudiantes;

        public Menu()
        {
            listaEstudiantes = new ListaEstudiantes();
        }

        public void MostrarMenuPrincipal()
        {
            int opcion = 0;
            while (opcion != 6)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║   SISTEMA DE GESTIÓN DE ESTUDIANTES  ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║  1. Agregar estudiante               ║");
                Console.WriteLine("║  2. Listar estudiantes               ║");
                Console.WriteLine("║  3. Buscar estudiante                ║");
                Console.WriteLine("║  4. Eliminar estudiante              ║");
                Console.WriteLine("║  5. Gestionar materias               ║");
                Console.WriteLine("║  6. Salir                            ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.Write("\n👉 Selecciona una opción: ");

                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: AgregarEstudiante(); break;
                    case 2: listaEstudiantes.Listar(); Pausa(); break;
                    case 3: BuscarEstudiante(); break;
                    case 4: EliminarEstudiante(); break;
                    case 5: GestionarMaterias(); break;
                    case 6: Console.WriteLine("\n👋 Hasta luego!"); break;
                    default: Console.WriteLine("\n⚠️  Opción inválida."); Pausa(); break;
                }
            }
        }

        private void AgregarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR ESTUDIANTE ===\n");

            int codigo = listaEstudiantes.GenerarCodigo();

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();

            Console.Write("Dirección: ");
            string direccion = Console.ReadLine();

            Console.Write("Celular: ");
            string celular = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Estudiante nuevo = new Estudiante(codigo, nombre, apellido, direccion, celular, email);
            listaEstudiantes.Agregar(nuevo);

            Pausa();
        }

        private void BuscarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("=== BUSCAR ESTUDIANTE ===\n");

            Console.Write("Ingresa el código del estudiante: ");
            int.TryParse(Console.ReadLine(), out int codigo);

            NodoEstudiante nodo = listaEstudiantes.Buscar(codigo);

            if (nodo == null)
            {
                Console.WriteLine("\n⚠️  Estudiante no encontrado.");
            }
            else
            {
                Console.WriteLine("\n--- DATOS DEL ESTUDIANTE ---");
                Console.WriteLine($"Código   : {nodo.Dato.Codigo}");
                Console.WriteLine($"Nombre   : {nodo.Dato.Nombre} {nodo.Dato.Apellido}");
                Console.WriteLine($"Dirección: {nodo.Dato.Direccion}");
                Console.WriteLine($"Celular  : {nodo.Dato.Celular}");
                Console.WriteLine($"Email    : {nodo.Dato.Email}");
            }

            Pausa();
        }

        private void EliminarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("=== ELIMINAR ESTUDIANTE ===\n");

            Console.Write("Ingresa el código del estudiante a eliminar: ");
            int.TryParse(Console.ReadLine(), out int codigo);

            listaEstudiantes.Eliminar(codigo);
            Pausa();
        }

        private void GestionarMaterias()
        {
            Console.Clear();
            Console.WriteLine("=== GESTIONAR MATERIAS ===\n");

            Console.Write("Ingresa el código del estudiante: ");
            int.TryParse(Console.ReadLine(), out int codigo);

            NodoEstudiante nodo = listaEstudiantes.Buscar(codigo);

            if (nodo == null)
            {
                Console.WriteLine("\n⚠️  Estudiante no encontrado.");
                Pausa();
                return;
            }

            Console.WriteLine($"\n✅ Estudiante encontrado: {nodo.Dato.Nombre} {nodo.Dato.Apellido}");

            int opcion = 0;
            while (opcion != 5)
            {
                Console.WriteLine("\n╔══════════════════════════════════════╗");
                Console.WriteLine($"║  Materias de: {nodo.Dato.Nombre,-23}║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║  1. Agregar materia                  ║");
                Console.WriteLine("║  2. Listar materias                  ║");
                Console.WriteLine("║  3. Modificar nota                   ║");
                Console.WriteLine("║  4. Eliminar materia                 ║");
                Console.WriteLine("║  5. Volver al menú principal         ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.Write("\n👉 Selecciona una opción: ");

                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: AgregarMateria(nodo); break;
                    case 2: nodo.Materias.Listar(); Pausa(); break;
                    case 3: ModificarNota(nodo); break;
                    case 4: EliminarMateria(nodo); break;
                    case 5: break;
                    default: Console.WriteLine("\n⚠️  Opción inválida."); Pausa(); break;
                }
            }
        }

        private void AgregarMateria(NodoEstudiante nodo)
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR MATERIA ===\n");

            Console.Write("Nombre de la materia: ");
            string nombre = Console.ReadLine();

            Console.Write("Nota (0.0 - 5.0): ");
            float.TryParse(Console.ReadLine(), out float nota);

            if (nota < 0 || nota > 5)
            {
                Console.WriteLine("\n⚠️  La nota debe estar entre 0.0 y 5.0.");
                Pausa();
                return;
            }

            Materia materia = new Materia(nombre, nota);
            nodo.Materias.Agregar(materia);
            Pausa();
        }

        private void ModificarNota(NodoEstudiante nodo)
        {
            Console.Clear();
            Console.WriteLine("=== MODIFICAR NOTA ===\n");

            nodo.Materias.Listar();

            Console.Write("\nNombre de la materia a modificar: ");
            string nombre = Console.ReadLine();

            Console.Write("Nueva nota (0.0 - 5.0): ");
            float.TryParse(Console.ReadLine(), out float nuevaNota);

            if (nuevaNota < 0 || nuevaNota > 5)
            {
                Console.WriteLine("\n⚠️  La nota debe estar entre 0.0 y 5.0.");
                Pausa();
                return;
            }

            nodo.Materias.ModificarNota(nombre, nuevaNota);
            Pausa();
        }

        private void EliminarMateria(NodoEstudiante nodo)
        {
            Console.Clear();
            Console.WriteLine("=== ELIMINAR MATERIA ===\n");

            nodo.Materias.Listar();

            Console.Write("\nNombre de la materia a eliminar: ");
            string nombre = Console.ReadLine();

            nodo.Materias.Eliminar(nombre);
            Pausa();
        }

        private void Pausa()
        {
            Console.Write("\nPresiona Enter para continuar...");
            Console.ReadLine();
        }
    }
}