﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.Core.Messages
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }
        protected Event()
        {
            Timestamp = DateTime.Now;
        }        
    }
}
