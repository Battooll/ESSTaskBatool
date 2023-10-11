using System;
using System.Collections.Generic;

namespace ESSTaskBatool.Data;

public partial class Tanent2info
{
    public uint Id { get; set; }

    public string? Name { get; set; }

    public int? Phone { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public virtual Tanent2user? Tanent2user { get; set; }
}
