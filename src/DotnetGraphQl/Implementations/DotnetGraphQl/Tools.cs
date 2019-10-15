namespace DotnetGraphQl
{
    public static class Tools
    {
        public static bool IsEqual(object updatedObject, object subscribedObject)
        {
            var updatedProperties = updatedObject.GetType().GetProperties();
            var subscribedProperties = subscribedObject.GetType().GetProperties();

            foreach (var updatedProperty in updatedProperties)
            {
                foreach (var subscribedProperty in subscribedProperties)
                {
                    if (updatedProperty.Name == subscribedProperty.Name 
                        && updatedProperty.PropertyType == subscribedProperty.PropertyType)
                    {
                        var updatedValue = updatedProperty.GetValue(updatedObject);
                        var subscribedValue = subscribedProperty.GetValue(subscribedObject);
                        if (updatedValue != subscribedValue)
                        {
                            return false;
                        }

                        break;
                    }
                }
            }
                
            return true;
        }
    }
}