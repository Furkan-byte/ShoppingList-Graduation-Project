using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string? State { get; set; }

        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser? appUser { get; set; }
        [ValidateNever]
        public IEnumerable<ListnProducts> ListnProducts { get; set; }
    }
}
