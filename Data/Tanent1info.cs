using System;
using System.Collections.Generic;

namespace ESSTaskBatool.Data;

public partial class Tanent1info
{
    public uint Id { get; set; }

    public string? Name { get; set; }

    public int? Phone { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? Username { get; set; }

    public virtual Tanent1user? Tanent1user { get; set; }
}
