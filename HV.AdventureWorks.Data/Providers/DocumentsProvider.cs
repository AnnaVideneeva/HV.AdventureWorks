using HV.AdventureWorks.Core.Data.Interfaces;
using System;
using System.Linq;
using HV.AdventureWorks.Data.Entities;
using HV.AdventureWorks.Data.Interfaces;

namespace HV.AdventureWorks.Data.Providers
{
    public class DocumentsProvider : IDocumentsProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentsProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public DocumentEntity GetByGuid(Guid guid)
        {
            return _unitOfWork.Repository<DocumentEntity>()
                .GetAsNoTracking()
                .FirstOrDefault(x => x.RowGuid == guid);
        }

        public DocumentEntity Create(DocumentEntity entity)
        {
            _unitOfWork.Repository<DocumentEntity>().Add(entity);
            _unitOfWork.SaveChanges();

            return entity;
        }
    }
}
