using System;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles:Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser,MemberDto>();
        CreateMap<RegisterDto, AppUser>();
        CreateMap<Dictionary<string, decimal>, AccountBalancesPostDto>()
                .ForMember(dest => dest.RnD, opt => opt.MapFrom(src => src["R&D"]))
                .ForMember(dest => dest.Canteen, opt => opt.MapFrom(src => src["Canteen"]))
                .ForMember(dest => dest.CeoCar, opt => opt.MapFrom(src => src["CEO’s car"]))
                .ForMember(dest => dest.Marketing, opt => opt.MapFrom(src => src["Marketing"]))
                .ForMember(dest => dest.ParkingFines, opt => opt.MapFrom(src => src["Parking fines"]));
        CreateMap<AccountBalancesPostDto,AccountBalances>();
        CreateMap<AccountBalances,AccountBalancesGetWithDateDto>();
    }
}
