using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EducationApp.Models.Data
{
    public class User : IdentityUser
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