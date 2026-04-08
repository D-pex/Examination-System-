using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Core.Requests;

public sealed record SubmitTestRequest
{
    public int UserId { get; set; }
    public int TestId { get; set; }
    public List<CreateAttemptRequest> CreateAttemptRequest { get; set; }
}