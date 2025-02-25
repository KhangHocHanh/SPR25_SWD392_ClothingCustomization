using System;
using System.Collections.Generic;

namespace BusinessObject.Model;

public partial class OrderStage
{
    public int OrderStageId { get; set; }

    public int OrderId { get; set; }

    public string OrderStageName { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public virtual Order Order { get; set; } = null!;
}
