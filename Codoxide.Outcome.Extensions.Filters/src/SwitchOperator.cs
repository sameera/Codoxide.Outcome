using Codoxide.OutcomeExtensions.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codoxide
{
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
                if (outcome.IsSuccessful) return outcome;
            }

            return new ExpectationFailure<T>(default);
        }

        public static async Task<Outcome<T>> Switch<T>(params Task<Outcome<T>>[] asyncOutcomes)
        {
            for (int i = 0; i < asyncOutcomes.Length; i++)
            {
                var outcome = await asyncOutcomes[i]; 
                if (outcome.IsSuccessful) return outcome;
            }

            return new ExpectationFailure<T>(default);
        }
    }
}
