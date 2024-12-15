using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class LocalizationPostProccess
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuildProject)
    {
        if (target != BuildTarget.iOS)
        {
            return;
        }

        // Названия приложения для разных языков
        string appNameEnglish = "Super football square"; // Название для английского языка
        string appNamePortuguese = "Super praça de futebol"; // Название для португальского языка

        // Путь к Info.plist
        string plistPath = Path.Combine(pathToBuildProject, "Info.plist");

        // Настройка локализации названия приложения
        AddLocalizedDisplayNames(plistPath, pathToBuildProject, appNameEnglish, appNamePortuguese);
        
        AddOtherLinkerFlag(pathToBuildProject);
    }
    private static void AddOtherLinkerFlag(string pathToBuildProject)
    {
        // Путь к Xcode проекту
        string pbxProjectPath = Path.Combine(pathToBuildProject, "Unity-iPhone.xcodeproj/project.pbxproj");

        // Загружаем проект
        PBXProject project = new PBXProject();
        project.ReadFromFile(pbxProjectPath);

        // Получаем идентификатор UnityFramework
        string unityFrameworkTarget = project.GetUnityFrameworkTargetGuid();

        // Проверяем наличие флага, чтобы не дублировать
        string otherLinkerFlags = project.GetBuildPropertyForAnyConfig(unityFrameworkTarget, "OTHER_LDFLAGS");
        if (otherLinkerFlags == null || !otherLinkerFlags.Contains("-ld64"))
        {
            // Добавляем флаг -ld64
            project.AddBuildProperty(unityFrameworkTarget, "OTHER_LDFLAGS", "-ld64");
            Debug.Log("Added -ld64 to Other Linker Flags for UnityFramework.");
        }

        // Сохраняем изменения
        project.WriteToFile(pbxProjectPath);
    }
    private static void AddLocalizedDisplayNames(string plistPath, string buildPath, string appNameEnglish, string appNamePortuguese)
    {
        // Проверяем наличие Info.plist
        if (!File.Exists(plistPath))
        {
            Debug.LogError($"Info.plist not found at {plistPath}");
            return;
        }

        // Путь к локализованным папкам
        string enLprojPath = Path.Combine(buildPath, "en.lproj");
        string ptLprojPath = Path.Combine(buildPath, "pt.lproj");

        // Создаем локализованные папки, если их нет
        if (!Directory.Exists(enLprojPath)) Directory.CreateDirectory(enLprojPath);
        if (!Directory.Exists(ptLprojPath)) Directory.CreateDirectory(ptLprojPath);

        // Создаем файлы InfoPlist.strings
        string enStringsPath = Path.Combine(enLprojPath, "InfoPlist.strings");
        string ptStringsPath = Path.Combine(ptLprojPath, "InfoPlist.strings");

        // Записываем локализованные строки
        File.WriteAllText(enStringsPath, $"\"CFBundleDisplayName\" = \"{appNameEnglish}\";");
        File.WriteAllText(ptStringsPath, $"\"CFBundleDisplayName\" = \"{appNamePortuguese}\";");

        Debug.Log("Localized display names added successfully!");
    }
}
