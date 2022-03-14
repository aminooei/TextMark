using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Html;
using System.ComponentModel;
using PagedList;

namespace TextMark.Models
{
    public class Projects_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Project_ID { get; set; }


        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "Project Name is required")]
        [StringLength(20, ErrorMessage = "Must be between 2 and 20 characters", MinimumLength = 2)]
        public string Project_Name { get; set; }

        [Display(Name = "Project Description")]
        [Required(ErrorMessage = "Project Description is required")]
        [StringLength(200, ErrorMessage = "Must be between 5 and 200 characters", MinimumLength = 5)]        
        public string Project_Description { get; set; }

        [Display(Name = "Is Activated?")]
        public bool Is_Active { get; set; }


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
        public int Project_ID { get; set; }
        [ForeignKey("Project_ID")]
        public virtual Projects_TB Projects_TB { get; set; }
    }

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

        //[Required(ErrorMessage = "Confirm Password is required")]
        //[StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        //[Compare("Password")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm Password")]
        //public string ConfirmPassword { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role is required")]
        public int Role_ID { get; set; }

        [ForeignKey("Role_ID")]
        public Roles_TB Roles_TB { get; set; }


    }
    
    public class Users_Access_Level_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "User ID")]
        [Required(ErrorMessage = "UserID is required")]
        public int User_ID { get; set; }

        [ForeignKey("User_ID")]
        public Users_TB Users_TB { get; set; }

        [DefaultValue(true)]
        [Display(Name = "Text Annotation Allowed?")]
        public bool Text_Annotation_Allowed { get; set; }

        [DefaultValue(true)]
        [Display(Name = "Text Classification Allowed?")]
        public bool Text_Classification_Allowed { get; set; }

        [Display(Name = "Source File Name")]
        [Required(ErrorMessage = "Source File Name is required")]
        [StringLength(1000, ErrorMessage = "Must be between 1 and 1000 characters", MinimumLength = 1)]
        public string Source_File_Name { get; set; }

        [Display(Name = "Project ID")]
        public int? Project_ID { get; set; }
        [ForeignKey("Project_ID")]
        public Projects_TB Projects_TB { get; set; }

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

        //[Display(Name = "Label BGColour ID")]
        //public int Label_BGColour_ID { get; set; }
        //[ForeignKey("Label_BGColour_ID")]
        //public Labels_BG_Colours_TB Labels_BG_Colours_TB { get; set; }

        [Display(Name = "Background Color")]
        [Required(ErrorMessage = "Background Color  is required")]
        [StringLength(20, ErrorMessage = "Must be between 3 and 20 characters", MinimumLength = 3)]
        public string Label_Background_Color { get; set; }

        [Display(Name = "Shortcut Key")]
        [Required(ErrorMessage = "Shortcut Key  is required")]
        [StringLength(1, ErrorMessage = "Must be between 1 character", MinimumLength = 1)]
        public string Label_ShortCut_Key { get; set; }

        [Display(Name = "Project ID")]
        public int? Project_ID { get; set; }
        [ForeignKey("Project_ID")]
        public Projects_TB Projects_TB { get; set; }

    }

    public class ClassificationLabels_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassificationLabel_ID { get; set; }

        [Display(Name = "Classification Label")]
        [Required(ErrorMessage = "Classification Label Text is required")]
        [StringLength(20, ErrorMessage = "Must be between 2 and 20 characters", MinimumLength = 2)]
        public string ClassificationLabel_Text { get; set; }
       
        [Display(Name = "Classification Background Color")]
        [Required(ErrorMessage = "Classification Background Color  is required")]
        [StringLength(20, ErrorMessage = "Must be between 3 and 20 characters", MinimumLength = 3)]
        public string ClassificationLabel_Background_Color { get; set; }

        [Display(Name = "Classification Shortcut Key")]
        [Required(ErrorMessage = "Classification Shortcut Key  is required")]
        [StringLength(1, ErrorMessage = "Must be between 1 character", MinimumLength = 1)]
        public string ClassificationLabel_ShortCut_Key { get; set; }

        [Display(Name = "Project ID")]
        public int? Project_ID { get; set; }
        [ForeignKey("Project_ID")]
        public Projects_TB Projects_TB { get; set; }
    }



    public class Annotations_TB
    {
        [Key]
        [Display(Name = "Annotation ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Annotation_ID { get; set; }

      
        [Display(Name = "ID")]
        public string Annotation_ID_InFile { get; set; }

        [Display(Name = "Tilte")]
        [Required(ErrorMessage = "Annotation Title is required")]
        [StringLength(200, ErrorMessage = "Must be between 5 and 200 characters", MinimumLength = 5)]
        public string Annotation_Title { get; set; }

        [Display(Name = "Body")]
        [Required(ErrorMessage = "Annotation Text is required")]
        [StringLength(10000, ErrorMessage = "Must be between 5 and 10000 characters", MinimumLength = 5)]
        public string Annotation_Text { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Annotation Date is required")]
        [StringLength(10, ErrorMessage = "Must be between 8 and 10 characters", MinimumLength = 8)]
        public string Annotation_Date { get; set; }

        [Display(Name = "Source")]
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
        [Display(Name = "Assigned Annotation ID")]
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

        [Display(Name = "Not Sure?")]
        public bool Not_Sure { get; set; }

        [Display(Name = "Comments: ")]        
        [StringLength(1000, ErrorMessage = "Must be Maximum 1000 characters", MinimumLength = 0)]
        public string Comments { get; set; }

        [Display(Name = "Number of Annotations")]
        public int Count_Annotations { get; set; }

        [Display(Name = "Annotation ID InText")]
        public string? Annotation_ID_InText { get; set; }

        [Display(Name = "Project ID")]
        public int? Project_ID { get; set; }

        
        [ForeignKey("Project_ID")]
        public Projects_TB Projects_TB { get; set; }   
    }

    public class AnnotatedTexts_Tags
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Assigned Annotation ID")]
        public int Assigned_TextAnnotation_ID { get; set; }

        [ForeignKey("Assigned_TextAnnotation_ID")]
        public Assigned_Annotations_ToUsers_TB Assigned_Annotations_ToUsers_TB { get; set; }

        public int AnnotationLabel_ID { get; set; }

        [ForeignKey("AnnotationLabel_ID")]
        public Labels_TB Labels_TB { get; set; }

        [Display(Name = "Annotated Substring")]
        public string Annotated_Substring { get; set; }

        [Display(Name = "Annotation ID InText")]
        public string Annotation_ID_InText { get; set; }
        public int Label_Start_Index { get; set; }
        public int Label_End_Index { get; set; }

    }
    public class ClassifiedTexts_Tags
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Assigned_TextClassification_ID { get; set; }
        [ForeignKey("Assigned_TextClassification_ID")]
        public Assigned_TextClassifications_ToUsers_TB Assigned_TextClassifications_ToUsers_TB { get; set; }

        public int ClassificationLabel_ID { get; set; }
        [ForeignKey("ClassificationLabel_ID")]
        public ClassificationLabels_TB ClassificationLabels_TB { get; set; }

    }

    public class Assigned_TextClassifications_ToUsers_TB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Assigned_TextClassification_ID { get; set; }

        [Display(Name = "User ID")]
        public int User_ID { get; set; }
        [ForeignKey("User_ID")]
        public Users_TB Users_TB { get; set; }

        [Display(Name = "TextClassification ID")]
        public int TextClassification_ID { get; set; }
        [ForeignKey("TextClassification_ID")]
        public Annotations_TB Annotations_TB { get; set; }

        [Display(Name = "Classified Tags")]
        //[Required(ErrorMessage = "Annotated Text is required")]
        [StringLength(10000, ErrorMessage = "Must be between 5 and 10000 characters", MinimumLength = 5)]
        public string TextClassification_HtmlTags { get; set; }

        [Display(Name = "Not Sure?")]
        public bool Not_Sure { get; set; }

        [Display(Name = "Comments: ")]
        [StringLength(1000, ErrorMessage = "Must be Maximum 1000 characters", MinimumLength = 0)]
        public string Comments { get; set; }

        [Display(Name = "Number of Classifications")]
        public int Count_Classifications { get; set; }

        [Display(Name = "Project ID")]
        public int? Project_ID { get; set; }
        [ForeignKey("Project_ID")]
        public Projects_TB Projects_TB { get; set; }
    }

    public class CL_UsersAnnotations_Home_Page
    {
        public IPagedList<Assigned_Annotations_ToUsers_TB> allAnnotations { get; set; }
        public List<Labels_TB> allLabels { get; set; }
        public Assigned_Annotations_ToUsers_TB Selected_Assigned_Annotation { get; set; }

        public HtmlString ShortcutKeys_Press_Script { get; set; }

        public int Selected_UserID { get; set; }
        public int PageNum { get; set; }
        public int NumRecordsInEachPage { get; set; }
        public int SelectedProjectID { get; set; }
        public int LoggedinUserID { get; set; }

        public int TotalNumPages { get; set; }
    }

    public class CL_UsersClassifications_Home_Page
    {
        public IPagedList<Assigned_TextClassifications_ToUsers_TB> allClassifications { get; set; }
        public List<ClassificationLabels_TB> allClassificationLabels { get; set; }
        public Assigned_TextClassifications_ToUsers_TB Selected_Assigned_Classification { get; set; }
        public List<ClassifiedTexts_Tags> all_ClassifiedText_Tags { get; set; }

        public HtmlString ClassificationShortcutKeys_Press_Script { get; set; }
        public int Selected_UserID { get; set; }
        public int PageNum { get; set; }
        public int NumRecordsInEachPage { get; set; }
        public int SelectedProjectID { get; set; }
        public int LoggedinUserID { get; set; }

        public int TotalNumPages { get; set; }
    }

    public class Details_Assigned_TextClassifications_ToUsers
    {
        public Assigned_TextClassifications_ToUsers_TB Assigned_TextClassifications_ToUsers_TB { get; set; }
        public List<ClassifiedTexts_Tags> ClassifiedTexts_Tags { get; set; }
        public List<ClassificationLabels_TB> allClassificationLabels { get; set; }
        public IPagedList<Assigned_TextClassifications_ToUsers_TB> allClassifications { get; set; }
        public int Selected_UserID { get; set; }
        public int PageNum { get; set; }

        public int TotalNumPages { get; set; }
    
}

    public class Details_Assigned_TextAnnotations_ToUsers
    {
        public Assigned_Annotations_ToUsers_TB Selected_Assigned_Annotation { get; set; }
        public List<AnnotatedTexts_Tags> Annotated_Tags { get; set; }
        public List<Labels_TB> allLabels { get; set; }
        public int Selected_UserID { get; set; }

        public IPagedList<Assigned_Annotations_ToUsers_TB> allAnnotations { get; set; }   
        public int PageNum { get; set; }

        public int TotalNumPages { get; set; }
    }
     public class List_Annotation_Records
    {
        public IPagedList<Annotations_TB> List_Annotation_Record { get; set; }
        public int PageNum { get; set; }
        public int TotalNumPages { get; set; }
    }
}
