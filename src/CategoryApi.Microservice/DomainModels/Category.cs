using System.ComponentModel;

namespace CategoryApi.Microservice.DomainModels;


[Table(name: "Categories", Schema = "dbo")]
public class Category
{

    // [Display(Name = "Category ID")]
    [DisplayName(displayName:"Category ID")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }


    [DisplayName(displayName: "Category Name")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [StringLength(maximumLength: 50, ErrorMessage = "{0} cannot contain more than {1} characters.")]
    public string CategoryName { get; set; } = string.Empty;

}
