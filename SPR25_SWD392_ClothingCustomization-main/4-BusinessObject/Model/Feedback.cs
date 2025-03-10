﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObject.Model;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int Rating { get; set; }

    public string Review { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }
    [JsonIgnore]

    public virtual User User { get; set; } = null!;
}
