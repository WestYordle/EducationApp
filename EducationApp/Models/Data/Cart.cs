using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationApp.Models.Data
{
    public class Cart
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название продукта")]
        [Display(Name = "Название")]
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public int ProductId { get; set; }
        /*
        //навигационное поле
        [ForeignKey("IdProduct")]
        public Product Product { get; set; }*/
    }
}
