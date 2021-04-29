using System.Threading.Tasks;
using Codoxide;
using Xunit;

namespace _.Given_multiple_parallel_functions
{
    public class When_forking_and_joining_functions
    {
        [Fact]
        public async Task It_will_await_all_forked_function()
        {
            var result = Task.FromResult(Outcome.Any())
                .Fork(() => new Task[] {
                    ExecuteAsync("step 1"),
                    ExecuteAsync("step 2"),
                    ExecuteAsync("step 3")
                })
                .Join(results => {
                    var (step1, step2, step3) = results;
                })
        }

        private async Task<Outcome<string>> ExecuteAsync(string retrurnValue)
        {
             await Task.Delay(0);
             return retrurnValue;           
        }
    }
}