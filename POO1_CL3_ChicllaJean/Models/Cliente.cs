using System.ComponentModel.DataAnnotations;

namespace POO1_CL3_ChicllaJean.Models
{
    public class Cliente
    {
        [Display(Name = "Id")]
        public string IdCLiente { get; set; }

        [Display(Name = "Nombres")]
        public string Nombres { get; set; }

        [Display(Name = "Tipo Documento")]
        public string TipoDocumento { get; set; }

        [Display(Name = "Nro Documento")]
        public string Documento { get; set; }

        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
    }
}