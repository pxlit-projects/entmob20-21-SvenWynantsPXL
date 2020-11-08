using System.Collections.Generic;

namespace StarWarsUniverse.Data.Repositories
{
    internal class ResultsPage<T>
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<T> Results { get; set; }
    }
}
