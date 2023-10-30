using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lesohem.Models;

public partial class User
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Не введен логин")]
    public string? Name { get; set; }

    public string? SurName { get; set; }
    [Required(ErrorMessage = "Не введен пароль")]
    public string? Password { get; set; }

    public string? Image { get; set; }

    public string? ErrorMessage { get; set; }

    public int? RoleId { get; set; }

    public int? GroupId { get; set; }

    public int? FormTrainingId { get; set; }

    public virtual FormTraining? FormTraining { get; set; }

    public virtual Group? Group { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<SocialNet> Socials { get; set; } = new List<SocialNet>();
}
