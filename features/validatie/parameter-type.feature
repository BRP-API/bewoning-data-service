#language: nl

@input-validatie
Functionaliteit: input validatie bij BRP API bevragingen

  Als eigenaar van de BRP API
  wil ik dat de input parameters worden gevalideerd op juistheid
  zodat ongeldige input niet kan leiden tot onjuiste gebruik en resultaten

  Regel: type is een verplichte parameter

    @fout-case
    Abstract Scenario: er zijn geen parameters opgegeven
      Als <zoek/raadpleeg type> wordt gezocht met de volgende parameters
      | naam | waarde |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
      | title    | Een of meerdere parameters zijn niet correct.               |
      | status   | 400                                                         |
      | detail   | De foutieve parameter(s) zijn: type.                        |
      | code     | paramsValidation                                            |
      | instance | /haalcentraal/api<url deel>                                 |
      En heeft de response invalidParams met de volgende gegevens
      | code     | name | reason                  |
      | required | type | Parameter is verplicht. |

      Voorbeelden:
      | zoek/raadpleeg type    | url deel                            |
      | personen               | /brp/personen                       |
      | reisdocumenten         | /reisdocumenten/reisdocumenten      |
      | verblijfplaatshistorie | /brphistorie/verblijfplaatshistorie |

    @fout-case
    Abstract Scenario: de 'type' parameter is niet opgegeven
      Als <zoek/raadpleeg type> wordt gezocht met de volgende parameters
      | naam             | waarde             |
      | <parameter naam> | <parameter waarde> |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
      | title    | Een of meerdere parameters zijn niet correct.               |
      | status   | 400                                                         |
      | detail   | De foutieve parameter(s) zijn: type.                        |
      | code     | paramsValidation                                            |
      | instance | /haalcentraal/api<url deel>                                 |
      En heeft de response invalidParams met de volgende gegevens
      | code     | name | reason                  |
      | required | type | Parameter is verplicht. |

      Voorbeelden:
      | zoek/raadpleeg type    | url deel                            | parameter naam      | parameter waarde |
      | personen               | /brp/personen                       | postcode            | 3077AW           |
      | reisdocumenten         | /reisdocumenten/reisdocumenten      | reisdocumentnummer  | NE3663258        |
      | verblijfplaatshistorie | /brphistorie/verblijfplaatshistorie | burgerservicenummer | 000000024        |

    @fout-case
    Abstract Scenario: de 'type' parameter bevat een onjuiste waarde
      Als <zoek/raadpleeg type> wordt gezocht met de volgende parameters
      | naam | waarde                  |
      | type | <waarde type parameter> |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                      |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1 |
      | title    | Een of meerdere parameters zijn niet correct.               |
      | status   | 400                                                         |
      | detail   | De foutieve parameter(s) zijn: type.                        |
      | code     | paramsValidation                                            |
      | instance | /haalcentraal/api<url deel>                                 |
      En heeft de response invalidParams met de volgende gegevens
      | code  | name | reason                           |
      | value | type | Waarde is geen geldig zoek type. |

      Voorbeelden:
      | zoek/raadpleeg type    | url deel                            | waarde type parameter               |
      | personen               | /brp/personen                       |                                     |
      | personen               | /brp/personen                       | zoekmetgeslachtsnaamengeboortedatum |
      | verblijfplaatshistorie | /brphistorie/verblijfplaatshistorie | raadpleegmetpeildatum               |
      | verblijfplaatshistorie | /brphistorie/verblijfplaatshistorie | RaadpleegMetPeriodes                |
      | verblijfplaatshistorie | /brphistorie/verblijfplaatshistorie | RaadpleegMetBurgerservicenummer     |
