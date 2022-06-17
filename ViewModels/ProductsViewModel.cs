using System;
using System.ComponentModel.DataAnnotations;

namespace brick_and_mortar_store.Models
{
    public class ProductsViewModel
    {
        public int ProductsId { get; set; }

        [Display(Prompt = " Enter Product Name")]
        public string ProductName { get; set; }

        [Display(Prompt = " Enter Product Discription")]
        public string ProductDescription { get; set; }

        [Display(Prompt = " Enter Product Quantity")]
        public int ProductQuantity { get; set; }

        [Display(Prompt = " Enter Product Price")]
        public double ProductPrice { get; set; }

        [Display(Prompt = " Upload Product Picture")]
        public string ProductPicture { get; set; }

        [Display(Prompt = " Date Added")]
        public DateTime DateAdded { get; set; }

        //Foreign Key - Farmer Details
        public string Farmers { get; set; }

        public string UserId { get; set; }
    }
}
