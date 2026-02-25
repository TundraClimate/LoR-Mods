using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Addloc
{
    internal class Artwork
    {
        public Artwork(string packageId, string packagePath)
        {
            this._artworkPath = packagePath + "\\Artwork\\";
        }

        public void LoadBattleUnitBufIcons()
        {
            string dirPath = this._artworkPath + "BattleUnitBuf";

            if (!Directory.Exists(dirPath))
            {
                return;
            }

            Dictionary<string, Sprite> iconDict = BattleUnitBuf._bufIconDictionary;

            foreach (string unitBuf in Walkdir.GetFilesRecursive(dirPath))
            {
                string fileName = Path.GetFileName(unitBuf);
                string unitBufId;

                if (fileName.EndsWith(".png") || fileName.EndsWith(".jpg"))
                {
                    unitBufId = fileName.Substring(0, fileName.Length - 4);
                }
                else
                {
                    continue;
                }

                iconDict.Add(unitBufId, Artwork.CreateSprite(unitBuf));
            }
        }

        private static Sprite CreateSprite(string path)
        {
            byte[] pngData = File.ReadAllBytes(path);

            Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            if (!ImageConversion.LoadImage(texture, pngData))
            {
                return null;
            }

            return Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                50f
            );
        }

        private string _artworkPath;
    }
}
