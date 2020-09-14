using Codoxide.OutcomeExtensions.Filters;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    using static Codoxide.OutcomeInternals.Utility;
    
    public static class SwitchOperator
    {
        public static Outcome<T> Switch<T>(params Outcome<T>[] outcomes)
        {
            for (int i = 0; i < outcomes.Length; i++)
            {
                if (outcomes[i].IsSuccessful) return outcomes[i];
            }

            return new ExpectationFailure<T>(default);
        }

        public static Outcome<T> Switch<T>(params Func<Outcome<T>>[] fns)
        {
            for (int i = 0; i < fns.Length; i++)
            {
                var outcome = fns[i]();
                if (CanProceedWithCase(outcome)) return outcome;
            }

            return new ExpectationFailure<T>(default);
        }

        public static async Task<Outcome<T>> Switch<T>(params Task<Outcome<T>>[] asyncOutcomes)
        {
            for (int i = 0; i < asyncOutcomes.Length; i++)
            {
                var outcome = await asyncOutcomes[i];
                if (CanProceedWithCase(outcome)) return outcome;
            }

            return new ExpectationFailure<T>(default);
        }

        public static async Task<Outcome<T>> Switch<T>(params Func<Task<Outcome<T>>>[] fns)
        {
            for (int i = 0; i < fns.Length; i++)
            {
                var outcome = await fns[i]();
                if (CanProceedWithCase(outcome)) return outcome;
            }

            return new ExpectationFailure<T>(default);
        }

        private static bool CanProceedWithCase<T>(Outcome<T> caseOutcome) =>
            caseOutcome.IsSuccessful || !(caseOutcome.FailureOrNull() is ExpectationFailure);
    }
}