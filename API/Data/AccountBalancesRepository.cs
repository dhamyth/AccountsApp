using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;

namespace API.Data;

public class AccountBalancesRepository(DataContext context, IMapper mapper) : IAccountBalancesRepository
{
    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<AccountBalances> AddAsync(AccountBalancesPostDto accountBalancesPostDto)
    {
        var accountBalances = mapper.Map<AccountBalances>(accountBalancesPostDto);
        await context.AccountBalances.AddAsync(accountBalances);
        return  accountBalances;
    }

}
