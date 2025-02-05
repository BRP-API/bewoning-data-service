const { Given } = require('@cucumber/cucumber');
const deepEqualInAnyOrder = require('deep-equal-in-any-order');
const should = require('chai').use(deepEqualInAnyOrder).should();
const { createPersoonMetWoonadres,
        createPersoonMetBriefadres,
        createPersoonMetVerblijfplaats,
        createPersoonMetVerblijfplaatsBuitenland,
        createWoonadres,
        createBriefadres,
        createVerblijfplaatsBuitenland,
        createVerblijfplaats,
        corrigeerVerblijfplaats } = require('./verblijfplaats');
const { createPersoon } = require('./persoon');
const { arrayOfArraysToDataTable } = require('./dataTableFactory');

Given(/^de persoon met burgerservicenummer '(\d*)' is ingeschreven op adres '(\w*)' met de volgende gegevens$/, function (burgerservicenummer, adresId, dataTable) {
    createPersoonMetWoonadres(this.context, burgerservicenummer, adresId, dataTable);
});

Given(/^de persoon met burgerservicenummer '(\d*)' heeft adres '(\w*)' als briefadres opgegeven met de volgende gegevens$/, function (burgerservicenummer, adresId, dataTable) {
    createPersoonMetBriefadres(this.context, burgerservicenummer, adresId, dataTable);
});

Given(/^de persoon met burgerservicenummer '(\d*)' is ingeschreven op een buitenlands adres met de volgende gegevens$/, function (burgerservicenummer, dataTable) {
    createPersoonMetVerblijfplaatsBuitenland(this.context, burgerservicenummer, dataTable);
});

Given(/^de persoon met burgerservicenummer '(\d*)' heeft de volgende 'verblijfplaats' gegevens$/, function (burgerservicenummer, dataTable) {
    createPersoonMetVerblijfplaats(this.context, burgerservicenummer, dataTable);
});

Given(/^de persoon heeft de volgende 'verblijfplaats' gegevens$/, function (dataTable) {  
    createVerblijfplaats(this.context, dataTable);
});

Given(/^de persoon is ?(?:vervolgens)? ingeschreven op adres '(\w*)' met de volgende gegevens$/, function (adresId, dataTable) {
    createWoonadres(this.context, adresId, dataTable);
});

Given(/^de persoon heeft ?(?:vervolgens)? adres '(\w*)' als briefadres opgegeven met de volgende gegevens$/, function (adresId, dataTable) {
    createBriefadres(this.context, adresId, dataTable);
});

Given(/^de persoon is ?(?:vervolgens)? ingeschreven op een buitenlands adres met de volgende gegevens$/, function (dataTable) {
    createVerblijfplaatsBuitenland(this.context, dataTable);
});

Given(/^de 'verblijfplaats' is gecorrigeerd naar de volgende gegevens$/, function (dataTable) {
    corrigeerVerblijfplaats(this.context, undefined, dataTable, undefined);
});

Given(/^de inschrijving is vervolgens gecorrigeerd als een inschrijving op adres '(.*)' met de volgende gegevens$/, function (adresId, dataTable) {
    corrigeerVerblijfplaats(this.context, adresId, dataTable, true);
});

Given(/^er zijn (\d*) personen ingeschreven op adres '(.*)' met de volgende gegevens$/, function (aantal, adresId, dataTable) {
    const adressenData = this.context.sqlData.find(e => Object.keys(e).includes('adres'));
    should.exist(adressenData, 'geen adressen gevonden');
    const adresIndex = adressenData.adres[adresId]?.index;
    should.exist(adresIndex, `geen adres gevonden met id '${adresId}'`);

    let i = 0;
    while(i < Number(aantal)) {
        i++;

        const burgerservicenummer = (i + '').padStart(9, '0');

        createPersoonMetWoonadres(this.context, burgerservicenummer, adresId, dataTable);
    }
});

Given(/^met datum aanvang adreshouding (\d*) zijn de volgende personen ingeschreven op adres '(.*)'$/, function (datumAanvang, adresId, dataTable) {
    // De dataTable moet minimaal een kolom 'burgerservicenummer (01.20)' hebben
     
    const verblijfplaatsData = [
        ['gemeente van inschrijving (09.10)', '0800'],
        ['datum aanvang adreshouding (10.30)', datumAanvang]
    ];

    for (dataRow of dataTable.hashes()) {
        // burgerservicenummer wordt uit de dataTable gehaald want moet apart worden opgegeven
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
        index = this.context.sqlData.findIndex(i => i.persoon!=undefined && i.persoon[0].find(p => p[0]=='burger_service_nr')[1]==burgerservicenummer);
        
        createWoonadres(this.context, adresId, arrayOfArraysToDataTable(verblijfplaatsData), index);
    };
});
