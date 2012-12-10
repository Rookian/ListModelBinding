using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ListModelBinding.Models
{
    public class ViewModel
    {
        public List<ItemModel> Items { get; set; }
        public List<SelectListItem> SelectListItems { get; set; }
        [Required]
        public Single Single { get; set; }
    }

    public class ListModel
    {
        public List<SelectListItem> SelectListItems { get; set; }
        [Required]
        public string SelectedItem { get; set; }
    }

    public class ItemModel
    {
        public int Id { get; set; }
        
        public string SelectedItem { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }


    public class Single
    {
        [Required]
        public string Name { get; set; }
    }
}