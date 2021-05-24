using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TextMark.Models
{
    public class Users_TB
    {  
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public int Role_ID { get; set; }
        public Roles_TB Roles_TB { get; set; }

        public ICollection<Assigned_Annotations_ToUsers_TB> Assigned_Annotations_ToUsers_TBs { get; set; }
    }
    public class Labels_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Label_ID { get; set; }

        [Required(ErrorMessage = "Label Text is required")]
        [StringLength(20, ErrorMessage = "Must be between 2 and 20 characters", MinimumLength = 2)]
        public string Label_Text { get; set; }

        public ICollection<Annotations_Labels_TB> Annotations_Labels_TBs { get; set; }
    }

    public class Annotations_Labels_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        

        public int Label_ID { get; set; }
        public Labels_TB Labels_TB { get; set; }

        public int Annotation_ID { get; set; }
        public Annotations_TB Annotations_TB { get; set; }
    }

    public class Annotations_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Annotation_ID { get; set; }

        [Required(ErrorMessage = "Annotation Text is required")]
        [StringLength(1000, ErrorMessage = "Must be between 5 and 1000 characters", MinimumLength = 5)]
        public string Annotation_Text { get; set; }

        public string Date { get; set; }

        public ICollection<Assigned_Annotations_ToUsers_TB> Assigned_Annotations_ToUsers_TBs { get; set; }
        public ICollection<Annotations_Labels_TB> Annotations_Labels_TBs { get; set; }
    }

    public class Assigned_Annotations_ToUsers_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int User_ID { get; set; }
        public Users_TB Users_TB { get; set; }

        public int Annotation_ID { get; set; }
        public Annotations_TB Annotations_TB { get; set; }

        public DateTime Date { get; set; }
    }


    public class Roles_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Role_ID { get; set; }

        [Required(ErrorMessage = "Role Text is required")]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        public string Role_Text { get; set; }
        public ICollection<Users_TB> Users_TBs { get; set; }
    }
}
