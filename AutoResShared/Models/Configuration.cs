using System;
public class Configuration()
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Path { get; set; }
    public string Resolution { get; set; } 
    public int Scale { get; set; }
}