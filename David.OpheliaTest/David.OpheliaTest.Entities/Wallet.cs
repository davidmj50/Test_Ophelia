using David.OpheliaTest.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace David.OpheliaTest.Entities
{
    public class Wallet : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Puntos")]
        public int Points { get; set; }

        #region Virtuals
        public virtual User User { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Active { get; set; }
        #endregion

    }
}
