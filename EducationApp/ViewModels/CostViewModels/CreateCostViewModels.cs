using EducationApp.Models.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EducationApp.ViewModels.CostViewModels
{
    public class CreateCostViewModels
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Присвойте id продукта")]
        [Display(Name = "ИД продукта")]
        public int IdProduct { get; set; }

        [Required(ErrorMessage = "Введите дату измененения цены")]
        [Display(Name = "Дата изменения цены")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        public float NewPrice { get; set; }
    }
}
