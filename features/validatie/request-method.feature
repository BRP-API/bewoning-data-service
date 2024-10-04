#language: nl

@input-validatie
Functionaliteit: BRP API bevragingen met behulp van de HTTP POST aanroep

  Als eigenaar van de BRP API
  wil ik dat bevragingen op de BRP API worden gedaan met behulp van de HTTP POST methode
  zodat de BRP API urls geen persoonsgegevens kunnen bevatten

  @fout-case
  Abstract Scenario: '<zoek/raadpleeg type>' wordt geraadpleegd/gezocht met behulp van een '<aanroep type>' aanroep
    Als <zoek/raadpleeg type> wordt gezocht met een '<aanroep type>' aanroep
    Dan heeft de response de volgende gegevens
    | naam     | waarde                                                      |
    | type     | https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.5 |
    | title    | Gebruikte bevragingsmethode is niet toegestaan.             |
    | status   | 405                                                         |
    | code     | methodNotAllowed                                            |
    | instance | /haalcentraal/api<url deel>                                 |

    Voorbeelden:
    | aanroep type | zoek/raadpleeg type    | url deel                            |
    | GET          | personen               | /brp/personen                       |
    | PUT          | reisdocumenten         | /reisdocumenten/reisdocumenten      |
    | PATCH        | verblijfplaatshistorie | /brphistorie/verblijfplaatshistorie |
    | DELETE       | bewoningen             | /bewoning/bewoningen                |
    | OPTIONS      | personen               | /brp/personen                       |
    | TRACE        | reisdocumenten         | /reisdocumenten/reisdocumenten      |
