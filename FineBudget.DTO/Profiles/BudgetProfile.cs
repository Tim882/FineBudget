using System;
using AutoMapper;
using DTOs.Requests;
using DTOs.Responses;
using Models.DbModels.MainModels;

namespace DTOs.Profiles
{
	public class BudgetProfile : Profile
	{
		public BudgetProfile()
		{
            CreateMap<AccountRequestDto, Account>();
			CreateMap<Account, AccountResponseDto>();
			CreateMap<AssetRequestDto, Asset>();
			CreateMap<Asset, AssetResponseDto>();
			CreateMap<LiabilityRequestDto, Liability>();
			CreateMap<Liability, LiabilityResponseDto>();
			CreateMap<CostRequestDto, Cost>();
			CreateMap<Cost, CostResponseDto>();
			CreateMap<IncomeRequestDto, Income>();
			CreateMap<Income, IncomeResponseDto>();
        }
	}
}

