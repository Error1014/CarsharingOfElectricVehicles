using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Filters
{
    public class DefoltFilter
    {
        public int Offset { get; set; }
        public int SizePage { get; set; }

        public DefoltFilter()
        {
            Offset = 0;
            SizePage = 10;
        }
        public DefoltFilter(int numPage, int sizePage)
        {
            Offset = numPage;
            SizePage = sizePage;
        }
    }
}
