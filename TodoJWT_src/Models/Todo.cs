namespace TodoJWT.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
        
        public int UsuarioId {get; set;}

        public Usuario Usuario { get; set; }
    }
}