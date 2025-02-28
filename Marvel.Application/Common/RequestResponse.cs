using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Marvel.Application.Common;

public record RequestResponse<T>
{
    public string Title { get;set;} = string.Empty;
    public string Detail  { get;set;} = string.Empty;
    public T Data { get;set;} = default(T)!;
    public int StatusCode { get;set;} = (int)HttpStatusCode.OK;
}