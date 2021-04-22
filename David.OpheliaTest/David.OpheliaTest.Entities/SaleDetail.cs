using David.OpheliaTest.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace David.OpheliaTest.Entities
{
    public class SaleDetail : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int SaleId { get; set; }

        public int ProductId { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Cantidad")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Descuento")]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Valor descuento")]
        public int DiscountValue { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Monto")]
        public decimal TotalSale { get; set; }

        #region Virtuals
        public virtual Sale Sale { get; set; }
        public virtual Product Product { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Active { get; set; }
        #endregion
    }
}
