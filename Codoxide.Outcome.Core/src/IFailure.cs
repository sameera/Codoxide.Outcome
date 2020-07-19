using System;

namespace Codoxide.Outcomes
{
    public interface IFailure
    {
        int FailureCode { get; }
        string Reason { get; }

        Exception AsException();
    }
}