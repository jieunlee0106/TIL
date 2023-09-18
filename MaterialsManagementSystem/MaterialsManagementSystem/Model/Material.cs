using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaterialsManagementSystem.Model
{
    public class Material
    {
        public int Number { get; set; }
        public string Status { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public string MaterialGroup { get; set; }
        public string UseFlag { get; set; }
        public DateTime CrtDt { get; set; }
        public DateTime UdtDt{ get; set; }

        public bool IsEditing { get; set; }

    }
}
