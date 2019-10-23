using Codoxide;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace _.When_continuing
{
    public class Given_a_failure_or_success
    {
        [Fact]
        public void It_continues_on_with_the_result_of_Continue_block()
        {
            this.GetFailingOutcome()
                .Continue(() => 300)
                .Then(i => i.Should().Be(300));
        }

        [Fact]
        public void It_provides_failure_to_Continue_block_if_needed()
        {
            this.GetFailingOutcome()
                .Continue((_, failure) => {
                    failure.Reason.Should().Be("Failure!");
                    return 0;
                });
        }

        [Fact]
        public async Task It_can_continue_from_async_failure()
        {
            await this.GetFailingOutcomeAsync()
                .Catch(failure => {
                    failure.Should().NotBeNull();
                })
                .Continue(() => 300)
                .Then(i => i.Should().Be(300))
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task It_can_continue_asyncly()
        {
            await this.GetFailingOutcomeAsync()
                .Continue((_, failure) => Task.FromResult(300))
                .Then(i => i.Should().Be(300))
                .Then(() => this.GetFailingOutcomeAsync())
                .Continue(() => 400)
                .Then(i => i.Should().Be(400))
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task It_can_continue_from_Action_type_blocks()
        {
            await this.GetSuccessOutcomeAsync()
                .Then(() => DoNothing())
                .Catch(() => DoNothing())
                .Continue(() => 100)
                .Then(i => i.Should().Be(100))
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task It_can_continue_from_Action_type_blocks_and_do_async()
        {
            var continuationTask = Task.FromResult(900);

            await this.GetSuccessOutcomeAsync()

                .Then(() => DoNothing())
                .Catch(() => DoNothing())
                .Continue(() => continuationTask)

                .Then(i => i.Should().Be(900))
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task It_can_continue_when_upgraded_to_async()
        {
            await this.GetSuccessOutcome()
                .Continue(() => GetSuccessOutcomeAsync())
                .Then(s => s.Should().Be("Success!"));
        }

        public Outcome<string> GetFailingOutcome() => Outcome<string>.Reject("Failure!");
        public Task<Outcome<string>> GetFailingOutcomeAsync() => Task.FromResult(Outcome<string>.Reject("Failure!"));

        public Outcome<string> GetSuccessOutcome() => (Outcome<string>)"Success!";
        public Task<Outcome<string>> GetSuccessOutcomeAsync() => Task.FromResult((Outcome<string>)"Success!");

        private void DoNothing() {}
    }
}
