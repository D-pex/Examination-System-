using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Persistence;
public class UserAnswer
{
    public int Id { get; set; }
    public int AttemptId { get; set; }
    public int QuestionId { get; set; }
    public int SelectedOptionId { get; set; }

  
    public  UserAttempt UserAttempt { get; set; }
    public Question Question { get; set; }
}