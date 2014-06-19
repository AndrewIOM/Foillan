using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Foillan.Models.Biodiversity;
using LINQtoCSV;

namespace Foillan.Models.Utilities
{
    public static class CommaSeperatedFileUtility
    {
        public static IEnumerable<Taxon> GetSpeciesFromCommaSeperatedFile(Stream fileStream)
        {
            var fileDescription = new CsvFileDescription
                              {
                                  SeparatorChar = ',',
                                  FirstLineHasColumnNames = true
                              };
            var fileContext = new CsvContext();

            var streamReader = new StreamReader(fileStream);
            var result = fileContext.Read<Taxon>(streamReader, fileDescription).ToList();

            foreach (var species in result)
            {
                if (String.IsNullOrEmpty(species.LatinName))
                {
                    throw new Exception("The comma seperated value file contains invalid species. " +
                                        "The LatinName' column cannot have empty cells");
                }
                species.Rank = TaxonRank.Species;
            }

            return result;
        }
    }
}
