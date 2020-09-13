using Codoxide;
using Codoxide.Outcomes;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

using static Codoxide.Outcomes.CatchFilters;

namespace _.Given_methods_that_throw_exceptions
{

    public class When_filtered_Catch_blocks_are_present: GivenMethodsThatThrow
    {
        [Fact]
        public void It_only_executes_the_block_with_matching_Exception()
        {
            Failure reported = null;

            var result = Outcome.Of(MethodThatThrows<NullReferenceException>)
                .Catch(A<ArgumentException>, () => {
                    Assert.False(true, "Should not have executed ArgumentException block.");
                    return string.Empty;
                })
                .Catch(A<NullReferenceException>, failure => {
                    reported = failure;
                    return "Worked around!";
                })
                .Catch(A<IndexOutOfRangeException>, () => {
                    Assert.False(true, "Should not have executed IndexOutOfRange block.");
                    return string.Empty;
                });

            reported.Should().NotBeNull();
            result.ResultOrDefault().Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task It_only_executes_the_block_with_matching_Exception_Async()
        {
            Failure reported = null;

            var result = await Outcome.Of(AsyncMethodThatThrows<NullReferenceException>)
                .Catch(A<ArgumentException>, () => {
                    Assert.False(true, "Should not have executed ArgumentException block.");
                    return string.Empty;
                })
                .Catch(A<NullReferenceException>, failure => {
                    reported = failure;
                    return "Worked around!";
                })
                .Catch(A<IndexOutOfRangeException>, () => {
                    Assert.False(true, "Should not have executed IndexOutOfRange block.");
                    return string.Empty;
                });

            reported.Should().NotBeNull();
            result.ResultOrDefault().Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void It_allows_specific_failure_types_to_be_caught()
        {
            Failure reported = null;

            var result = Outcome<string>.Reject(new ExpectedFailure())
                .Catch(A<KnownFailure>, () => {
                    Assert.False(true, "Should not have executed ArgumentException block.");
                    return string.Empty;
                })
                .Catch(A<ExpectedFailure>, failure => {
                    reported = failure;
                    return "Worked around!";
                })
                .Catch(A<Failure>, () => {
                    Assert.False(true, "Should not have executed IndexOutOfRange block.");
                    return string.Empty;
                })
                .ResultOrThrow(); // Force assertions to be thrown.

            reported.Should().NotBeNull();
            reported.Should().BeOfType<ExpectedFailure>();
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void It_will_allow_KnownFailures_to_be_caught()
        {
            Failure reported = null;
            Failure known = null;

            var result = Outcome.Of(MethodThatThrows<ArgumentException>)
                .Catch(A<ArgumentException>, (failure, ignorable) => {
                    reported = failure;
                    return ignorable;
                })
                .Catch(A<IndexOutOfRangeException>, () => {
                    Assert.False(true, "Should not have executed IndexOutOfRange block.");
                    return string.Empty;
                })
                .Catch(A<KnownFailure>, failure => {
                    known = failure;
                    return "Worked aorund";
                })
                .ResultOrThrow();

            reported.Should().NotBeNull();
            known.Should().NotBeNull();
            result.Should().NotBeNullOrEmpty();
        }
        class ExpectedFailure : Failure
        {
            public ExpectedFailure() : base("Another!")
            {
            }
        }
    }
}
