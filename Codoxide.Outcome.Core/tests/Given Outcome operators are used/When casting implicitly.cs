using Codoxide;
using Xunit;
using FluentAssertions;
using System;
using System.Threading.Tasks;

namespace _.Given_Outcome_operators_are_used
 {
     public class When_casting_implicitly
     {

        [Fact]
         public void It_accepts_Outcomes_with_compatible_results()
         {
             var original = new Outcome<int>(10);
             Outcome<object> casted = original;

             casted.IsSuccessful.Should().BeTrue();
             casted.ResultOrThrow().Should().Be(10);
         }
        
        [Fact]
        public void It_can_cast_to_a_Exception_tuple()
        {
            const string exceptionMessage = "Something wrong";
            (int i, Exception e) = this.FailWith(exceptionMessage);

            e.Should().NotBeNull();
            e.Message.Should().Be(exceptionMessage);
            i.Should().Be(default);

            (int y, Exception f) = Outcome.Of(10);

            f.Should().BeNull();
            y.Should().Be(10);
        }

        private (int, Exception) FailWith(string text) => Outcome<int>.Reject(text);

    }
}