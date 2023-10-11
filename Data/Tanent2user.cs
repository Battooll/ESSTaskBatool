using System;
using System.Collections.Generic;

namespace ESSTaskBatool.Data;

public partial class Tanent2user
{
    public int Id { get; set; }

    public uint Tanentid { get; set; }

    public string? Name { get; set; }

    public int? Phone { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public virtual Tanent2info Tanent { get; set; } = null!;
}
