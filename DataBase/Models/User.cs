namespace TimeTable.DataBase.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Role { get; set; }
    }
}
