using System.ComponentModel.DataAnnotations;

namespace XPointBE.Models;

public enum UserRoleEnum
{
    [Display(Name = "Usuario")]
    User,
    [Display(Name = "Empleado")]
    Empleado,
    [Display(Name = "Administrador")]
    Admin
}