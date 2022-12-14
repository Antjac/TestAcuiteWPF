using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAcuite.Class
{
    public class NotificationMessage : ValueChangedMessage<String>
    {
        public NotificationMessage(String message) : base(message)
        {
        }
    }
    
}
