using System;
using System.ComponentModel.DataAnnotations;

namespace HV.AdventureWorks.Services.Models
{
    public class Document
    {
        public string  DocumentNode { get; set; }

        public short DocumentLevel { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public int Owner { get; set; }

        [Required]
        public bool FolderFlag { get; set; }

        [Required]
        [StringLength(400)]
        public string FileName { get; set; }

        [Required]
        [StringLength(8)]
        public string FileExtension { get; set; }

        [Required]
        [StringLength(5)]
        public string Revision { get; set; }

        [Required]
        public int ChangeNumber { get; set; }

        [Required]
        public short Status { get; set; }

        public string DocumentSummary { get; set; }

        public byte[] File { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}