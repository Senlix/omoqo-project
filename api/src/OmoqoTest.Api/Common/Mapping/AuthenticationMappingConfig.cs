using Mapster;
using OmoqoTest.Application.Authentication.Commands.Register;
using OmoqoTest.Application.Authentication.Common;
using OmoqoTest.Contracts.Authentication;

namespace OmoqoTest.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, AuthenticationResult>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}