using System;
using System.Collections.Generic;

namespace lesohem.Models;

public partial class Group
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
