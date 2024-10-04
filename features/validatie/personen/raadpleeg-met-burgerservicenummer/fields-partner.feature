#language: nl

@input-validatie
Functionaliteit: partner velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat partner veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                                    |
    | partners                                                  |
    | partners.aangaanHuwelijkPartnerschap                      |
    | partners.aangaanHuwelijkPartnerschap.datum                |
    | partners.aangaanHuwelijkPartnerschap.datum.langFormaat    |
    | partners.aangaanHuwelijkPartnerschap.datum.type           |
    | partners.aangaanHuwelijkPartnerschap.datum.datum          |
    | partners.aangaanHuwelijkPartnerschap.datum.onbekend       |
    | partners.aangaanHuwelijkPartnerschap.datum.jaar           |
    | partners.aangaanHuwelijkPartnerschap.datum.maand          |
    | partners.aangaanHuwelijkPartnerschap.land                 |
    | partners.aangaanHuwelijkPartnerschap.land.code            |
    | partners.aangaanHuwelijkPartnerschap.land.omschrijving    |
    | partners.aangaanHuwelijkPartnerschap.plaats               |
    | partners.aangaanHuwelijkPartnerschap.plaats.code          |
    | partners.aangaanHuwelijkPartnerschap.plaats.omschrijving  |
    | partners.burgerservicenummer                              |
    | partners.geboorte                                         |
    | partners.geboorte.datum                                   |
    | partners.geboorte.datum.langFormaat                       |
    | partners.geboorte.datum.type                              |
    | partners.geboorte.datum.datum                             |
    | partners.geboorte.datum.onbekend                          |
    | partners.geboorte.datum.jaar                              |
    | partners.geboorte.datum.maand                             |
    | partners.geboorte.land                                    |
    | partners.geboorte.land.code                               |
    | partners.geboorte.land.omschrijving                       |
    | partners.geboorte.plaats                                  |
    | partners.geboorte.plaats.code                             |
    | partners.geboorte.plaats.omschrijving                     |
    | partners.geslacht                                         |
    | partners.geslacht.code                                    |
    | partners.geslacht.omschrijving                            |
    | partners.naam                                             |
    | partners.naam.adellijkeTitelPredicaat                     |
    | partners.naam.adellijkeTitelPredicaat.code                |
    | partners.naam.adellijkeTitelPredicaat.omschrijving        |
    | partners.naam.adellijkeTitelPredicaat.soort               |
    | partners.naam.geslachtsnaam                               |
    | partners.naam.voorletters                                 |
    | partners.naam.voornamen                                   |
    | partners.naam.voorvoegsel                                 |
    | partners.ontbindingHuwelijkPartnerschap                   |
    | partners.ontbindingHuwelijkPartnerschap.datum             |
    | partners.ontbindingHuwelijkPartnerschap.datum.langFormaat |
    | partners.ontbindingHuwelijkPartnerschap.datum.type        |
    | partners.ontbindingHuwelijkPartnerschap.datum.datum       |
    | partners.ontbindingHuwelijkPartnerschap.datum.onbekend    |
    | partners.ontbindingHuwelijkPartnerschap.datum.jaar        |
    | partners.ontbindingHuwelijkPartnerschap.datum.maand       |
    | partners.soortVerbintenis                                 |
    | partners.soortVerbintenis.code                            |
    | partners.soortVerbintenis.omschrijving                    |
