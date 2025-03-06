using BusinessObject.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Model;

public partial class OrderStage
{
    public int OrderStageId { get; set; }

    public int OrderId { get; set; }
   
    public OrderStageEnum OrderStageName { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [JsonIgnore]// Loại bỏ vòng lặp khi serialize
    public virtual Order Order { get; set; } = null!;
}
