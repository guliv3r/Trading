using MediatR;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;
using Trading.Core.Repositories;

namespace Trading.Core.Commands.Company
{
    public class RemoveCompanyCommand : AppRequest
    {
        public RemoveCompanyCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    internal class RemoveCompanyCommandHandler : IRequestHandler<RemoveCompanyCommand, Response>
    {
        private readonly ICompanyRepository _companyRepository;

        public RemoveCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository is IMarketRepository ? companyRepository : throw new ArgumentNullException(nameof(companyRepository));
        }

        public async Task<Response> Handle(RemoveCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _companyRepository.Remove(request.Id);
                return new Response.Successed<bool>(true);
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_REMOVE_COMPANY", ex.Message);
            }
        }
    }
}
