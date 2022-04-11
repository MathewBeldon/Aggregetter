using Aggregetter.Aggre.Application.Contracts.Identity;
using Aggregetter.Aggre.Application.Models.Authentication;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Accounts.Commands.CreateAccount
{
    internal class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountCommandResponse>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public async Task<CreateAccountCommandResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var registrationRequest = _mapper.Map<RegistrationRequest>(request);
            var registrationResponse = await _authenticationService.RegisterAsync(registrationRequest);

            return new CreateAccountCommandResponse(registrationResponse);
        }
    }
}
