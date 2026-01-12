using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class ModResource
{
    public static void RegisterResources()
    {
        ModResource.RegisterArtwork();

        LocalizedTextLoader.Instance.LoadBattleEffectTexts(GlobalGameManager.Instance.CurrentOption.language);
    }

    private static void RegisterArtwork()
    {
        string artworkPath = string.Format("{0}\\Artwork\\", Path.GetDirectoryName(typeof(TestMod).Assembly.Location));
        Dictionary<string, Sprite> iconDict = BattleUnitBuf._bufIconDictionary;

        iconDict.Add(BattleUnitBuf_TestCustomBuf.id, ModResource.CreateSprite(artworkPath + "TestCustomBuf.png"));
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
}
