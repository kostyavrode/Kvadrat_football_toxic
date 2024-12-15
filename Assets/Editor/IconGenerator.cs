using UnityEditor;
using UnityEngine;
using System.IO;

public class IconGenerator
{
    [MenuItem("Tools/Generate App Icons")]
    public static void GenerateIcons()
    {
        // Путь к иконке 1024x1024
        string sourcePath = "Assets/Icons/AppIcon_EN.png";

        // Папка для сохранения
        string outputFolder = "Assets/Icons/AppIcon_EN.appiconset";

        // Создаем папку, если ее нет
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Размеры и названия для iOS
        var iconSizes = new (string name, int size)[]
        {
            ("AppIcon20x20@1x.png", 20),
            ("AppIcon20x20@2x.png", 40),
            ("AppIcon20x20@3x.png", 60),
            ("AppIcon29x29@1x.png", 29),
            ("AppIcon29x29@2x.png", 58),
            ("AppIcon29x29@3x.png", 87),
            ("AppIcon40x40@1x.png", 40),
            ("AppIcon40x40@2x.png", 80),
            ("AppIcon40x40@3x.png", 120),
            ("AppIcon60x60@2x.png", 120),
            ("AppIcon60x60@3x.png", 180),
            ("AppIcon76x76@1x.png", 76),
            ("AppIcon76x76@2x.png", 152),
            ("AppIcon83.5x83.5@2x.png", 167),
            ("AppIcon1024x1024.png", 1024)
        };

        // Загружаем исходное изображение
        Texture2D sourceTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(sourcePath);

        if (sourceTexture == null)
        {
            Debug.LogError("Исходная иконка не найдена!");
            return;
        }

        // Генерация иконок
        foreach (var (name, size) in iconSizes)
        {
            Texture2D resizedTexture = ResizeTexture(sourceTexture, size, size);
            byte[] pngData = resizedTexture.EncodeToPNG();
            File.WriteAllBytes(Path.Combine(outputFolder, name), pngData);
        }

        AssetDatabase.Refresh();
        Debug.Log("Иконки успешно сгенерированы!");
    }

    private static Texture2D ResizeTexture(Texture2D source, int width, int height)
    {
        RenderTexture rt = RenderTexture.GetTemporary(width, height);
        RenderTexture.active = rt;
        Graphics.Blit(source, rt);

        Texture2D result = new Texture2D(width, height);
        result.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        result.Apply();

        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);

        return result;
    }
}
