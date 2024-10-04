#language: nl

@input-validatie
Functionaliteit: kind velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat kind veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                             |
    | kinderen                                           |
    | kinderen.burgerservicenummer                       |
    | kinderen.geboorte                                  |
    | kinderen.geboorte.datum                            |
    | kinderen.geboorte.datum.langFormaat                |
    | kinderen.geboorte.datum.type                       |
    | kinderen.geboorte.datum.datum                      |
    | kinderen.geboorte.datum.onbekend                   |
    | kinderen.geboorte.datum.jaar                       |
    | kinderen.geboorte.datum.maand                      |
    | kinderen.geboorte.land                             |
    | kinderen.geboorte.land.code                        |
    | kinderen.geboorte.land.omschrijving                |
    | kinderen.geboorte.plaats                           |
    | kinderen.geboorte.plaats.code                      |
    | kinderen.geboorte.plaats.omschrijving              |
    | kinderen.naam                                      |
    | kinderen.naam.adellijkeTitelPredicaat              |
    | kinderen.naam.adellijkeTitelPredicaat.code         |
    | kinderen.naam.adellijkeTitelPredicaat.omschrijving |
    | kinderen.naam.adellijkeTitelPredicaat.soort        |
    | kinderen.naam.geslachtsnaam                        |
    | kinderen.naam.voorletters                          |
    | kinderen.naam.voornamen                            |
    | kinderen.naam.voorvoegsel                          |
