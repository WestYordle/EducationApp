using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EducationApp.Models.Data;

namespace EducationApp.ViewModels.СategoryViewModels
{
    public class CreateСategoryViewModels
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование категории")]
        [Display(Name = "Категория")]
        public string NameСategory { get; set; }

    }
}
