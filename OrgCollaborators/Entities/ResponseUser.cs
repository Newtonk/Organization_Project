using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrgCollaborators.Entities
{
    public class ResponseUser
    {
        public Pessoa Pessoa { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }
    }
}
