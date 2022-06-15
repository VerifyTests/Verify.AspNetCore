using Microsoft.AspNetCore.Mvc.Controllers;

class ControllerProvider :
    ControllerFeatureProvider
{
    Type[] controllers;

    public ControllerProvider(Type[] controllers) =>
        this.controllers = controllers;

    protected override bool IsController(TypeInfo type) =>
        controllers.Contains(type);
}