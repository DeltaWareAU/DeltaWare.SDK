using System;
using System.Threading;

namespace DeltaWare.SDK.Core.Collections.Heap.Exceptions
{
    public class HeapAccessViolationException : Exception
    {
        private HeapAccessViolationException(string message) : base(message)
        {
        }

        internal static HeapAccessViolationException UnexpectedThread(Thread thread)
        {
            return new HeapAccessViolationException($"An Unexpected Thread has Attempted to Access The Heap - ManagedThreadId {thread.ManagedThreadId}. The Heap can only be Accessed by the Specified Number of Threads when Calling the CreateReader and CreateWriter Methods.");
        }

        internal static HeapAccessViolationException UnAllocatedReaders()
        {
            return new HeapAccessViolationException("New Readers Cannot be Created Until the Existing Readers have been Allocated.");
        }
    }
}
