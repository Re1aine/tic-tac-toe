using VContainer;

public static class VContainerExtensions
{
    public static T ResolveInstance<T>(this IObjectResolver resolver) =>
        (T)resolver.Resolve(new RegistrationBuilder(typeof(T), Lifetime.Transient).Build());
    
    public static T ResolveInstance<T>(this IObjectResolver resolver, Lifetime lifetime)
    {
        var registrationBuilder = new RegistrationBuilder(typeof(T), lifetime);
        Registration registration = registrationBuilder.Build();
        return (T)resolver.Resolve(registration);
    }
    
    public static T ResolveInstance<T, TParam>(this IObjectResolver resolver, TParam parameter)
    {
        var registrationBuilder = new RegistrationBuilder(typeof(T), Lifetime.Transient);
        registrationBuilder.WithParameter(parameter);
        return (T)resolver.Resolve(registrationBuilder.Build());
    }
    
}