using System.ComponentModel.DataAnnotations;

namespace pd311_mvc_aspnet.Models
{
    public class Category : BaseModel<string>
    {
        [Required(ErrorMessage = "Обов'язкове поле")]
        [MaxLength(100, ErrorMessage = "Максимально довжина 100 символів")]
        public string? Name { get; set; }

        public List<Product> Products { get; set; } = [];
    }
}
