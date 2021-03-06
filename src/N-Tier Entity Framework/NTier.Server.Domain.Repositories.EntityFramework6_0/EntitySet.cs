﻿// Copyright (c) Trivadis. All rights reserved. See license.txt in the project root for license information.

using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using NTier.Common.Domain.Model;
using NTier.Server.Domain.Repositories.Linq;

namespace NTier.Server.Domain.Repositories.EntityFramework
{
    internal sealed class EntitySet<TEntity> : IEntitySet<TEntity> 
        where TEntity : Entity
    {
        #region Private fields

        private readonly ObjectSet<TEntity> _objectSet;
        
        #endregion Private fields

        #region Constructor

        internal EntitySet(ObjectSet<TEntity> objectSet)
        {
            _objectSet = objectSet;
        }

        #endregion Constructor

        #region IEntitySet<T>

        public void ApplyChanges(TEntity entity)
        {
            _objectSet.ApplyChanges(entity);
        }

        public void Attach(TEntity entity)
        {
            _objectSet.Attach(entity);
        }
        
        public void Detach(TEntity entity)
        {
            _objectSet.Detach(entity);
        }
        
        public void Add(TEntity entity)
        {
            entity.MarkAsAdded();
            _objectSet.AddObject(entity);
        }
        
        public void Remove(TEntity entity)
        {
            entity.MarkAsDeleted();
            _objectSet.DeleteObject(entity);
        }

        public IEntityQueryable<TEntity> AsQueryable()
        {
            return new EntityQueryable<TEntity>(_objectSet, ExpressionVisitor.Visit, false);
        }

        public IEntityQueryable<TEntity> AsNoTrackingQueryable()
        {
            return new EntityQueryable<TEntity>(_objectSet, ExpressionVisitor.Visit, true);
        }

        #endregion IEntitySet<T>

        #region IEnumerator<T>

        public IEnumerator<TEntity> GetEnumerator()
        {
            return ((IEnumerable<TEntity>)_objectSet).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerator<T>
    }
}
