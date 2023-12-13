using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EducationApp.Models.Data
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название продукта")]
        [Display(Name = "Название")]
        public string NameProduct { get; set; }

        [Required(ErrorMessage = "Введите название производителя")]
        [Display(Name = "Производитель")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Введите категорию")]
        [Display(Name = "Категория")]
        public int IdСategory { get; set; }

        //навигационное поле
        [ForeignKey("IdСategory")]
        public Сategory Сategory { get; set; }
    }
}
