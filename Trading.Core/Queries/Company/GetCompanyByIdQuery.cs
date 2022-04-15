using MediatR;
using Trading.Core.Mapper;
using Trading.Core.Models;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;
using Trading.Core.Repositories;

namespace Trading.Core.Query.Company
{
    public class GetCompanyByIdQuery : AppRequest
    {
        public GetCompanyByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    internal class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Response>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }

        public async Task<Response> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _companyRepository.GetById(request.Id);
                return new Response.Successed<CompanyDto>(res.ToDomain());
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_GET_COMPANY", ex.Message);
            }
        }
    }
}
