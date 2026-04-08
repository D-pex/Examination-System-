using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Core.Requests;

public sealed record CreateTestRequest 
{
       public string Name { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }

}