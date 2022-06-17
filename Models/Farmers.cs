using System.ComponentModel.DataAnnotations;

namespace brick_and_mortar_store.Models
{
    public class Farmers
    {
        public int FarmersId { get; set; }

        [Display(Prompt = " Enter Farmer Name")]
        public string FarmerName { get; set; }

        [Display(Prompt = " Enter Farmer Surname")]
        public string FarmerSurname { get; set; }

        [Display(Prompt = " Enter Farmer Age")]
        public int FarmerAge { get; set; }

        [Display(Prompt = " Enter Farmer Phone Number")]
        public string FarmerPhoneNo { get; set; }

        [Display(Prompt = " Enter Farmer Email Aadress")]
        public string FarmerEmail { get; set; }

        [Display(Prompt = " Enter A Default Password")]
        public string FarmerPassword { get; set; }

        [Display(Prompt = " Upload Picture Of Farmer")]
        public string FarmerPicture { get; set; }

        [Display(Prompt = " Enter The Name of Farm ")]
        public string FarmName { get; set; }

        [Display(Prompt = " Enter Farm Aadress")]
        public string FarmAadress { get; set; }

        public string role { get; set; }

        public string UserId { get; set; }
    }
}
