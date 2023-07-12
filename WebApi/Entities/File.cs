using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class File
{
    public int Id { get; set; }

    public byte[] Data { get; set; } = null!;

    public virtual ICollection<FileMetadatum> FileMetadata { get; set; } = new List<FileMetadatum>();
}
