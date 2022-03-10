using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrgCollaborators.Entities
{
    public class Pessoa
    {
        public string Cpf { get; set; }
        public int IdPessoa { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Pessoa(string name, string email, string cpf)
        {
            this.Name = name;
            this.Cpf = cpf;
            this.Email = email;
        }

        public virtual Superior Superior { get; set; }

        public virtual Colaborador Colaborador { get; set; }
    }

    public class Superior
    {
        public int IdSuperior { get; set; }

        public int IdPessoa { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public virtual ICollection<Superior_Colaborador> Colaboradores { get; set; }
    }

    public class Colaborador
    {
        public int IdColaborador { get; set; }
        public int IdPessoa { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public virtual ICollection<Superior_Colaborador> Superiores { get; set; }
    }

    public class Superior_Colaborador
    {
        public int IdSuperior_Colaborador { get; set; }
        public int IdSuperior { get; set; }
        public int IdColaborador { get; set; }

        public int IdCargo { get; set; }

        public int IdSetor { get; set; }

        public  Colaborador Colaborador { get; set; }
        public  Superior Superior { get; set; }

        public Setor Setor { get; set; }

        public Cargo Cargo { get; set; }

    }


}
