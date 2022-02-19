using System;
using System.Diagnostics;

namespace DeltaWare.SDK.Core.Console.Helpers
{
    public static class ConsoleHelper
    {
        /// <summary>
        /// Executes the specified action writing the job name to the log, once complete appends
        /// completed and the running time.
        /// </summary>
        /// <param name="name">The name to be appended to the log.</param>
        /// <param name="action">The action to be performed.</param>
        public static void WriteProcessingLog(string name, Action action)
        {
            System.Console.Write($"{name}: ");

            Stopwatch stopwatch = Stopwatch.StartNew();

            action.Invoke();

            stopwatch.Stop();

            System.Console.WriteLine($"Completed in {stopwatch.Elapsed.ToHumanReadableString()}");
        }

        /// <summary>
        /// Executes the specified action writing the job name to the log, once complete appends
        /// completed and the running time.
        /// </summary>
        /// <param name="name">The name to be appended to the log.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>The specified value.</returns>
        public static TResult WriteProcessingLog<TResult>(string name, Func<TResult> action)
        {
            System.Console.Write($"{name}: ");

            Stopwatch stopwatch = Stopwatch.StartNew();

            TResult result = action.Invoke();

            stopwatch.Stop();

            System.Console.WriteLine($"Completed in {stopwatch.Elapsed.ToHumanReadableString()}");

            return result;
        }
    }
}