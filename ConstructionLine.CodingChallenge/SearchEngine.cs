using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.

        }


        public SearchResults Search(SearchOptions options)
        {
            // TODO: search logic goes here.

            var searchResults = new SearchResults()
            {
                Shirts = new List<Shirt>(),
                ColorCounts = new List<ColorCount>(),
                SizeCounts = new List<SizeCount>()
            };


            // Search for colour
            options.Colors.ForEach(searchShirtColor =>
            {
                var colorSearchResult = _shirts.Where(x => x.Color == searchShirtColor);
                searchResults.Shirts.AddRange(colorSearchResult);
            });

            // Color Counts.

            foreach (var item in Color.All)
            {
                var search = searchResults.Shirts.Where(x => x.Color.Id == item.Id).Select(x => x.Color).ToList();

                searchResults.ColorCounts.Add(new ColorCount()
                {
                    Color = item,
                    Count = search.Count()
                });
            }

            // Size Counts
            foreach (var item in Size.All)
            {
                var search = searchResults.Shirts.Where(x => x.Size.Id == item.Id).Select(x => x.Size).ToList();

                searchResults.SizeCounts.Add(new SizeCount()
                {
                    Size = item,
                    Count = search.Count()
                });
            }

            return searchResults;
        }
    }
}