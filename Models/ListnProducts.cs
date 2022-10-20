using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ListnProducts
    {
        public int ID { get; set; }
        public string? Description { get; set; }
        public int? ShoppingListId { get; set; }
        [ForeignKey("ShoppingListId")]
        [ValidateNever]
        public ShoppingList ShoppingList { get; set; }

        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }

    }
}
