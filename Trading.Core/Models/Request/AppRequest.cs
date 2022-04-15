using MediatR;

namespace Trading.Core.Models.Request
{
    public class AppRequest : BaseAppRequest, IRequest<Response.Response>
    {

    }
}
