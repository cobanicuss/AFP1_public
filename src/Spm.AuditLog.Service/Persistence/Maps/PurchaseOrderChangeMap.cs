﻿using FluentNHibernate.Mapping;
using Spm.AuditLog.Service.Domain;

namespace Spm.AuditLog.Service.Persistence.Maps
{
    public class PurchaseOrderChangeMap : ClassMap<PurchaseOrderChange>
    {
        public PurchaseOrderChangeMap()
        {
            Table(Constants.PurchaseOrderChangeTableName);
            Id(x => x.Id);
            Map(x => x.PurchaseOrderNumber);
            Map(x => x.SagaReferenceId);
            Map(x => x.MessageType);
            Map(x => x.FromEndpoint);
            Map(x => x.Action);
            Map(x => x.Leg);
            Map(x => x.LegCount);
            Map(x => x.DateTimeMessageRecieved);
            Map(x => x.DateTimeMessageSendToHere);
            Map(x => x.MessageData);
        }
    }
}