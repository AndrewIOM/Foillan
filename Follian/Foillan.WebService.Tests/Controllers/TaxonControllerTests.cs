using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Foillan.DataTransferObjects;
using Foillan.DataTransferObjects.Tests.TestBuilders;
using Foillan.Models.Biodiversity;
using Foillan.Models.Tests.TestBuilders;
using Foillan.WebService.Controllers;
using Moq;
using NUnit.Framework;

namespace Foillan.WebService.Tests.Controllers
{
    [TestFixture]
    public class TaxonControllerTests
    {
        private readonly Taxon _TestTaxon = new Taxon
        {
            Id = 6,
            LatinName = "testTaxon",
            Rank = TaxonRank.Phylum
        };

        //GET: /Api/Taxon?rank={rank}

        [Test]
        public void Get_RankNotSent_ThrowsHttpResponseException()
        {
            var service = new TaxonServiceTestBuilder().ReturnsSpecies().Build();
            var sut = new TaxonController(service);
            Assert.Throws<HttpResponseException>(() => sut.Get(TaxonRank.Null));
        }

        [Test]
        public void Get_RankIsNotValid_ThrowsHttpResponseException()
        {
            var service = new TaxonServiceTestBuilder().ReturnsSpecies().Build();
            var sut = new TaxonController(service);
            Assert.Throws<HttpResponseException>(() => sut.Get(TaxonRank.Null));
        }

        [Test]
        public void Get_ValidRankWithNoDatabaseResults_ReturnsEmptyListOfTaxonDTOs()
        {
            var service = new TaxonServiceTestBuilder().Build();
            var sut = new TaxonController(service);
            var actual = sut.Get(TaxonRank.Species);
            Assert.IsEmpty(actual);
        }

        [Test]
        public void Get_ValidRankWithDatabaseResults_ReturnsListOfTaxonDTOs()
        {
            var service = new TaxonServiceTestBuilder().ReturnsSpecies().Build();
            var sut = new TaxonController(service);
            var actual = sut.Get(TaxonRank.Species);
            Assert.IsNotEmpty(actual);
        }

        //GET: /Api/Taxon?parent={id}

        [Test]
        public void Get_UnderpostedParentId_BadRequest()
        {
            var service = new TaxonServiceTestBuilder().Build();
            var sut = new TaxonController(service);
            var result = sut.Get(0);
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void Get_TaxonServiceCannotFindParentWithId_BadRequest()
        {
            var service = new TaxonServiceTestBuilder().Build();
            var sut = new TaxonController(service);
            var result = sut.Get(78798);
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void Get_ValidParentIdAndParentHasChildTaxa_ReturnsListOfTaxonDTOs()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Get_ValidParentIdAndParentHasNoChildren_ReturnsEmptyListOfTaxonDTOs()
        {
            Assert.Inconclusive();
        }

        //GET: /Api/Taxon/{id}

        [Test]
        public void GetTaxon_UnderpostedId_BadRequest()
        {
            var service = new TaxonServiceTestBuilder().Build();
            var sut = new TaxonController(service);
            var result = sut.GetTaxon(0);
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void GetTaxon_TaxonServiceReturnsTaxonUsingId_OkResultWithTaxonDTO()
        {
            var service = new TaxonServiceTestBuilder().ReturnsTaxonOfId(6, _TestTaxon).Build();
            var sut = new TaxonController(service);
            var result = sut.GetTaxon(6) as OkNegotiatedContentResult<TaxonDTO>;
            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Content.Id, "The taxon IDs were different between that requested and recieved");
        }

        [Test]
        public void GetTaxon_TaxonServiceReturnsNullUsingId_NotFoundResult()
        {
            var service = new TaxonServiceTestBuilder().ReturnsTaxonOfId(6, null).Build();
            var sut = new TaxonController(service);
            var result = sut.GetTaxon(6) as NotFoundResult;
            Assert.IsNotNull(result);
        }

        // POST /api/Taxon

        [Test]
        public void Post_UnderpostedTaxonDTO_BadRequest()
        {
            var service = new TaxonServiceTestBuilder().Build();
            var sut = new TaxonController(service);
            var result = sut.Post(null);
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void Post_InvalidModel_BadRequest()
        {
            var invalidDto = new TaxonDTO();
            var service = new TaxonServiceTestBuilder().Build();
            var sut = new TaxonController(service);
            sut.ModelState.AddModelError("Fake Error", "This is a dummy error");
            var result = sut.Post(invalidDto);
            Assert.IsInstanceOf<InvalidModelStateResult>(result);
        }

        [Test]
        public void Post_ValidModel_AddTaxonCalledOnServiceExactlyOnce()
        {
            var service = new TaxonServiceTestBuilder().BuildMock();
            var sut = new TaxonController(service.Object);
            sut.Post(new TaxonDTOTestBuilder().ValidInitial().Build());
            service.Verify(m => m.AddTaxonWithTaxonomy(It.IsAny<Taxon>(),
                It.IsAny<IDictionary<TaxonRank, string>>()), Times.Once);
        }

        [Test]
        public void Post_ValidModel_TaxonGeneratedForServiceHasPropertiesFromDTO()
        {
            var service = new TaxonServiceTestBuilder().BuildMock();
            var sut = new TaxonController(service.Object);

            Taxon result = null;
            service.Setup(h => h.AddTaxonWithTaxonomy(It.IsAny<Taxon>(), It.IsAny<IDictionary<TaxonRank, string>>()))
                .Callback<Taxon>(r => result = r);

            sut.Post(new TaxonDTOTestBuilder().ValidInitial().Build());


        }

        [Test]
        public void Post_ValidModel_ChangesSavedUsingServiceExactlyOnce()
        {
            Assert.Inconclusive();
        }

        //PUT /api/Taxon/{id}

        [Test]
        public void Put_UnderpostedId_BadRequest()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Put_UnderpostedTaxon_BadRequest()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Put_TaxonRetrievedFromServiceUsingId()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Put_NoExistingTaxon_BadRequest()
        {

        }

        [Test]
        public void Put_NewTaxonInvalid_BadRequest()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Put_TaxonAlreadyExists_DescriptionUpdated()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Put_TaxonAlreadyExists_LatinNameUpdated()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Put_TaxonAlreadyExists_SaveChangesCalledOnce()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Put_TaxonAlreadyExists_ReturnsOkResult()
        {
            Assert.Inconclusive();
        }

    }
}