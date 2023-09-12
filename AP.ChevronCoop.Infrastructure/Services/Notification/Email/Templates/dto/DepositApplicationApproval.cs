using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto
{
    public class DepositApplicationApproval
    {
        public string Name { get; set; }
        public string DepositName { get; set; }
    }

    public class DepositAction
    {
        public string Name { get; set; }
        public string ActionMessage { get; set; }
    }
}
