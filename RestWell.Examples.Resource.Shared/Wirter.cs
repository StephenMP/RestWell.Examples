using System;

namespace RestWell.Examples.Resource.Shared
{
    public static class Writer
    {
        public static void WriteValues(string[] values)
        {
            Console.WriteLine("Values from the API:");

            foreach (var value in values)
            {
                Console.WriteLine($"\t{value}");
            }
        }
    }
}
