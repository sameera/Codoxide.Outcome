﻿using Codoxide;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace _.When_Outcomes_fail
{
    public class Given_there_is_a_catch_handlers
    {
        [Fact]
        public void It_lets_the_continue_handler_to_return_a_differently_typed_result()
        {
            this.GetFailingOutcome()
                .Continue((result, failure) => 100)
                .Then(value => {
                    value.Should().Be(100);
                });
        }

        [Fact]
        public async Task It_lets_the_asycn_continue_handler_to_return_a_differently_typed_result()
        {
            await this.GetFailingOutcomeAsync()
                .Continue((result, failure) => 1000)
                .Then(value => value.Should().Be(1000));
        }

        [Fact]
        public async Task It_supports_async_operations_in_continue_handler()
        {
            await this.GetFailingOutcomeAsync()
                .Continue((result, failure) => Task.FromResult(1000))
                .Then(value => value.Should().Be(1000));
        }

        public Outcome<string> GetFailingOutcome() => Outcome<string>.Reject("Failure!");
        public Task<Outcome<string>> GetFailingOutcomeAsync() => Task.FromResult(Outcome<string>.Reject("Failure!"));

    }
}