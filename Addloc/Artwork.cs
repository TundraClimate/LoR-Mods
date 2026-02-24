namespace Addloc
{
    internal class Artwork
    {
        public Artwork(string packageId, string packagePath)
        {
            this._artworkPath = packagePath + "\\Artwork\\";
        }

        private string _artworkPath;
    }
}
