using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infotrack.Sales.SearchEngine.Contracts
{
    public class SearchHistoryResponse
    {
        public string Keyword { get; set; }

        public string Url { get; set; }

        public int Rank { get; set; }

        public DateTime SearchDate { get; set; }
    }
}
