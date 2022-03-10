using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrgCollaborators.Entities
{
    public class RequestUser
    {
        public string Cpf { get; set; }
        public int IdPessoa { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool isColab { get; set; }

        public bool isSuperior { get; set; }
    }
}
