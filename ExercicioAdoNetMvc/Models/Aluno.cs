using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExercicioAdoNetMvc.Models
{
    public class Aluno
    {
        public int Id { get; set; }

        [Display(Name ="Informe o nome do estudante")]
        [Required(ErrorMessage ="O nome deve ser informado")]
        [StringLength(50, MinimumLength =3, ErrorMessage ="O nome deve ter no mínimo 3 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Informe o sexo do estudante")]
        [Required(ErrorMessage = "O Sexo deve ser informado")]
        public string Sexo { get; set; }

        [Display(Name = "Informe o email do estudante")]
        [Required(ErrorMessage = "O Email deve ser informado")]
        [EmailAddress(ErrorMessage ="E-mail inválido")]
        public string Email { get; set; }

        [Display(Name = "Informe a data de nascimento do estudante")]
        [Required(ErrorMessage ="Informe a data de nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Nascimento { get; set; }
    }
}
