namespace Addloc
{
    public class ModResource
    {
        public static void RegisterMOD<T>()
            where T : ModPackage<T>, new()
        {
            Artwork artwork = new Artwork(ModPackage<T>.PackageId, ModPackage<T>.AssemblyPath);

            artwork.LoadBattleUnitBufIcons();
            artwork.LoadStoryIcons("_Overlay");

            LocalizeXml xml = new LocalizeXml();
        }
    }
}
