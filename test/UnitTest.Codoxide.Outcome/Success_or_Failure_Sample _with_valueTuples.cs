﻿//using Codoxide;
//using Codoxide.Outcomes;
//using FluentAssertions;
//using System;
//using Xunit;
//using static Codoxide.FixedOutcomes;

//namespace UnitTest.Codoxide.Outcome
//{
//    public class Success_or_Failure_Sample_wit_ValueTyuples
//    {
//        [Fact]
//        public void Working_with_a_function_that_could_sometimes_fail()
//        {
//            (int result, Failure fail) = FunctionThatCallsTheFailingFunctionAndDoesOtherUsefulStuff(false)
//                            .Then(value => {
//                                value.Should().Be(100);
//                                return ++value;
//                            })
//                            .Then(value => {
//                                value.Should().Be(101);
//                                return ++value;
//                            })
//                            .Catch(failure => {
//                                Assert.False(true, "This should never be hit because the outcome is successful");
//                            });

//            result.Should().Be(102);


//            (result, fail) = FunctionThatCallsTheFailingFunctionAndDoesOtherUsefulStuff(true)
//                        .Then(value => {
//                            Assert.False(true, "This should never be hit because the function has failed");
//                        })
//                        .Then(value => {
//                            Assert.False(true, "This should also not be hit because the function has failed");
//                        })
//                        .Catch(failure => {
//                            Assert.IsType<InvalidOperationException>(failure.Exception);
//                        });

//            result.Should().Be(default(int));
//        }

//        private const int _fixed_initial_value = 100;

//        private (int result, Failure failure) FunctionThatCallsTheFailingFunctionAndDoesOtherUsefulStuff(bool fail)
//        {
//            // Assume we did other useful stuff here
//            try
//            {
//                var result = TheFunctionThatCanFail(fail);
//                return (result, null);
//            }
//            catch (InvalidOperationException ex)
//            {
//                return (0, Fail("It failed!", ex));
//            }
//        }

//        private int TheFunctionThatCanFail(bool fail)
//        {
//            if (!fail)
//            {
//                return _fixed_initial_value;
//            }
//            else
//            {
//                throw new InvalidOperationException("The random failure");
//            }
//        }
//    }
//}
