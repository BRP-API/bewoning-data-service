#language: nl

@protocollering
Functionaliteit: protocollering van leeftijd in gezag persoon beperkt 

  Regel: Met fields gevraagde velden worden geprotocolleerd als de elementnummers volgens Logisch ontwerp BRP
    Dit is een 6-cijferige code, met zo nodig voorloopnul voor categorieën.

    Abstract Scenario: Met fields vragen om <fields> wordt vastgelegd als gevraagde rubrieken <gevraagde rubrieken>
      Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
      | pl_id |
      | 1004  |
      En de response van de downstream api heeft de volgende headers
      | x-geleverde-pls |
      | 1004            |
      Als personen wordt gezocht met de volgende parameters
      | naam                             | waarde                                  |
      | type                             | ZoekMetAdresseerbaarObjectIdentificatie |
      | adresseerbaarObjectIdentificatie | 0599010051001502                        |
      | fields                           | <fields>                                |
      Dan heeft de persoon met burgerservicenummer '000000024' de volgende 'protocollering' gegevens
      | request_gevraagde_rubrieken |
      | <gevraagde rubrieken>       |

      Voorbeelden:
      | fields         | gevraagde rubrieken |
      | geboorte.datum | 010310              |
      | leeftijd       | PAGL01              |
