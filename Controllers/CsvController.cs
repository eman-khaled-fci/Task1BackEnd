using System.Globalization;
using CsvHelper;
using EmanTask1.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

[Route("api/[controller]")]
[ApiController]
public class CsvController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public CsvController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadCsv(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");

            var filePath = Path.Combine(_environment.ContentRootPath, "uploads", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("File uploaded successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    [HttpGet("convert-to-excel")]
    public IActionResult ConvertToExcel()
    {
        var csvDataList = GetCsvDataFromFile();

        var excelPackage = new ExcelPackage();
        var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

        worksheet.Cells.LoadFromCollection(csvDataList, true);

        var excelBytes = excelPackage.GetAsByteArray();

        var fileName = "converted.xlsx";

        Response.Headers.Add("Content-Disposition", new[] { $"attachment; filename={fileName}" });

        return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }




    [HttpGet("get-csv-data")]
    public List<CsvData> GetCsvDataFromFile()
    {
        var filePath = Path.Combine(_environment.ContentRootPath, "uploads", "csvFile.csv");

        try
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<CsvData>().ToList();
            }
        }
        catch (Exception ex)
        {

            return new List<CsvData>();
        }
    }
}
