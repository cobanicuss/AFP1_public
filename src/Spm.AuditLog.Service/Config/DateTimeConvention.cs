using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Spm.AuditLog.Service.Config
{
    public class DateTimeConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Type == typeof(DateTime) || x.Type == typeof(DateTime?));
        }
        public void Apply(IPropertyInstance instance)
        {
            instance.CustomSqlType("DateTime2"); //specify that the sql column is DateTime2//
            instance.CustomType("DateTime2"); //set the nhibernate type as well//
        }
    }
}