namespace Codoxide.Outcomes
{
    public class KnownFailure : Failure
    {
        public KnownFailure(Failure failure) : base(failure) { }
    }
}