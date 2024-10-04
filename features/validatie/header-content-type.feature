#language: nl

@input-validatie
Functionaliteit: Content-Type header waarde bij BRP API bevragingen

  Als eigenaar van de BRP API
  wil ik dat de parameters voor het bevragen van de BRP API wordt gegeven als JSON en moet de parameter waarden vallen in de UTF-8 karakter set
  zodat de parameters zonder conversie kunnen worden gebruikt voor zoek akties

  Regel: De ondersteunde Content-Type content type en charset zijn respectievelijk alleen application/json en utf-8

    @fout-case
    Abstract Scenario: '<media type>' is opgegeven als Content-Type waarde
      Als <zoek/raadpleeg type> wordt gezocht met de volgende parameters
      | naam                 | waarde                   |
      | type                 | <waarde zoek type param> |
      | <naam 1e param>      | <waarde 1e param>        |
      | <naam 2e param>      | <waarde 2e param>        |
      | <naam 3e param>      | <waarde 3e param>        |
      | header: Content-Type | <media type>             |
      Dan heeft de response de volgende gegevens
      | naam     | waarde                                                       |
      | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.13 |
      | title    | Media Type wordt niet ondersteund.                           |
      | detail   | Ondersteunde content type: application/json; charset=utf-8.  |
      | code     | unsupportedMediaType                                         |
      | status   | 415                                                          |
      | instance | /haalcentraal/api<url deel>                                  |

      Voorbeelden:
      | media type                       | zoek/raadpleeg type    | waarde zoek type param          | naam 1e param       | waarde 1e param | naam 2e param | waarde 2e param | naam 3e param | waarde 3e param | url deel                            |
      | application/xml                  | personen               | RaadpleegMetBurgerservicenummer | burgerservicenummer | 000000012       | fields        | naam            |               |                 | /brp/personen                       |
      | text/csv                         | reisdocumenten         | RaadpleegMetReisdocumentnummer  | reisdocumentnummer  | AB1234567       | fields        | houder          |               |                 | /reisdocumenten/reisdocumenten      |
      | application/json; charset=cp1252 | verblijfplaatshistorie | RaadpleegMetPeildatum           | burgerservicenummer | 000000012       | peildatum     | 2020-04-01      |               |                 | /brphistorie/verblijfplaatshistorie |
      | */*                              | verblijfplaatshistorie | RaadpleegMetPeriode             | burgerservicenummer | 000000012       | datumVan      | 2020-04-01      | datumTot      | 2021-05-01      | /brphistorie/verblijfplaatshistorie |
      | */*; charset=utf-8               | personen               | RaadpleegMetBurgerservicenummer | burgerservicenummer | 000000012       | fields        | naam            |               |                 | /brp/personen                       |
      | */*;charset=utf-8                | reisdocumenten         | RaadpleegMetReisdocumentnummer  | reisdocumentnummer  | AB1234567       | fields        | houder          |               |                 | /reisdocumenten/reisdocumenten      |

    Abstract Scenario: '<media type>' is opgegeven als Content-Type waarde
      Als <zoek/raadpleeg type> wordt gezocht met de volgende parameters
      | naam                 | waarde                   |
      | type                 | <waarde zoek type param> |
      | <naam 1e param>      | <waarde 1e param>        |
      | <naam 2e param>      | <waarde 2e param>        |
      | <naam 3e param>      | <waarde 3e param>        |
      | header: Content-Type | <media type>             |
      Dan heeft de response 0 <response type>

      Voorbeelden:
      | media type                      | zoek/raadpleeg type    | waarde zoek type param              | naam 1e param       | waarde 1e param | naam 2e param | waarde 2e param | naam 3e param | waarde 3e param | response type    |
      | application/json                | personen               | RaadpleegMetBurgerservicenummer     | burgerservicenummer | 000000012       | fields        | naam            |               |                 | personen         |
      | application/json;charset=utf-8  | reisdocumenten         | RaadpleegMetReisdocumentnummer      | reisdocumentnummer  | AB1234567       | fields        | houder          |               |                 | reisdocumenten   |
      | application/json; charset=utf-8 | verblijfplaatshistorie | RaadpleegMetPeildatum               | burgerservicenummer | 000000012       | peildatum     | 2020-04-01      |               |                 | verblijfplaatsen |
      | application/json;charset=Utf-8  | verblijfplaatshistorie | RaadpleegMetPeriode                 | burgerservicenummer | 000000012       | datumVan      | 2020-04-01      | datumTot      | 2021-05-01      | verblijfplaatsen |
      | application/json; charset=UTF-8 | personen               | ZoekMetGeslachtsnaamEnGeboortedatum | geslachtsnaam       | brănduş-dendyuk | geboortedatum | 1983-05-26      | fields        | naam            | personen         |


  Regel: De default Content-Type waarde en charset zijn respectievelijk application/json en utf-8

    Abstract Scenario: Er is geen Content-Type header met waarde opgegeven
      Als <zoek/raadpleeg type> wordt gezocht met de volgende parameters
      | naam            | waarde                   |
      | type            | <waarde zoek type param> |
      | <naam 1e param> | <waarde 1e param>        |
      | <naam 2e param> | <waarde 2e param>        |
      | <naam 3e param> | <waarde 3e param>        |
      Dan heeft de response 0 <response type>

      Voorbeelden:
      | zoek/raadpleeg type    | waarde zoek type param          | naam 1e param       | waarde 1e param | naam 2e param | waarde 2e param | naam 3e param | waarde 3e param | response type    |
      | personen               | RaadpleegMetBurgerservicenummer | burgerservicenummer | 000000012       | fields        | naam            |               |                 | personen         |
      | reisdocumenten         | RaadpleegMetReisdocumentnummer  | reisdocumentnummer  | AB1234567       | fields        | houder          |               |                 | reisdocumenten   |
      | verblijfplaatshistorie | RaadpleegMetPeildatum           | burgerservicenummer | 000000012       | peildatum     | 2020-04-01      |               |                 | verblijfplaatsen |
      | verblijfplaatshistorie | RaadpleegMetPeriode             | burgerservicenummer | 000000012       | datumVan      | 2020-04-01      | datumTot      | 2021-05-01      | verblijfplaatsen |

    Abstract Scenario: Er is een lege waarde opgegeven voor de Content-Type header
      Als <zoek/raadpleeg type> wordt gezocht met de volgende parameters
      | naam                 | waarde                   |
      | type                 | <waarde zoek type param> |
      | <naam 1e param>      | <waarde 1e param>        |
      | <naam 2e param>      | <waarde 2e param>        |
      | <naam 3e param>      | <waarde 3e param>        |
      | header: Content-Type |                          |
      Dan heeft de response 0 <response type>

      Voorbeelden:
      | zoek/raadpleeg type    | waarde zoek type param          | naam 1e param       | waarde 1e param | naam 2e param | waarde 2e param | naam 3e param | waarde 3e param | response type    |
      | personen               | RaadpleegMetBurgerservicenummer | burgerservicenummer | 000000012       | fields        | naam            |               |                 | personen         |
      | reisdocumenten         | RaadpleegMetReisdocumentnummer  | reisdocumentnummer  | AB1234567       | fields        | houder          |               |                 | reisdocumenten   |
      | verblijfplaatshistorie | RaadpleegMetPeildatum           | burgerservicenummer | 000000012       | peildatum     | 2020-04-01      |               |                 | verblijfplaatsen |
      | verblijfplaatshistorie | RaadpleegMetPeriode             | burgerservicenummer | 000000012       | datumVan      | 2020-04-01      | datumTot      | 2021-05-01      | verblijfplaatsen |
