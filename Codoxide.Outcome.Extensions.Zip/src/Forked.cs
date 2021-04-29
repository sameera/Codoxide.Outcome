using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Codoxide
{
    public struct Forked<T1, T2>
    {
        public Task<Outcome<T1>> Task1 { get; }
        public Task<Outcome<T2>> Task2 { get; }

        public Forked(Task<Outcome<T1>> task1, Task<Outcome<T2>> task2)
        {
            this.Task1 = task1;
            this.Task2 = task2;
        }

        public Forked<T1, T2, T3> Fork<T3>(Func<Task<Outcome<T3>>> other) 
            => new Forked<T1, T2, T3>(this.Task1, this.Task2, other());

        public async Task<Outcome<T>> Join<T>(Func<T1, T2, AggregateFailure, T> joiner)
        {
            try
            {
                await Task.WhenAll();
                var (t1, f1) = await this.Task1;
                var (t2, f2) = await this.Task2;

                AggregateFailure? failure = (f1 != null || f2 != null) ? new AggregateFailure(f1, f2) : null;
                
                return Outcome.Of(joiner(t1, t2, failure!));
            }
            catch (Exception e)
            {
                return Outcome.Of(joiner(default, default, new AggregateFailure(e)));
            }
        }

        public async Task<Outcome<T>> Join<T>(Func<T1, T2, T> joiner)
        {
            try
            {
                await Task.WhenAll();
                var (t1, f1) = await this.Task1;
                var (t2, f2) = await this.Task2;

                bool hasErrors = f1 != null || f2 != null;
                AggregateFailure? failure = hasErrors ? new AggregateFailure(f1!, f2!) : null;
                
                return hasErrors ? Outcome<T>.Reject(failure) : Outcome.Of(joiner(t1, t2));
            }
            catch (Exception e)
            {
                return Outcome<T>.Reject(new AggregateFailure(e));
            }
        }
    }


    public struct Forked<T1, T2, T3>
    {
        public Task<Outcome<T1>> Task1 { get; }
        public Task<Outcome<T2>> Task2 { get; }
        public Task<Outcome<T3>> Task3 { get; }

        public Forked(Task<Outcome<T1>> task1, Task<Outcome<T2>> task2, Task<Outcome<T3>> task3)
        {
            this.Task1 = task1;
            this.Task2 = task2;
            this.Task3 = task3;
        }

        public Forked<T1, T2, T3, T4> Fork<T4>(Func<Task<Outcome<T3>>> other)
            => new Forked<T1, T2, T3, T4>(this.Task1, this.Task2, this.Task3, other());

        public async Task<Outcome<T>> Join<T>(Func<T1, T2, T3, AggregateFailure?, T> joiner)
        {
            try
            {
                await Task.WhenAll();
                var (t1, f1) = await this.Task1;
                var (t2, f2) = await this.Task2;
                var (t3, f3) = await this.Task3;

                AggregateFailure? failure = (f1 != null || f2 != null || f3 != null) ? new AggregateFailure(f1, f2, f3) : null;

                return Outcome.Of(joiner(t1, t2, t3, failure));
            }
            catch (Exception e)
            {
                return Outcome.Of(joiner(default!, default!, default!, new AggregateFailure(e)));
            }
        }

        public async Task<Outcome<T>> Join<T>(Func<T1, T2, T3, T> joiner)
        {
            try
            {
                await Task.WhenAll();
                var (t1, f1) = await this.Task1;
                var (t2, f2) = await this.Task2;
                var (t3, f3) = await this.Task3;

                bool hasErrors = f1 != null || f2 != null || f3 != null;
                AggregateFailure? failure = hasErrors ? new AggregateFailure(f1!, f2!, f3!) : null;

                return hasErrors ? Outcome<T>.Reject(failure) : Outcome.Of(joiner(t1, t2, t3));
            }
            catch (Exception e)
            {
                return Outcome<T>.Reject(new AggregateFailure(e));
            }
        }
    }

    public struct Forked<T1, T2, T3, T4>
    {
        public Task<Outcome<T1>> Task1 { get; }
        public Task<Outcome<T2>> Task2 { get; }
        public Task<Outcome<T3>> Task3 { get; }
        public Task<Outcome<T4>> Task4 { get; }

        public Forked(Task<Outcome<T1>> task1, Task<Outcome<T2>> task2, Task<Outcome<T3>> task3, Task<Outcome<T4>> task4)
        {
            this.Task1 = task1;
            this.Task2 = task2;
            this.Task3 = task3;
            this.Task4 = task4;
        }

        public Forked<T1, T2, T3, T4, T5> Fork<T5>(Func<Task<Outcome<T5>>> other)
            => new Forked<T1, T2, T3, T4, T5>(this.Task1, this.Task2, this.Task3, this.Task4, other());

        public async Task<Outcome<T>> Join<T>(Func<T1, T2, T3, AggregateFailure?, T> joiner)
        {
            try
            {
                await Task.WhenAll();
                var (t1, f1) = await this.Task1;
                var (t2, f2) = await this.Task2;
                var (t3, f3) = await this.Task3;

                AggregateFailure? failure = (f1 != null || f2 != null || f3 != null) ? new AggregateFailure(f1, f2, f3) : null;

                return Outcome.Of(joiner(t1, t2, t3, failure));
            }
            catch (Exception e)
            {
                return Outcome.Of(joiner(default!, default!, default!, new AggregateFailure(e)));
            }
        }

        public async Task<Outcome<T>> Join<T>(Func<T1, T2, T3, T> joiner)
        {
            try
            {
                await Task.WhenAll();
                var (t1, f1) = await this.Task1;
                var (t2, f2) = await this.Task2;
                var (t3, f3) = await this.Task3;

                bool hasErrors = f1 != null || f2 != null || f3 != null;
                AggregateFailure? failure = hasErrors ? new AggregateFailure(f1!, f2!, f3!) : null;

                return hasErrors ? Outcome<T>.Reject(failure) : Outcome.Of(joiner(t1, t2, t3));
            }
            catch (Exception e)
            {
                return Outcome<T>.Reject(new AggregateFailure(e));
            }
        }
    }
}