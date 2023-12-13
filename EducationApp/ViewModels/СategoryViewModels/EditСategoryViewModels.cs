using System.ComponentModel.DataAnnotations;

namespace EducationApp.ViewModels.СategoryViewModels
{
    public class EditСategoryViewModels
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование категории")]
        [Display(Name = "Категория")]
        public string NameСategory { get; set; }
    }
}
