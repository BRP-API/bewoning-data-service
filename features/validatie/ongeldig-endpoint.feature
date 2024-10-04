#language: nl

@input-validatie
Functionaliteit: correcte afhandeling bij ongeldige BRP API bevragingen

  @fout-case
  Abstract Scenario: een niet-bestaand endpoint wordt aangeroepen
    Als <response type> wordt gezocht met de volgende parameters
    | naam | waarde                          |
    | type | RaadpleegMetBurgerservicenummer |
    Dan heeft de response de volgende gegevens
    | naam     | waarde                                                      |
    | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4 |
    | title    | Opgevraagde resource bestaat niet.                          |
    | status   | 404                                                         |
    | instance | /haalcentraal/api<url deel>                                 |

    Voorbeelden:
    | response type    | url deel                      |
    | ingezetenen      | /brp/ingezetenen              |
    | paspoorten       | /reisdocumenten/paspoorten    |
    | verblijfhistorie | /brphistorie/verblijfhistorie |
