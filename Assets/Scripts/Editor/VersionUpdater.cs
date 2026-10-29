using System;
using System.Diagnostics;
using UnityEditor;

public static class VersionUpdater
{
    private enum UpdateType
    {
        Major,
        Minor,
        Patch
    }

    private const string MenuName = "Version Tag";

    private static int[] GetCurrentVersionNumbers ()
    {
        int[] versionNumbers = new int[3];

        try
        {
            string currentVersion = PlayerSettings.bundleVersion;
            string[] versionParts = currentVersion.Split('.');

            if (versionParts.Length != versionNumbers.Length)
                throw new Exception("Current bundle version doesn't have 'major.minor.patch' format.");

            for (int i = 0; i < versionParts.Length; i++)
                versionNumbers[i] = Convert.ToInt32(versionParts[i]);
        }
        catch (Exception exception)
        {
            UnityEngine.Debug.LogError(exception.Message);
        }

        return versionNumbers;
    }

    private static string GetBundleVersion (int[] versionNumbers)
    {
        string bundleVersion = string.Empty;

        try
        {
            if (versionNumbers.Length != 3)
                throw new Exception("Version numbers must be three to comply with the 'major.minor.patch' format.");

            for (int i = 0; i < versionNumbers.Length; i++)
            {
                bundleVersion += versionNumbers[i].ToString();

                if (i < versionNumbers.Length - 1)
                    bundleVersion += ".";
            }
        }
        catch (Exception exception)
        {
            UnityEngine.Debug.LogError(exception.Message);
        }

        return bundleVersion;
    }

    private static string GetIncreasedBundleVersion (UpdateType updateType)
    {
        int[] versionNumbers = GetCurrentVersionNumbers();

        switch (updateType)
        {
            case UpdateType.Patch:
                versionNumbers[2]++;
                break;

            case UpdateType.Minor:
                versionNumbers[1]++;
                versionNumbers[2] = 0;
                break;

            case UpdateType.Major:
                versionNumbers[0]++;
                versionNumbers[1] = 0;
                versionNumbers[2] = 0;
                break;
        }

        return GetBundleVersion(versionNumbers);
    }

    private static void TrySetVersion (UpdateType updateType)
    {
        const string targetBranch = "main";

        try
        {
            if (!ValidateCurrentBranch(targetBranch))
                throw new Exception($"cannot change version on a branch that is not '{targetBranch}'");

            string newVersion = GetIncreasedBundleVersion(updateType);
            int newAndroidCode = PlayerSettings.Android.bundleVersionCode + 1;
            int newiOSBuildNumber = Convert.ToInt32(PlayerSettings.iOS.buildNumber) + 1;

            string title = $"{updateType} update!";

            string message = $"Are you sure you want to increment the product version? {Environment.NewLine}" +
                             $"WARNING: This action will attempt to commit, tag and push the change to version control. {Environment.NewLine}" +
                             $"{Environment.NewLine}" +
                             $"Application version: {PlayerSettings.bundleVersion} → {newVersion} {Environment.NewLine}" +
                             $"Android bundle code: {PlayerSettings.Android.bundleVersionCode} → {newAndroidCode} {Environment.NewLine}" +
                             $"iOS build number: {PlayerSettings.iOS.buildNumber} → {newiOSBuildNumber} {Environment.NewLine}";

            if (EditorUtility.DisplayDialog(title, message, "Yes", "No"))
            {
                UpdateAppVersion(newVersion, newAndroidCode, newiOSBuildNumber);
                CommitTagAndPushVersionChange(newVersion, newAndroidCode, newiOSBuildNumber);

                EditorUtility.DisplayDialog("Success!", "The product version has increased and is ready for a build", "Ok");
            }
        }
        catch (Exception e)
        {
            EditorUtility.DisplayDialog("Error!", $"The product version could not be changed: {e.Message}", "Ok");
        }
    }

    private static bool ValidateCurrentBranch (string targetBranch)
    {
        string args = "/C " + $"git rev-parse --abbrev-ref HEAD";

        ProcessStartInfo processStartInfo = new ProcessStartInfo()
        {
            FileName = "cmd.exe",
            Arguments = args,
            WindowStyle = ProcessWindowStyle.Hidden,
            UseShellExecute = false,
            RedirectStandardOutput = true
        };

        Process process = Process.Start(processStartInfo);

        process.WaitForExit();

        string checkedOutBranch = process.StandardOutput.ReadLine();

        process.Close();

        return checkedOutBranch == targetBranch;
    }

    private static void UpdateAppVersion (string newVersion, int newAndroidCode, int newiOSBuildNumber)
    {
        PlayerSettings.bundleVersion = newVersion;
        PlayerSettings.Android.bundleVersionCode = newAndroidCode;
        PlayerSettings.iOS.buildNumber = newiOSBuildNumber.ToString();

        AssetDatabase.SaveAssets();
    }

    private static void CommitTagAndPushVersionChange (string newVersion, int androidCode, int iOSBuildNumber)
    {
        string args = "/C " +
                      $"git add \"ProjectSettings/ProjectSettings.asset\"&&" +
                      $"git commit -m \"Update version to {newVersion} (Android {androidCode} and iOS {iOSBuildNumber})\"&&" +
                      $"git tag \"{newVersion}\"&&" +
                      $"git push&&" +
                      $"git push --tags";

        ProcessStartInfo processStartInfo = new ProcessStartInfo()
        {
            FileName = "cmd.exe",
            Arguments = args,
            WindowStyle = ProcessWindowStyle.Hidden
        };

        Process process = Process.Start(processStartInfo);

        process.WaitForExit();
        process.Close();
    }


    [MenuItem(MenuName + "/Version/New patch...")]
    private static void SetNewPatch ()
    {
        TrySetVersion(UpdateType.Patch);
    }

    [MenuItem(MenuName + "/Version/New minor...")]
    private static void SetNewMinor ()
    {
        TrySetVersion(UpdateType.Minor);
    }

    [MenuItem(MenuName + "/Version/New major...")]
    private static void SetNewMajor ()
    {
        TrySetVersion(UpdateType.Major);
    }
}