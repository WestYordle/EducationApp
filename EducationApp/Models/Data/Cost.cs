using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EducationApp.Models.Data
{
    public class Cost
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Присвойте id продукта")]
        [Display(Name = "ИД продукта")]
        public int IdProduct { get; set; }

        [ForeignKey("IdProduct")]
        public  Product Product { get; set; }

        [Required(ErrorMessage = "Введите дату измененения цены")]
        [Display(Name = "Дата изменения цены")]
        public  DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        public float NewPrice { get; set; }
    }
}
