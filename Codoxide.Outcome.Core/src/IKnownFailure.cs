namespace Codoxide.Outcomes
{
    public interface IKnownFailure: IFailure
    {
    }

    public class KnownFailure : Failure, IKnownFailure
    {
        public KnownFailure(Failure failure): base(failure) { }
    }
}
