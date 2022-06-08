using Omikron.SharedKernel.Infrastructure.Data.Model.Bud;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Omikron.SharedKernel.Infrastructure.Accumulators
{
	public class TransactionsAccumulator
	{
		private const string _transfersInSubcategory = "transfers_in";
		private const string _transfersOutSubCategory = "transfers_out";
		private const string _transfersCategory = "Transfers";

		public TransactionsAccumulator(IEnumerable<Transaction> existingTransactions, IEnumerable<Merchant> existingMerchants, IEnumerable<Account> userAccounts)
		{
			ExistingTransactions = existingTransactions;
			ExistingMerchants = existingMerchants;
			UserAccounts = userAccounts;
			TransactionsToUpdate = new List<Transaction>();
			TransactionsToAdd = new List<Transaction>();
			MerchantsToAdd = new List<Merchant>();
		}

		public IEnumerable<Transaction> ExistingTransactions { get; }
		public IEnumerable<Merchant> ExistingMerchants { get; }
		public IEnumerable<Account> UserAccounts { get; }
		public List<Transaction> TransactionsToUpdate { get; }
		public List<Transaction> TransactionsToAdd { get; }
		public List<Merchant> MerchantsToAdd { get; }

		public TransactionsAccumulator Accumulate(BudListTransactionsResponse transaction)
		{
			var existingTransaction = ExistingTransactions.FirstOrDefault(predicate: t => t.BudTransactionId == transaction.Id);

			if (existingTransaction != null)
			{
				if (TransactionsDiffer(transaction, existingTransaction))
				{
					UpdateTransactionData(transaction, existingTransaction);
					TransactionsToUpdate.Add(item: existingTransaction);
				}
			}
			else
			{
				var parentAccount = UserAccounts.FirstOrDefault(a => a.BudAccountId == transaction.RawTransaction.AccountId);
				if (parentAccount == null)
				{
					return this;
				}

				var newTransaction = FactoryNewTransaction(transaction, parentAccount);
				TransactionsToAdd.Add(newTransaction);
			}

			return this;
		}

		private Transaction FactoryNewTransaction(BudListTransactionsResponse transaction, Account parentAccount)
		{
			var newTransaction = new Transaction()
			{
				AccountId = parentAccount.Id,
				Amount = transaction.RawTransaction.Amount.Amount.ToDecimal(),
				Currency = transaction.RawTransaction.Amount.Currency,
				Date = transaction.RawTransaction.BookingDateTime,
				TransactionInformation = transaction.RawTransaction.TransactionInformation,
				CreditDebitIndicator = transaction.RawTransaction.CreditDebitIndicator,
				TransactionStatus = transaction.RawTransaction.Status,
				BudTransactionId = transaction.Id
			};

			SetCategoryTransaction(transaction, newTransaction);

			if (transaction.Enrichment.Merchant != null)
			{
				LinkMerchant(transaction, newTransaction);
			}

			return newTransaction;
		}

		private void UpdateTransactionData(BudListTransactionsResponse transaction, Transaction existingTransaction)
		{
			existingTransaction.TransactionStatus = transaction.RawTransaction.Status;
			existingTransaction.Amount = transaction.RawTransaction.Amount.Amount.ToDecimal();
			existingTransaction.CreditDebitIndicator = transaction.RawTransaction.CreditDebitIndicator;
			existingTransaction.TransactionInformation = transaction.RawTransaction.TransactionInformation;
			SetCategoryTransaction(transaction, existingTransaction);

			if (transaction.Enrichment.Merchant != null)
			{
				LinkMerchant(transaction, existingTransaction);
			}
		}

		private static void SetCategoryTransaction(BudListTransactionsResponse transaction, Transaction newTransaction)
		{
			var subCategory = GetBudTransactionSubCategory(transaction);
			if (subCategory == _transfersInSubcategory || subCategory == _transfersOutSubCategory)
			{
				newTransaction.Category = _transfersCategory;
				return;
			}

			newTransaction.Category = GetBudTransactionCategoryInTitleCase(transaction);
		}

		private static bool TransactionsDiffer(BudListTransactionsResponse transaction, Transaction existingTransaction)
		{
			return existingTransaction.TransactionStatus == null || 
				   existingTransaction.CreditDebitIndicator == null || 
				   existingTransaction.TransactionStatus != transaction.RawTransaction.Status ||
				   existingTransaction.Amount != transaction.RawTransaction.Amount.Amount.ToDecimal() ||
				   existingTransaction.CreditDebitIndicator != transaction.RawTransaction.CreditDebitIndicator ||
				   existingTransaction.TransactionInformation != transaction.RawTransaction.TransactionInformation ||
				   TransactionCategoryDiffer(transaction, existingTransaction) ||
				   (existingTransaction.MerchantId == null && transaction.Enrichment.Merchant != null);
		}

		private static bool TransactionCategoryDiffer(BudListTransactionsResponse transaction, Transaction existingTransaction)
		{
			var subCategory = GetBudTransactionSubCategory(transaction);

			return (subCategory == _transfersInSubcategory || subCategory == _transfersOutSubCategory) && existingTransaction.Category != _transfersCategory
					|| GetBudTransactionCategoryInTitleCase(transaction) != existingTransaction.Category;
		}

		private static string GetBudTransactionSubCategory(BudListTransactionsResponse transaction)
		{
			return transaction.Enrichment.Categoriser.Subcategories.OrderByDescending(c => c.Confidence.ToDecimal()).First()?.SubcategoryName;
		}

		private static string GetBudTransactionCategoryInTitleCase(BudListTransactionsResponse transaction)
		{
			return transaction.Enrichment.Categoriser?.Categories.OrderByDescending(c => c.Confidence.ToDecimal()).First()?.CategoryName.ToTitleCase().Replace("_", " ");
		}

		private void LinkMerchant(BudListTransactionsResponse transaction, Transaction currentTransaction)
		{
			var merchantExistis = ExistingMerchants.FirstOrDefault(m => m.Name == transaction.Enrichment.Merchant.Id);
			if (merchantExistis != null)
			{
				currentTransaction.MerchantId = merchantExistis.Id;
				return;
			}

			var merchantAdded = MerchantsToAdd.FirstOrDefault(m => m.Name == transaction.Enrichment.Merchant.Id);

			if (merchantAdded == null)
			{
				merchantAdded = new Merchant()
				{
					Id = Guid.NewGuid(),
					Name = transaction.Enrichment.Merchant.Id,
					DisplayName = transaction.Enrichment.Merchant.MerchantName,
					Logo = transaction.Enrichment.Merchant.MerchantLogo
				};

				MerchantsToAdd.Add(merchantAdded);
			}
			currentTransaction.MerchantId = merchantAdded.Id;
		}
	}
}
