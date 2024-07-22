using App.Models;
using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class ReadingLKP
{
    public int Id { get; set; }
    public string Name { get; set; }= String.Empty;
    public string Unit { get; set; } = String.Empty;
    public virtual ICollection<ActualReadings> ActualReadings { get; set; }

}
