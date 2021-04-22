using David.OpheliaTest.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace David.OpheliaTest.Entities
{
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        [Display(Name = "Nombres")]
        public string FisrtName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Nombre de usuario")]
        [MaxLength(10, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Contraseña")]
        [MaxLength(15, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0}, es requerido")]
        [Display(Name = "Número de celular")]
        [MaxLength(15, ErrorMessage = "El campo {0}, debe tener una longitud máxima de {1} caractéres")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "El campo {0}, es requerido")]
        public int RoleId { get; set; }
        #region Virtuals
        public virtual Role Role { get; set; }
        public virtual Wallet Bullet { get; set; }

        [JsonIgnore]
        public virtual ICollection<Sale> Sales { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Active { get; set; }
        #endregion
    }
}
