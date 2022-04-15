using AutoMapper;
using MediatR;
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
        private readonly IMapper _mapper;

        public GetCompaniesQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _companyRepository.Get();
                var aa = _mapper.Map<List<Trading.Infrastructure.Database.Models.Company>, List<CompanyDto>>(res);
                return new Response.Successed<List<CompanyDto>>(aa);
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_GET_COMPANIES", ex.Message);
            }
        }
    }
}
