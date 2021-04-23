using David.OpheliaTest.Entities.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace David.OpheliaTest.Entities
{
    public class Product : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Nombre producto")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe de tener una longitud máxima de {1} caractéres")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Descripción del producto")]
        [MaxLength(2000, ErrorMessage = "El campo {0} debe de tener una longitud máxima de {1} caractéres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Precio del producto")]
        public decimal Price { get; set; }

        public string Image { get; set; }

        #region Virtuals
        [Required]
        public int CategoryId { get; set; }
        [JsonProperty("Category")]
        public virtual Category Category { get; set; }

        [JsonIgnore]
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Active { get; set; }
        #endregion

    }
}
