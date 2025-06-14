﻿using MediatR;
using PlataformaEducacaoOnline.Core.Messages;

namespace PlataformaEducacaoOnline.Core.DomainObjects
{
    public class DomainNotification : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }

        public DomainNotification(string key, string value)
        {
            Timestamp = DateTime.Now;
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
        }
    }
}
