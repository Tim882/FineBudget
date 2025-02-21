﻿using DTOs.Requests;
using DTOs.Responses;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Interfaces
{
    public interface IAssetDataService
    {
        public Task<List<AssetResponseDto>> GetAllAsync();
        public Task<AssetResponseDto> GetByIdAsync(Guid id);
        public Task<AssetResponseDto> CreateAsync(AssetRequestDto account);
        public Task<AssetResponseDto> UpdateAsync(Guid id, AssetRequestDto account);
        public Task<bool> DeleteAsync(Guid id);
    }
}
