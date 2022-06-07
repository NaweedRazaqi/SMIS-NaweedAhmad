using System;
using System.Collections.Generic;

namespace Clean.UI.ssModel
{
    public partial class Documents
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadDate { get; set; }
        public string ObjectSchema { get; set; }
        public string ObjectName { get; set; }
        public short RecordId { get; set; }
        public string Root { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string EncryptionKey { get; set; }
        public short? StatusId { get; set; }
        public short? ScreenId { get; set; }
        public DateTime? LastDownloadDate { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentSource { get; set; }
        public DateTime? DocumentDate { get; set; }
        public short DocumentTypeId { get; set; }
        public short CreatedBy { get; set; }
    }
}
