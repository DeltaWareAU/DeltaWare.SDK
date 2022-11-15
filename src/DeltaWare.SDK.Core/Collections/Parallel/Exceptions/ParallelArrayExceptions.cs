using System;
using System.Threading;

namespace DeltaWare.SDK.Core.Collections.Parallel.Exceptions
{
    internal static class ParallelArrayExceptions
    {
        public static AccessViolationException UnallocatedThreadException(Thread thread)
            => new($"An Unallocated Thread Attempted to Access the Array - ManagedThreadId {thread.ManagedThreadId}. The Array can only be Accessed by the Specified Number of Threads when Calling the CreateReader and CreateWriter Methods.");

        public static AccessViolationException UnallocatedReaderException()
            => new("New Array Readers Cannot be Created Until the Existing Readers have been Fully Allocated.");

        public static IndexOutOfRangeException WriterOutOfRangeException()
            => new("An ArrayWriter has Attempted to Write to an Unallocated Index.");

        public static IndexOutOfRangeException UnallocatedReadAccess(int index)
            => new($"An ArrayReader has Attempted to Read from an Unallocated Heap Index[{index}].");
    }
}
