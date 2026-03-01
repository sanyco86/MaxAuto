namespace MaxAuto.WebApi.Domain.Models.Requests;

public sealed class WorkshopRequest
{
    public required string Name { get; set; }
    public required string Location { get; set; }
    public string? Address { get; set; }
}
