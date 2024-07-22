using System;
using System.Security.Permissions;

public class Device
{
    public int Id { get; set; }

    public int Branch_Id { get; set; }

    public string Ip { get; set; }

    public string Port { get; set; }

    public DateTime Last_Reading_Time { get; set; }

    public string type { get; set; }

    public string Name { get; set; }

    public string Model { get; set; }

    public string UserName { get; set; }

    public string PassWord { get; set; }

    public DateTime Installation_Date { get; set; }

    public string Life_Time { get; set; }

    public string Maunfacturer { get; set; }

    public string Made_In_Counry { get; set; }

    public int State { get; set; }
}
