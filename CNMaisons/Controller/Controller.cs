﻿using CNMaisons.Domain;
using CNMaisons.TechnicalService;

namespace CNMaisons.Controller
{
    public class Controller
    {
        public bool AddProperty(Property property)
        {
            bool success = false;
            Properties properties = new Properties();
            success = properties.AddProperty(property);
            return success;
        }
    }
}