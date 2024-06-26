using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace TollBooth
{
    public class ExportLicensePlates
    {
        [FunctionName("ExportLicensePlates")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
            ILogger log)
        {
            int exportedCount = 0;
            log.LogInformation("Finding license plate data to export");

            var databaseMethods = new DatabaseMethods(log);
            var licensePlates = await databaseMethods.GetLicensePlatesToExport();
            if (licensePlates.Any())
            {
                log.LogInformation($"Retrieved {licensePlates.Count} license plates");
                var fileMethods = new FileMethods(log);
                var uploaded = await fileMethods.GenerateAndSaveCsv(licensePlates);
                if (uploaded)
                {
                    await databaseMethods.MarkLicensePlatesAsExported(licensePlates);
                    exportedCount = licensePlates.Count;
                    log.LogInformation("Finished updating the license plates");
                }
                else
                {
                    log.LogInformation(
                        "Export file could not be uploaded. Skipping database update that marks the documents as exported.");
                }

                log.LogInformation($"Exported {exportedCount} license plates");
            }
            else
            {
                log.LogWarning("No license plates to export");
            }

            if (exportedCount == 0)
            {
                return new NoContentResult();
            }
            else
            {
                return new OkObjectResult($"Exported {exportedCount} license plates");
            }
        }
    }
}
