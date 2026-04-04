using Core;
using Models;

namespace UI
{
    public class Menu
    {
        private ListaEstudiantes listaEstudiantes;

        public Menu()
        {
            listaEstudiantes = new ListaEstudiantes();
        }

        // Muestra el menú principal y gestiona la navegación
        public void MostrarMenuPrincipal()
        {
            int opcion = 0;
            while (opcion != 6)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
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
                Console.ResetColor();
                Console.Write("\n👉 Selecciona una opción: ");

                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: AgregarEstudiante(); break;
                    case 2: listaEstudiantes.Listar(); Pausa(); break;
                    case 3: BuscarEstudiante(); break;
                    case 4: EliminarEstudiante(); break;
                    case 5: GestionarMaterias(); break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n👋 Hasta luego!");
                        Console.ResetColor();
                        break;
                    default:
                        MostrarError("Opción inválida.");
                        Pausa();
                        break;
                }
            }
        }

        // Solicita los datos del estudiante y lo agrega a la lista
        private void AgregarEstudiante()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== AGREGAR ESTUDIANTE ===\n");
            Console.ResetColor();

            int codigo = listaEstudiantes.GenerarCodigo();

            // Validar que el nombre solo tenga letras
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre) || !nombre.All(c => char.IsLetter(c) || c == ' '))
            {
                MostrarError("El nombre solo puede contener letras.");
                Pausa(); return;
            }

            // Validar que el apellido solo tenga letras
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(apellido) || !apellido.All(c => char.IsLetter(c) || c == ' '))
            {
                MostrarError("El apellido solo puede contener letras.");
                Pausa(); return;
            }

            // Validar que la dirección no esté vacía
            Console.Write("Dirección: ");
            string direccion = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(direccion))
            {
                MostrarError("La dirección no puede estar vacía.");
                Pausa(); return;
            }

            // Validar que el celular solo tenga números y tenga entre 7 y 15 dígitos
            Console.Write("Celular: ");
            string celular = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(celular) || !celular.All(char.IsDigit) || celular.Length < 7 || celular.Length > 15)
            {
                MostrarError("El celular solo puede contener números (entre 7 y 15 dígitos).");
                Pausa(); return;
            }

            // Validar que el email tenga formato básico
            Console.Write("Email: ");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@') || !email.Contains('.'))
            {
                MostrarError("El email no tiene un formato válido. Ejemplo: nombre@correo.com");
                Pausa(); return;
            }

            Estudiante nuevo = new Estudiante(codigo, nombre, apellido, direccion, celular, email);
            listaEstudiantes.Agregar(nuevo);

            Pausa();
        }

        // Busca un estudiante por código y muestra sus datos
        private void BuscarEstudiante()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== BUSCAR ESTUDIANTE ===\n");
            Console.ResetColor();

            Console.Write("Ingresa el código del estudiante: ");
            if (!int.TryParse(Console.ReadLine(), out int codigo))
            {
                MostrarError("El código debe ser un número.");
                Pausa(); return;
            }

            NodoEstudiante nodo = listaEstudiantes.Buscar(codigo);

            if (nodo == null)
            {
                MostrarError("Estudiante no encontrado.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n--- DATOS DEL ESTUDIANTE ---");
                Console.ResetColor();
                Console.WriteLine($"Código   : {nodo.Dato.Codigo}");
                Console.WriteLine($"Nombre   : {nodo.Dato.Nombre} {nodo.Dato.Apellido}");
                Console.WriteLine($"Dirección: {nodo.Dato.Direccion}");
                Console.WriteLine($"Celular  : {nodo.Dato.Celular}");
                Console.WriteLine($"Email    : {nodo.Dato.Email}");
            }

            Pausa();
        }

        // Elimina un estudiante de la lista por su código
        private void EliminarEstudiante()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== ELIMINAR ESTUDIANTE ===\n");
            Console.ResetColor();

            Console.Write("Ingresa el código del estudiante a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int codigo))
            {
                MostrarError("El código debe ser un número.");
                Pausa(); return;
            }

            listaEstudiantes.Eliminar(codigo);
            Pausa();
        }

        // Gestiona el submenú de materias de un estudiante específico
        private void GestionarMaterias()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== GESTIONAR MATERIAS ===\n");
            Console.ResetColor();

            Console.Write("Ingresa el código del estudiante: ");
            if (!int.TryParse(Console.ReadLine(), out int codigo))
            {
                MostrarError("El código debe ser un número.");
                Pausa(); return;
            }

            NodoEstudiante nodo = listaEstudiantes.Buscar(codigo);

            if (nodo == null)
            {
                MostrarError("Estudiante no encontrado.");
                Pausa(); return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Estudiante encontrado: {nodo.Dato.Nombre} {nodo.Dato.Apellido}");
            Console.ResetColor();

            int opcion = 0;
            while (opcion != 5)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n╔══════════════════════════════════════╗");
                Console.WriteLine($"║  Materias de: {nodo.Dato.Nombre,-23}║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║  1. Agregar materia                  ║");
                Console.WriteLine("║  2. Listar materias                  ║");
                Console.WriteLine("║  3. Modificar nota                   ║");
                Console.WriteLine("║  4. Eliminar materia                 ║");
                Console.WriteLine("║  5. Volver al menú principal         ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.ResetColor();
                Console.Write("\n👉 Selecciona una opción: ");

                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: AgregarMateria(nodo); break;
                    case 2: nodo.Materias.Listar(); Pausa(); break;
                    case 3: ModificarNota(nodo); break;
                    case 4: EliminarMateria(nodo); break;
                    case 5: break;
                    default:
                        MostrarError("Opción inválida.");
                        Pausa(); break;
                }
            }
        }

        // Agrega una materia con su nota al estudiante
        private void AgregarMateria(NodoEstudiante nodo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== AGREGAR MATERIA ===\n");
            Console.ResetColor();

            Console.Write("Nombre de la materia: ");
            string nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre) || !nombre.All(c => char.IsLetter(c) || c == ' '))
            {
                MostrarError("El nombre de la materia solo puede contener letras.");
                Pausa(); return;
            }

            Console.Write("Nota (0.0 - 5.0): ");
            if (!float.TryParse(Console.ReadLine(), out float nota) || nota < 0 || nota > 5)
            {
                MostrarError("La nota debe ser un número entre 0.0 y 5.0.");
                Pausa(); return;
            }

            Materia materia = new Materia(nombre, nota);
            nodo.Materias.Agregar(materia);
            Pausa();
        }

        // Modifica la nota de una materia existente
        private void ModificarNota(NodoEstudiante nodo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== MODIFICAR NOTA ===\n");
            Console.ResetColor();

            nodo.Materias.Listar();

            Console.Write("\nNombre de la materia a modificar: ");
            string nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MostrarError("El nombre no puede estar vacío.");
                Pausa(); return;
            }

            Console.Write("Nueva nota (0.0 - 5.0): ");
            if (!float.TryParse(Console.ReadLine(), out float nuevaNota) || nuevaNota < 0 || nuevaNota > 5)
            {
                MostrarError("La nota debe ser un número entre 0.0 y 5.0.");
                Pausa(); return;
            }

            nodo.Materias.ModificarNota(nombre, nuevaNota);
            Pausa();
        }

        // Elimina una materia de la lista del estudiante
        private void EliminarMateria(NodoEstudiante nodo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== ELIMINAR MATERIA ===\n");
            Console.ResetColor();

            nodo.Materias.Listar();

            Console.Write("\nNombre de la materia a eliminar: ");
            string nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MostrarError("El nombre no puede estar vacío.");
                Pausa(); return;
            }

            nodo.Materias.Eliminar(nombre);
            Pausa();
        }

        // Muestra un mensaje de error en color rojo
        private void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n⚠️  {mensaje}");
            Console.ResetColor();
        }

        // Pausa la ejecución hasta que el usuario presione Enter
        private void Pausa()
        {
            Console.Write("\nPresiona Enter para continuar...");
            Console.ReadLine();
        }
    }
}