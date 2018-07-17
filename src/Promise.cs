//// TODO: Need more time to figure this out.
//// IMORTANT: Do not use (yet)!

//using System;
//using System.Diagnostics.Contracts;
//using System.Runtime.CompilerServices;
//using System.Threading.Tasks;

//namespace System.Runtime.CompilerServices
//{
//    public sealed class AsyncMethodBuilderAttribute : Attribute
//    {
//        public AsyncMethodBuilderAttribute(Type taskLike) { }
//    }
//}

//namespace Codoxide
//{
//    [AsyncMethodBuilder(typeof(PromiseBuilder<>))]
//    public class Promise<T>
//    {
//        private readonly Task<Outcome<T>> _task;

//        public Promise(Func<Outcome<T>> fn)
//        {
//            Contract.Requires(fn != null);
//            _task = new Task<Outcome<T>>(fn);
//        }

//        public Promise(Task<Outcome<T>> task)
//        {
//            _task = task;
//        }

//        public TaskAwaiter<Outcome<T>> GetAwaiter()
//        {
//            return _task.GetAwaiter();
//        }

//        public static implicit operator Promise<T>(Task<Outcome<T>> task) => new Promise<T>(task);
//    }

//    // Following was adopted from the code presented by JamesFaix (https://github.com/jamesfaix)
//    // @ https://stackoverflow.com/questions/43220955/c-sharp-7-why-cant-i-return-this-awaitable-type-from-an-async-method
//    public class PromiseBuilder<T>
//    {
//        private AsyncTaskMethodBuilder<Outcome<T>> _innerBuilder;

//        public PromiseBuilder()
//        {
//            _innerBuilder = new AsyncTaskMethodBuilder<Outcome<T>>();
//        }

//        public static PromiseBuilder<T> Create() =>
//            new PromiseBuilder<T>();

//        public Promise<T> Task =>
//            default(Promise<T>);

//        public void Start<TStateMachine>(ref TStateMachine stateMachine)
//            where TStateMachine : IAsyncStateMachine =>
//            _innerBuilder.Start(ref stateMachine);

//        public void SetStateMachine(IAsyncStateMachine stateMachine) =>
//            _innerBuilder.SetStateMachine(stateMachine);

//        public void SetResult(Outcome<T> result) => _innerBuilder.SetResult(result);

//        public void SetResult(T result) => _innerBuilder.SetResult(new Outcome<T>(result));

//        public void SetException(Exception exception) => _innerBuilder.SetResult((Outcome<T>)exception);

//        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
//            where TAwaiter : INotifyCompletion
//            where TStateMachine : IAsyncStateMachine =>

//            _innerBuilder.AwaitOnCompleted(ref awaiter, ref stateMachine);

//        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
//            where TAwaiter : ICriticalNotifyCompletion
//            where TStateMachine : IAsyncStateMachine =>
//            _innerBuilder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
//    }

//    //public class ValuePromise<T>
//    //{
//    //    private readonly Task<ValueTuple<T, Failure>> _task;

//    //    public ValuePromise(Task<ValueTuple<T, Failure>> task)
//    //    {
//    //        _task = task;
//    //    }

//    //    public TaskAwaiter<ValueTuple<T, Failure>> GetAwaiter()
//    //    {
//    //        return _task.GetAwaiter();
//    //    }

//    //    public static implicit operator ValuePromise<T>(Task<ValueTuple<T, Failure>> task) => new ValuePromise<T>(task);
//    //}
//}
