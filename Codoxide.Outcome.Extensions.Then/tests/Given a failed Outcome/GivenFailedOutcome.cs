using System;
using Codoxide;

namespace _.Given_a_failed_Outcome
{
    public class GivenFailedOutcome
    {
        protected const string InitialRejectionReason = "Already rejected";
        protected static readonly Outcome<string> TheFailedOutcome = Outcome<string>.Reject(InitialRejectionReason);
    }
}
