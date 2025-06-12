using System.ComponentModel.DataAnnotations;

namespace XPointBE.Models;

public enum ReservaEstadoEnum
{
    [Display(Name = "Pendiente")]
    Pendiente,
    [Display(Name = "Confirmada")]
    Confirmada,
    [Display(Name = "En Proceso")]
    EnProceso,
    [Display(Name = "Completada")]
    Completada,
    [Display(Name = "Cancelada")]
    Cancelada
}