using System.ComponentModel.DataAnnotations;

namespace UserManagerApp.Models
{
    public class ApiKey
    {
        public int Id { get; set; }
        public required string Client {  get; set; }
        public required string Value { get; set; }
    }
}
