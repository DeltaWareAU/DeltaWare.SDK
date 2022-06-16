// ReSharper disable once CheckNamespace
namespace System.Threading.Tasks
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Awaits a <see cref="Task"/> in a non blocking context and consumes an exception if thrown.
        /// </summary>
        /// <param name="task">The <see cref="Task"/> to be awaited.</param>
        /// <param name="onException">
        /// Specify the action to be taken if an <see cref="Exception"/> is thrown.
        /// </param>
        public static async void FireAndForget(this Task task, Action<Exception> onException = null)
        {
            try
            {
                await task;
            }
            catch (Exception exception)
            {
                onException?.Invoke(exception);
            }
        }

        public static async Task<TDestination> CastAsync<TDestination>(this ValueTask<object> task)
        {
            object source = await task;

            return (TDestination)source;
        }
    }
}