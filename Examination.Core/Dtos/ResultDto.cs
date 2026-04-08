using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Core.Dtos;

public sealed record ResultDto(int Score,bool IsPassed,int TotalQuestion)
{
    public int Score { get; set; }
    public bool IsPassed { get; set; }
    public int TotalQuestion { get; set; }
}