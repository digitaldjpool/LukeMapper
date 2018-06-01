using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LukeMapper
{
    public class LukeSearchResult<T>
    {
        public int TotalHits { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}