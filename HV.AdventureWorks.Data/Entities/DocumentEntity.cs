using Microsoft.EntityFrameworkCore;
using System;

namespace HV.AdventureWorks.Data.Entities
{
    public class DocumentEntity
    {
        public HierarchyId DocumentNode { get; set; }
        public short DocumentLevel { get; set; }
        public string Title { get; set; }
        public int Owner { get; set; }
        public bool FolderFlag { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Revision { get; set; }
        public int ChangeNumber { get; set; }
        public byte Status { get; set; }
        public string DocumentSummary { get; set; }
        public byte[] File { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}