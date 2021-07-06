using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [StringLength(20, ErrorMessage = "Must be between 5 and 20 characters", MinimumLength = 5)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role is required")]
        public int Role_ID { get; set; }

        [ForeignKey("Role_ID")]
        public Roles_TB Roles_TB { get; set; }
        //  public ICollection<Assigned_Annotations_ToUsers_TB> Assigned_Annotations_ToUsers_TBs { get; set; }

        //[Display(Name = "Project ID")]
        //public int? Project_ID { get; set; }

        //[ForeignKey("Project_ID")]
        //public Projects_TB Projects_TB { get; set; }
    }
    public class Annotations_Labels_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Anno_Label_ID { get; set; }

        public int Annotation_ID { get; set; }
        [ForeignKey("Annotation_ID")]
        public Annotations_TB Annotations_TB { get; set; }

        public int Label_ID { get; set; }
        [ForeignKey("Label_ID")]
        public Labels_TB Labels_TB { get; set; }
    }
    public class Labels_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Label_ID { get; set; }

        [Display(Name = "Label")]
        [Required(ErrorMessage = "Label Text is required")]
        [StringLength(20, ErrorMessage = "Must be between 2 and 20 characters", MinimumLength = 2)]
        public string Label_Text { get; set; }


        [Display(Name = "Project ID")]
        public int? Project_ID { get; set; }
        [ForeignKey("Project_ID")]
        public Projects_TB Projects_TB { get; set; }

    }
    public class Projects_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Project_ID { get; set; }

        
        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "Project Name is required")]
        [StringLength(20, ErrorMessage = "Must be between 2 and 20 characters", MinimumLength = 2)]
        public string Project_Name { get; set; }
    }
    public class Annotations_TB
    {
        [Key]
        [Display(Name = "Annotation ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Annotation_ID { get; set; }

        [Display(Name = "Annotation Tilte")]
        [Required(ErrorMessage = "Annotation Title is required")]
        [StringLength(30, ErrorMessage = "Must be between 5 and 1000 characters", MinimumLength = 5)]
        public string Annotation_Title { get; set; }

        [Display(Name = "Annotation Text")]
        [Required(ErrorMessage = "Annotation Text is required")]
        [StringLength(1000, ErrorMessage = "Must be between 5 and 1000 characters", MinimumLength = 5)]
        public string Annotation_Text { get; set; }

        [Display(Name = "Project ID")]
        public int? Project_ID { get; set; }
        [ForeignKey("Project_ID")]
        public Projects_TB Projects_TB { get; set; }

    }
    public class Assigned_Annotations_ToUsers_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Assigned_Anno_ID { get; set; }

        [Display(Name = "User ID")]
        public int User_ID { get; set; }
        [ForeignKey("User_ID")]
        public Users_TB Users_TB { get; set; }

        [Display(Name = "Annotation ID")]
        public int Annotation_ID { get; set; }
        [ForeignKey("Annotation_ID")]
        public Annotations_TB Annotations_TB { get; set; }

        [Display(Name = "Annotated Text")]
        //[Required(ErrorMessage = "Annotated Text is required")]
        [StringLength(1000, ErrorMessage = "Must be between 5 and 1000 characters", MinimumLength = 5)]
        public string Annotated_Text { get; set; }


        [Display(Name = "Project ID")]
        public int? Project_ID { get; set; }
        [ForeignKey("Project_ID")]
        public Projects_TB Projects_TB { get; set; }


        
    }
    public class Roles_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Role_ID { get; set; }

        [Required(ErrorMessage = "Role Text is required")]
        [StringLength(20, ErrorMessage = "Must be between 4 and 20 characters", MinimumLength = 4)]
        [Display(Name = "Role")]
        public string Role_Text { get; set; }


        [Display(Name = "Project ID")]
        public int? Project_ID { get; set; }

        [ForeignKey("Project_ID")]
        public virtual Projects_TB Projects_TB { get; set; }    
    }
    public class CL_Users_Home_Page
    {
        public List<Assigned_Annotations_ToUsers_TB> allAnnotations { get; set; }
        public List<Labels_TB> allLabels { get; set; }
    }
}
