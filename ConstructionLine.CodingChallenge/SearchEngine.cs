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
            searchResults.ColorCounts.AddRange(_shirts.GroupBy(shirt => shirt.Color)
                      .SelectMany(groupOfSearch => groupOfSearch.Select
                      (
                          eachShirt => new ColorCount()
                          {
                              Color = eachShirt.Color,
                              Count = searchResults.Shirts.Count(shirt => shirt.Color == eachShirt.Color)
                          }
                      )));

            // Size Counts
            searchResults.SizeCounts.AddRange(_shirts.GroupBy(shirt => shirt.Color)
                     .SelectMany(groupOfSearch => groupOfSearch.Select
                     (
                         eachShirt => new SizeCount()
                         {
                             Size = eachShirt.Size,
                             Count = searchResults.Shirts.Count(shirt => shirt.Size == eachShirt.Size)
                         }
                     )));


            return searchResults;
        }
    }
}