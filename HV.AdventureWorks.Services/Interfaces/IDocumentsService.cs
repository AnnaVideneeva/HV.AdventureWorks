using System.Threading.Tasks;

namespace HV.AdventureWorks.Services.Interfaces
{
    public interface IDocumentsService
    {
        Task UploadAsync(string fileName, byte[] file, string fileMimeType);
    }
}
