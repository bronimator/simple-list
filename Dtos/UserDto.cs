namespace UserListTestApp.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int TypeId { get; set; }

        public DateTime? Last_visit_date { get; set; }
    }
}
