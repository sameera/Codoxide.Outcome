﻿using Codoxide;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _.When_handling_fucntions_that_can_throw_exceptions
{
    public class Given_Catch_block_rethrows: TestsWithMethodsThatThrow
    {
        public void It_returns_a_new_failure_with_the_latest_exception()
        {
            var outcome = Outcome.Of(() => this.MethodThatThrowsException())
                            .Catch(error => this.MethodThatThrows<FormatException>());

            outcome.IsSuccessful.Should().BeFalse();

            var failure = outcome.FailureOrNull();
            failure.Should().NotBeNull();
            failure.AsException().Should().BeOfType<FormatException>();
        }

        public async Task It_returns_a_new_failure_with_the_latest_exception_even_when_thrown_async()
        {
            var outcome = await Outcome.Of(() => this.AsyncMethodThatThrowsException())
                            .Catch(error => this.AsyncMethodThatThrows<FormatException>());

            outcome.IsSuccessful.Should().BeFalse();

            var failure = outcome.FailureOrNull();
            failure.Should().NotBeNull();
            failure.AsException().Should().BeOfType<FormatException>();
        }
    }
}