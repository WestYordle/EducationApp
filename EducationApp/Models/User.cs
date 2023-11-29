﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CustomIdentityApp.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]
        public string? LastName
        { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string? FirstName{ get; set; }
        [Required(ErrorMessage = "Введите отчество")]
        [Display(Name = "Отчество")]
        public string? Patronymic { get; set; }
    }
}