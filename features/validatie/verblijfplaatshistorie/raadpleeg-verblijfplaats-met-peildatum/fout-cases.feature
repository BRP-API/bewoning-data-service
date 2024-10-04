#language: nl

@input-validatie
Functionaliteit: input validatie bij raadplegen verblijfplaats historie met peildatum

  Regel: burgerservicenummer is een verplichte parameter

    @fout-case
    Scenario: De burgerservicenummer parameter is niet opgegeven
      Als verblijfplaatshistorie wordt gezocht met de volgende parameters
      | naam      | waarde                |
      | type      | RaadpleegMetPeildatum |
      | peildatum | 2020-01-01            |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
      | title    | Een of meerdere parameters zijn niet correct.               |
      | status   | 400                                                         |
      | detail   | De foutieve parameter(s) zijn: burgerservicenummer.         |
      | code     | paramsValidation                                            |
      | instance | /haalcentraal/api/brphistorie/verblijfplaatshistorie        |
      En heeft de response invalidParams met de volgende gegevens
      | code     | name                | reason                  |
      | required | burgerservicenummer | Parameter is verplicht. |

  Regel: Een burgerservicenummer is een string bestaande uit exact 9 cijfers

    @fout-case
    Scenario: De burgerservicenummer parameter is leeg
      Als verblijfplaatshistorie wordt gezocht met de volgende parameters
      | naam                | waarde                |
      | type                | RaadpleegMetPeildatum |
      | burgerservicenummer |                       |
      | peildatum           | 2020-01-01            |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
      | title    | Een of meerdere parameters zijn niet correct.               |
      | status   | 400                                                         |
      | detail   | De foutieve parameter(s) zijn: burgerservicenummer.         |
      | code     | paramsValidation                                            |
      | instance | /haalcentraal/api/brphistorie/verblijfplaatshistorie        |
      En heeft de response invalidParams met de volgende gegevens
      | code     | name                | reason                  |
      | required | burgerservicenummer | Parameter is verplicht. |

    @fout-case
    Abstract Scenario: De burgerservicenummer parameter heeft een ongeldige waarde
      Als verblijfplaatshistorie wordt gezocht met de volgende parameters
      | naam                | waarde                |
      | type                | RaadpleegMetPeildatum |
      | burgerservicenummer | <burgerservicenummer> |
      | peildatum           | 2020-01-01            |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
      | title    | Een of meerdere parameters zijn niet correct.               |
      | status   | 400                                                         |
      | detail   | De foutieve parameter(s) zijn: burgerservicenummer.         |
      | code     | paramsValidation                                            |
      | instance | /haalcentraal/api/brphistorie/verblijfplaatshistorie        |
      En heeft de response invalidParams met de volgende gegevens
      | code    | name                | reason                                      |
      | pattern | burgerservicenummer | Waarde voldoet niet aan patroon ^[0-9]{9}$. |

      Voorbeelden:
      | burgerservicenummer |
      | 12345678            |
      | 1234567890          |
      | A                   |
      | 12345678A           |
      | 123456789A          |
      | 1234 6789           |
      | 123456789+          |
      | 1234.67890          |


  Regel: peildatum is een verplichte parameter in RFC 3339 (yyyy-mm-dd) formaat

    @fout-case
    Scenario: De peildatum parameter is niet opgegeven
      Als verblijfplaatshistorie wordt gezocht met de volgende parameters
      | naam                | waarde                |
      | type                | RaadpleegMetPeildatum |
      | burgerservicenummer | 000000012             |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
      | title    | Een of meerdere parameters zijn niet correct.               |
      | status   | 400                                                         |
      | detail   | De foutieve parameter(s) zijn: peildatum.                   |
      | code     | paramsValidation                                            |
      | instance | /haalcentraal/api/brphistorie/verblijfplaatshistorie        |
      En heeft de response invalidParams met de volgende gegevens
      | code     | name      | reason                  |
      | required | peildatum | Parameter is verplicht. |

    @fout-case
    Scenario: De peildatum parameter is leeg
      Als verblijfplaatshistorie wordt gezocht met de volgende parameters
      | naam                | waarde                |
      | type                | RaadpleegMetPeildatum |
      | burgerservicenummer | 000000012             |
      | peildatum           |                       |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
      | title    | Een of meerdere parameters zijn niet correct.               |
      | status   | 400                                                         |
      | detail   | De foutieve parameter(s) zijn: peildatum.                   |
      | code     | paramsValidation                                            |
      | instance | /haalcentraal/api/brphistorie/verblijfplaatshistorie        |
      En heeft de response invalidParams met de volgende gegevens
      | code     | name      | reason                  |
      | required | peildatum | Parameter is verplicht. |

    @fout-case
    Abstract Scenario: De peildatum parameter heeft een ongeldige waarde
      Als verblijfplaatshistorie wordt gezocht met de volgende parameters
      | naam                | waarde                |
      | type                | RaadpleegMetPeildatum |
      | burgerservicenummer | 000000012             |
      | peildatum           | <peildatum>           |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
      | title    | Een of meerdere parameters zijn niet correct.               |
      | status   | 400                                                         |
      | detail   | De foutieve parameter(s) zijn: peildatum.                   |
      | code     | paramsValidation                                            |
      | instance | /haalcentraal/api/brphistorie/verblijfplaatshistorie        |
      En heeft de response invalidParams met de volgende gegevens
      | code | name      | reason                        |
      | date | peildatum | Waarde is geen geldige datum. |

      Voorbeelden:
      | peildatum      |
      | 20190101       |
      | 01/02/2019     |
      | 1 januari 2019 |
      | 2019-1-1       |
      | 2019-01-001    |
      | 2019-01-01-    |
      | -2019-01-01    |
      | 2019-13-01     |
      | 2019-06-31     |
      | 2019-02-29     |
      | 2021-00-00     |
      | 0000-00-00     |
      | --             |
      | - -            |

  
  Regel: Alleen gespecificeerde parameters bij het opgegeven raadpleeg type mogen worden gebruikt

    @fout-case
    Abstract Scenario: De vraag bevat een niet-gespecificeerde parameter
      Als verblijfplaatshistorie wordt gezocht met de volgende parameters
      | naam                | waarde                |
      | type                | RaadpleegMetPeildatum |
      | burgerservicenummer | 000000012             |
      | peildatum           | 2020-01-01            |
      | <parameter>         | 2019-01-01            |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
      | title    | Een of meerdere parameters zijn niet correct.               |
      | status   | 400                                                         |
      | detail   | De foutieve parameter(s) zijn: <parameter>.                 |
      | code     | paramsValidation                                            |
      | instance | /haalcentraal/api/brphistorie/verblijfplaatshistorie        |
      En heeft de response invalidParams met de volgende gegevens
      | code         | name        | reason                      |
      | unknownParam | <parameter> | Parameter is niet verwacht. |

      Voorbeelden:
      | parameter                         | waarde              |
      | datumVan                          | 2019-01-01          |
      | datumTot                          | 2020-12-31          |
      | iets                              | geks                |
      | gemeenteVanInschrijving           | 0530                |
      | geheimhouding                     | true                |
      | fields                            | burgerservicenummer |
      | adresseerbaarObjectIdentificatie  | 0800010000000001    |
      | exclusiefVerblijfplaatsBuitenland | true                |

    @fout-case
    Scenario: De vraag bevat meerdere niet-gespecificeerde parameters
      Als verblijfplaatshistorie wordt gezocht met de volgende parameters
      | naam                | waarde                |
      | type                | RaadpleegMetPeildatum |
      | burgerservicenummer | 000000012             |
      | peildatum           | 2020-01-01            |
      | datumVan            | 2020-01-01            |
      | datumTot            | 2020-01-02            |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
      | title    | Een of meerdere parameters zijn niet correct.               |
      | status   | 400                                                         |
      | detail   | De foutieve parameter(s) zijn: datumVan, datumTot.          |
      | code     | paramsValidation                                            |
      | instance | /haalcentraal/api/brphistorie/verblijfplaatshistorie        |
      En heeft de response invalidParams met de volgende gegevens
      | code         | name     | reason                      |
      | unknownParam | datumVan | Parameter is niet verwacht. |
      | unknownParam | datumTot | Parameter is niet verwacht. |
