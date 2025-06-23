namespace XPointBE.Dtos.Ciudad;

public class CiudadResponse
{
    public int limit { get; set; }
    public int skip { get; set; }
    public int total { get; set; }
    public List<Models.Ciudad> data { get; set; }
}
