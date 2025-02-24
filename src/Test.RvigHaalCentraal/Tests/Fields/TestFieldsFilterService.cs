using Newtonsoft.Json;
using System.Runtime.Serialization;
using Rvig.BrpApi.Bewoningen.Exceptions;
using Rvig.BrpApi.Bewoningen.Fields;

namespace Test.RvigHaalCentraal.Tests.Fields;

[DataContract]
public class TestHuman
{
	[DataMember(Name = "identification", EmitDefaultValue = false)]
	public string? Identification { get; set; }

	[DataMember(Name = "name", EmitDefaultValue = false)]
	public TestName? Name { get; set; }

	[DataMember(Name = "pets", EmitDefaultValue = false)]
	public List<TestPet>? Pets { get; set; }
}

[DataContract]
public class TestName
{
	[DataMember(Name = "firstName", EmitDefaultValue = false)]
	public string? FirstName { get; set; }

	[DataMember(Name = "lastName", EmitDefaultValue = false)]
	public string? LastName { get; set; }

	[DataMember(Name = "royalty", EmitDefaultValue = false)]
	public TestRoyalty? Royalty { get; set; }
}

[DataContract]
public class TestRoyalty
{
	[DataMember(Name = "royaltyCode", EmitDefaultValue = false)]
	public string? RoyaltyCode { get; set; }

	[DataMember(Name = "royaltySummary", EmitDefaultValue = false)]
	public string? RoyaltySummary { get; set; }

	[DataMember(Name = "royaltyType", EmitDefaultValue = false)]
	public RoyaltyType? Type { get; set; }
}

[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum RoyaltyType
{
	/// <summary>
	/// Enum TitelEnum for titel
	/// </summary>
	[EnumMember(Value = "title")]
	TitleEnum = 1,

	/// <summary>
	/// Enum PredicaatEnum for predicaat
	/// </summary>
	[EnumMember(Value = "predicate")]
	PredicateEnum = 2
}

[DataContract]
public class TestBirth
{
	[DataMember(Name = "date", EmitDefaultValue = false)]
	public string? Date { get; set; }

	[DataMember(Name = "country", EmitDefaultValue = false)]
	public string? Country { get; set; }
}

[DataContract]
public class TestPet
{
	[DataMember(Name = "name", EmitDefaultValue = false)]
	public TestName? Name { get; set; }

	[DataMember(Name = "birth", EmitDefaultValue = false)]
	public TestBirth? Birth { get; set; }

	[DataMember(Name = "breed", EmitDefaultValue = false)]
	public string? Breed { get; set; }
}

[TestClass]
[TestCategory("Fields")]
public class TestFieldsFilterService
{
	protected readonly FieldsSettingsModel _fieldsSettingsModel =
		new FieldsSettingsModel("fields")
		{
			ForbiddenProperties = new List<string>(),
			PropertiesToDiscard = new List<string>(),
			MandatoryProperties = new List<string>(),
			SetChildPropertiesIfExistInScope = new Dictionary<string, string>(),
			SetPropertiesIfContextPropertyNotNull = new Dictionary<string, string>(),
			ShortHandMappings = new Dictionary<string, string>()
		};
	protected readonly FieldsFilterService _fieldsExpandFilterService = new();

	/// <summary>
	/// burgerserviceletter is an unknown scope and must throw an exception.
	/// </summary>
	[TestMethod]
	public void TestValidateScopeWithIncorrectScope()
	{
		Assert.ThrowsException<InvalidParamsException>(() => _fieldsExpandFilterService.ValidateScope<TestHuman>("burgerserviceletter", _fieldsSettingsModel));
	}

	/// <summary>
	/// kinderen.naampje.voornamen is an unknown scope which contains multiple levels of objects but still must throw an exception
	/// </summary>
	[TestMethod]
	public void TestValidateScopeWithIncorrectScopeWithChildProperties()
	{
		Assert.ThrowsException<InvalidParamsException>(() => _fieldsExpandFilterService.ValidateScope<TestHuman>("kinderen.name.firstName", _fieldsSettingsModel));
	}

	/// <summary>
	/// Identification is a valid scope and the object must return with only identification while other properties are default.
	/// </summary>
	[TestMethod]
	public void TestApplyScopeIdentification()
	{
		var source = new TestHuman
		{
			Identification = "123456789",
			Name = new TestName
			{
				LastName = "Test"
			}
		};
		var resultHuman = _fieldsExpandFilterService.ApplyScope(source, "identification", _fieldsSettingsModel);

		resultHuman.Should().NotBeNull();
		resultHuman.Should().NotBeEquivalentTo(source);
		resultHuman.Identification.Should().NotBeNullOrWhiteSpace();
		resultHuman.Identification.Should().BeEquivalentTo(source.Identification);
		resultHuman.Name.Should().BeNull();
	}

	/// <summary>
	/// Name is a valid scope and the object must return with only name while other properties are default.
	/// The properties within name however must be filled as name is the parent and therefore all children with value should be shown.
	/// </summary>
	[TestMethod]
	public void TestApplyScopeName()
	{
		var source = new TestHuman
		{
			Identification = "123456789",
			Name = new TestName
			{
				FirstName = "Holy",
				LastName = "Test"
			}
		};
		var resultHuman = _fieldsExpandFilterService.ApplyScope(source, "name", _fieldsSettingsModel);

		resultHuman.Should().NotBeNull();
		resultHuman.Should().NotBeEquivalentTo(source);
		resultHuman.Name.Should().NotBeNull();
		// resultHuman and resultHuman.Name should not be null as we are checking this above.
		resultHuman!.Name!.LastName.Should().NotBeNullOrWhiteSpace();
		resultHuman.Name.FirstName.Should().NotBeNullOrWhiteSpace();
		resultHuman.Name.Should().BeEquivalentTo(source.Name);
		resultHuman.Identification.Should().BeNullOrWhiteSpace();
	}

	/// <summary>
	/// Name is a valid scope and the object must return with only name while other properties are default.
	/// As name.lastName is requested, firstName must be null as it was not requested.
	/// </summary>
	[TestMethod]
	public void TestApplyScopeLastName()
	{
		var source = new TestHuman
		{
			Identification = "123456789",
			Name = new TestName
			{
				FirstName = "Holy",
				LastName = "Test"
			}
		};
		var resultHuman = _fieldsExpandFilterService.ApplyScope(source, "name.lastName", _fieldsSettingsModel);

		resultHuman.Should().NotBeNull();
		resultHuman.Should().NotBeEquivalentTo(source);
		resultHuman.Name.Should().NotBeNull();
		// resultHuman and resultHuman.Name should not be null as we are checking this above.
		resultHuman!.Name!.LastName.Should().NotBeNullOrWhiteSpace();
		resultHuman.Name.FirstName.Should().BeNullOrWhiteSpace();
		resultHuman.Name.Should().NotBeEquivalentTo(source.Name);
		resultHuman.Name.LastName.Should().BeEquivalentTo(source.Name.LastName);
		resultHuman.Identification.Should().BeNullOrWhiteSpace();
	}

	/// <summary>
	/// Name is a valid scope and the object must return with only name while other properties are default.
	/// As name.lastName is requested, firstName must be null as it was not requested.
	/// name.LastName however does not have a value so it is expected to receive a result where name is not null but the value of lastName is null.
	/// </summary>
	[TestMethod]
	public void TestApplyScopeEmptyLastName()
	{
		var source = new TestHuman
		{
			Identification = "123456789",
			Name = new TestName
			{
				FirstName = "Holy"
			}
		};
		var resultHuman = _fieldsExpandFilterService.ApplyScope(source, "name.lastName", _fieldsSettingsModel);

		resultHuman.Should().NotBeNull();
		resultHuman.Should().NotBeEquivalentTo(source);
		resultHuman.Name.Should().NotBeNull();
		// resultHuman and resultHuman.Name should not be null as we are checking this above.
		resultHuman!.Name!.LastName.Should().BeNullOrWhiteSpace();
		resultHuman.Name.FirstName.Should().BeNullOrWhiteSpace();
		resultHuman.Name.Should().NotBeEquivalentTo(source.Name);
		resultHuman.Identification.Should().BeNullOrWhiteSpace();
	}

	/// <summary>
	/// Pets is a valid scope but given TestHuman does not own pets. Therefore this will filter all results away leaving an empty TestHuman object.
	/// </summary>
	[TestMethod]
	public void TestApplyScopeWithScopeThatResultsInEmptyObject()
	{
		var source = new TestHuman
		{
			Identification = "123456789",
			Name = new TestName
			{
				LastName = "Test"
			}
		};
		var resultHuman = _fieldsExpandFilterService.ApplyScope(source, "pets", _fieldsSettingsModel);

		resultHuman.Should().NotBeNull();
		resultHuman.Should().NotBeEquivalentTo(source);
		resultHuman.Name.Should().BeNull();
		resultHuman.Identification.Should().BeNullOrWhiteSpace();
		resultHuman.Pets.Should().BeNull();
	}

	/// <summary>
	/// Pets is a valid scope and given TestHuman does own pets. Therefore this will filter all results away leaving a TestHuman object with pets.
	/// </summary>
	[TestMethod]
	public void TestApplyScopeLists()
	{
		var source = new TestHuman
		{
			Identification = "123456789",
			Pets = new List<TestPet>
			{
				new TestPet
				{
					Breed = "Golden Retriever",
					Name = new TestName
					{
						LastName = "Goldie"
					}
				}
			}
		};
		var resultHuman = _fieldsExpandFilterService.ApplyScope(source, "pets", _fieldsSettingsModel);

		resultHuman.Should().NotBeNull();
		resultHuman.Should().NotBeEquivalentTo(source);
		resultHuman.Identification.Should().BeNullOrWhiteSpace();
		resultHuman.Pets.Should().NotBeNull();
		resultHuman.Pets.Should().HaveCount(1);
		// resultHuman and resultHuman.Pets should not be null as we are checking this above.
		resultHuman!.Pets!.Single().Breed.Should().NotBeNullOrWhiteSpace();
		resultHuman!.Pets!.Single().Breed.Should().BeEquivalentTo(source.Pets.Single().Breed);
		resultHuman!.Pets!.Single().Name.Should().NotBeNull();
		resultHuman!.Pets!.Single().Name.Should().BeEquivalentTo(source.Pets.Single().Name);
	}

	/// <summary>
	/// pets.name.firstName is a valid scope but pets in given TestName do not have names. Therefore this will filter all results away leaving a TestHuman object with an array of one empty pet object.
	/// </summary>
	[TestMethod]
	public void TestApplyScopeListsNameScopeWithoutNameValues()
	{
		var source = new TestHuman
		{
			Identification = "123456789",
			Pets = new List<TestPet>
			{
				new TestPet
				{
					Breed = "Golden Retriever",
					Name = new TestName()
				}
			}
		};
		var resultHuman = _fieldsExpandFilterService.ApplyScope(source, "pets.name.firstName", _fieldsSettingsModel);

		resultHuman.Should().NotBeNull();
		resultHuman.Should().NotBeEquivalentTo(source);
		resultHuman.Identification.Should().BeNullOrWhiteSpace();
		resultHuman.Pets.Should().NotBeNull();
		resultHuman.Pets.Should().HaveCount(1);
		// resultHuman and resultHuman.Pets should not be null as we are checking this above.
		resultHuman!.Pets!.Single().Breed.Should().BeNullOrWhiteSpace();
		resultHuman!.Pets!.Single().Name.Should().NotBeNull();
		// resultHuman.Pets.Name should not be null as we are checking this above.
		resultHuman!.Pets!.Single().Name!.FirstName.Should().BeNullOrWhiteSpace();
	}

	/// <summary>
	/// pets.name.firstName is a valid scope but pets in given TestName do not have names. Therefore this will filter all results away leaving a TestHuman object with an array of one empty pet object.
	/// </summary>
	[TestMethod]
	public void TestApplyScopeListsWithoutValues()
	{
		var source = new TestHuman
		{
			Identification = "123456789",
			Pets = new List<TestPet>
			{
				new TestPet
				{
					Breed = "Golden Retriever",
					Name = new TestName(),
					Birth = new TestBirth()
				}
			}
		};
		var resultHuman = _fieldsExpandFilterService.ApplyScope(source, "pets.name,pets.birth.date", _fieldsSettingsModel);

		resultHuman.Should().NotBeNull();
		resultHuman.Should().NotBeEquivalentTo(source);
		resultHuman.Identification.Should().BeNullOrWhiteSpace();
		resultHuman.Pets.Should().NotBeNull();
		resultHuman.Pets.Should().HaveCount(1);
		// resultHuman and resultHuman.Pets should not be null as we are checking this above.
		resultHuman!.Pets!.Single().Breed.Should().BeNullOrWhiteSpace();
		resultHuman!.Pets!.Single().Name.Should().NotBeNull();
		// resultHuman.Pets.Name should not be null as we are checking this above.
		resultHuman!.Pets!.Single().Name!.FirstName.Should().BeNullOrWhiteSpace();
		resultHuman!.Pets!.Single().Birth.Should().NotBeNull();
		// resultHuman.Pets.Name should not be null as we are checking this above.
		resultHuman!.Pets!.Single().Birth!.Date.Should().BeNullOrWhiteSpace();
	}

	/// <summary>
	/// pets.name.firstName is a valid scope but pets in given TestName do not have names. Therefore this will filter all results away leaving a TestHuman object with an array of one empty pet object.
	/// </summary>
	[TestMethod]
	public void TestApplyScopeWithMultipleFieldsOfSameObjectWhereOneIsNull()
	{
		var source = new TestHuman
		{
			Identification = "123456789",
			Name = new TestName
			{
				Royalty = new TestRoyalty
				{
					Type = RoyaltyType.TitleEnum,
					RoyaltyCode = "B",
					RoyaltySummary = "Baron"
				},
				FirstName = null,
				LastName = "Chase"
			}
		};
		var resultHuman = _fieldsExpandFilterService.ApplyScope(source, "name&name.firstName&name.lastName&name.royalty", _fieldsSettingsModel);

		resultHuman.Should().NotBeNull();
		resultHuman.Should().NotBeEquivalentTo(source);
		resultHuman.Identification.Should().BeNullOrWhiteSpace();
		// resultHuman should not be null as we are checking this above.
		resultHuman!.Name.Should().NotBeNull();
		// resultHuman.Name should not be null as we are checking this above.
		resultHuman!.Name!.FirstName.Should().BeNullOrWhiteSpace();
		resultHuman!.Name!.LastName.Should().Be("Chase");
		resultHuman!.Name!.Royalty.Should().NotBeNull();
		// resultHuman.Name.Royalty should not be null as we are checking this above.
		resultHuman!.Name!.Royalty!.RoyaltyCode.Should().Be("B");
		resultHuman!.Name!.Royalty!.RoyaltySummary.Should().Be("Baron");
		resultHuman!.Name!.Royalty!.Type.Should().Be(RoyaltyType.TitleEnum);
	}

	/// <summary>
	/// Earlier iterations of ApplyScope resulted in one item in the list because the IsDefault method didn't account for potential other values in other properties.
	/// An empty object must be revealed if an item has other values other than the requested. By doing this, a user can understand that there is actually
	/// an item available but the fields must be less specific.
	/// </summary>
	[TestMethod]
	public void TestApplyScopeWithListRelationWhereOneItemHasNoValueForMentionedField()
	{
		var source = new TestHuman
		{
			Identification = "123456789",
			Pets = new List<TestPet>
			{
				new TestPet
				{
					Breed = "Golden Retriever"
				},
				new TestPet
				{
					Name = new TestName
					{
						FirstName = "Thor"
					}
				}
			}
		};
		var resultHuman = _fieldsExpandFilterService.ApplyScope(source, "pets.breed", _fieldsSettingsModel);

		resultHuman.Should().NotBeNull();
		resultHuman.Should().NotBeEquivalentTo(source);
		resultHuman.Identification.Should().BeNullOrWhiteSpace();
		// resultHuman should not be null as we are checking this above.
		resultHuman!.Name.Should().BeNull();
		// resultHuman.Name should not be null as we are checking this above.
		resultHuman!.Pets.Should().NotBeEmpty();
		resultHuman!.Pets.Should().HaveCount(2);
		resultHuman!.Pets![0].Breed.Should().Be("Golden Retriever");
		resultHuman!.Pets![1].Breed.Should().BeNullOrWhiteSpace();
	}
}
