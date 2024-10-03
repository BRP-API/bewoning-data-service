# language: nl

Functionaliteit: leveren van de gemeentecode(s) van het gevraagde adresseerbaar object in de gevraagde periode

  Als autorisatie & protocollering service
  wil ik de gemeentecode(s) van het gevraagde adresseerbaar object in de gevraagde periode ontvangen in een response header
  zodat ik kan bepalen of de afnemer is geautoriseerd om de bewoning van het adresseerbaar object te bevragen

  Scenario: gevraagde periode ligt in een inschrijving op het adresseerbaar object
    Gegeven adres 'A1' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0800                 | 0800010000000001                         |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2022-01-01         |
    | datumTot                         | 2023-01-01         |
    | adresseerbaarObjectIdentificatie | 0800010000000001   |
    Dan heeft de response de volgende headers
    | naam                      | waarde |
    | x-geleverde-gemeentecodes | 0800   |

  Scenario: gevraagde periode ligt in meerdere inschrijving op het adresseerbaar object
    Gegeven adres 'A1' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0800                 | 0800010000000001                         |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20200212                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2022-01-01         |
    | datumTot                         | 2023-01-01         |
    | adresseerbaarObjectIdentificatie | 0800010000000001   |
    Dan heeft de response de volgende headers
    | naam                      | waarde |
    | x-geleverde-gemeentecodes | 0800   |

  Scenario: geen inschrijvingen op het adresseerbaar object in de gevraagde periode
    Gegeven adres 'A1' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0800                 | 0800010000000001                         |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20200212                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2009-01-01         |
    | datumTot                         | 2010-01-01         |
    | adresseerbaarObjectIdentificatie | 0800010000000001   |
    Dan heeft de response de volgende headers
    | naam                      | waarde |
    | x-geleverde-gemeentecodes |        |
    En heeft de response 0 bewoningen

  Scenario: geen inschrijvingen meer op het adresseerbaar object in de gevraagde periode
    Gegeven adres 'A1' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0800                 | 0800010000000001                         |
    En adres 'A2' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0518                 | 0518010000000002                         |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0518                              | 20200114                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2021-01-01         |
    | datumTot                         | 2022-01-01         |
    | adresseerbaarObjectIdentificatie | 0800010000000001   |
    Dan heeft de response de volgende headers
    | naam                      | waarde |
    | x-geleverde-gemeentecodes |        |
    En heeft de response 0 bewoningen

  Scenario: gevraagde periode ligt deels in een inschrijving op het adresseerbaar object
    Gegeven adres 'A1' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0800                 | 0800010000000001                         |
    En adres 'A2' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0518                 | 0518010000000002                         |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0518                              | 20200114                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2019-01-01         |
    | datumTot                         | 2022-01-01         |
    | adresseerbaarObjectIdentificatie | 0800010000000001   |
    Dan heeft de response de volgende headers
    | naam                      | waarde |
    | x-geleverde-gemeentecodes | 0800   |

  Scenario: gevraagde periode ligt geheel/deels in meerdere inschrijvingen op het adresseerbaar object
    Gegeven adres 'A1' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0800                 | 0800010000000001                         |
    En adres 'A2' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0518                 | 0518010000000002                         |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0518                              | 20200114                           |
    En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20200212                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2019-01-01         |
    | datumTot                         | 2022-01-01         |
    | adresseerbaarObjectIdentificatie | 0800010000000001   |
    Dan heeft de response de volgende headers
    | naam                      | waarde |
    | x-geleverde-gemeentecodes | 0800   |

  Scenario: gevraagde periode ligt in de onzekerheidsperiode van een inschrijving op het adresseerbaar object
    Gegeven adres 'A1' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0800                 | 0800010000000001                         |
    En adres 'A2' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0518                 | 0518010000000002                         |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A2' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    En de persoon is vervolgens ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0518                              | 20200000                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2020-04-01         |
    | datumTot                         | 2020-10-01         |
    | adresseerbaarObjectIdentificatie | 0800010000000001   |
    Dan heeft de response de volgende headers
    | naam                      | waarde |
    | x-geleverde-gemeentecodes | 0800   |

  Scenario: gevraagde periode ligt in de onzekerheidsperiode van een volgende inschrijving
    Gegeven adres 'A1' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0800                 | 0800010000000001                         |
    En adres 'A2' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0518                 | 0518010000000002                         |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A1' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0518                              | 20200000                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2020-04-01         |
    | datumTot                         | 2020-10-01         |
    | adresseerbaarObjectIdentificatie | 0800010000000001   |
    Dan heeft de response de volgende headers
    | naam                      | waarde |
    | x-geleverde-gemeentecodes | 0800   |

  Scenario: het gevraagde adresseerbaar object is door herindeling in een andere gemeente komen te liggen en de gevraagde periode ligt ná de herindeling
    Gegeven adres 'A3' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0530                 | 0530010000000003                         |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A3' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0530                              | 20100818                           |
    En adres 'A3' is op '2023-05-26' infrastructureel gewijzigd met de volgende gegevens
    | gemeentecode (92.10) |
    | 0800                 |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2023-07-01         |
    | datumTot                         | 2023-08-01         |
    | adresseerbaarObjectIdentificatie | 0530010000000003   |
    Dan heeft de response de volgende headers
    | naam                      | waarde |
    | x-geleverde-gemeentecodes | 0800   |

  Scenario: het gevraagde adresseerbaar object is door herindeling in een andere gemeente komen te liggen en de gevraagde periode ligt vóór de herindeling
    Gegeven adres 'A3' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0530                 | 0530010000000003                         |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A3' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0530                              | 20100818                           |
    En adres 'A3' is op '2023-05-26' infrastructureel gewijzigd met de volgende gegevens
    | gemeentecode (92.10) |
    | 0800                 |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2022-01-01         |
    | datumTot                         | 2023-01-01         |
    | adresseerbaarObjectIdentificatie | 0530010000000003   |
    Dan heeft de response de volgende headers
    | naam                      | waarde |
    | x-geleverde-gemeentecodes | 0533   |

  Scenario: het gevraagde adresseerbaar object is door herindeling in een andere gemeente komen te liggen en de gevraagde periode ligt vóór de herindeling
    Gegeven adres 'A3' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0530                 | 0530010000000003                         |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A3' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0530                              | 20100818                           |
    En adres 'A3' is op '2023-05-26' infrastructureel gewijzigd met de volgende gegevens
    | gemeentecode (92.10) |
    | 0800                 |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2022-01-01         |
    | datumTot                         | 2023-01-01         |
    | adresseerbaarObjectIdentificatie | 0530010000000003   |
    Dan heeft de response de volgende headers
    | naam                      | waarde |
    | x-geleverde-gemeentecodes | 0533   |

  Scenario: het gevraagde adresseerbaar object is door herindeling in een andere gemeente komen te liggen en de herindeling vindt plaats binnen de gevraagde periode
    Gegeven adres 'A3' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0530                 | 0530010000000003                         |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A3' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0530                              | 20100818                           |
    En adres 'A3' is op '2023-05-26' infrastructureel gewijzigd met de volgende gegevens
    | gemeentecode (92.10) |
    | 0800                 |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2023-01-01         |
    | datumTot                         | 2023-07-01         |
    | adresseerbaarObjectIdentificatie | 0530010000000003   |
    Dan heeft de response de volgende headers
    | naam                      | waarde    |
    | x-geleverde-gemeentecodes | 0533,0800 |

  Scenario: het gevraagde adresseerbaar object is door herindeling in een andere gemeente komen te liggen en de gevraagde periode ligt vóór de herindeling én in de gevraagde periode is het adresseerbaar object niet bewoond
    Gegeven adres 'A3' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0800                 | 0800010000000003                         |
    En adres 'A2' heeft de volgende gegevens
    | gemeentecode (92.10) | identificatiecode verblijfplaats (11.80) |
    | 0518                 | 0518010000000002                         |
    En de persoon met burgerservicenummer '000000024' is ingeschreven op adres 'A3' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0800                              | 20100818                           |
    En adres 'A3' is op '2022-01-01' infrastructureel gewijzigd naar adres 'A4' met de volgende gegevens
    | gemeentecode (92.10) |
    | 0530                 |
    En de persoon is vervolgens ingeschreven op adres 'A2' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0518                              | 20221101                           |
    En de persoon met burgerservicenummer '000000048' is ingeschreven op adres 'A4' met de volgende gegevens
    | gemeente van inschrijving (09.10) | datum aanvang adreshouding (10.30) |
    | 0530                              | 20230901                           |
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2023-07-01         |
    | datumTot                         | 2023-08-01         |
    | adresseerbaarObjectIdentificatie | 0800010000000003   |
    Dan heeft de response de volgende headers
    | naam                      | waarde |
    | x-geleverde-gemeentecodes |        |
    En heeft de response 0 bewoningen

  Scenario: gevraagde adresseerbaar object bestaat niet
    Als bewoningen wordt gezocht met de volgende parameters
    | naam                             | waarde             |
    | type                             | BewoningMetPeriode |
    | datumVan                         | 2010-01-01         |
    | datumTot                         | 2010-08-17         |
    | adresseerbaarObjectIdentificatie | 1234010000123456   |
    Dan heeft de response de volgende headers
    | naam                      | waarde |
    | x-geleverde-gemeentecodes |        |
    En heeft de response 0 bewoningen
