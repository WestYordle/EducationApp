using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EducationApp.Models.Data
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        [ForeignKey("IdUser")]
        public required User User { get; set; }

        [Required(ErrorMessage = "Введите дату заказа")]
        [Display(Name = "Дата заказа")]
        public required DateTime Date { get; set; }
    }
}
