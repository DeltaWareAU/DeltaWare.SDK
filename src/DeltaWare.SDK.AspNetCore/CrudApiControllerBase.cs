using AutoMapper;
using DeltaWare.SDK.Data;
using DeltaWare.SDK.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeltaWare.SDK.AspNetCore
{
    public abstract class CrudApiControllerBase<TDto, TRepository, TEntity, TIdentifier> : ControllerBase where TDto : class where TRepository : IRepositoryBase<TEntity, TIdentifier> where TEntity : Entity<TIdentifier> where TIdentifier : struct
    {
        private readonly ILogger? _logger;

        private readonly IMapper _mapper;

        private readonly TRepository _repository;

        protected CrudApiControllerBase(TRepository repository, IMapper mapper, ILogger? logger = null)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _logger = logger;
        }

        protected abstract Task<int> SaveChangesAsync();

        protected abstract TIdentifier GetIdentifier(TDto data);

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync()
        {
            List<TEntity> entities;

            try
            {
                entities = await _repository.GetAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "An exception was encountered whilst retrieving data from {repository}", typeof(TRepository));

                return StatusCode(500);
            }

            if (entities?.Count == 0)
            {
                return NoContent();
            }

            try
            {
                return Ok(_mapper.MapMany<TEntity, TDto>(entities).ToList());
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "An exception was encountered whilst mapping {sourceType} to {destinationType}", typeof(TEntity), typeof(TDto));

                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAsync(TIdentifier id)
        {
            TEntity entity;

            try
            {
                entity = await _repository.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "An exception was encountered whilst retrieving data from {repository}", typeof(TRepository));

                return StatusCode(500);
            }

            if (entity == null)
            {
                return NotFound();
            }

            try
            {
                return Ok(_mapper.Map<TDto>(entity));
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "An exception was encountered whilst mapping {sourceType} to {destinationType}", typeof(TEntity), typeof(TDto));

                return StatusCode(500);
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync([FromBody] TDto? data)
        {
            if (data is null)
            {
                _logger?.LogDebug("Could not create {type} as it was not provided", typeof(TDto));

                return BadRequest($"A {typeof(TDto)} must be provided");
            }

            TEntity entity;

            try
            {
                entity = _mapper.Map<TEntity>(data);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "An exception was encountered whilst mapping {sourceType} to {destinationType}", typeof(TEntity), typeof(TDto));

                return StatusCode(500);
            }

            try
            {
                entity = await _repository.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "An exception was encountered whilst adding data to {repository}", typeof(TRepository));

                return StatusCode(500);
            }

            try
            {
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "An exception was encountered whilst saving changes");

                return StatusCode(500);
            }

            return Created(HttpContext.Request.Path, entity.Id);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(TIdentifier id)
        {
            TEntity entity;

            try
            {
                entity = await _repository.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "An exception was encountered whilst retrieving data from {repository}", typeof(TRepository));

                return StatusCode(500);
            }

            if (entity == null)
            {
                return NotFound();
            }

            try
            {
                _repository.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "An exception was encountered whilst deleting data from {repository}", typeof(TRepository));

                return StatusCode(500);
            }

            try
            {
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "An exception was encountered whilst saving changes");

                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPut]
        public virtual async Task<IActionResult> UpdateAsync(TDto? data)
        {
            if (data is null)
            {
                _logger?.LogDebug("Could not create {type} as it was not provided", typeof(TDto));

                return BadRequest($"A {typeof(TDto)} must be provided");
            }

            TEntity entity;

            try
            {
                entity = await _repository.GetAsync(GetIdentifier(data));
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "An exception was encountered whilst retrieving data from {repository}", typeof(TRepository));

                return StatusCode(500);
            }

            try
            {
                _mapper.Map(data, entity);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "An exception was encountered whilst mapping {sourceType} to {destinationType}", typeof(TEntity), typeof(TDto));

                return StatusCode(500);
            }

            try
            {
                _repository.Update(entity);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "An exception was encountered whilst updating data from {repository}", typeof(TRepository));

                return StatusCode(500);
            }

            try
            {
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "An exception was encountered whilst saving changes");

                return StatusCode(500);
            }

            return Ok();
        }
    }
}