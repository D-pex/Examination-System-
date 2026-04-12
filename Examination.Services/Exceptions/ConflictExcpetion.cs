using System;
using System.Collections.Generic;
using System.Text;

namespace Examination.Services.Exceptions;

public sealed class ConflictException(string message) : Exception(message); 
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}