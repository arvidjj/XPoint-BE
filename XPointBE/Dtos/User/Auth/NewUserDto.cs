﻿namespace XPointBE.Dtos.User.Auth;

public class NewUserDto
{
    public string Nombre { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
    
}