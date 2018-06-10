using Codoxide.Outcomes;
using System;
using System.Threading.Tasks;

namespace Codoxide
{
    public class OutcomeFinalizer<T, ReturnType>
    {
        private readonly Outcome<T> _outcome;
        private readonly ReturnType _fallback;

        private bool _isHandled;
        private ReturnType _returnValue;

        public OutcomeFinalizer(Outcome<T> outcome) : this(outcome, default(ReturnType))
        {
        }

        public OutcomeFinalizer(Outcome<T> outcome, ReturnType fallbackValue)
        {
            _outcome = outcome;
            _fallback = fallbackValue;
        }

        public ReturnType Unwrap() => _isHandled ? _returnValue : _fallback;

        internal OutcomeFinalizer<T, ReturnType> OnSuccess(Func<T, ReturnType> handler)
        {
            if (_outcome.IsSuccessful)
            {
                _isHandled = true;
                _returnValue = handler(_outcome.Result);
            }
            return this;
        }

        public OutcomeFinalizer<T, ReturnType> Catch<ExceptionType>(Func<ExceptionType, ReturnType> handler) where ExceptionType : Exception
        {
            if (!_isHandled && !_outcome.IsSuccessful && _outcome.Failure.Exception is ExceptionType)
            {
                _isHandled = true;
                _returnValue = handler((ExceptionType)_outcome.Failure.Exception);
            }
            return this;
        }

        public OutcomeFinalizer<T, ReturnType> Catch(Func<Failure, ReturnType> handler)
        {
            if (!_isHandled && !_outcome.IsSuccessful)
            {
                _isHandled = true;
                _returnValue = handler(_outcome.Failure);
            }

            return this;
        }

        public async Task<OutcomeFinalizer<T, ReturnType>> Catch<ExceptionType>(Func<ExceptionType, Task<ReturnType>> handler) where ExceptionType : Exception
        {
            if (!_isHandled && !_outcome.IsSuccessful && _outcome.Failure.Exception is ExceptionType)
            {
                _isHandled = true;
                _returnValue = await handler((ExceptionType)_outcome.Failure.Exception);
            }
            return this;
        }

        //public async Task<OutcomeFinalizer<T, ReturnType>> Catch(Func<Exception, Task<ReturnType>> handler)
        //{
        //    if (!_isHandled && !_outcome.IsSuccessful && _outcome.Failure.Exception != null)
        //    {
        //        _isHandled = true;
        //        _returnValue = await handler(_outcome.Failure.Exception);
        //    }

        //    return this;
        //}

        public async Task<OutcomeFinalizer<T, ReturnType>> Catch(Func<Failure, Task<ReturnType>> handler)
        {
            if (!_isHandled && !_outcome.IsSuccessful)
            {
                _isHandled = true;
                _returnValue = await handler(_outcome.Failure);
            }

            return this;
        }
    }
}
