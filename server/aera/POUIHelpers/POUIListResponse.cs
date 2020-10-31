using System.Collections.Generic;

namespace aera_core.POUIHelpers
{
    public class POUIListResponse<T>
    {
        public IReadOnlyCollection<T> items { get; }
        public bool hasNext { get; }

        public POUIListResponse(IReadOnlyCollection<T> items, bool hasNext=false)
        {
            this.items = items;
            this.hasNext = hasNext;
        }
    }
}