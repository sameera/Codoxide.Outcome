namespace Codoxide.OutcomeInternals
{
    public static class InternalAccessExtensions
    {
        public static T GetResultUnchecked<T>(this Outcome<T> @this) => @this.Result;
    }
}
