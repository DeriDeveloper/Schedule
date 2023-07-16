using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class Audience
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
