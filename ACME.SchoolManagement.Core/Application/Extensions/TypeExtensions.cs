namespace ACME.SchoolManagement.Core.Application.Extensions
{
    public static class TypeExtensions
    {
        public static string GetFriendlyName(this Type type)
        {
            var friendlyName = type.Name;
            if (!type.IsGenericType) return friendlyName;

            var iBacktick = friendlyName.IndexOf('`');
            if (iBacktick > 0) friendlyName = friendlyName.Remove(iBacktick);

            var genericParameters = type.GetGenericArguments().Select(x => x.GetFriendlyName());
            friendlyName += "<" + string.Join(", ", genericParameters) + ">";

            return friendlyName;
        }

        public static IEnumerable<Type> GetImplementedInterfacesFromTypes(this Type implementedType, IEnumerable<Type> types)
        {
            if (types == null)
                throw new ArgumentNullException("types");
            var results = new List<Type>();
            foreach (var type in types)
            {
                if (type.IsClass)
                {
                    var typeInterfaces = type.GetInterfaces();
                    foreach (var interf in typeInterfaces)
                    {
                        if (interf.IsGenericType && interf.GetGenericTypeDefinition() == implementedType.GetGenericTypeDefinition())
                            results.Add(type);
                    }
                }
            }
            return results;
        }
    }
}
