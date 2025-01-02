using System;
using System.Collections.Generic;

namespace AquaFeedShop.core.Models;

public partial class ChatMessage
{
    public int MessageId { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string MessageContent { get; set; } = null!;

    public DateTime? SendDatetime { get; set; }

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
