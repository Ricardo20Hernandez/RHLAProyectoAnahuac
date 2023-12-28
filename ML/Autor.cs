namespace ML
{
    public class Autor
    {
        //prueba
        public int IdAutor { get; set; }
        public string Nombre { get; set;}

        

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }


        public string FechaNacimiento { get; set; }

        public byte[] Foto { get; set; }

        public List<object> Autores { get; set; }

    }
   
}