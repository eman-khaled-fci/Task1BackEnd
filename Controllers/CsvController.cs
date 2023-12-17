using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Microsoft.VisualBasic.FileIO;

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
            var filePath = Path.Combine(_environment.ContentRootPath, "uploads", "csvFile.csv");
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
    public List<Dictionary<string, string>> GetCsvDataFromFile()
    {
        var filePath = Path.Combine(_environment.ContentRootPath, "uploads", "csvFile.csv");
        try
        {
            var csvDataList = new List<Dictionary<string, string>>();
            using (var parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                var headers = parser.ReadFields();
                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();
                    var csvData = new Dictionary<string, string>();
                    // Populate the dictionary using headers as keys
                    for (var i = 0; i < headers.Length; i++)
                    {
                        csvData[headers[i]] = fields[i];
                    }
                    csvDataList.Add(csvData);
                }
            }
            Console.WriteLine(csvDataList);
            return csvDataList;
        }
        catch (Exception ex)
        {
            return new List<Dictionary<string, string>>();
        }
    }


}
