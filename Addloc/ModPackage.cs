using System.Reflection;

namespace Addloc
{
    public abstract class ModPackage<T> : ModInitializer
        where T : class, new()
    {
        public abstract string packageId
        {
            get;
        }

        private static readonly T _instance = new T();

        public static string PackageId
        {
            get
            {
                PropertyInfo packageId = typeof(T).GetProperty("packageId", BindingFlags.Instance | BindingFlags.Public);

                if (packageId == null || packageId != typeof(string))
                {
                    string name = typeof(T).Name;

                    throw new InvalidOperationException(name + " must be implement the 'packageId' property with public.");
                }

                return (string)packageId.GetValue(_instance);
            }
        }

        public static string AssemblyPath
        {
            get
            {
                return typeof(T).GetType().Assembly.Location;
            }
        }
    }
}
