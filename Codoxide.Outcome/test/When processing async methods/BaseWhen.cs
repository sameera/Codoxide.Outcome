using Codoxide;
using Codoxide.Outcomes;
using System.Threading.Tasks;

namespace _.When_processing_async_methods
{
    public class BaseWhen: TestSpec
    {
        protected (int result, Failure failure) DoValueOutcome()
        {
            return (_theValueResult, null);
        }

        protected async Task<Outcome<string>> DoAsyncOutcome()
        {
            await Task.Delay(1);
            return new Outcome<string>(_theResult);
        }

        protected readonly string _theResult = "THE_RESULT";
        protected readonly int _theValueResult = 100;
    }
}
