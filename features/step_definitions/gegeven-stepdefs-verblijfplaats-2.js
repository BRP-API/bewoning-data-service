const { Given } = require('@cucumber/cucumber');
const deepEqualInAnyOrder = require('deep-equal-in-any-order');
const should = require('chai').use(deepEqualInAnyOrder).should();
const { createWoonadres } = require('./verblijfplaats');
const { createPersoon } = require('./persoon');
const { arrayOfArraysToDataTable } = require('./dataTableFactory');
const { createVerblijfplaats } = require('./persoon-2');

Given(/^met datum aanvang adreshouding (\d*) zijn de volgende personen ingeschreven op adres '(.*)'$/, function (datumAanvang, adresId, dataTable) {
    // De dataTable moet minimaal een kolom 'burgerservicenummer (01.20)' hebben

    const verblijfplaatsData = [
        ['gemeente van inschrijving (09.10)', '0800'],
        ['datum aanvang adreshouding (10.30)', datumAanvang]
    ];

    for (dataRow of dataTable.hashes()) {
        // burgerservicenummer wordt uit de dataTable gehaald want moet apart worden opgegeven bij creeren van persoon
        if (!dataRow.hasOwnProperty('burgerservicenummer (01.20)')) {
            throw new Error("In de data tabel ontbreekt kolom 'burgerservicenummer (01.20)'");
        }
        burgerservicenummer = dataRow['burgerservicenummer (01.20)'];
        delete dataRow['burgerservicenummer (01.20)'];

        // maak eerst de persoon en dan de verblijfplaats
        createPersoon(this.context, burgerservicenummer, arrayOfArraysToDataTable(Object.entries(dataRow)));
        createWoonadres(this.context, adresId, arrayOfArraysToDataTable(verblijfplaatsData));
    };
});

Given(/^vervolgens zijn de volgende personen met datum aanvang adreshouding (\d*) ingeschreven op adres '(.*)'$/, function (datumAanvang, adresId, dataTable) {
    // De dataTable moet minimaal een kolom 'burgerservicenummer (01.20)' hebben

    const verblijfplaatsData = [
        ['gemeente van inschrijving (09.10)', '0800'],
        ['datum aanvang adreshouding (10.30)', datumAanvang]
    ];

    for (dataRow of dataTable.hashes()) {
        burgerservicenummer = dataRow['burgerservicenummer (01.20)'];

        // bepaal de index waar deze persoon in de sqlData is opgeslagen
        index = this.context.sqlData.findIndex(i => i.persoon != undefined && i.persoon[0].find(p => p[0] == 'burger_service_nr')[1] == burgerservicenummer);

        createWoonadres(this.context, adresId, arrayOfArraysToDataTable(verblijfplaatsData), index);
    };
});
