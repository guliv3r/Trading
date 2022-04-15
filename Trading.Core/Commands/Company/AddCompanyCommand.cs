using MediatR;
using Trading.Core.Mapper;
using Trading.Core.Models;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;
using Trading.Core.Repositories;

namespace Trading.Core.Commands.Company
{
    public class AddCompanyCommand : AppRequest
    {
        public AddCompanyCommand(CompanyDto company)
        {
            Company = company;
        }

        public CompanyDto Company { get; set; }
    }

    internal class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, Response>
    {
        private readonly ICompanyRepository _companyRepository;

        public AddCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }

        public async Task<Response> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _companyRepository.Add(request.Company.ToEntity());
                return new Response.Successed<CompanyDto>(res.ToDomain());
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_ADD_COMPANY", ex.Message);
            }
        }
    }
}
