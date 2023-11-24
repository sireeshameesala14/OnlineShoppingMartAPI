using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class LoginDetails
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    [Key]
    public long UserId { get; set; }
    public string UserType { get; set; } = null!;
    public bool IsActive { get; set; }
}
