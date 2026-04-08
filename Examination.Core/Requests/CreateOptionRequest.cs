using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Core.Requests;

public sealed record CreateOptionRequest
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
}