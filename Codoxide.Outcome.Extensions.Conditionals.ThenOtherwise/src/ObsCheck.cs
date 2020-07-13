using System;

namespace Codoxide
{
    public class ObsCheck
    {
        public ObsCheck()
        {
            new Outcome<string>().Then(true, Console.WriteLine, () => Console.WriteLine());
        }
    }
}