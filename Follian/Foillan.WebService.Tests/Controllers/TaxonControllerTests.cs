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
            var service = new TaxonServiceTestBuilder().BuildMock();
            var sut = new TaxonController(service.Object);
            sut.Post(new TaxonDTOTestBuilder().ValidInitial().Build());
            service.Verify(m => m.SaveChanges(), Times.Once);
        }

        //PUT /api/Taxon/{id}

        [Test]
        public void Put_UnderpostedId_BadRequest()
        {
            var service = new TaxonServiceTestBuilder().Build();
            var sut = new TaxonController(service);
            var result = sut.Put(0, _TestTaxonDTO);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void Put_UnderpostedTaxon_BadRequest()
        {
            var service = new TaxonServiceTestBuilder().Build();
            var sut = new TaxonController(service);
            var result = sut.Put(6, null);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void Put_TaxonRetrievedFromServiceUsingId()
        {
            var service = new TaxonServiceTestBuilder().ReturnsTaxonOfId(6, _TestTaxon).BuildMock();
            var sut = new TaxonController(service.Object);
            var result = sut.Put(6, _TestTaxonDTO);
            service.Verify(m => m.GetTaxonById(6), Times.Once);
        }

        [Test]
        public void Put_NoExistingTaxon_BadRequest()
        {
            var service = new TaxonServiceTestBuilder().Build();
            var sut = new TaxonController(service);
            var result = sut.Put(6, _TestTaxonDTO);
        }

        [Test]
        public void Put_IdAndTaxonIdDoNotMatch_BadRequest()
        {
            var service = new TaxonServiceTestBuilder().Build();
            var sut = new TaxonController(service);
            var result = sut.Put(21, _TestTaxonDTO); //Taxon has ID of 6
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void Put_NewTaxonInvalid_BadRequest()
        {
            var service = new TaxonServiceTestBuilder().Build();
            var sut = new TaxonController(service);
            sut.ModelState.AddModelError("Test Error", "Test error description.");
            var result = sut.Put(21, _TestTaxonDTO);
            Assert.IsInstanceOf<BadRequestErrorMessageResult>(result);
        }

        [Test]
        public void Put_TaxonAlreadyExists_DescriptionUpdated()
        {
            var updated = _TestTaxonDTO;
            updated.Description = "Updated description";
            var service = new TaxonServiceTestBuilder().ReturnsTaxonOfId(6, _TestTaxon).Build();
            var sut = new TaxonController(service);
            var result = sut.Put(6, updated) as OkNegotiatedContentResult<TaxonDTO>;
            Assert.AreEqual(updated.Description, result.Content.Description);
        }

        [Test]
        public void Put_TaxonAlreadyExists_LatinNameUpdated()
        {
            var updated = _TestTaxonDTO;
            updated.LatinName = "Updated name";
            var service = new TaxonServiceTestBuilder().ReturnsTaxonOfId(6, _TestTaxon).Build();
            var sut = new TaxonController(service);
            var result = sut.Put(6, updated) as OkNegotiatedContentResult<TaxonDTO>;
            Assert.AreEqual(updated.LatinName, result.Content.LatinName);
        }

        [Test]
        public void Put_TaxonAlreadyExists_SaveChangesCalledOnce()
        {
            var service = new TaxonServiceTestBuilder().ReturnsTaxonOfId(6, _TestTaxon).BuildMock();
            var sut = new TaxonController(service.Object);
            sut.Put(6, _TestTaxonDTO);
            service.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Test]
        public void Put_TaxonAlreadyExists_ReturnsOkResult()
        {
            var service = new TaxonServiceTestBuilder().ReturnsTaxonOfId(6, _TestTaxon).Build();
            var sut = new TaxonController(service);
            var result = sut.Put(6, _TestTaxonDTO);
            Assert.IsInstanceOf<OkNegotiatedContentResult<TaxonDTO>>(result);
        }

        private readonly Taxon _TestTaxon = new Taxon
        {
            Id = 6,
            LatinName = "testTaxon",
            Rank = TaxonRank.Phylum
        };

        private readonly TaxonDTO _TestTaxonDTO = new TaxonDTO
        {
            Id = 6,
            LatinName = "testTaxon",
            Rank = TaxonRank.Phylum,
            Taxonomy = new Taxonomy
            {
                Kingdom = "Test Kingdom"
            }
        };
    }
}