using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationApp.Models.Data
{
    public class Сategory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование категории")]
        [Display(Name = "Категория")]
        public string NameСategory { get; set; }

        public ICollection<Product> Product { get; set; }

    }
}
