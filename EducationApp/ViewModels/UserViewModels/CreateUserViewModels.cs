using System.ComponentModel.DataAnnotations;

namespace EducationApp.ViewModels.UserViewModels
{
    public class CreateUserViewModels
    {
        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public required string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string? Patronymic { get; set; }
    }
}
