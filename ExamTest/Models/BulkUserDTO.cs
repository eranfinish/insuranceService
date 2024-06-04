namespace ExamTest.Models
{
    public class BulkUserDTO
    {
        public List<UserDTO> Users { get; set; }
    }
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

}
