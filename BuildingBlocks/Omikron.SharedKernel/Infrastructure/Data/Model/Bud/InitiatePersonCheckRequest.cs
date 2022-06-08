using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
	public class InitiatePersonCheckRequest
    {
        [JsonPropertyName("person")]
        public BudPerson Person { get; set; }

        [JsonPropertyName("custom_data")]
        public List<BudCustomData> CustomData { get; set; }

        [JsonPropertyName("header")]
        public BudHeader Header { get; set; }
    }

    public class BudBankAccount
    {
        [JsonPropertyName("account_number")]
        public string AccountNumber { get; set; }

        [JsonPropertyName("sort_code")]
        public string SortCode { get; set; }
    }

    public class BudCurrentAddress
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("county")]
        public string County { get; set; }

        [JsonPropertyName("line_five")]
        public object LineFive { get; set; }

        [JsonPropertyName("line_four")]
        public object LineFour { get; set; }

        [JsonPropertyName("line_six")]
        public object LineSix { get; set; }

        [JsonPropertyName("line_three")]
        public string LineThree { get; set; }

        [JsonPropertyName("name_number")]
        public string NameNumber { get; set; }

        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonPropertyName("town")]
        public string Town { get; set; }
    }

    public class BudIdentificationDetail
    {
        [JsonPropertyName("issuing_country")]
        public string IssuingCountry { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class BudPhone
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class BudPreviousAddress
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("county")]
        public string County { get; set; }

        [JsonPropertyName("line_five")]
        public object LineFive { get; set; }

        [JsonPropertyName("line_four")]
        public object LineFour { get; set; }

        [JsonPropertyName("line_six")]
        public object LineSix { get; set; }

        [JsonPropertyName("line_three")]
        public string LineThree { get; set; }

        [JsonPropertyName("name_number")]
        public string NameNumber { get; set; }

        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonPropertyName("town")]
        public string Town { get; set; }
    }

    public class BudPerson
    {
        [JsonPropertyName("bank_account")]
        public BudBankAccount BankAccount { get; set; }

        [JsonPropertyName("current_address")]
        public BudCurrentAddress CurrentAddress { get; set; }

        [JsonPropertyName("date_of_birth")]
        public string DateOfBirth { get; set; }

        [JsonPropertyName("email_address")]
        public List<string> EmailAddress { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("identification_details")]
        public List<BudIdentificationDetail> IdentificationDetails { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("middle_name")]
        public string MiddleName { get; set; }

        [JsonPropertyName("phone")]
        public List<BudPhone> Phone { get; set; }

        [JsonPropertyName("previous_address")]
        public List<BudPreviousAddress> PreviousAddress { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }

    public class BudCustomData
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class BudHeader
    {
        [JsonPropertyName("profile_uuid")]
        public string ProfileUuid { get; set; }

        [JsonPropertyName("transaction_reference")]
        public string TransactionReference { get; set; }
    }
}
