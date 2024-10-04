#language: nl

@input-validatie
Functionaliteit: immigratie velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat immigratie veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                           |
    | immigratie                                       |
    | immigratie.datumVestigingInNederland             |
    | immigratie.datumVestigingInNederland.langFormaat |
    | immigratie.datumVestigingInNederland.type        |
    | immigratie.datumVestigingInNederland.datum       |
    | immigratie.datumVestigingInNederland.onbekend    |
    | immigratie.datumVestigingInNederland.jaar        |
    | immigratie.datumVestigingInNederland.maand       |
    | immigratie.indicatieVestigingVanuitBuitenland    |
    | immigratie.landVanwaarIngeschreven               |
    | immigratie.landVanwaarIngeschreven.code          |
    | immigratie.landVanwaarIngeschreven.omschrijving  |
    | immigratie.vanuitVerblijfplaatsOnbekend          |
