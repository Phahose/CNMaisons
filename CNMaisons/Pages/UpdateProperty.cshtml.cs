using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace CNMaisons.Pages
{
    public class UpdatePropertyModel : PageModel
    {
        public string ErrorMessage {get; set; } = string.Empty;
        public string SucceessMessage { get; set; } = string.Empty;
        [BindProperty]
        [RegularExpression("[A-Za-z]{2}[0-9]{5}", ErrorMessage = "The PropertyID must be in the Format CN00001")]
        public string PropertyID { get; set; } = string.Empty;
        [BindProperty]
        public string PropertyName { get; set; } = string.Empty;
        [BindProperty]
        public string PropertyValue { get; set; } = string.Empty;
        [BindProperty]
        public string State { get; set; } = string.Empty;
        [BindProperty]
        public string Country { get; set; } = string.Empty;
        [BindProperty]
        public string PropertyAddress { get; set; } = string.Empty;
        [BindProperty]
        public string PropertyType { get; set; } = string.Empty;
        [BindProperty]
        public int RoomNumber { get; set; }
        [BindProperty]
        public string Description { get; set; } = string.Empty;
        [BindProperty]
        public IFormFile Image1 { get; set; }
        [BindProperty]
        public IFormFile Image2 { get; set; }
        [BindProperty]
        public IFormFile Image3 { get; set; }
        [BindProperty]
        public IFormFile Image4 { get; set; }
        [BindProperty]
        public IFormFile Image5 { get; set; }
        [BindProperty]
        public IFormFile Image6 { get; set; }
        [BindProperty]
        public IFormFile Image7 { get; set; }
        [BindProperty]
        public IFormFile Image8 { get; set; }
        [BindProperty]
        public IFormFile Image9 { get; set; }
        [BindProperty]
        public IFormFile Image10 { get; set; }
        [BindProperty]
        public string Submit { get; set; } = string.Empty;
        [BindProperty]
        public decimal PropertyPrice { get; set; }
        public Property Property { get; set; } = new();
        public void OnGet()
        {
            if (HttpContext.Session.GetString("PropertyID") != null)
            {
                PropertyID = HttpContext.Session.GetString("PropertyID")!;
            }

            BCS controller = new BCS();
            Property = controller.GetPropertyByID(PropertyID);
        }
        public void OnPost()
        {
            string pattern = @"[A-Za-z]{2}[0-9]{5}$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            ModelState.Clear();
            if (PropertyID == null)
            {
                ModelState.AddModelError("PropertyIDError", "The Property ID is Required");
            }
            if (PropertyName == null)
            {
                ModelState.AddModelError("PropertyNameError", "The Property Name is Required");
            }
            if (State == null)
            {
                ModelState.AddModelError("StateError", "The State is Required");
            }
            if (Country == null)
            {
                ModelState.AddModelError("CountryError", "The Country is Required");
            }
            if (PropertyAddress == null)
            {
                ModelState.AddModelError("PropertyAddressError", "The Property Address is Required");
            }
            if (PropertyType == null)
            {
                ModelState.AddModelError("PropertyTypeError", "The Property Type is Required");
            }
            if (RoomNumber <= 0)
            {
                ModelState.AddModelError("RoomNumberError", "The RoomNumber is Required");
            }
            if (PropertyPrice <= 0)
            {
                ModelState.AddModelError("PropertyPriceError", "The Price is Required");
            }
            if (Description == null)
            {
                ModelState.AddModelError("DescriptionError", "The Description is Required");
            }
            if (Image1 == null)
            {
                ModelState.AddModelError("Image1Error", "Image1 is required");
            }
            if (Image2 == null)
            {
                ModelState.AddModelError("Image2Error", "Image2 is required");
            }
            if (Image3 == null)
            {
                ModelState.AddModelError("Image3Error", "Image3 is required");
            }
            if (Image4 == null)
            {
                ModelState.AddModelError("Image4Error", "Image4 is required");
            }


            if (ModelState.IsValid)
            {
                Property property = new();
                byte[] image1Bytes = ConvertToByteArray(Image1);
                byte[] image2Bytes = ConvertToByteArray(Image2);
                byte[] image3Bytes = ConvertToByteArray(Image3);
                byte[] image4Bytes = ConvertToByteArray(Image4);
                byte[] image5Bytes = ConvertToByteArray(Image5);
                byte[] image6Bytes = ConvertToByteArray(Image6);
                byte[] image7Bytes = ConvertToByteArray(Image7);
                byte[] image8Bytes = ConvertToByteArray(Image8);
                byte[] image9Bytes = ConvertToByteArray(Image9);
                byte[] image10Bytes = ConvertToByteArray(Image10);

                BCS controller = new();
                property = controller.GetPropertyByID(PropertyID);
                property = new()
                {
                    PropertyID = PropertyID,
                    PropertyName = PropertyName,
                    PropertyLocationState = State,
                    PropertyLocationCountry = Country,
                    PropertyAddress = PropertyAddress,
                    PropertyType = PropertyType,
                    NumberOfRooms = RoomNumber,
                    PropertyDescription = Description,
                    PropertyPrice = PropertyPrice,
                    Image1 = image1Bytes,
                    Image2 = image2Bytes,
                    Image3 = image3Bytes,
                    Image4 = image4Bytes,
                    Image5 = image5Bytes,
                    Image6 = image6Bytes,
                    Image7 = image7Bytes,
                    Image8 = image8Bytes,
                    Image9 = image9Bytes,
                    Image10 = image10Bytes,
                };
                controller.UpdateProperty(property);
                SucceessMessage = "The Property Updated SuccessFully";
                
            }
        }
        private byte[] ConvertToByteArray(IFormFile file)
        {
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            else { return null; }
        }
    }
}
