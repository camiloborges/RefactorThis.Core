using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RefactorThis.Core.Domain
{
    public partial class ProductOption
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Product Id is Required")]
        [DisplayName("Product Id")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description is Required")]
        [MinLength(2)]
        [MaxLength(500)]
        [DisplayName("Description")]
        public string Description { get; set; }

        public ProductOption()
        {
        }

        public ProductOption(Guid id, Guid productId, string name, string description)
        {
            Id = id;
            ProductId = productId;
            Name = name;
            Description = description;
        }
    }
}