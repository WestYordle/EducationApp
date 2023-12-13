using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EducationApp.Models.Data
{
    public class StructureOrder
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        [ForeignKey("IdOrder")]
        public required Order Order { get; set; }

        [ForeignKey("IdProduct")]
        public required Product Product { get; set; }

        [Required(ErrorMessage = "Введите количество товаров")]
        [Display(Name = "Количество товаров")]
        public required int Cost { get; set; }
    }
}
