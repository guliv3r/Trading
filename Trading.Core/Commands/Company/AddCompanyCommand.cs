using AutoMapper;
using MediatR;
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
        private readonly IMapper _mapper;

        public AddCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _companyRepository.Add(_mapper.Map<Infrastructure.Database.Models.Company>(request.Company));
                return new Response.Successed<CompanyDto>(_mapper.Map<CompanyDto>(res));
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_ADD_COMPANY", ex.Message);
            }
        }
    }
}
