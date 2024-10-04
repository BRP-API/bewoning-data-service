#language: nl

@protocollering
Functionaliteit: Protocollering zoekrubrieken ZoekMetAdresseerbaarObjectIdentificatie

  Achtergrond:
    Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | pl_id |
    | 1001  |
    En de response van de downstream api heeft de volgende headers
    | x-geleverde-pls |
    | 1001            |

Regel: Gebruikte parameters worden vertaald naar elementnummers volgens Logisch ontwerp BRP en vastgelegd in het veld 'request_zoek_rubrieken'.

  Scenario: Zoek persoon met alleen de verplichte parameters
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0599010000219679                        |
    | fields                           | burgerservicenummer                     |
    Dan heeft de persoon met burgerservicenummer '000000024' de volgende 'protocollering' gegevens
    | request_zoek_rubrieken |
    | 081180                 |

  Scenario: Zoek persoon met parameter gemeenteVanInschrijving
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0599010000219679                        |
    | gemeenteVanInschrijving          | 0599                                    |
    | fields                           | burgerservicenummer                     |
    Dan heeft de persoon met burgerservicenummer '000000024' de volgende 'protocollering' gegevens
    | request_zoek_rubrieken |
    | 080910, 081180         |


Regel: Gebruik van de parameter inclusiefOverledenPersonen wordt niet vastgelegd in veld 'request_zoek_rubrieken'.

  Scenario: Zoek persoon met inclusiefOverledenPersonen
    Als personen wordt gezocht met de volgende parameters
    | naam                             | waarde                                  |
    | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
    | adresseerbaarObjectIdentificatie | 0599010000219679                        |
    | fields                           | burgerservicenummer                     |
    | inclusiefOverledenPersonen       | true                                    |
    Dan heeft de persoon met burgerservicenummer '000000024' de volgende 'protocollering' gegevens
    | request_zoek_rubrieken |
    | 081180                 |
