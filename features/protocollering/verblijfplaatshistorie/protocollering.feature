#language: nl

@protocollering
Functionaliteit: Protocolleren van raadplegen van verblijfplaatshistorie
  Zoeken en raadplegen van gegevens van burgers worden "geprotocolleerd" (formeel gelogd met als doel de burger te informeren 
  over welke instantie voor welke taak welke gegevens heeft geraadpleegd).

  Bij protocollering wordt voor elk request in 'request_zoek_rubrieken' vastgelegd welke parameters zijn gebruikt
  en in 'request_gevraagde_rubrieken' vastgelegd welke gegevens daarbij zijn gevraagd.
  Zowel bij verblijfplaatshistorie op peildatum als bij verblijfplaatshistorie op periode worden verblijfplaatsen gezocht met 
  burgerservicenummer (01.01.20) plus actuele en historische datum aanvang (08.10.30, 08.13.20, 58.10.30, 58.13.20).
  Aangezien verblijfplaatshistorie altijd in zijn geheel wordt gevraagd -er is geen parameter fields- wordt verblijfplaatshistorie
  als één rubriek opgenomen in 'request_gevraagde_rubrieken': PX.VP.07.

    Achtergrond:
      Gegeven de geauthenticeerde consumer heeft de volgende 'claim' gegevens
      | afnemerID | gemeenteCode |
      | 000008    | 0800         |
      En de persoon met burgerservicenummer '000000012' heeft de volgende gegevens
      | pl_id |
      | 1001  |
      En de response van de downstream api heeft de volgende headers
      | x-geleverde-pls |
      | 1001            |
    
  
  Regel: Verblijfplaatshistorie wordt geprotocolleerd

    Scenario: Raadpleeg verblijfplaatshistorie met peildatum
      Als verblijfplaatshistorie wordt gezocht met de volgende parameters
      | naam                | waarde                |
      | type                | RaadpleegMetPeildatum |
      | burgerservicenummer | 000000012             |
      | peildatum           | 2023-05-26            |
      Dan heeft de persoon met burgerservicenummer '000000012' de volgende 'protocollering' gegevens
      | request_zoek_rubrieken                 | request_gevraagde_rubrieken |
      | 010120, 081030, 081320, 581030, 581320 | PXVP07                      |

    Scenario: Raadpleeg verblijfplaatshistorie met periode
      Als verblijfplaatshistorie wordt gezocht met de volgende parameters
      | naam                | waarde              |
      | type                | RaadpleegMetPeriode |
      | burgerservicenummer | 000000012           |
      | datumVan            | 2023-01-01          |
      | datumTot            | 2024-01-01          |
      Dan heeft de persoon met burgerservicenummer '000000012' de volgende 'protocollering' gegevens
      | request_zoek_rubrieken                 | request_gevraagde_rubrieken |
      | 010120, 081030, 081320, 581030, 581320 | PXVP07                      |
