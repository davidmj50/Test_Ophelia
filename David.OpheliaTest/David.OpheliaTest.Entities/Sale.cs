using David.OpheliaTest.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace David.OpheliaTest.Entities
{
    public class Sale : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Comprobante de venta")]
        [MaxLength(100, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        public string SaleReference { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Total venta")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Fecha de venta")]
        public DateTime SaleDate { get; set; }

        public int UserId { get; set; }

        #region Virtuals
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Active { get; set; }
        #endregion
    }
}
