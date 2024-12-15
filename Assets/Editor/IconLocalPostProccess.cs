using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using UnityEngine;

public class IconLocalPostProccess
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuildProject)
    {
        if (target != BuildTarget.iOS)
        {
            return;
        }

        // Путь к папке с иконками
        string assetsPath = Path.Combine(pathToBuildProject, "Assets.xcassets");

        // Создаем локализованные папки для английского и португальского
        CreateLocalizedAppIcon(assetsPath, "en.lproj", "AppIcon_EN");
        CreateLocalizedAppIcon(assetsPath, "pt.lproj", "AppIcon_PT");

        // Настройка Info.plist для поддержки локализованных иконок
        string plistPath = Path.Combine(pathToBuildProject, "Info.plist");
        AddIconsToPlist(plistPath);
    }

    private static void CreateLocalizedAppIcon(string basePath, string localeFolder, string iconName)
    {
        // Путь к папке локализации
        string localizedPath = Path.Combine(basePath, localeFolder);

        // Создаем папку, если ее нет
        if (!Directory.Exists(localizedPath))
        {
            Directory.CreateDirectory(localizedPath);
        }

        // Путь к иконке (здесь предполагается, что вы заранее подготовили иконки в проекте Unity)
        string sourceIconPath = Path.Combine("Assets/Icons", iconName + ".appiconset");
        string targetIconPath = Path.Combine(localizedPath, iconName + ".appiconset");

        if (!Directory.Exists(sourceIconPath))
        {
            Debug.LogError($"Icon source folder not found: {sourceIconPath}");
            return;
        }

        // Копируем файлы иконки в папку локализации
        CopyDirectory(sourceIconPath, targetIconPath);
    }

    private static void CopyDirectory(string sourceDir, string targetDir)
    {
        Directory.CreateDirectory(targetDir);

        foreach (var file in Directory.GetFiles(sourceDir))
        {
            string destFile = Path.Combine(targetDir, Path.GetFileName(file));
            File.Copy(file, destFile, true);
        }

        foreach (var dir in Directory.GetDirectories(sourceDir))
        {
            string destDir = Path.Combine(targetDir, Path.GetFileName(dir));
            CopyDirectory(dir, destDir);
        }
    }

    private static void AddIconsToPlist(string plistPath)
    {
        if (!File.Exists(plistPath))
        {
            Debug.LogError($"Info.plist not found at {plistPath}");
            return;
        }

        // Чтение текущего содержимого Info.plist
        string plistContent = File.ReadAllText(plistPath);

        // Добавляем ключи для локализованных иконок
        if (!plistContent.Contains("CFBundleIcons"))
        {
            plistContent = plistContent.Replace(
                "</dict>",
                @"<key>CFBundleIcons</key>
    <dict>
        <key>CFBundlePrimaryIcon</key>
        <dict>
            <key>CFBundleIconFiles</key>
            <array>
                <string>AppIcon</string>
            </array>
        </dict>
    </dict>
</dict>");
        }

        // Сохраняем изменения
        File.WriteAllText(plistPath, plistContent);
    }
}
