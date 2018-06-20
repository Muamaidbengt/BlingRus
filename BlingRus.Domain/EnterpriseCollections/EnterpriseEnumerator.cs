using System.Collections;
using System.Collections.Generic;

namespace BlingRus.Domain.EnterpriseCollections
{
    public sealed class EnterpriseEnumerator<T> : EnterpriseBag, IEnumerator<T>
    {
        private readonly IEnumerator<T> _innerEnumerator;
        

        public EnterpriseEnumerator(IEnumerable<T> thing)
        {
            _innerEnumerator = thing.GetEnumerator();
        }

        public bool MoveNext()
        {
            ActivateTheEnterprise();
            return _innerEnumerator.MoveNext();
        }

        public void Reset()
        {
            _innerEnumerator.Reset();
        }

        public T Current => _innerEnumerator.Current;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            _innerEnumerator.Dispose();
        }
    }
}