using HV.AdventureWorks.Services.Models;
using System;
using System.Threading.Tasks;

namespace HV.AdventureWorks.Services.Interfaces
{
    public interface IDocumentsService
    {
        Document GetByGuid(Guid guid);
        Task UploadToBlobAsync(string fileName, byte[] file, string fileMimeType, string documentNode, string fileExtension);
        Task UploadToDatabaseAsync(string fileName, byte[] file, string documentNode, string fileExtension);
    }
}
