using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BlingRus.Domain.EnterpriseCollections
{
    public class EnterpriseQueryableWrapper<T> : IQueryable<T>
    {
        private readonly IQueryable<T> _wrappedQueryable;

        public EnterpriseQueryableWrapper(IQueryable<T> wrappedQueryable)
        {
            _wrappedQueryable = wrappedQueryable;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _wrappedQueryable.GetEnumerator();
            //return new EnterpriseEnumerator<T>(_wrappedQueryable);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType => _wrappedQueryable.ElementType;
        public Expression Expression => _wrappedQueryable.Expression;
        public IQueryProvider Provider => _wrappedQueryable.Provider;
    }
}