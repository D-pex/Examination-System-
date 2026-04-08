using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Core.Dtos;

public sealed record TestDto(int Id,string Name,string Subject,int Duration)
{
    public string Name { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
}