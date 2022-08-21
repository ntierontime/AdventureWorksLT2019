namespace Framework.Models
{
    public class PaginationResponse
    {
        public PaginationResponse(int totalCount, int count, int pageIndex, int pageSize, PaginationOptions paginationOption = PaginationOptions.Paged)
        {
            TotalCount = totalCount;
            Count = count;
            PageSize = pageSize;
            PageIndex = pageIndex;

            LastPageIndex = (int)Math.Ceiling((double)TotalCount / (double)PageSize);
            StartIndex = (PageIndex - 1) * PageSize + 1;
            EndIndex = StartIndex + Count - 1;
            EnableFirstAndPrevious = TotalCount > PageSize && PageIndex != 1;
            EnableLastAndNext = TotalCount > PageSize && PageIndex != LastPageIndex;
            PreCurrent = PageIndex > 1 ? Enumerable.Range(PageIndex - (PageIndex > 2 ? 2 : 1), PageIndex > 2 ? 2 : 1).ToArray() : Enumerable.Empty<int>().ToArray();
            PostCurrent = LastPageIndex - PageIndex >= 1 ? Enumerable.Range(PageIndex + 1, LastPageIndex - PageIndex > 1 ? 2 : 1).ToArray() : Enumerable.Empty<int>().ToArray();
            ThreeDotPreCurrent = PageIndex > 3;
            ThreeDotPostCurrent = LastPageIndex - PageIndex >= 3;
            PaginationOption = paginationOption;
        }
        public int PageSize { get; set; } = 10; // default 10 items per pages
        public int PageIndex { get; set; } = 1; // start from 1

        public int TotalCount { get; set; }
        public int Count { get; set; } // all records in page
        public int LastPageIndex { get; private set; }
        public int StartIndex { get; private set; } // start item index of current page
        public int EndIndex { get; private set; }

        public bool EnableFirstAndPrevious { get; private set; }
        public bool EnableLastAndNext { get; private set; }

        public int[] PreCurrent { get; private set; }
        public int[] PostCurrent { get; private set; }
        public bool ThreeDotPreCurrent { get; private set; }
        public bool ThreeDotPostCurrent { get; private set; }
        public PaginationOptions PaginationOption { get; set; }
    }
}

