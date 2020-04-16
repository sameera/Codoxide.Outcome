using Codoxide;
using Xunit;
using FluentAssertions;
 
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
     }
 }