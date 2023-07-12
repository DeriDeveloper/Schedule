using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class FileMetadatum
{
    public int Id { get; set; }

    public int FileId { get; set; }

    public string MimeType { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual File File { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
