using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Persistence
public class Test
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public bool IsPublished { get; set; }

    public DateTime CreatedAt { get; set; }

    
    public List<Question> Questions { get; set; } = new();
    public List<UserAttempt> UserAttempts { get; set; } = new();
}
