using System.Collections.Generic;
using System.Linq;
using System;

namespace JSMCodeChallenge.Helpers
{
    public static class Pagination
    {
        private static int defaultPage = 1;
        private static int minPageNumber = 1;
        private static int defaultPageSize = 10;
        private static int maxPageSize = 50;
        private static int minPageSize = 1;

        public class Page
        {
            public int Number { get; }
            public int Size { get; }
            public List<object> Content { get; }

            public Page(int number, int size, IEnumerable<object> content)
            {
                Number = number;
                Size = size;
                Content = content.ToList();
            }
        }

        public static Page GetPage(IEnumerable<object> objects, int? page, int? pageSize)
        {
            pageSize = pageSize.HasValue ? Math.Max(Math.Min(pageSize.Value, maxPageSize), minPageSize) : defaultPageSize;
            page = page.HasValue ? Math.Max(page.Value, minPageNumber) : defaultPage;
            int offset = (page.Value - 1) * pageSize.Value;
            return new Page(page.Value, Math.Min(pageSize.Value, objects.Count()), objects.Skip(offset).Take(pageSize.Value).ToList());
        }

    }
}