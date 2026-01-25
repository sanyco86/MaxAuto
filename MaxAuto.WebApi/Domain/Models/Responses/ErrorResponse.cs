namespace MaxAuto.WebApi.Domain.Models.Responses;

/// <summary>
/// Represents an error response.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Gets or sets the title of the error.
    /// </summary>
    public string Titel { get; set; }
    
    /// <summary>
    /// Gets or sets the status code of the error.
    /// </summary>
    public int StatusCode { get; set; }
    
    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public string Message { get; set; }
}
