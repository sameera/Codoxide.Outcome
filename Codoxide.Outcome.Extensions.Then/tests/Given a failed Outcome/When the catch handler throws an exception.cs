using System;
using Xunit;
using Codoxide;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codoxide.Outcomes;

namespace _.Given_a_failed_Outcome
{
    public class When_the_catch_handler_throws_an_exception: GivenFailedOutcome
    {
        [Theory]
        [MemberData(nameof(UsageScenarios))]
        public void It_returns_a_failure_that_contains_the_thrown_exception(Outcome<string> finalOutcome)
        {
            finalOutcome.IsSuccessful.Should().BeFalse();

            var thrown = finalOutcome.FailureOrNull().AsException();
            thrown.Should().BeOfType<InvalidCastException>();
            thrown.Message.Should().StartWith("Dummy");
        }

        [Theory]
        [MemberData(nameof(AsncUsageSenarios))]
        public async Task It_returns_a_failure_that_contains_the_exception_thrown_asynchronously(Task<Outcome<string>> finalAsyncOutcome)
        {
            var finalOutcome = await finalAsyncOutcome;
            
            finalOutcome.IsSuccessful.Should().BeFalse();

            var thrown = finalOutcome.FailureOrNull().AsException();
            thrown.Should().BeOfType<InvalidCastException>();
            thrown.Message.Should().StartWith("Dummy");
        }

        public static IEnumerable<object[]> UsageScenarios()
        {
            static object[] wrap(Outcome<string> o) => new object[] { o };

            static Outcome<string> rejecetd() => Outcome<string>.Reject("FirstRejetion");

            // Action overload
            yield return wrap(
                rejecetd().Catch(() => {
                    ActionThatThrowsException("Action");
                    Console.WriteLine("Action!");
                })
            );

            // Action<Failure>
            yield return wrap(
                rejecetd().Catch(failure => {
                    ActionThatThrowsException("Action<T>");
                    Console.WriteLine("Action<T>: " + failure.Reason);
                })
            );

            // Func<Failure>
            yield return wrap(
                rejecetd().Catch(() =>{
                    ActionThatThrowsException("Func<Failure>");
                    return "Func<Failure>";
                })
            );

            // Func<Failure, Failure>
            yield return wrap(
                rejecetd().Catch(failure => {
                    ActionThatThrowsException("Func<Failure, Failure>");
                    return "Func<Failure, Failure>: " + failure.Reason;
                })
            );

            // Func<T>
            yield return wrap(
                    rejecetd().Catch(() =>{
                        ActionThatThrowsException("Func<T>");
                        return "Func<T>";
                    })
                );

            // Func<Failure, T>
            yield return wrap(
                    rejecetd().Catch(failure => {
                        ActionThatThrowsException("Func<Failure, T>");
                        return "Func<Failure, T>";
                    })
                );

            // Func<Outcome<T>>
            yield return wrap(
                    rejecetd().Catch(() => {
                        ActionThatThrowsException("Func<Outcome<T>>");
                        return new Outcome<string>("Func<Outcome<T>>");
                    })
                );

            // Func<Failure, Outcome<T>>
            yield return wrap(
                    rejecetd().Catch(failrue => {
                        ActionThatThrowsException("Func<Failure, Outcome<T>>");
                        return new Outcome<string>("Func<Failure, Outcome<T>>");
                    })
                );
        }

        public static IEnumerable<object[]> AsncUsageSenarios()
        {
            static object[] wrap(Task<Outcome<string>> o) => new object[] { o };
            static Task<Outcome<string>> rejecetd() => Task.FromResult(Outcome<string>.Reject("First Rejection"));
            
            // Action
            yield return wrap(
                    rejecetd().Catch(() => {
                        ActionThatThrowsException("Action");
                        Console.WriteLine("Action");
                    })
                );
            
            // Action<Failure>
            yield return wrap(
                rejecetd().Catch(failure => {
                    ActionThatThrowsException("Action<Failure>");
                    Console.WriteLine("Action<T>");
                })
            );

            // Func<T>
            yield return wrap(
                rejecetd().Catch(() => {
                    ActionThatThrowsException("Func<T>");
                    return "Func<T>";
                })
            );
            
            // Func<Failure, T>
            yield return wrap(
                rejecetd().Catch(failure => {
                    ActionThatThrowsException("Func<Failure, T>");
                    return "Func<T>: " + failure.Reason;
                })
            );

            // Func<Task>
            yield return wrap(
                    rejecetd().Catch(() => {
                        ActionThatThrowsException("Func<Task>");
                        return Task.CompletedTask;
                    })
                );

            // Func<Failure, Task>
            yield return wrap(
                rejecetd().Catch(failure => {
                    ActionThatThrowsException("Func<Failure, Task>");
                    return Task.CompletedTask;
                })
            );

            // Func<Task<T>>
            yield return wrap(
                rejecetd().Catch(() => {
                    ActionThatThrowsException("Func<Task<T>>");
                    return Task.FromResult("Func<Task<T>>");
                })
            );

            // Func<Failure, Task<T>>
            yield return wrap(
                rejecetd().Catch(failure => {
                    ActionThatThrowsException("Func<Failure, Task<T>>");
                    return Task.FromResult("Func<Failure, Task<T>>");
                })
            );
            
            // Func<Outcome<T>>
            yield return wrap(
                rejecetd().Catch(() => {
                    ActionThatThrowsException("Func<Outcome<T>>");
                    return new Outcome<string>("Func<Outcome<T>>");
                })
            );
            
            // Func<Task<Outcome<T>>>
            yield return wrap(
                rejecetd().Catch(() => {
                    ActionThatThrowsException("Func<Task<Outcome<T>>>");
                    return Task.FromResult(new Outcome<string>("Func<Task<Outcome<T>>>"));
                })
            );
            
            //  Func<Failure, Task<Outcome<T>>>
            yield return wrap(
                rejecetd().Catch(failure => {
                    ActionThatThrowsException("Func<Failure, Task<Outcome<T>>>");
                    return Task.FromResult(new Outcome<string>("Func<Failure, Task<Outcome<T>>>: " + failure.Reason));
                })
            );
            
            // Func<Failure>
            yield return wrap(
                rejecetd().Catch(() => {
                    ActionThatThrowsException("Func<Task<Outcome<T>>>");
                    return new Failure("Func<Task<Outcome<T>>>");
                })
            );  
            
            // Func<Failure, Failure>
            yield return wrap(
                rejecetd().Catch(failure => {
                    ActionThatThrowsException("Func<Task<Outcome<T>>>");
                    return new Failure("Func<Task<Outcome<T>>>: " + failure.Reason);
                })
            );  
            
            // Func<Failure>
            yield return wrap(
                rejecetd().Catch(() =>{
                    ActionThatThrowsException("Func<Failure>");
                    return Task.FromResult("Func<Failure>");
                })
            );

            // Func<Failure, Failure>
            yield return wrap(
                rejecetd().Catch(failure => {
                    ActionThatThrowsException("Func<Failure, Failure>");
                    return Task.FromResult("Func<Failure, Failure>: " + failure.Reason);
                })
            );
        }

        private static void ActionThatThrowsException(string overload) => throw new InvalidCastException("Dummy: " + overload);

        private static Task<Outcome<string>> AsyncDummyFunc() => Task.FromResult(new Outcome<string>("AsyncDummy"));

        private static Outcome<string> Rejecetd() => Outcome<string>.Reject("FirstRejetion");
    }
}
