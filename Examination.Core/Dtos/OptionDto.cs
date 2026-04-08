using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Core.Dtos;
public sealed record OptionDto(int Id,string OptionText) 
{
    public int Id { get; set; }
    public string OptionText { get; set; }
}