using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Core.Requests;

public sealed record CreateQuestionRequest 
{
    public sealed record SubmitTestRequest(
    int UserId,
    int TestId,
    List<AnswerRequest> Answers
);
}