using System.Collections.Generic;

public class CommandRequest
{
    public string CommandText { get; set; }
    public Context Context { get; set; }
    public ImageContext ImageContext { get; set; }
}

public class Context
{
    public string ActiveView { get; set; }
    public List<SelectedElement> SelectedElements { get; set; }
}

public class SelectedElement
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
}

public class ImageContext
{
    public string Filename { get; set; }
    public string Base64 { get; set; }
}
