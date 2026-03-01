namespace MaxAuto.WebApi.Domain.Models.Responses;

public sealed class WorkshopResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
    public string? Address { get; set; }
}