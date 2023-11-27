using Microsoft.AspNetCore.Mvc;
using MediatR;
using MapsterMapper;
using OmoqoTest.Application.Ships.Queries.List;
using OmoqoTest.Contracts.Ships;
using OmoqoTest.Domain.Common;
using OmoqoTest.Domain.Entities;
using OmoqoTest.Application.Ships.Common;
using OmoqoTest.Application.Ships.Commands.Add;
using OmoqoTest.Application.Ships.Commands.Delete;
using OmoqoTest.Application.Ships.Queries.Get;
using OmoqoTest.Application.Ships.Commands.Update;

namespace OmoqoTest.Api.Controllers
{
    [Route("[controller]")]
    public class ShipsController(ISender mediator, IMapper mapper) : MediatRController(mediator, mapper)
    {

        /// <summary>
        /// Get all Ships
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ShipListResult>>> List([FromQuery] ShipListRequest request)
        {
            return await ExecuteAsync<ShipListQuery, ShipListRequest, PaginatedList<Ship>, PaginatedList<ShipListResult>>(request);
        }

        /// <summary>
        /// Get a Ship
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ShipListResult>> Get(int id)
        {
            return await ExecuteAsync<ShipGetQuery, int, Ship, ShipListResult>(id);
        }

        /// <summary>
        /// Add a Ship
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ShipResult>> Add(ShipAddRequest request)
        {
            return await ExecuteAsync<ShipAddCommand, ShipAddRequest, Ship, ShipResult>(request);
        }

        /// <summary>
        /// Update a Ship
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ShipResult>> Update(ShipUpdateRequest request)
        {
            return await ExecuteAsync<ShipUpdateCommand, ShipUpdateRequest, Ship, ShipResult>(request);
        }

        /// <summary>
        /// Remove a Ship
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ShipResult>> Remove(int id)
        {
            return await ExecuteAsync<ShipRemoveCommand, int, Ship, ShipResult>(id, true);
        }

        /// <summary>
        /// Remove a List of Ships
        /// </summary>
        /// <param name="shipRemoveRequest"></param>
        /// <returns></returns>        
        [HttpDelete]
        public async Task<ActionResult<bool>> Remove([FromBody] ShipRemoveRequest shipRemoveRequest)
        {

            return await ExecuteAsync<ShipBulkRemoveCommand, ShipRemoveRequest, bool>(shipRemoveRequest, true);
        }
    }
}