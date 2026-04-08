using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Core.Dtos;

    public sealed record QuestionDto(int Id,string QuestionText,List<OptionDto> Options) 
{
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<OptionDto> Options { get; set; } = new();
    }