using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

class InternalControllerProvider :
    ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo type)
    {
        if (!type.IsClass)
        {
            return false;
        }

        if (type.IsAbstract)
        {
            return false;
        }

        if (type.ContainsGenericParameters)
        {
            return false;
        }

        if (type.IsDefined(typeof(NonControllerAttribute)))
        {
            return false;
        }

        return type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase) ||
               type.IsDefined(typeof(ControllerAttribute));
    }
}