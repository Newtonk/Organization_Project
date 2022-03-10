using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrgCollaborators.Entities
{
    public class Cargo
    {
        public int IdCargo { get; set; }

        public string Name { get; set; }

        public int IdSetor { get; set; }

        public virtual Setor Setor { get; set; }
        public virtual ICollection<Superior_Colaborador> Pessoas { get; set; }

    }
}
