using System.ComponentModel.DataAnnotations;

namespace pd311_mvc_aspnet.Models
{
    public interface IBaseModel<T>
    {
        T? Id { get; set; }
    }

    public class BaseModel<T> : IBaseModel<T>
    {
        [Key]
        public T? Id { get; set; }
    }
}
