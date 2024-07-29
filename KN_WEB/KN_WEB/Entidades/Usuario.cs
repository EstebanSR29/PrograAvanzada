namespace KN_WEB.Entidades
{
    public class Usuario
    {
        public int Consecutivo { get; set; }

        public string Correo { get; set; }

        public string Password { get; set; }
        public string ConfirmarContrasenna { get; set; }

        public string Cedula { get; set; }

        public string Nombre { get; set; }

        public bool Estado { get; set; }

        public byte IdRol {  get; set; }
    }
}