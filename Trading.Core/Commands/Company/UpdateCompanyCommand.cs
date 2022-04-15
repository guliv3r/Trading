using MediatR;
using Trading.Core.Mapper;
using Trading.Core.Models;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;
using Trading.Core.Repositories;

namespace Trading.Core.Commands.Company
{
    public class UpdateMarketCommand : AppRequest
    {
        public UpdateMarketCommand(CompanyDto company)
        {
            Company = company;
        }

        public CompanyDto Company { get; set; }
    }

    internal class UpdateCompanyCommandHandler : IRequestHandler<UpdateMarketCommand, Response>
    {
        private readonly ICompanyRepository _companyRepository;

        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }

        public async Task<Response> Handle(UpdateMarketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _companyRepository.Update(request.Company.ToEntity());
                return new Response.Successed<CompanyDto>(res.ToDomain());
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_UPDATE_COMPANY", ex.Message);
            }
        }
    }
}
