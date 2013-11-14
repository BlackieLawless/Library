﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Library.DataContracts.Abstract;

namespace Library.DataContracts.Concrete
{
    [DataContract]
    public class Notification : UniqueObject<Notification>
    {
        public Request Request {
            get;
            set;
        }

        public NotificationType NotificationType {
            get;
            set;
        }


    }
}
