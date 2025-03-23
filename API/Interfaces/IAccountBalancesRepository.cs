using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IAccountBalancesRepository
{
    Task<bool> SaveAllAsync();
    Task<AccountBalances> AddAsync(AccountBalancesPostDto accountBalancesPostDto);
    Task<AccountBalances> GetAccountBalancesAsync();
}
