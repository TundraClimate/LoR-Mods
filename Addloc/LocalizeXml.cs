namespace Addloc
{
    public class LocalizeXml
    {
        public LocalizeXml(string packageId, string packagePath)
        {
            this._packageId = packageId;
            this._localizePath = packagePath + "\\Localize\\";
        }

        private string _packageId;

        private string _localizePath;
    }
}
