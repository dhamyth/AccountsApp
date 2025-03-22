using System;

namespace API.DTOs;

public class AccountBalancesPostDto
{
    public decimal RnD { get; set; }
    public decimal Canteen { get; set; }
    public decimal CeoCar { get; set; }
    public decimal Marketing { get; set; }
    public decimal ParkingFines { get; set; }
}
