using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Html;

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
       
    }
    //public class Annotations_Labels_TB
    //{
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Anno_Label_ID { get; set; }

    //    public int Annotation_ID { get; set; }
    //    [ForeignKey("Annotation_ID")]
    //    public Annotations_TB Annotations_TB { get; set; }

    //    public int Label_ID { get; set; }
    //    [ForeignKey("Label_ID")]
    //    public Labels_TB Labels_TB { get; set; }
    //}

    public class Labels_BG_Colours_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Label_BGColour_ID { get; set; }

        [Display(Name = "Background Color")]
        [Required(ErrorMessage = "Background Color  is required")]
        [StringLength(20, ErrorMessage = "Must be between 3 and 20 characters", MinimumLength = 3)]
        public string Label_Background_Color { get; set; }

        [Display(Name = "Shortcut Key")]
        [Required(ErrorMessage = "Shortcut Key  is required")]
        [StringLength(1, ErrorMessage = "Must be between 1 character", MinimumLength = 1)]
        public string Label_ShortCut_Key { get; set; }

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

        [Display(Name = "Label BGColour ID")]
        public int Label_BGColour_ID { get; set; }
        [ForeignKey("Label_BGColour_ID")]
        public Labels_BG_Colours_TB Labels_BG_Colours_TB { get; set; }


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

      
        [Display(Name = "Annotation ID in the Original File")]
        public string Annotation_ID_InFile { get; set; }

        [Display(Name = "Annotation Tilte")]
        [Required(ErrorMessage = "Annotation Title is required")]
        [StringLength(200, ErrorMessage = "Must be between 5 and 200 characters", MinimumLength = 5)]
        public string Annotation_Title { get; set; }

        [Display(Name = "Annotation Body")]
        [Required(ErrorMessage = "Annotation Text is required")]
        [StringLength(10000, ErrorMessage = "Must be between 5 and 10000 characters", MinimumLength = 5)]
        public string Annotation_Text { get; set; }

        [Display(Name = "Annotation Date")]
        [Required(ErrorMessage = "Annotation Date is required")]
        [StringLength(10, ErrorMessage = "Must be between 8 and 10 characters", MinimumLength = 8)]
        public string Annotation_Date { get; set; }

        [Display(Name = "Annotation Source")]
        [Required(ErrorMessage = "Annotation Source is required")]
        [StringLength(100, ErrorMessage = "Must be between 3 and 100 characters", MinimumLength = 3)]
        public string Annotation_Source { get; set; }

        [Display(Name = "Source File Name")]
        [Required(ErrorMessage = "Source File Name is required")]
        [StringLength(1000, ErrorMessage = "Must be between 3 and 1000 characters", MinimumLength = 3)]
        public string Source_File_Name { get; set; }

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
        [StringLength(10000, ErrorMessage = "Must be between 5 and 10000 characters", MinimumLength = 5)]
        public string Annotated_Text { get; set; }

        [Display(Name = "Annotation ID")]
        public int Count_Annotations { get; set; }

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

    //public class ReadExcel
    //{
    //    [Required(ErrorMessage = "Please select file")]
    //    [FileExt(Allow = ".xls,.xlsx", ErrorMessage = "Only excel file")]
    //    public HttpPostedFileBase file { get; set; }
    //}
    public class CL_Users_Home_Page
    {
        public List<Assigned_Annotations_ToUsers_TB> allAnnotations { get; set; }
        public List<Labels_TB> allLabels { get; set; }
        public Assigned_Annotations_ToUsers_TB Selected_Assigned_Annotation { get; set; }

        public HtmlString ShortcutKeys_Press_Script { get; set; }
    }
}
