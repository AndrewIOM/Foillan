﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Foillan.Models.Biodiversity;
using Foillan.Models.DataAccess.Abstract;

namespace Foillan.Models.Tests.DummyClasses
{
    public class InMemoryTaxonRepository : IRepository<Taxon>
    {
        private List<Taxon> _taxaInDatabase;
        private List<AdditionalDetails> _speciesDetails;
        private int _id;

        public InMemoryTaxonRepository()
        {
            _taxaInDatabase = new List<Taxon>();
            _speciesDetails = new List<AdditionalDetails>();
            AddDummyData();
        }

        public virtual Taxon Add(Taxon entity)
        {
            _taxaInDatabase.Add(entity);
            return _taxaInDatabase.Last();
        }

        public void Update(Taxon entity)
        {
            var result = _taxaInDatabase.Find(m => m.Id.Equals(entity.Id));
            if (result != null)
            {
                _taxaInDatabase.Remove(result);
            }
            _taxaInDatabase.Add(entity);
        }

        public virtual void Delete(Taxon entity)
        {
            _taxaInDatabase.Remove(entity);
        }

        public virtual Taxon GetById(int id)
        {
            return _taxaInDatabase.Find(t => t.Id.Equals(id));
        }

        public virtual IEnumerable<Taxon> GetAll()
        {
            return _taxaInDatabase;
        }

        public virtual IQueryable<Taxon> FindBy(Expression<Func<Taxon, bool>> predicate)
        {
            //Returns all regardless of expression
            var result = _taxaInDatabase.Where(m => m.Id > 0);
            return result.AsQueryable();
        }

        private void AddDummyData()
        {
            _taxaInDatabase.Add(new Taxon {Id = 1, Rank = TaxonRank.Life, Description = "Life"});
        }

    }
}
