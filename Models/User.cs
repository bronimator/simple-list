namespace UserListTestApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public UserType Type { get; set; }

        public DateTime? Last_visit_date { get; set; }
    }
}
