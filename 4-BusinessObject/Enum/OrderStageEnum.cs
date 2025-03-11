namespace BusinessObject.Enum
{
    public enum OrderStageEnum
    {
        PlacedOrder = 1,    // Đơn hàng đã đặt
        Confirmed = 2,      // Đơn hàng đã xác nhận
        Shipped = 3,        // Đã giao hàng
        Delivered = 4,      // Đã nhận hàng
        Cancelled = 5       // Đơn hàng bị hủy
    }
}
