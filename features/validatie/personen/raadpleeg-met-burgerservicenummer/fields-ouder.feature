#language: nl

@input-validatie
Functionaliteit: ouder velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat ouder veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                                     |
    | ouders                                                     |
    | ouders.burgerservicenummer                                 |
    | ouders.datumIngangFamilierechtelijkeBetrekking             |
    | ouders.datumIngangFamilierechtelijkeBetrekking.langFormaat |
    | ouders.datumIngangFamilierechtelijkeBetrekking.type        |
    | ouders.datumIngangFamilierechtelijkeBetrekking.datum       |
    | ouders.datumIngangFamilierechtelijkeBetrekking.onbekend    |
    | ouders.datumIngangFamilierechtelijkeBetrekking.jaar        |
    | ouders.datumIngangFamilierechtelijkeBetrekking.maand       |
    | ouders.geboorte                                            |
    | ouders.geboorte.datum                                      |
    | ouders.geboorte.datum.langFormaat                          |
    | ouders.geboorte.datum.type                                 |
    | ouders.geboorte.datum.datum                                |
    | ouders.geboorte.datum.onbekend                             |
    | ouders.geboorte.datum.jaar                                 |
    | ouders.geboorte.datum.maand                                |
    | ouders.geboorte.land                                       |
    | ouders.geboorte.land.code                                  |
    | ouders.geboorte.land.omschrijving                          |
    | ouders.geboorte.plaats                                     |
    | ouders.geboorte.plaats.code                                |
    | ouders.geboorte.plaats.omschrijving                        |
    | ouders.geslacht                                            |
    | ouders.geslacht.code                                       |
    | ouders.geslacht.omschrijving                               |
    | ouders.naam                                                |
    | ouders.naam.adellijkeTitelPredicaat                        |
    | ouders.naam.adellijkeTitelPredicaat.code                   |
    | ouders.naam.adellijkeTitelPredicaat.omschrijving           |
    | ouders.naam.adellijkeTitelPredicaat.soort                  |
    | ouders.naam.geslachtsnaam                                  |
    | ouders.naam.voorletters                                    |
    | ouders.naam.voornamen                                      |
    | ouders.naam.voorvoegsel                                    |
    | ouders.ouderAanduiding                                     |
