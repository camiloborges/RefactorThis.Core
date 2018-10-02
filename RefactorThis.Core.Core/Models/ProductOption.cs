using RefactorThis.Core.SharedKernel;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RefactorThis.Core.Models
{
    public partial class ProductOption: BaseEntity
    {

        public Guid ProductId { get; set; }


        public string Description { get; set; }
       
    }
}