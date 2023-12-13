using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EducationApp.ViewModels.ProductViewModels
{
    public class CreateProductViewModels
    {

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
    }
}
