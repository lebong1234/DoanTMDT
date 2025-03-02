using System;
using System.Collections.Generic;

namespace Webbanson.Data;

public partial class BlogPost
{
    public string PostId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateOnly PostDate { get; set; }

    public string? ImageUrl { get; set; }
}
