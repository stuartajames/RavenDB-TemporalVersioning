﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Raven.Abstractions.Data;
using Raven.Client.Indexes;
using Raven.Client.Linq;

namespace Raven.Client.Bundles.TemporalVersioning
{
    /// <summary>
    /// Interface for temporal operations that can be applied to a document session
    /// </summary>
    public interface ISyncTemporalSessionOperation
    {
        /// <summary>
        /// Loads the specified entity with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        T Load<T>(string id);

        /// <summary>
        /// Loads the specified entities with the specified ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        T[] Load<T>(params string[] ids);

        /// <summary>
        /// Loads the specified entities with the specified ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        T[] Load<T>(IEnumerable<string> ids);

        /// <summary>
        /// Loads the specified entities with the specified id after applying
        /// conventions on the provided id to get the real document id.
        /// </summary>
        /// <remarks>
        /// This method allows you to call:
        /// Load{Post}(1)
        /// And that call will internally be translated to 
        /// Load{Post}("posts/1");
        /// 
        /// Or whatever your conventions specify.
        /// </remarks>
        T Load<T>(ValueType id);

        /// <summary>
        /// Queries the specified index using Linq.
        /// </summary>
        /// <typeparam name="T">The result of the query</typeparam>
        /// <param name="indexName">Name of the index.</param>
        IRavenQueryable<T> Query<T>(string indexName);

        /// <summary>
        /// Dynamically queries RavenDB using LINQ
        /// </summary>
        /// <typeparam name="T">The result of the query</typeparam>
        IRavenQueryable<T> Query<T>();

        /// <summary>
        /// Queries the index specified by <typeparamref name="TIndexCreator"/> using Linq.
        /// </summary>
        /// <typeparam name="T">The result of the query</typeparam>
        /// <typeparam name="TIndexCreator">The type of the index creator.</typeparam>
        /// <returns></returns>
        IRavenQueryable<T> Query<T, TIndexCreator>()
            where TIndexCreator : AbstractIndexCreationTask, new();

        /// <summary>
        /// Begin a load while including the specified path 
        /// </summary>
        /// <param name="path">The path.</param>
        ITemporalLoaderWithInclude<object> Include(string path);

        /// <summary>
        /// Begin a load while including the specified path 
        /// </summary>
        /// <param name="path">The path.</param>
        ITemporalLoaderWithInclude<T> Include<T>(Expression<Func<T, object>> path);

        /// <summary>
        /// Begin a load while including the specified path 
        /// </summary>
        /// <param name="path">The path.</param>
        ITemporalLoaderWithInclude<T> Include<T, TInclude>(Expression<Func<T, object>> path);

        /// <summary>
        /// Stores the specified entity with the specified etag
        /// </summary>
        void Store(object entity, Etag etag);

        /// <summary>
        /// Stores the specified entity with the specified etag, under the specified id
        /// </summary>
        void Store(object entity, Etag etag, string id);

        /// <summary>
        /// Stores the specified dynamic entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Store(dynamic entity);

        /// <summary>
        /// Stores the specified dynamic entity, under the specified id
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="id">The id to store this entity under. If other entity exists with the same id it will be overridden.</param>
        void Store(dynamic entity, string id);
    }
}
