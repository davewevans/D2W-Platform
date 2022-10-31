namespace D2W.WebPortal.DTOs
{
    public class WorkOrderItemDto
    {
        public Guid Id { get; set; }

        public string Item { get; set; }

        public string Description { get; set; }
      
        public Guid? WorkOrderId { get; set; }

        public WorkOrderDto WorkOrder { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public string Color { get; set; }

        public string Fabric { get; set; }
    }
}
