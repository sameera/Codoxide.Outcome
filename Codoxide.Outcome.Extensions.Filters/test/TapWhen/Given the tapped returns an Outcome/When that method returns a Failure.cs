using System;
using System.Threading.Tasks;
using Codoxide;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace _.TapWhen.Given_the_tapped_returns_an_Outcome
{
    public class When_that_method_returns_a_Failure
    {
        [Fact]
        public void It_will_be_treated_as_a_failure_of_the_chain()
        {
            Func<int, bool> handler = A.Fake<Func<int, bool>>();
            var oc = Outcome.Any()
                .Map(() => 100)
                .TapWhen(true, () => Outcome<string>.Reject("Failed!"))
                .Map(handler);

            oc.IsSuccessful.Should().BeFalse();
            A.CallTo(() => handler.Invoke(A<int>.Ignored)).MustNotHaveHappened();
            
            oc = Outcome.Any()
                .Map(() => 100)
                .TapWhen(() => true, () => Outcome<string>.Reject("Failed!"))
                .Map(handler);

            oc.IsSuccessful.Should().BeFalse();
            A.CallTo(() => handler.Invoke(A<int>.Ignored)).MustNotHaveHappened();
        }
        
        [Fact]
        public async Task It_will_be_treated_as_a_failure_of_the_chain_async()
        {
            Func<int, bool> handler = A.Fake<Func<int, bool>>();
            
            var oc = await Outcome.Any().ForAsync()
                .Map(() => 100)
                .TapWhen(() => true, () => Outcome<string>.Reject("Failed!"))
                .Map(handler);

            oc.IsSuccessful.Should().BeFalse();
            A.CallTo(() => handler.Invoke(A<int>.Ignored)).MustNotHaveHappened();
            
            oc = await Outcome.Any()
                .Map(() => 100)
                .TapWhen(() => true, () => Outcome<string>.Reject("Failed!").ForAsync())
                .Map(handler);

            oc.IsSuccessful.Should().BeFalse();
            A.CallTo(() => handler.Invoke(A<int>.Ignored)).MustNotHaveHappened();
        }
    }
}