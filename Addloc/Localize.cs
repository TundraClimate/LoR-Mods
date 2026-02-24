namespace Addloc
{
    public class ModResource : Singleton<ModResource>
    {
        public void RegisterMOD<T>()
            where T : ModPackage<T>, new()
        {
            Artwork artwork = new Artwork(ModPackage<T>.PackageId, ModPackage<T>.AssemblyPath);
        }
    }
}
