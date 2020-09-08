namespace Codoxide.Outcomes
{
    public class KnownFailure : Failure
    {
        public KnownFailure(string reason, int failureCode = 100) : base(reason, failureCode) { }
        public KnownFailure(Failure failure) : base(failure) { }
    }
}