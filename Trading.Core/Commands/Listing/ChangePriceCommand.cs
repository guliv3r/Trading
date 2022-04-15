using MediatR;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;
using Trading.Core.Repositories;

namespace Trading.Core.Commands.Listing
{
    public class ChangePriceCommand : AppRequest
    {
        public ChangePriceCommand(ChangePriceRequest changePriceRequest)
        {
            ChangePriceRequest = changePriceRequest;
        }

        public ChangePriceRequest ChangePriceRequest { get; set; }

        internal class ChangePriceCommandHandler : IRequestHandler<ChangePriceCommand, Response>
        {
            private readonly ICompanyRepository _companyRepository;

            public ChangePriceCommandHandler(ICompanyRepository companyRepository)
            {
                _companyRepository = companyRepository is null ? throw new ArgumentNullException(nameof(companyRepository)) : companyRepository;
            }

            public async Task<Response> Handle(ChangePriceCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _companyRepository.ChangePrice(request.ChangePriceRequest);
                    return new Response.Successed<bool>(true);
                }
                catch (Exception ex)
                {
                    return new Response.Failed("ERROR_CHANGE_PRICE", ex.Message);
                }
            }
        }
    }
}
