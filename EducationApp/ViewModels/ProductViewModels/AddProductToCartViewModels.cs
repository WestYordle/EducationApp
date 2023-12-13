using System.ComponentModel.DataAnnotations;

namespace EducationApp.ViewModels.ProductViewModels
{
    public class AddProductToCartViewModels
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите стоимость")]
        [Display(Name = "Стоимость")]
        public float Amount { get; set; }
    }
}
