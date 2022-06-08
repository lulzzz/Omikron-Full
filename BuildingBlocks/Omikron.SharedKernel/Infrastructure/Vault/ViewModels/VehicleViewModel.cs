using System;
using System.Linq;
using AutoMapper;
using Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities;

namespace Omikron.SharedKernel.Infrastructure.Vault.ViewModels
{
    public class VehicleViewModel
    {
        public Guid VehicleId { get; set; }
        public string VehicleName { get; set; }
        public string Registration { get; set; }
        public int Mileage { get; set; }
        public decimal VehicleValue { get; set; }
        public bool AutomaticallyReValueVehicle { get; set; }
        public string FinanceAgreementName { get; set; }
        public decimal? NewFinanceBalance { get; set; }
        public string Reference { get; set; }
        public string Notes { get; set; }
    }

    public class VehicleViewModelProfile : Profile
    {
        public VehicleViewModelProfile()
        {
            CreateMap<Vehicle, VehicleViewModel>()
                .ForMember(x => x.VehicleValue, x => x.MapFrom(s => s.VehicleValues.OrderByDescending(vehicleValue => vehicleValue.CreatedAt).First().Amount))
                .ForMember(x => x.VehicleName, x => x.MapFrom(s => s.Name))
                .ForMember(x => x.AutomaticallyReValueVehicle, x => x.MapFrom(s => s.AutomaticallyReValueVehicle))
                .ForMember(x => x.Mileage, x => x.MapFrom(s => int.Parse(s.Mileage)))
                .ForMember(x => x.Registration, x => x.MapFrom(s => s.Registration))
                .ForMember(x => x.VehicleId, x => x.MapFrom(s => s.Id))

                .ForMember(x => x.Notes, x => x.MapFrom(s => s.FinancialAgreement == null ? "" : s.FinancialAgreement.Notes))
                .ForMember(x => x.FinanceAgreementName, x => x.MapFrom(s => s.FinancialAgreement == null ? "" : s.FinancialAgreement.Name))
                .ForMember(x => x.Reference, x => x.MapFrom(s => s.FinancialAgreement == null ? "" : s.FinancialAgreement.ReferenceNumber))
                .ForMember(x => x.NewFinanceBalance, x =>
                {
                    x.NullSubstitute(0m);
                    x.MapFrom(vehicle => vehicle.FinancialAgreement.AccountBalances.OrderByDescending(transaction => transaction.CreatedAt).First().Amount);
                });
        }
    }
}