# Codoxide.Outcome<T>

`Codoxide.Outcome<T>` is a suite of classes and extension methods that assist you in writing Functional-style C# code. At the core of it is the `Outcome<T>` that represents the outcome of an operation similar to the `Try[T]` monad in [Scala](https://www.scala-lang.org/api/2.9.3/scala/util/Try.html).

The `Codoxide.Outcome` helps you write functional-programming style code without going all-out on the functional philosophy. 

NuGet: https://www.nuget.org/packages/Codoxide.Outcome

## Why?

Firstly, I'm not a strong functional-programming advocate; nor am I even an expert. Having programmed in OOP for nearly 20 years, I'd imagine my transition, to functional would be a very very slow process; if ever it even happens. I believe that would hold true for most C# developers. And I know it's true for my team. I wrote `Outcome<T>` to enforce few disciplines borrowed from functional programming.

There are of course, other similar C# libraries (e.g. [LanguageExt](https://languageext.readthedocs.io/en/latest/README.html#getting-started)) that allows you to write functional-style code. `Outcome<T>` is different in that:

1. Low allocations (`Outcome<T>` is a value type) which makes it inexpensive to construct.
2. Deconstructs in to a value tuple of `(T, Failure)` or `(T, Exception)` ***
3. Effortlessly wraps async code
4. Extensively modular, allowing you to pick just the features you need.

*** If you are writing a Class Library and you do not want to force `Outcome<T>` on your users, you can simply return `(T, Exception)` tuples instead.


Sample code:

```C#
using Codoxide;
using FluentAssertions;
using System;
using Xunit;

namespace UnitTest.Codoxide.Outcome
{
    public class Success_or_Failure_Sample_wit_ValueTyuples
    {
        [Fact]
        public void Working_with_a_function_that_could_sometimes_fail()
        {
            (int result, Failure fail) = FunctionThatCallsTheFailingFunctionAndDoesOtherUsefulStuff(false)
                            .Then(value => {
                                value.Should().Be(100);
                                return ++value;
                            })
                            .Then(value => {
                                value.Should().Be(101);
                                return ++value;
                            })
                            .Catch(failure => {
                                Assert.False(true, "This should never be hit because the outcome is successful");
                            });

            result.Should().Be(102);


            (result, fail) = FunctionThatCallsTheFailingFunctionAndDoesOtherUsefulStuff(true)
                        .Then(value => {
                            Assert.False(true, "This should never be hit because the function has failed");
                        })
                        .Then(value => {
                            Assert.False(true, "This should also not be hit because the function has failed");
                        })
                        .Catch(failure => {
                            Assert.IsType<InvalidOperationException>(failure.Exception);
                        });

            result.Should().Be(default(int));
        }

        private const int _fixed_initial_value = 100;

        private (int result, Failure failure) FunctionThatCallsTheFailingFunctionAndDoesOtherUsefulStuff(bool fail)
        {
            // Assume we did other useful stuff here
            try
            {
                var result = TheFunctionThatCanFail(fail);
                return (result, null);
            }
            catch (InvalidOperationException ex)
            {
                return (0, Fail("It failed!", ex));
            }
        }

        private int TheFunctionThatCanFail(bool fail)
        {
            if (!fail)
            {
                return _fixed_initial_value;
            }
            else
            {
                throw new InvalidOperationException("The randome failure");
            }
        }
    }

    public class Success_or_Failure_Sample
    {
        [Fact]
        public void Working_with_a_function_that_could_sometimes_fail()
        {
            int result = FunctionThatCallsTheFailingFunctionAndDoesOtherUsefulStuff(false)
                            .Then(value => {
                                value.Should().Be(100);
                                return ++value;
                            })
                            .Then(value => {
                                value.Should().Be(101);
                                return ++value;
                            })
                            .Catch(failure => {
                                Assert.False(true, "This should never be hit because the outcome is successful");
                            })
                            .ResultOrDefault();

            result.Should().Be(102);


            result = FunctionThatCallsTheFailingFunctionAndDoesOtherUsefulStuff(true)
                        .Then(value => {
                            Assert.False(true, "This should never be hit because the function has failed");
                        })
                        .Then(value => {
                            Assert.False(true, "This should also not be hit because the function has failed");
                        })
                        .Catch(failure => {
                            Assert.IsType<InvalidOperationException>(failure.Exception);
                        })
                        .ResultOrDefault();

            result.Should().Be(default(int));
        }

        private const int _fixed_initial_value = 100;

        private Outcome<int> FunctionThatCallsTheFailingFunctionAndDoesOtherUsefulStuff(bool fail)
        {
            // Assume we did other useful stuff here
            try
            {
                var result = TheFunctionThatCanFail(fail);
                return new Outcome<int>(result);
            }
            catch (InvalidOperationException ex)
            {
                return Outcome<int>.Reject("It failed!", ex);
            }
        }

        private int TheFunctionThatCanFail(bool fail)
        {
            if (!fail)
            {
                return _fixed_initial_value;
            }
            else
            {
                throw new InvalidOperationException("The randome failure");
            }
        }
    }
}
```
Working with async/await:

```c#
namespace _.When_processing_async_methods
{
    public class Given_explicit_await_statements_are_not_used
    {
        [Fact]
        public async void It_still_waits_for_the_statements_to_end()
        {
            await this.DoAsyncParameterizedOutcome(100d)
                .Then(number => {
                    return this.DoAsyncParameterizedOutcome(number);
                }).Then(result => {
                    result.Should().BeOfType(typeof(double));
                    return this.DoAsyncOutcome();
                }).Then(result => {
                    result.Should().Be(_theResult);
                });
        }
  
        private async Task<Outcome<double>> DoAsyncParameterizedOutcome(double number)
        {
            await Task.Delay(1000);
            return number;
        }
        
        private async Task<Outcome<string>> DoAsyncOutcome()
        {
            await Task.Delay(1);
            return new Outcome<string>(_theResult);
        }

        private readonly string _theResult = "THE_RESULT";
        private readonly int _theValueResult = 100;
    }
}
```

Even fancier with conditional execution:

```c#
public class Given_the_Outcome_is_async
{
    [Fact]
    public async Task It_only_executes_the_handlers_for_which_the_condition_is_true()
    {
        bool[] hitCounter = new bool[4];
 
        await Begin()
            .Then(value => value == 100, async value => {
                // Predicate evaluates to true. Should hit.
                hitCounter[0] = true;
 
                await Task.Delay(1);
            })
            .Then(value => value == 101, async () => {
                // Predicate evalutes to false. Should not hit.
                hitCounter[1] = true;
 
                await Task.Delay(1);
            })
            .Then(true, async () => {
                // Condition is true. Should hit.
                hitCounter[2] = true;
 
                await Task.Delay(1);
            })
            .Then(false, async () => {
                // Condition is false. Should not hit.
                hitCounter[3] = true;
 
                await Task.Delay(1);
            });
 
        hitCounter.Should().ContainInOrder(new[] {
            true, false, true, false
        });
    }
 
    private async Task<Outcome<int>> Begin()
    {
        await Task.Delay(1);
        return new Outcome<int>(_initialOutcome);
    }
 
    private const int _initialOutcome = 100;
}
```



### Release Notes

#### 2.1
The `GenericOutcomes<T>` static class that allows you to generate ValueOutcomes with little noise. E.g.
```c#
    using static GenericOutcomes<int>;

    public class Wrapping_results_with_GenericOutcomes
    {
        private const int _fixed_initial_value = 100;

        private (int result, Failure failure) TheFunctionThatCanFail(bool fail)
        {
            if (!fail)
            {
                return Success(_fixed_initial_value);
            }
            else
            {
                return Error("The random failure");
            }
        }

        [Fact]
        public void ValueOutcomes_can_be_generated_with_GenericOutcomes()
        {
            (int result, Failure failure) = TheFunctionThatCanFail(false);
            result.Should().Be(_fixed_initial_value);
            failure.Should().BeNull();

            (result, failure) = TheFunctionThatCanFail(true);
            result.Should().Be(default(int));
            failure.Should().NotBeNull();
            failure.Reason.Should().Be("The random failure");
        }
```
