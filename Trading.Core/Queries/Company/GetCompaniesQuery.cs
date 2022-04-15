using MediatR;
using Trading.Core.Mapper;
using Trading.Core.Models;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;
using Trading.Core.Repositories;

namespace Trading.Core.Query.Company
{
    public class GetCompaniesQuery : AppRequest
    {
    }

    internal class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, Response>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompaniesQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }

        public async Task<Response> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _companyRepository.Get();
                return new Response.Successed<List<CompanyDto>>(res.Select(x => x.ToDomain()).ToList());
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_GET_COMPANIES", ex.Message);
            }
        }
    }
}
