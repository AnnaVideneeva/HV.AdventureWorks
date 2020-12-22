using HV.AdventureWorks.Data.Entities;
using System;

namespace HV.AdventureWorks.Data.Interfaces
{
    public interface IDocumentsProvider
    {
        DocumentEntity GetByGuid(Guid guid);

        DocumentEntity Create(DocumentEntity entity);
    }
}
