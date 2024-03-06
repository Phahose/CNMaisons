using CNMaisons.Domain;
using CNMaisons.TechnicalService;

namespace CNMaisons.Controller
{
    public class BCS
    {
        public bool AddProperty(Property property)
        {
            bool success = false;
            Properties properties = new Properties();
            success = properties.AddProperty(property);
            return success;
        }

        public List<Property> GetProperties()
        {
            List<Property> PropertyList = new List<Property>();
            Properties Properties = new Properties();
            PropertyList = Properties.GetProperty();
            return PropertyList;
        }
    }
}
