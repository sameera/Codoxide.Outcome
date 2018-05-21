using System;

namespace Codoxide
{
    using static FixedOutcomes;

    static partial class OutcomeExtensions
    {
        public static Outcome<T> ThenTry<T>(this Outcome<T> outcome, Action action)
        {
            if (outcome.IsSuccessful)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    return new Outcome<T>(Fail(ex));
                }
            }

            return outcome;
        }
    }
}
