﻿using System;
using System.Collections.Generic;

namespace lesohem.Models;

public partial class Country
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
