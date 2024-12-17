using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;
using UnityEngine;
public class LocalizationPostProcess
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuildProject)
    {
        if (target != BuildTarget.iOS)
            return;

        // Получаем название проекта из Unity
        string appName = PlayerSettings.productName;

        // Путь к директории Xcode проекта
        string localizationFolderPath = Path.Combine(pathToBuildProject, "en.lproj");

        // Проверяем, существует ли папка для локализации, если нет — создаем её
        if (!Directory.Exists(localizationFolderPath))
        {
            Directory.CreateDirectory(localizationFolderPath);
        }

        // Путь к файлу InfoPlist.strings
        string stringsFilePath = Path.Combine(localizationFolderPath, "InfoPlist.strings");

        // Контент для английской локализации, используя настоящее название проекта
        string localizedContent = $"\"CFBundleDisplayName\" = \"{appName}\";";

        // Записываем контент в файл
        File.WriteAllText(stringsFilePath, localizedContent);

        // Добавляем локализованный файл в Xcode проект
        string pbxProjectPath = PBXProject.GetPBXProjectPath(pathToBuildProject);
        PBXProject project = new PBXProject();
        project.ReadFromFile(pbxProjectPath);

        string mainTargetGuid = project.GetUnityMainTargetGuid();

        // Добавляем файл InfoPlist.strings в проект
        string localizationPath = $"{localizationFolderPath}/InfoPlist.strings";
        string fileGuid = project.AddFile(localizationPath, localizationPath, PBXSourceTree.Source);

        // Привязываем файл к основной цели
        project.AddFileToBuild(mainTargetGuid, fileGuid);

        project.WriteToFile(pbxProjectPath);

        Debug.Log($"English localization file created with app name '{appName}' and added to Xcode project: {stringsFilePath}");
    }
}
