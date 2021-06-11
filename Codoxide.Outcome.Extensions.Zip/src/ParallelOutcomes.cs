using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Codoxide.Outcomes;

namespace Codoxide
{
    using ExtractorFunc = Func<Task<(object, Failure)>>;
    using DestructOutcome = ValueTuple<object, Failure>;

    public struct Forks<T1, T2>
    {
        public Task<Outcome<T1>> Task1 { get; set; }
        public Task<Outcome<T2>> Task2 { get; set; }
    }

    public struct Forks<T1, T2, T3>
    {
        public Task<Outcome<T1>> Task1 { get; set; }
        public Task<Outcome<T2>> Task2 { get; set; }
        public Task<Outcome<T3>> Task3 { get; set; }
    }

    // public struct Parallels<T1, T2, T3>
    // {
    //     public Task<Outcome<T1>> Task1 { get; set; }
    //     public Task<Outcome<T2>> Task2 { get; set; }
    //     
    //     
    // }
    //
    // public struct Parallels<T1, T2>
    // {
    //     public Task<Outcome<T1>> Task1 { get; set; }
    //     public Task<Outcome<T2>> Task2 { get; set; }
    // }
    
    public class ParallelOutcomes
    {
        private readonly IList<Task> _tasks = new List<Task>(5);
        private readonly IList<ExtractorFunc> _extractors = new List<ExtractorFunc>(5);
        
        public IEnumerable<Task> Tasks => _tasks;

        public ParallelOutcomes()
        {
        }

        public ParallelOutcomes(Task initial)
        {
            _tasks.Add(initial);
        }

        public ParallelOutcomes Fork<T>(Func<Task<Outcome<T>>> fn)
        {
            var task = fn();
            _tasks.Add(task);
            
            async Task<(object, Failure)> extractor() => await task;
            _extractors.Add(extractor);

            return this;
        }

        public async Task<Outcome<T>> Join<T>(Func<object, Task<T>>[] handler)
        {
            try
            {
                await Task.WhenAll(_tasks);

                var results = new (object result, Failure failure)[_tasks.Count];
                for (int i = results.Length - 1; i >= 0; i--)
                {
                    results[i] = await _extractors[i]();
                    if (results[i].failure == null) handler[i](results[i].result);
                }

                return results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}