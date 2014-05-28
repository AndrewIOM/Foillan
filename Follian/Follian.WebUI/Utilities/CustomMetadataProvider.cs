using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Foillan.WebUI.Utilities
{
    public class CustomMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType,
            string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            if (modelType.IsEnum && metadata.TemplateHint == null)
            {
                metadata.TemplateHint = "Enum";
            }
            return metadata;
        }
    }
}