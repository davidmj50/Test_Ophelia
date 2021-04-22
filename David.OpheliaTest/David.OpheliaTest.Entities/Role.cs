using David.OpheliaTest.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace David.OpheliaTest.Entities
{
    public class Role : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Rol")]
        [MaxLength(30, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        public string Name { get; set; }
        #region Virtuals
        public virtual ICollection<User> Users { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Active { get; set; }
        #endregion
    }
}
