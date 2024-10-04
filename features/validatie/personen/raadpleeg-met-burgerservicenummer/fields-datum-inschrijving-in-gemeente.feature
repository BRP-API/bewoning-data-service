#language: nl

@input-validatie
Functionaliteit: datumInschrijvinginGemeente veld vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat datumInschrijvingInGemeente veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                  |
    | datumInschrijvingInGemeente             |
    | datumInschrijvingInGemeente.langFormaat |
    | datumInschrijvingInGemeente.type        |
    | datumInschrijvingInGemeente.datum       |
    | datumInschrijvingInGemeente.onbekend    |
    | datumInschrijvingInGemeente.jaar        |
    | datumInschrijvingInGemeente.maand       |
