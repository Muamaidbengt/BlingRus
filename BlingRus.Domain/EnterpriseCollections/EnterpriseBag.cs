using System;
using System.Threading;

namespace BlingRus.Domain.EnterpriseCollections
{
    public abstract class EnterpriseBag
    {
        private readonly Random _enterpriseGenerator = new Random();
        protected void ActivateTheEnterprise()
        {
            var enterpriseCoefficient = _enterpriseGenerator.Next(1, 16) / 10d;
            enterpriseCoefficient *= enterpriseCoefficient;
            var enterpriseFactor = (int) Math.Pow(10, enterpriseCoefficient);

            // Wow! Very enterprise! Such scalable!
            Thread.Sleep(enterpriseFactor);
        }
    }
}