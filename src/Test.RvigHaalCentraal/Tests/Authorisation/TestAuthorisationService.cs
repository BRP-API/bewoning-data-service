﻿using Rvig.Data.Base.Postgres.Authorisation;
using Rvig.Data.Base.Postgres.DatabaseModels;

namespace Test.RvigHaalCentraal.Tests.Authorisation
{
	[TestClass]
	public class TestAuthorisationService
	{
		internal class TestPersoon : DbPersoonActueelWrapper
		{
			public TestPersoon() { }
		}

		[TestMethod]
		public void TestApplyWithCorrectAuthorisations()
		{
			// Arrange
			var testPersoon = new TestPersoon
			{
				Persoon = new lo3_pl_persoon
				{
					burger_service_nr = 999994669,
					geboorte_datum = 30121961,
					geslachts_naam = "Streeveld"
				},
				Partners = new List<lo3_pl_persoon>
				{
					new lo3_pl_persoon
					{
						burger_service_nr = 999994559,
						geboorte_datum = 30121951,
						geslachts_naam = "Streeveld"
					}
				}
			};
			var autorisatie = new DbAutorisatie
			{
				afnemer_code = 990008,
				adres_vraag_bevoegdheid = 1,
				ad_hoc_medium = "N",
				ad_hoc_rubrieken = "10110 10120 10210 10220 10230 10240 10310 10320 10330 10410 16110 18110 18120 18210 18220 18230 18510 18610 20110 20120 20210 20220 20230 20240 20310 20320 20330 20410 26210 28110 28120 28210 28220 28230 28510 28610 30110 30120 30210 30220 30230 30240 30310 30320 30330 30410 36210 38110 38120 38210 38220 38230 38510 38610 40510 46310 46410 46510 48210 48220 48230 48510 48610 50110 50120 50210 50220 50230 50240 50310 50320 50330 50410 50610 50620 50630 50710 50720 50730 50740 51510 58110 58120 58210 58220 58230 58510 58610 60810 60820 60830 68110 68120 68210 68220 68230 68510 68610 76710 76720 76810 76910 77010 78710 80910 80920 81010 81020 81030 81110 81115 81120 81130 81140 81150 81160 81170 81180 81190 81210 81310 81320 81330 81340 81350 81410 81420 87210 87510 88510 88610 90110 90120 90210 90220 90230 90240 90310 90320 90330 98110 98120 98210 98220 98230 98510 98610 98910 103910 103920 103930 108510 108610 113210 113310 118210 118220 118230 118510 118610 123510 123520 123530 123540 123550 123560 123570 123610 128210 128220 128230 128510 128610 133110 133120 133130 133810 133820 138210 138220 138230 510110 510120 510210 510220 510230 510240 510310 510320 510330 510410 516110 518110 518120 518210 518220 518230 518510 518610 520110 520120 520210 520220 520230 520240 520310 520320 520330 520410 526210 528110 528120 528210 528220 528230 528510 528610 530110 530120 530210 530220 530230 530240 530310 530320 530330 530410 536210 538110 538120 538210 538220 538230 538510 538610 540510 546310 546410 546510 548210 548220 548230 548510 548610 550110 550120 550210 550220 550230 550240 550310 550320 550330 550410 550610 550620 550630 550710 550720 550730 550740 551510 558110 558120 558210 558220 558230 558510 558610 560810 560820 560830 568110 568120 568210 568220 568230 568510 568610 580910 580920 581010 581020 581030 581110 581115 581120 581130 581140 581150 581160 581170 581180 581190 581210 581310 581320 581330 581340 581350 581410 581420 587210 587510 588510 588610 590110 590120 590210 590220 590230 590240 590310 590320 590330 598110 598120 598210 598220 598230 598510 598610 603910 603920 603930 608510 608610 613210 613310 618210 618220 618230 618510 618610"
			};
			var authorizedRubrieken = autorisatie.ad_hoc_rubrieken
									.Split(' ')
									.Select(x => int.Parse(x))
									.ToList();
			// Act
			var persoonFiltered = AuthorisationService.Apply(testPersoon, autorisatie!, false, authorizedRubrieken);

			// Assert
			persoonFiltered.Should().NotBeNull();

			// persoonFiltered is already validated as not null.
			persoonFiltered
				.Should().NotBeNull()
				.And.BeEquivalentTo(testPersoon);
		}

		[TestMethod]
		public void TestApplyWithNoAuthorisationsButIsBinnengemeentelijk()
		{
			// Arrange
			var testPersoon = new TestPersoon
			{
				Persoon = new lo3_pl_persoon
				{
					burger_service_nr = 999994669,
					geboorte_datum = 30121961,
					geslachts_naam = "Streeveld"
				},
				Partners = new List<lo3_pl_persoon>
				{
					new lo3_pl_persoon
					{
						burger_service_nr = 999994559,
						geboorte_datum = 30121951,
						geslachts_naam = "Streeveld"
					}
				}
			};
			var autorisatie = new DbAutorisatie
			{
				afnemer_code = 990008,
				adres_vraag_bevoegdheid = 1,
				ad_hoc_medium = "N",
				ad_hoc_rubrieken = ""
			};

			// Act
			var persoonFiltered = AuthorisationService.Apply(testPersoon, autorisatie!, true, new List<int>());

			// Assert
			persoonFiltered.Should().NotBeNull()
				.And.BeEquivalentTo(testPersoon);
		}

		[TestMethod]
		public void TestApplyWithNoAuthorisations()
		{
			// Arrange
			var testPersoon = new TestPersoon
			{
				Persoon = new lo3_pl_persoon
				{
					burger_service_nr = 999994669,
					geboorte_datum = 30121961,
					geslachts_naam = "Streeveld"
				},
				Partners = new List<lo3_pl_persoon>
				{
					new lo3_pl_persoon
					{
						burger_service_nr = 999994559,
						geboorte_datum = 30121951,
						geslachts_naam = "Streeveld"
					}
				}
			};
			var autorisatie = new DbAutorisatie
			{
				afnemer_code = 990008,
				adres_vraag_bevoegdheid = 1,
				ad_hoc_medium = "N",
				ad_hoc_rubrieken = ""
			};

			// Act
			var persoonFiltered = AuthorisationService.Apply(testPersoon, autorisatie!, false, new List<int>());

			// Assert
			persoonFiltered.Should().NotBeNull()
				.And.NotBeEquivalentTo(testPersoon);

			// persoonFiltered is already validated as not null.
			persoonFiltered!.Persoon.Should().NotBeNull();
			persoonFiltered!.Persoon.burger_service_nr.Should().BeNull();
			persoonFiltered!.Persoon.geboorte_datum.Should().BeNull();
			persoonFiltered!.Persoon.geslachts_naam.Should().BeNull();

			persoonFiltered!.Partners.Should().NotBeNull();
			persoonFiltered!.Partners.Should().NotBeEmpty();

			persoonFiltered!.Partners[0].burger_service_nr.Should().BeNull();
			persoonFiltered!.Partners[0].geboorte_datum.Should().BeNull();
			persoonFiltered!.Partners[0].geslachts_naam.Should().BeNull();
		}

		[TestMethod]
		public void TestApplyWithLimitedAuthorisations()
		{
			// Arrange
			var testPersoon = new TestPersoon
			{
				Persoon = new lo3_pl_persoon
				{
					burger_service_nr = 999994669,
					geboorte_datum = 30121961,
					geslachts_naam = "Streeveld"
				},
				Partners = new List<lo3_pl_persoon>
				{
					new lo3_pl_persoon
					{
						burger_service_nr = 999994559,
						geboorte_datum = 30121951,
						geslachts_naam = "Streeveld"
					}
				}
			};
			var autorisatie = new DbAutorisatie
			{
				afnemer_code = 990008,
				adres_vraag_bevoegdheid = 1,
				ad_hoc_medium = "N",
				ad_hoc_rubrieken = "10120"
			};
			var authorizedRubrieken = autorisatie.ad_hoc_rubrieken
									.Split(' ')
									.Select(x => int.Parse(x))
									.ToList();
			// Act
			var persoonFiltered = AuthorisationService.Apply(testPersoon, autorisatie!, false, authorizedRubrieken);

			// Assert
			persoonFiltered.Should().NotBeNull();

			// persoonFiltered is already validated as not null.
			persoonFiltered!.Persoon
				.Should().NotBeNull()
				.And.NotBeEquivalentTo(testPersoon.Persoon);
			persoonFiltered!.Persoon.burger_service_nr
				.Should().HaveValue()
				.And.Be(testPersoon.Persoon.burger_service_nr);
			persoonFiltered!.Persoon.geboorte_datum.Should().BeNull();
			persoonFiltered!.Persoon.geslachts_naam.Should().BeNull();

			persoonFiltered!.Partners.Should().NotBeNull();
			persoonFiltered!.Partners.Should().NotBeEmpty();

			persoonFiltered!.Partners[0].burger_service_nr.Should().BeNull();
			persoonFiltered!.Partners[0].geboorte_datum.Should().BeNull();
			persoonFiltered!.Partners[0].geslachts_naam.Should().BeNull();
		}
	}
}
