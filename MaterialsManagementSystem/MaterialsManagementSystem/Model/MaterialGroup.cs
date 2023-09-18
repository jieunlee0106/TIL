using System;

namespace MaterialsManagementSystem.Model
{
    public class MaterialGroup
    {
        public string CodeId { get; set; }
        public string CodeName { get; set; }
        public string UseFlag { get; set; }
        public DateTime CrtDt { get; set; }
        public DateTime UdtDt { get; set; }
    }
}
