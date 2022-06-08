using System;
using Omikron.SharedKernel.Domain;
using AutoMapper;

namespace Omikron.SharedKernel.ViewModels
{
    public class BaseViewModel<TKey> where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }

        public static implicit operator string(BaseViewModel<TKey> value)
        {
            return value?.ToString();
        }
    }

    public class BaseViewModelProfile : Profile
    {
        public BaseViewModelProfile()
        { 
            CreateMap<BaseEntity<int>, BaseViewModel<int>>().ReverseMap();
            CreateMap<BaseEntity<Guid>, BaseViewModel<Guid>>().ReverseMap();
        }
    }
}