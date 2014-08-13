var TaxonViewModel = function () {
    var self = this;

    //Taxonomy
    var speciesUri = '/api/taxon/';
    self.taxa = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.currentView = '';

    self.newTaxon = {
        LatinName: ko.observable(),
        Rank: ko.observable(),
        Description: ko.observable()
    };

    self.newTaxonTaxonomy = {
        Kingdom: ko.observable(),
        Phylum: ko.observable(),
        Class: ko.observable(),
        Order: ko.observable(),
        Family: ko.observable(),
        Genus: ko.observable(),
        Species: ko.observable()
    };

    //GBIF Search
    var gbifUri = "http://api.gbif.org/v1/species/";
    self.searchResults = ko.observableArray();

    self.gbifSearchCriteria = {
        SearchTerm: ko.observable(),
        SearchRank: ko.observable()
    };

    //Functions
    function ajaxHelper(uri, method, dataType, data) {
        self.error('');
        return $.ajax({
            type: method,
            url: uri,
            dataType: dataType,
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXhr, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllSpecies() {
        ajaxHelper(speciesUri + '?rank=species', 'GET', 'json').done(function (data) {
            self.taxa(data);
        });
    }

    self.getSpeciesDetail = function (item) {
        ajaxHelper(speciesUri + item.Id, 'GET', 'json').done(function (data) {
            self.detail(data);
        });
    };

    self.addTaxon = function () {
        var taxon = {
            LatinName: self.newTaxon.LatinName(),
            Description: self.newTaxon.Description(),
            Rank: self.newTaxon.Rank(),
            Taxonomy: {
                Kingdom: self.newTaxonTaxonomy.Kingdom(),
                Phylum: self.newTaxonTaxonomy.Phylum(),
                Class: self.newTaxonTaxonomy.Class(),
                Order: self.newTaxonTaxonomy.Order(),
                Family: self.newTaxonTaxonomy.Family(),
                Genus: self.newTaxonTaxonomy.Genus(),
                Species: self.newTaxonTaxonomy.Species()
            }
        };

        ajaxHelper(speciesUri, 'POST','json', taxon).done(function (item) {
            self.taxa.push(item);
        });
    };

    self.gbifSearch = function () {
        var searchString = 'search?q=' + self.gbifSearchCriteria.SearchTerm() + '&rank=' + self.gbifSearchCriteria.SearchRank();
        ajaxHelper(gbifUri + searchString, 'GET', 'jsonp').done(function (data) {
            self.searchResults(data.results);
        });
    }

    self.useGbifSpecies = function (item) {
        self.newTaxon.LatinName(item.canonicalName);
        self.newTaxon.Rank(item.rank);
        self.newTaxonTaxonomy.Kingdom(item.kingdom);
        self.newTaxonTaxonomy.Phylum(item.phylum);
        self.newTaxonTaxonomy.Order(item.order);
        self.newTaxonTaxonomy.Class(item.class);
        self.newTaxonTaxonomy.Family(item.family);
        self.newTaxonTaxonomy.Genus(item.genus);
        self.newTaxonTaxonomy.Species(item.species);
    };

    // Fetch the initial data.
    getAllSpecies();
};

$(document).ready(function () {
    ko.applyBindings(new TaxonViewModel());
});