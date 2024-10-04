#language: nl

@input-validatie
Functionaliteit: datumEersteInschrijvingGBA veld vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat datumEersteInschrijvingGBA veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                 |
    | datumEersteInschrijvingGBA             |
    | datumEersteInschrijvingGBA.langFormaat |
    | datumEersteInschrijvingGBA.type        |
    | datumEersteInschrijvingGBA.datum       |
    | datumEersteInschrijvingGBA.onbekend    |
    | datumEersteInschrijvingGBA.jaar        |
    | datumEersteInschrijvingGBA.maand       |
