using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Web_Project_Tour_and_Travel.Models
{
    [Table("TblReg")]
    public class Reservation
    {
        [Key]
        public int RegId { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "Max 40 Char Allowed")]
        public string Name { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Max 20 Char Allowed")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Wrong Email Pattern")]
        public string Email { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "Max 40 Char Allowed")]
        public string Offers { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "Max 40 Char Allowed")]
        public string Service { get; set; }
        [Required]
        [MaxLength(11, ErrorMessage = "Max 11 Char Allowed")]
        public string cellNo { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Max 13 Char Allowed")]
        public string NIC { get; set; }

    }

    [Table("TblPayment")]
    public class Payment
    {
        public int id { get; set; }
        public string paymentMode { get; set; }
        public virtual Reservation Reservation { get; set; }
        public int RegId { get; set; }
    }

    [Table("TblPackage")]
    public class Pkg
    {
        public int id { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "Max 40 Char Allowed")]
        public string Offer { get; set; }
        public string Image { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Max 100 Char Allowed")]
        public string Description { get; set; }

    }

    [Table("TblServices")]
    public class Services
    {
        public int id { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "Max 40 Char Allowed")]
        public string Service { get; set; }
        public string Image { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Max 100 Char Allowed")]
        public string Description { get; set; }

    }

    [Table("TblLogin")]
    public class Login
    {
        public int id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Max 20 Char Allowed")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Wrong Email Pattern")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(15, ErrorMessage = "Max 15 Char Allowed")]
        [MinLength(8, ErrorMessage = "Your Password is too Weak The Length Of Password Must Be Greater Than 8 Char")]
        public string Password { get; set; }
    }

    public class cs : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Pkg> Packages { get; set; }
        public DbSet<Services> Servicess { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }

}
