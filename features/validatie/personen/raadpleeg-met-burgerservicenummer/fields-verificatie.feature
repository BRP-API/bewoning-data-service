#language: nl

@input-validatie
Functionaliteit: verificatie velden vragen met fields (persoon)

  Regel: verificatie mag niet worden gevraagd, omdat het automatisch wordt geleverd

    @fout-case
    Abstract Scenario: veld <fields> mag niet worden gevraagd, omdat het automatisch wordt geleverd
      Als personen wordt gezocht met de volgende parameters
      | naam                | waarde                          |
      | type                | RaadpleegMetBurgerservicenummer |
      | burgerservicenummer | 000000024                       |
      | fields              | <fields>                        |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
      | title    | Een of meerdere parameters zijn niet correct.               |
      | status   | 400                                                         |
      | detail   | De foutieve parameter(s) zijn: fields[0].                   |
      | code     | paramsValidation                                            |
      | instance | /haalcentraal/api/brp/personen                              |
      En heeft de response invalidParams met de volgende gegevens
      | code   | name      | reason                                        |
      | fields | fields[0] | Parameter bevat een niet toegestane veldnaam. |

      Voorbeelden:
      | fields                        |
      | verificatie                   |
      | verificatie.datum             |
      | verificatie.datum.type        |
      | verificatie.datum.datum       |
      | verificatie.datum.jaar        |
      | verificatie.datum.maand       |
      | verificatie.datum.onbekend    |
      | verificatie.datum.langFormaat |
      | verificatie.omschrijving      |
