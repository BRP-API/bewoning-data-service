#language: nl

@input-validatie
Functionaliteit: nationaliteit velden vragen met fields bij raadplegen met burgerservicenummer

  Abstract Scenario: de fields parameter bevat nationaliteit veld(en): <fields>
    Als personen wordt gezocht met de volgende parameters
    | naam                | waarde                          |
    | type                | RaadpleegMetBurgerservicenummer |
    | burgerservicenummer | 000000048                       |
    | fields              | <fields>                        |
    Dan heeft de response 0 personen

    Voorbeelden:
    | fields                                            |
    | nationaliteiten                                   |
    | nationaliteiten.redenOpname                       |
    | nationaliteiten.redenOpname.code                  |
    | nationaliteiten.redenOpname.omschrijving          |
    | nationaliteiten.type                              |
    | nationaliteiten.datumIngangGeldigheid             |
    | nationaliteiten.datumIngangGeldigheid.langFormaat |
    | nationaliteiten.datumIngangGeldigheid.type        |
    | nationaliteiten.datumIngangGeldigheid.datum       |
    | nationaliteiten.datumIngangGeldigheid.onbekend    |
    | nationaliteiten.datumIngangGeldigheid.jaar        |
    | nationaliteiten.datumIngangGeldigheid.maand       |
    | nationaliteiten.nationaliteit                     |
    | nationaliteiten.nationaliteit.code                |
    | nationaliteiten.nationaliteit.omschrijving        |
