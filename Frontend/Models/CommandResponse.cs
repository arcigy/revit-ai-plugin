using System.Collections.Generic;

public class CommandResponse
{
    public Workflow Workflow { get; set; }
    public string[] Errors { get; set; }
}

public class Workflow
{
    public List<ActionStep> Steps { get; set; }
}
