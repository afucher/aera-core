using System.Collections.Generic;

namespace aera_core.POUIHelpers
{
    public class POUIListResponse<T>
    {
        public IReadOnlyCollection<T> items { get; }

        public POUIListResponse(IReadOnlyCollection<T> items)
        {
            this.items = items;
        }
    }
}