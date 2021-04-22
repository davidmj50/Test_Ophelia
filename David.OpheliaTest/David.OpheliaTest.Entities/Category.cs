using David.OpheliaTest.Entities.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace David.OpheliaTest.Entities
{
    public class Category : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El campo {0} debe de tener una longitud máxima de {1} caractéres")]
        [Display(Name = "Nombre Categoria")]
        public string Category_Name { get; set; }

        [MaxLength(2000, ErrorMessage = "El campo {0} debe de tener una longitud máxima de {1} caractéres")]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        #region Virtuals
        [JsonProperty("Products")]
        public virtual ICollection<Product> Products { get; set; }
        public DateTime LastUpdate { get; set ; }
        public bool Active { get; set; }
        #endregion
    }
}
