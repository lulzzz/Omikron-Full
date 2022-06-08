using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Omikron.SharedKernel.Infrastructure.Data.Model.Bud
{
	public class BudListTransactionsResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        [JsonPropertyName("raw_transaction")]
        public BudRawTransaction RawTransaction { get; set; }

        [JsonPropertyName("enrichment")]
        public BudTransactionEnrichment Enrichment { get; set; }
    }

    public class BudTransactionAmount
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class BudBankTransactionCode
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("sub_code")]
        public string SubCode { get; set; }
    }

    public class BudProprietaryBankTransactionCode
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }
    }

    public class BudTransactionBalance
    {
        [JsonPropertyName("amount")]
        public BudTransactionAmount Amount { get; set; }

        [JsonPropertyName("credit_debit_indicator")]
        public string CreditDebitIndicator { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class BudTransactionMerchantDetails
    {
        [JsonPropertyName("merchant_name")]
        public string MerchantName { get; set; }

        [JsonPropertyName("merchant_category_code")]
        public string MerchantCategoryCode { get; set; }
    }

    public class BudTransactionInstructedAmount
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class BudTransactionCurrencyExchange
    {
        [JsonPropertyName("contract_identification")]
        public string ContractIdentification { get; set; }

        [JsonPropertyName("exchange_rate")]
        public string ExchangeRate { get; set; }

        [JsonPropertyName("instructed_amount")]
        public BudTransactionInstructedAmount InstructedAmount { get; set; }

        [JsonPropertyName("quotation_date")]
        public string QuotationDate { get; set; }

        [JsonPropertyName("source_currency")]
        public string SourceCurrency { get; set; }

        [JsonPropertyName("target_currency")]
        public string TargetCurrency { get; set; }

        [JsonPropertyName("unit_currency")]
        public string UnitCurrency { get; set; }
    }

    public class BudTransactionPostalAddress
    {
        [JsonPropertyName("address_type")]
        public string AddressType { get; set; }

        [JsonPropertyName("building_number")]
        public string BuildingNumber { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("country_subdivision")]
        public string CountrySubdivision { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; }

        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }

        [JsonPropertyName("street_name")]
        public string StreetName { get; set; }

        [JsonPropertyName("sub_department")]
        public string SubDepartment { get; set; }

        [JsonPropertyName("town_name")]
        public string TownName { get; set; }

        [JsonPropertyName("address_line")]
        public List<string> AddressLine { get; set; }
    }

    public class BudTransactionCreditorAgent
    {
        [JsonPropertyName("scheme_name")]
        public string SchemeName { get; set; }

        [JsonPropertyName("identification")]
        public string Identification { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("postal_address")]
        public BudTransactionPostalAddress PostalAddress { get; set; }
    }

    public class BudTransactionCreditorAccount
    {
        [JsonPropertyName("scheme_name")]
        public string SchemeName { get; set; }

        [JsonPropertyName("identification")]
        public string Identification { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("secondary_identification")]
        public string SecondaryIdentification { get; set; }
    }

    public class BudTransactionDebtorAgent
    {
        [JsonPropertyName("scheme_name")]
        public string SchemeName { get; set; }

        [JsonPropertyName("identification")]
        public string Identification { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("postal_address")]
        public BudTransactionPostalAddress PostalAddress { get; set; }
    }

    public class BudTransactionDebtorAccount
    {
        [JsonPropertyName("scheme_name")]
        public string SchemeName { get; set; }

        [JsonPropertyName("identification")]
        public string Identification { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("secondary_identification")]
        public string SecondaryIdentification { get; set; }
    }

    public class BudTransactionCardInstrument
    {
        [JsonPropertyName("card_scheme_name")]
        public string CardSchemeName { get; set; }

        [JsonPropertyName("card_authorisation_type")]
        public string CardAuthorisationType { get; set; }

        [JsonPropertyName("cardholder_name")]
        public string CardholderName { get; set; }

        [JsonPropertyName("identification")]
        public string Identification { get; set; }
    }

    public class BudTransactionChargeAmount
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class BudTransactionSupplementaryData
    {
        [JsonPropertyName("unknown_elements")]
        public bool UnknownElements { get; set; }
    }

    public class BudRawTransaction
    {
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; }

        [JsonPropertyName("transaction_id")]
        public string TransactionId { get; set; }

        [JsonPropertyName("transaction_reference")]
        public string TransactionReference { get; set; }

        [JsonPropertyName("amount")]
        public BudTransactionAmount Amount { get; set; }

        [JsonPropertyName("credit_debit_indicator")]
        public string CreditDebitIndicator { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("booking_date_time")]
        public DateTime BookingDateTime { get; set; }

        [JsonPropertyName("value_date_time")]
        public DateTime? ValueDateTime { get; set; }

        [JsonPropertyName("address_line")]
        public string AddressLine { get; set; }

        [JsonPropertyName("transaction_information")]
        public string TransactionInformation { get; set; }

        [JsonPropertyName("bank_transaction_code")]
        public BudBankTransactionCode BankTransactionCode { get; set; }

        [JsonPropertyName("proprietary_bank_transaction_code")]
        public BudProprietaryBankTransactionCode ProprietaryBankTransactionCode { get; set; }

        [JsonPropertyName("balance")]
        public BudTransactionBalance Balance { get; set; }

        [JsonPropertyName("merchant_details")]
        public BudTransactionMerchantDetails MerchantDetails { get; set; }

        [JsonPropertyName("statement_reference")]
        public List<string> StatementReference { get; set; }

        [JsonPropertyName("currency_exchange")]
        public BudTransactionCurrencyExchange CurrencyExchange { get; set; }

        [JsonPropertyName("creditor_agent")]
        public BudTransactionCreditorAgent CreditorAgent { get; set; }

        [JsonPropertyName("creditor_account")]
        public BudTransactionCreditorAccount CreditorAccount { get; set; }

        [JsonPropertyName("debtor_agent")]
        public BudTransactionDebtorAgent DebtorAgent { get; set; }

        [JsonPropertyName("debtor_account")]
        public BudTransactionDebtorAccount DebtorAccount { get; set; }

        [JsonPropertyName("card_instrument")]
        public BudTransactionCardInstrument CardInstrument { get; set; }

        [JsonPropertyName("charge_amount")]
        public BudTransactionChargeAmount ChargeAmount { get; set; }

        [JsonPropertyName("supplementary_data")]
        public BudTransactionSupplementaryData SupplementaryData { get; set; }
    }

    public class BudTransactionCategory
    {
        [JsonPropertyName("category_name")]
        public string CategoryName { get; set; }

        [JsonPropertyName("confidence")]
        public string Confidence { get; set; }
    }

    public class BudTransactionSubcategory
    {
        [JsonPropertyName("subcategory_name")]
        public string SubcategoryName { get; set; }

        [JsonPropertyName("confidence")]
        public string Confidence { get; set; }
    }

    public class BudTransactionCategoriser
    {
        [JsonPropertyName("categories")]
        public List<BudTransactionCategory> Categories { get; set; }

        [JsonPropertyName("subcategories")]
        public List<BudTransactionSubcategory> Subcategories { get; set; }
    }

    public class BudTransactionMerchant
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("merchant_name")]
        public string MerchantName { get; set; }

        [JsonPropertyName("merchant_logo")]
        public string MerchantLogo { get; set; }

        [JsonPropertyName("merchant_confidence")]
        public decimal MerchantConfidence { get; set; }
    }

    public class BudTransactionRegularPayments
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("score")]
        public string Score { get; set; }

        [JsonPropertyName("period")]
        public string Period { get; set; }
    }

    public class BudTransactionCarbonTracker
    {
        [JsonPropertyName("co2_amount_kg")]
        public double Co2AmountKg { get; set; }
    }

    public class BudTransactionEnrichment
    {
        [JsonPropertyName("categoriser")]
        public BudTransactionCategoriser Categoriser { get; set; }

        [JsonPropertyName("merchant")]
        public BudTransactionMerchant Merchant { get; set; }

        [JsonPropertyName("regular_payments")]
        public BudTransactionRegularPayments RegularPayments { get; set; }

        [JsonPropertyName("carbon_tracker")]
        public BudTransactionCarbonTracker CarbonTracker { get; set; }
    }
}
