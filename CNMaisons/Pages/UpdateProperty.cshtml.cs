#nullable disable
using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using CNMaisons.TechnicalService;

namespace CNMaisons.Pages
{
    [Authorize]
    public class UpdatePropertyModel : PageModel
    {
        public string ErrorMessage { get; set; } = string.Empty;
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
        public bool Occupied { get; set; }
        public string Email { get; set; }
        public User Users { get; set; } = new User();
        [BindProperty]
        public string MyCheckBox { get; set; }
        public Employee Employee { get; set; } = new Employee();
        public void OnGet()
        {
            if (HttpContext.Session.GetString("PropertyID") != null)
            {
                PropertyID = HttpContext.Session.GetString("PropertyID")!;
            }

            if (HttpContext.Session.GetString("Email") != null)
            {
                Email = HttpContext.Session.GetString("Email")!;
            }
            CNMPMS controller = new CNMPMS();
            Users = controller.GetUserByEmail(Email);
            Property = controller.GetPropertyByID(PropertyID);
            Employee = controller.GetAllEmployees(Email);
        }
        public void OnPost()
        {
            if (HttpContext.Session.GetString("PropertyID") != null)
            {
                PropertyID = HttpContext.Session.GetString("PropertyID")!;
            }
            if (HttpContext.Session.GetString("Email") != null)
            {
                Email = HttpContext.Session.GetString("Email")!;
            }
            CNMPMS controller = new CNMPMS();
            Users = controller.GetUserByEmail(Email);
            Employee = controller.GetAllEmployees(Email);
            Property = controller.GetPropertyByID(PropertyID);
            ModelState.Clear();
            #region Validation
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
            #endregion

            if (ModelState.IsValid)
            {
                Property property = new();
                byte[] image1Bytes = ConvertToByteArray(Image1!);
                byte[] image2Bytes = ConvertToByteArray(Image2!);
                byte[] image3Bytes = ConvertToByteArray(Image3!);
                byte[] image4Bytes = ConvertToByteArray(Image4!);
                byte[] image5Bytes = ConvertToByteArray(Image5);
                byte[] image6Bytes = ConvertToByteArray(Image6);
                byte[] image7Bytes = ConvertToByteArray(Image7);
                byte[] image8Bytes = ConvertToByteArray(Image8);
                byte[] image9Bytes = ConvertToByteArray(Image9);
                byte[] image10Bytes = ConvertToByteArray(Image10);

                if (MyCheckBox == "on")
                {
                    Occupied = true;
                }
                else
                {
                    Occupied = false;
                }
                property = new()
                {
                    PropertyID = PropertyID!,
                    PropertyName = PropertyName!,
                    PropertyLocationState = State!,
                    PropertyLocationCountry = Country!,
                    PropertyAddress = PropertyAddress!,
                    PropertyType = PropertyType!,
                    NumberOfRooms = RoomNumber,
                    PropertyDescription = Description!,
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
                    Occupied = Occupied
                };
                if (Image1 == null || Image2 == null || Image3 == null || Image4 == null || Image5 == null || Image6 == null || Image7 == null || Image8 == null || Image9 == null || Image10 == null)
                {
                    #region Image Null Checker
                    property = new();
                    property.PropertyID = PropertyID!;
                    property.PropertyName = PropertyName!;
                    property.PropertyLocationState = State!;
                    property.PropertyLocationCountry = Country!;
                    property.PropertyAddress = PropertyAddress!;
                    property.PropertyType = PropertyType!;
                    property.NumberOfRooms = RoomNumber;
                    property.PropertyDescription = Description!;
                    property.PropertyPrice = PropertyPrice;
                    property.Occupied = Occupied;
                    if (Image1 == null)
                    {
                        property.Image1 = Property.Image1!;
                    }
                    else
                    {
                        property.Image1 = ConvertToByteArray(Image1);
                    }
                    if (Image2 == null)
                    {
                        property.Image2 = Property.Image2!;
                    }
                    else
                    {
                        property.Image2 = ConvertToByteArray(Image2);
                    }
                    if (Image3 == null)
                    {
                        property.Image3 = Property.Image3!;
                    }
                    else
                    {
                        property.Image3 = ConvertToByteArray(Image3);
                    }

                    if (Image4 == null)
                    {
                        property.Image4 = Property.Image4!;
                    }
                    else
                    {
                        property.Image4 = ConvertToByteArray(Image4);
                    }
                    if (Image5 == null)
                    {
                        property.Image5 = Property.Image5!;
                    }
                    else
                    {
                        property.Image5 = ConvertToByteArray(Image5);
                    }
                    if (Image6 == null)
                    {
                        property.Image6 = Property.Image6!;
                    }
                    else
                    {
                        property.Image6 = ConvertToByteArray(Image7);
                    }
                    if (Image7 == null)
                    {
                        property.Image7 = Property.Image7!;
                    }
                    else
                    {
                        property.Image7 = ConvertToByteArray(Image7);
                    }
                    if (Image8 == null)
                    {
                        property.Image8 = Property.Image8!;
                    }
                    else
                    {
                        property.Image8 = ConvertToByteArray(Image8);
                    }
                    if (Image9 == null)
                    {
                        property.Image9 = Property.Image9!;
                    }
                    else
                    {
                        property.Image9 = ConvertToByteArray(Image9);
                    }
                    if (Image10 == null)
                    {
                        property.Image10 = Property.Image10!;
                    }
                    else
                    {
                        property.Image10 = ConvertToByteArray(Image10);
                    }

                    #endregion

                }
                bool success = controller.UpdateProperty(property);

                if (success)
                {
                    SucceessMessage = "The Property Updated SuccessFully";
                    HttpContext.Session.SetString("PropertyHasBeenupdated", "True");
                }
                else
                {
                    SucceessMessage = "An Error Occured When trying to Update this Property";
                }

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