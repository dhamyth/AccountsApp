using System;
using API.DTOs;
using API.Interfaces;
using API.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Policy = MemberPolicy.RequireAdminOrMemberRole)]
public class AccountBalanceController(IMapper mapper,
        IAccountBalancesRepository accountBalancesRepository): BaseApiController
{
    [HttpGet("balance")]
    public async Task<ActionResult<AccountBalancesGetWithDateDto>> GetAccountBalances()
    {
        var accountBalance = await accountBalancesRepository.GetAccountBalancesAsync();

        if (accountBalance == null) return NotFound("No balance");

        return mapper.Map<AccountBalancesGetWithDateDto>(accountBalance);
        
    }

}
