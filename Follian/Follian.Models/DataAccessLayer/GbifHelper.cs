using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization.Json;
using Foillan.Models.Biodiversity;

namespace Foillan.Models.DataAccessLayer
{
    public static class GbifHelper
    {
        public static Taxon GenerateTaxonByNameAndRank(String latinName, TaxonRank rank)
        {
            var urlRequest = String.Format("http://api.gbif.org/v0.9/species/match?strict=true&rank={0}&name={1}", rank, latinName);
            var result = GetNameUsagePage(urlRequest);

            if (result.UsageKey == 0)
            {
                throw new Exception(String.Format("GBIF did not return a valid 'species detail' response for {0}.", latinName));
            }

            var resultantRank = (TaxonRank)Enum.Parse(typeof(TaxonRank), result.Rank, true);
            var newTaxon = new Taxon()
            {
                Rank = resultantRank,
                LatinName = result.CanonicalName,
            };

            return newTaxon;
        }

        public static int GetGbifIdForTaxon(String latinName, TaxonRank rank) 
        {
            var urlRequest = String.Format("http://api.gbif.org/v0.9/species/match?strict=true&rank={0}&name={1}", rank, latinName);
            var result = GetNameUsagePage(urlRequest);

            if (result.UsageKey == 0)
            {
                throw new Exception("Couldn't find GBIF ID from name ");
            }

            return result.UsageKey;
        }

        public static Taxon GetTaxonById(int id)
        {
            var urlRequest = String.Format("http://api.gbif.org/v0.9/species/{0}", id);
            var result = GetNameUsagePage(urlRequest);

            if (result == null)
            {
                return null;
            }

            var resultantRank = (TaxonRank)Enum.Parse(typeof(TaxonRank), result.Rank, true);
            var newTaxon = new Taxon()
            {
                Rank = resultantRank,
                LatinName = result.CanonicalName,
            };

            return newTaxon;
        }

        public static Dictionary<TaxonRank, string> GetTaxonomyDictionary(int id)
        {
            var urlRequest = String.Format("http://api.gbif.org/v0.9/species/{0}", id);
            var result = GetNameUsagePage(urlRequest);

            if (result == null)
            {
                return null;
            }

            var taxonomy = new Dictionary<TaxonRank, string> // Puffin
            {
                {TaxonRank.Kingdom, result.Kingdom},
                {TaxonRank.Phylum, result.Phylum},
                {TaxonRank.Class, result.Class},
                {TaxonRank.Order, result.Order},
                {TaxonRank.Family, result.Family},
                {TaxonRank.Genus, result.Genus}
            };
            return taxonomy;
        }

        private static GbifSpecies GetNameUsagePage(string requestUrl)
        {
            try
            {
                var request = WebRequest.Create(requestUrl) as HttpWebRequest;
                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                            throw new Exception(String.Format(
                                "Server error (HTTP {0}: {1}).",
                                response.StatusCode,
                                response.StatusDescription));
                        var jsonSerializer = new DataContractJsonSerializer(typeof(GbifSpecies));
                        var objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                        var jsonResponse = objResponse as GbifSpecies;
                        return jsonResponse;
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
