namespace UserListTestApp.Models
{
    public class UserFilteredDto
    {
        public string Name { get; set; }

        public int? TypeId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
