using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Persistence;
public class UserAttempt
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TestId { get; set; }

    public int Score { get; set; }
    public bool IsPassed { get; set; }
    public DateTime AttemptDate { get; set; }


    public User User { get; set; }
    public Test Test { get; set; }
    public List<UserAnswer> UserAnswers { get; set; } = new ();
}
