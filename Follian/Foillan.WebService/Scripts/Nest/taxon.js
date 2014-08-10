var TaxonViewModel = function () {
    var self = this;
    self.taxa = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();

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

    var speciesuri = '/api/taxon/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllSpecies() {
        ajaxHelper(speciesuri + '?rank=species', 'GET').done(function (data) {
            self.taxa(data);
        });
    }

    self.getSpeciesDetail = function (item) {
        ajaxHelper(speciesuri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    };

    self.addTaxon = function (formElement) {
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

        ajaxHelper(speciesuri, 'POST', taxon).done(function (item) {
            self.taxa.push(item);
        });
    };

    // Fetch the initial data.
    getAllSpecies();
};

$(document).ready(function () {
    ko.applyBindings(new TaxonViewModel());
});