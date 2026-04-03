namespace SistemaEstudiantes.Models
{
    public class Materia
    {
        public string Nombre { get; set; }
        public float Nota { get; set; }

        public Materia(string nombre, float nota)
        {
            Nombre = nombre;
            Nota = nota;
        }
    }
}