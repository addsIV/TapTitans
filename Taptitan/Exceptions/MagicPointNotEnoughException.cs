using System.Runtime.Serialization;

namespace Taptitan.Exceptions;

public class MagicPointNotEnoughException :Exception
{
    public MagicPointNotEnoughException()
    {
    }

    protected MagicPointNotEnoughException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public MagicPointNotEnoughException(string? message) : base(message)
    {
    }

    public MagicPointNotEnoughException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}