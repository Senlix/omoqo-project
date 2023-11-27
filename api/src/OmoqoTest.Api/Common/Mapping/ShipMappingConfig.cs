using Mapster;
using OmoqoTest.Application.Ships.Commands.Add;
using OmoqoTest.Application.Ships.Commands.Delete;
using OmoqoTest.Application.Ships.Common;
using OmoqoTest.Application.Ships.Queries.Get;
using OmoqoTest.Application.Ships.Queries.List;
using OmoqoTest.Contracts.Ships;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Api.Common.Mapping
{
    public class ShipMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ShipListRequest, ShipListQuery>();

            config.NewConfig<int, ShipGetQuery>()
                .Map(dest => dest.Id, src => src);

            config.NewConfig<ShipAddRequest, ShipAddCommand>();
            config.NewConfig<Ship, ShipResult>();

            config.NewConfig<int, ShipRemoveCommand>()
                .Map(dest => dest.Id, src => src);
        }
    }
}