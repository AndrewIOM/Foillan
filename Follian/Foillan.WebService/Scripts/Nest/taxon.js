var TaxonVIewModel = function () {
    var self = this;
    self.taxa = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();

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
    }

    // Fetch the initial data.
    getAllSpecies();
};

ko.applyBindings(new TaxonVIewModel());