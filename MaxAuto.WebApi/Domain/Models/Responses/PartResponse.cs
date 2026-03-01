namespace MaxAuto.WebApi.Domain.Models.Responses;

public sealed class PartResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
