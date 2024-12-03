#language: nl

@api
Functionaliteit: raadpleeg bewoning levert naam bewoner

Als provider van de bewoning informatie service
wil ik dat de naam en de geslachtsaanduiding van de bewoner wordt geleverd
zodat ik de volledige naam van de bewoner kan leveren aan de consumer van de Bewoning API

  Achtergrond:
  Gegeven adres 'A1' heeft de volgende gegevens
  | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
  | 0800                 | 0800000000000001                         |

Regel: Naam van bewoner wordt geleverd bij raadplegen van bewoning

  Scenario: naam van bewoner wordt geleverd bij het raadplegen van bewoning met periode
    Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | voornamen (02.10) | voorvoegsel (02.30) | geslachtsnaam (02.40) |
    | Robin Sam         | van den             | Aedel                 |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2010-09-01         |
    | datumTot                         | 2014-08-01         |
    | adresseerbaarObjectIdentificatie | 0800000000000001   |
    Dan heeft de response een bewoning met de volgende gegevens
    | naam                             | waarde                    |
    | periode                          | 2010-09-01 tot 2014-08-01 |
    | adresseerbaarObjectIdentificatie | 0800000000000001          |
    En heeft de bewoning een bewoner met de volgende gegevens
    | burgerservicenummer |
    | 000000032           |
    En heeft de bewoner de volgende 'naam' gegevens
    | naam                                 | waarde    |
    | voornamen                            | Robin Sam |
    | voorvoegsel                          | van den   |
    | geslachtsnaam                        | Aedel     |

  Scenario: naam van bewoner wordt geleverd bij het raadplegen van bewoning met peildatum
    Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | voornamen (02.10) | voorvoegsel (02.30) | geslachtsnaam (02.40) |
    | Robin Sam         | van den             | Aedel                 |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde               |
    | type                             | BewoningMetPeildatum |
    | peildatum                        | 2010-09-01           |
    | adresseerbaarObjectIdentificatie | 0800000000000001     |
    Dan heeft de response een bewoning met de volgende gegevens
    | naam                             | waarde                    |
    | periode                          | 2010-09-01 tot 2010-09-02 |
    | adresseerbaarObjectIdentificatie | 0800000000000001          |
    En heeft de bewoning een bewoner met de volgende gegevens
    | burgerservicenummer |
    | 000000024           |
    En heeft de bewoner de volgende 'naam' gegevens
    | naam                                 | waarde    |
    | voornamen                            | Robin Sam |
    | voorvoegsel                          | van den   |
    | geslachtsnaam                        | Aedel     |

Regel: Naam van mogelijke bewoner wordt geleverd bij raadplegen van bewoning in onzekerheidsperiode

  Scenario: naam van mogelijke bewoner wordt geleverd bij het raadplegen van bewoning met periode in onzekerheidsperiode
    Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | voornamen (02.10) | voorvoegsel (02.30) | geslachtsnaam (02.40) |
    | Robin Sam         | van den             | Aedel                 |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100000                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2010-09-01         |
    | datumTot                         | 2011-01-01         |
    | adresseerbaarObjectIdentificatie | 0800000000000001   |
    En heeft de response een bewoning met de volgende gegevens
    | naam                             | waarde                    |
    | periode                          | 2010-09-01 tot 2011-01-01 |
    | adresseerbaarObjectIdentificatie | 0800000000000001          |
    En heeft de bewoning een mogelijke bewoner met de volgende gegevens
    | burgerservicenummer | naam.voornamen | naam.voorvoegsel | naam.geslachtsnaam |
    | 000000024           | Robin Sam      | van den          | Aedel              |

  Scenario: naam van mogelijke bewoner wordt geleverd bij het raadplegen van bewoning met peildatum in onzekerheidsperiode
    Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | voornamen (02.10) | voorvoegsel (02.30) | geslachtsnaam (02.40) |
    | Robin Sam         | van den             | Aedel                 |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100000                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde               |
    | type                             | BewoningMetPeildatum |
    | peildatum                        | 2010-09-01           |
    | adresseerbaarObjectIdentificatie | 0800000000000001     |
    En heeft de response een bewoning met de volgende gegevens
    | naam                             | waarde                    |
    | periode                          | 2010-09-01 tot 2010-09-02 |
    | adresseerbaarObjectIdentificatie | 0800000000000001          |
    En heeft de bewoning een mogelijke bewoner met de volgende gegevens
    | burgerservicenummer | naam.voornamen | naam.voorvoegsel | naam.geslachtsnaam | 
    | 000000024           | Robin Sam      | van den          | Aedel              |

Regel: Als een (mogelijke) bewoner ook voornamen en/of geslachtsnaam met diakrieten heeft, dan wordt deze geleverd
    - als voornamen/geslachtsnaam diakrieten bevat zijn beide gevuld. De velden bevatten verschillende waarden.
    - als voornamen/geslachtsnaam geen diakrieten bevat is alleen voornamen (02.10)/geslachtsnaam (02.40) gevuld.

  Abstract Scenario: voornamen met of zonder diakrieten van bewoner wordt geleverd bij raadplegen van bewoning met periode
    Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | naam                      | waarde               |
    | voornamen (02.10)         | <voornamen>          |
    | voornamen (diakrieten)    | <voornamen_diak>     |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2010-09-01         |
    | datumTot                         | 2014-08-01         |
    | adresseerbaarObjectIdentificatie | 0800000000000001   |
    Dan heeft de response een bewoning met de volgende gegevens
    | naam                             | waarde                    |
    | periode                          | 2010-09-01 tot 2014-08-01 |
    | adresseerbaarObjectIdentificatie | 0800000000000001          |
    En heeft de bewoning een bewoner met de volgende gegevens
    | burgerservicenummer |
    | 000000024           |
    En heeft de bewoner de volgende 'naam' gegevens
    | naam          | waarde                |
    | voornamen     | <voornamen_resultaat> |

    Voorbeelden:
    | voornamen               | voornamen_diak         | voornamen_resultaat      | omschrijving                 |
    | Zailenor Aleez Delta    | Żáïŀëñøŕ Åłéèç Đëļŧå   | Żáïŀëñøŕ Åłéèç Đëļŧå     | voornamen met diakrieten     |
    | Robin Sam               |                        | Robin Sam                | voornamen zonder diakrieten  |

  Abstract Scenario: geslachtsnaam met en zonder diakrieten van bewoner wordt geleverd bij raadplegen van bewoning met periode
    Gegeven de persoon met burgerservicenummer '000000024' heeft de volgende gegevens
    | naam                        | waarde               |
    | geslachtsnaam (02.40)       | <geslachtsnaam>      |
    | geslachtsnaam (diakrieten)  | <geslachtsnaam_diak> |
    En de persoon is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2010-09-01         |
    | datumTot                         | 2014-08-01         |
    | adresseerbaarObjectIdentificatie | 0800000000000001   |
    Dan heeft de response een bewoning met de volgende gegevens
    | naam                             | waarde                    |
    | periode                          | 2010-09-01 tot 2014-08-01 |
    | adresseerbaarObjectIdentificatie | 0800000000000001          |
    En heeft de bewoning een bewoner met de volgende gegevens
    | burgerservicenummer |
    | 000000024           |
    En heeft de bewoner de volgende 'naam' gegevens
    | naam          | waarde                    |
    | geslachtsnaam | <geslachtsnaam_resultaat> |

    Voorbeelden:
    | geslachtsnaam           | geslachtsnaam_diak     | geslachtsnaam_resultaat  | omschrijving                     |
    | Zailenor Aleez Delta    | Żáïŀëñøŕ Åłéèç Đëļŧå   | Żáïŀëñøŕ Åłéèç Đëļŧå     | geslachtsnaam met diakrieten     |
    | Robin Sam               |                        | Robin Sam                | geslachtsnaam zonder diakrieten  |