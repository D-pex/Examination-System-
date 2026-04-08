using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Core.Dto;

public sealed record ReportDtos(string UserName,string TestName,int Score, bool IsPassed, DateTime AttemptDate) 
{ 
    public string UserName { get; set; }
    public string TestName { get; set; }
    public int Score { get; set; }
    public bool IsPassed { get; set; }
    public DateTime AttemptDate { get; set; }
}