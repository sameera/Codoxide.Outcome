using System;
using System.Collections.Generic;
using System.Linq;
using Codoxide.Outcomes;

namespace Codoxide
{
    public class AggregateFailure: Failure
    {
        private const string AggregateReason = "Multiple failures occured.";
        
        public AggregateFailure(params Exception?[] exceptions): base(AggregateReason)
        {
            Failures = exceptions.Select(e => e == null ? null : new Failure(e.Message, e)).ToArray();
        }
        
        public AggregateFailure(params Failure?[] failures): base(AggregateReason)
        {
            this.Failures = failures;
        }

        public IEnumerable<Failure?> Failures { get; }
    }
}