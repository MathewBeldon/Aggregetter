using Aggregetter.Aggre.Application.Contracts.Identity;
using Aggregetter.Aggre.Application.Models.Authentication;
using AutoMapper;
using Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Accounts.Queries.AuthenticateAccount
{
    public sealed class AuthenticateAccountQueryHandler : IRequestHandler<AuthenticateAccountQuery, AuthenticateAccountQueryResponse>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        public AuthenticateAccountQueryHandler(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public async ValueTask<AuthenticateAccountQueryResponse> Handle(AuthenticateAccountQuery request, CancellationToken cancellationToken)
        {
            var authenticationRequest = _mapper.Map<AuthenticationRequest>(request);
            var authenticationResponse = await _authenticationService.AuthenticateAsync(authenticationRequest);

            return new AuthenticateAccountQueryResponse(authenticationResponse);
        }
    }
}
