using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IPORevamp.Data
{
    public class FeaturesModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Feature name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Feature description is required")]
        [StringLength(250, ErrorMessage = "Feature descrpitioncan not be beyond 250 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Feature price is required")]
        [DataType(DataType.Currency, ErrorMessage = "Please provide a valid amount for feature price")]
        public string Price { get; set; }
        public bool IsSelected { get; set; }
        public string MachineName { get; set; }

    }

    public class FeaturesMultipleIdView
    {
        [Required(ErrorMessage = "Please specify the features you want to assign to this event")]
        public int[] FeatureIds { get; set; }
    }
}
