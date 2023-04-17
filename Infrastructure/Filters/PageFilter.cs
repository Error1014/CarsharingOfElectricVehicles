using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Filters
{
    public class PageFilter
    {
        public int NumPage { get; set; }
        public int SizePage { get; set; }

        public PageFilter()
        {
            NumPage = 1;
            SizePage = 10;
        }
        public PageFilter(int numPage, int sizePage)
        {
            NumPage = numPage;
            SizePage = sizePage;
        }
    }
}
