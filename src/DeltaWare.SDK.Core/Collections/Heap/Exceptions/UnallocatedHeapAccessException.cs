using System;
/* Unmerged change from project 'DeltaWare.SDK.Core (net5.0)'
Before:
using System.Threading.Tasks;
using DeltaWare.SDK.Core.Collections.Heap.Writer;
After:
using System.Threading.Tasks;
*/


namespace DeltaWare.SDK.Core.Collections.Heap.Exceptions
{
    public class UnallocatedHeapAccessException : Exception
    {
        public UnallocatedHeapAccessException(string message) : base(message)
        {
        }

        public static UnallocatedHeapAccessException UnallocatedWriteAccess()
        {
            return new UnallocatedHeapAccessException("A HeapWriter has Attempted to Write to an Unallocated Heap Index.");
        }
    }
}
