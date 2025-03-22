using System.Text;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace API.Controllers;

public class FileUploadController(IMapper mapper,
        IAccountBalancesRepository accountBalancesRepository) : BaseApiController
{
    [HttpPost("upload")]
    public async Task<ActionResult<AccountBalancesGetWithDateDto>> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        using var stream = new MemoryStream();

        await file.CopyToAsync(stream);
        stream.Position = 0;

        var sourceDict = new Dictionary<string, decimal>();

        if (fileExtension == ".xlsx" || fileExtension == ".xls")
        {
            // Process Excel file with EPPlus
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets[0]; // First sheet

            if (worksheet == null || worksheet.Dimension == null)
                return BadRequest("Excel worksheet has no cells");

            int rowCount = worksheet.Dimension?.Rows ?? 0;

            if (rowCount != 5)
                return BadRequest("Excel file must have 5 rows");

            for (int row = 1; row <= rowCount; row++)
            {
                var cells = worksheet.Cells[row, 1, row, worksheet.Dimension!.Columns];
                var cellValues = cells.Select(c => c.Text).
                    Where(t => !string.IsNullOrEmpty(t)).ToList();

                if (cellValues.Count != 2)
                    return BadRequest("Excel file must have 2 columns");

                string category = string.Join(" ", cellValues.Take(cellValues.Count - 1));
                string amountStr = cellValues.Last().Replace(",", "");

                if (decimal.TryParse(amountStr, out decimal amount))
                {
                    sourceDict.Add(category, amount);
                }
                else
                {
                    return BadRequest($"Account: {category} has invalid amount");
                }
            }
        }
        else if (fileExtension == ".txt" || fileExtension == ".tsv")
        {
            // Process tab-separated file
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 2) continue;

                    string category = string.Join(" ", parts.Take(parts.Length - 1));
                    string amountStr = parts[^1].Replace(",", "");

                    if (decimal.TryParse(amountStr, out decimal amount))
                    {
                        if (category.Contains("CEO")) category = "CEO’s car";
                        sourceDict.Add(category, amount);
                    }
                    else
                    {
                        return BadRequest($"Account: {category} has invalid amount");
                    }
                }
            }
        }

        var accountBalancesPostDto = mapper.Map<AccountBalancesPostDto>(sourceDict);

        var accountBalance = await accountBalancesRepository.AddAsync(accountBalancesPostDto);
        if(await accountBalancesRepository.SaveAllAsync())
        {
            return Ok(mapper.Map<AccountBalancesGetWithDateDto>(accountBalance));
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "Account balance save unsuccessful");
        }

       
    }

}
