﻿namespace XPointBE.Dtos.User;

public class UserSimpleDto
{
    public string Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telefono { get; set; }
}