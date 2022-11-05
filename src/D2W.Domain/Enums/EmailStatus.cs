using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Domain.Enums;
public enum EmailStatus
{
    Draft, // Email created but not sent.
    Sent, // Email sent but not opened.
    Received // Email received.
}
