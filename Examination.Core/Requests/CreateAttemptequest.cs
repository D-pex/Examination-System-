using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Core.Requests;

public sealed record CreateAttemptRequest
{
    public int QuestionId { get; set; } 
    public int SelectedOptionId { get; set; } 
}