using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_CRUD.Models
{
    public class Funcionario
    {
        [Required(ErrorMessage = "O Campo de Id é Obrigatorio")]
        public int FuncionarioId { get; set; }
        [Required(ErrorMessage="O Campo de Nome é Obrigatorio")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O Campo de Cidade é Obrigatorio")]
        public string Cidade { get; set; }
        public string Departamento { get; set; }
        public string Sexo { get; set; }
    }
}