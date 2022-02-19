using DeltaWare.SDK.Core.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace AutoMapper
{
    public static class MapperExtensions
    {
        /// <summary>
        /// Executes a mapping on the source objects to new destination objects.
        /// </summary>
        /// <returns>Returns a mapper <see cref="IEnumerable{T}"/>.</returns>
        public static IEnumerable<TDestination> MapMany<TSource, TDestination>(this IMapper mapper, IEnumerable<TSource> source)
        {
            return source
                .Select(s => mapper.Map<TDestination>(s));
        }

        /// <summary>
        /// Executes a mapping on the source objects to new destination objects with the supplied
        /// mapping options.
        /// </summary>
        /// <returns>Returns a mapper <see cref="IEnumerable{T}"/>.</returns>
        public static IEnumerable<TDestination> MapMany<TSource, TDestination>(this IMapper mapper, IEnumerable<TSource> source, Action<IMappingOperationOptions<object, TDestination>> opts)
        {
            return source
                .Select(s => mapper.Map(s, opts));
        }

        /// <summary>
        /// Executes a mapping on a <see cref="IPagedResult{TResult}"/> data source objects to new
        /// destination objects.
        /// </summary>
        /// <returns>Returns a mapped <see cref="IPagedResult{TResult}"/></returns>
        public static IPagedResult<TDestination> MapPagedResult<TSource, TDestination>(this IMapper mapper, IPagedResult<TSource> source) where TDestination : class where TSource : class
        {
            return new PagedResult<TDestination>
            (
                mapper.MapMany<TSource, TDestination>(source.Data).ToArray(),
                source.TotalRecords,
                source.FilteredRecords
            );
        }

        /// <summary>
        /// Executes a mapping on a <see cref="IPagedResult{TResult}"/> data source objects to new
        /// destination objects with the supplied mapping options.
        /// </summary>
        /// <returns>Returns a mapped <see cref="IPagedResult{TResult}"/></returns>
        public static IPagedResult<TDestination> MapPagedResult<TSource, TDestination>(this IMapper mapper, IPagedResult<TSource> source, Action<IMappingOperationOptions<object, TDestination>> opts) where TDestination : class where TSource : class
        {
            return new PagedResult<TDestination>
            (
                mapper.MapMany(source.Data, opts).ToArray(),
                source.TotalRecords,
                source.FilteredRecords
            );
        }
    }
}