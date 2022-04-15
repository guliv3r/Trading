using AutoMapper;
using MediatR;
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
        private readonly IMapper _mapper;

        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response> Handle(UpdateMarketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _companyRepository.Update(_mapper.Map<Infrastructure.Database.Models.Company>(request.Company));
                return new Response.Successed<CompanyDto>(_mapper.Map<CompanyDto>(res));
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_UPDATE_COMPANY", ex.Message);
            }
        }
    }
}
