namespace MaxAuto.WebApi.Domain.Models.Responses;

public sealed class WorkResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
