using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace build
{
    public class BuildScript
    {
        static readonly string Eol = Environment.NewLine;
        
        private static readonly string[] Secrets =
            {"androidKeystorePass", "androidKeyaliasName", "androidKeyaliasPass"};

        
        [MenuItem("Builds/CI")]
        public static void TestBuildGame() {
            string[] args = {
                "-platform=Android", 
                "-versionCode=1",
                "-ANDROID_KEYSTORE_PASS=****",
                "-ANDROID_KEYALIAS_NAME=****",
                "-ANDROID_KEYALIAS_PASS=****",
            };
            BuildGameImplementation(args, new Dictionary<string, string>(),shutdown: false);
        }
        [UsedImplicitly]
        public static void BuildGame() => BuildGameImplementation(Environment.GetCommandLineArgs(), GetValidatedOptions());
        static void BuildGameImplementation(string[] args, Dictionary<string, string> options, bool shutdown = true)
        {
            string platform = args.FirstOrDefault(arg => arg.StartsWith("-platform="))?.Replace("-platform=", "");
            string versionCodeString = args.FirstOrDefault(arg => arg.StartsWith("-versionCode="))?.Replace("-versionCode=", "");
            string versionString = args.FirstOrDefault(arg => arg.StartsWith("-version="))?.Replace("-version=", "");
            string androidKeystorePass = options["androidKeystorePass"];
            string androidKeyAliasName = options["androidKeyaliasName"];
            string androidKeyAliasPass = options["androidKeyaliasPass"];
            if (!int.TryParse(versionCodeString, out int versionCode))
                throw new ArgumentException($"versionCode argument not formatted correctly: {versionCodeString}");

            string[] scenes;
            string defineSymbols;
            string appendix = "";
            BuildTarget buildTarget;

            switch (platform)
            {
                case "Android":
                    // KeystoreDebug(options);
                    EditorUserBuildSettings.buildAppBundle = true;
                    PlayerSettings.Android.useCustomKeystore = true;
                    PlayerSettings.Android.keyaliasPass = androidKeyAliasPass;
                    PlayerSettings.Android.keyaliasName = androidKeyAliasName;
                    PlayerSettings.Android.keystorePass = androidKeystorePass;
                    PlayerSettings.Android.keystoreName = "user.keystore";

                    scenes = new[] { "Assets/Scenes/Menu.unity", "Assets/Scenes/Game.unity" };
                    defineSymbols = "MOBILE";
                    buildTarget = BuildTarget.Android;
                    PlayerSettings.applicationIdentifier = "com.massivecreationlab.workshop.furious_poultry";
                    PlayerSettings.Android.bundleVersionCode = versionCode;
                    PlayerSettings.bundleVersion = versionString;
                    appendix = ".aab";
                    break;
                case "iOS":
                    scenes = new[] { "Assets/Scenes/Menu.unity", "Assets/Scenes/Game.unity" };
                    defineSymbols = "MOBILE";
                    buildTarget = BuildTarget.iOS;
                    PlayerSettings.applicationIdentifier = "com.massivecreationlab.workshop.furious_poultry";
                    PlayerSettings.iOS.buildNumber = versionCode.ToString();
                    PlayerSettings.bundleVersion = versionString;
                    break;
                case "LinuxStandalone64":
                    scenes = new[] { "Assets/Scenes/Menu.unity", "Assets/Scenes/Game.unity" };
                    defineSymbols = "DESKTOP";
                    buildTarget = BuildTarget.StandaloneLinux64;
                    break;
                case "StandaloneWindows64":
                    scenes = new[] { "Assets/Scenes/Menu.unity", "Assets/Scenes/Game.unity" };
                    defineSymbols = "DESKTOP";
                    buildTarget = BuildTarget.StandaloneWindows64;
                    appendix = ".exe";
                    break;
                case "Mac":
                    scenes = new[] { "Assets/Scenes/Menu.unity", "Assets/Scenes/Game.unity" };
                    defineSymbols = "DESKTOP";
                    buildTarget = BuildTarget.StandaloneOSX;
                    break;
                default:
                    Debug.LogError("Unknown game name. Please provide a valid game name.");
                    return;
            }

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = $"./Builds/{platform}/{platform}{appendix}",
                target = buildTarget,
                options = BuildOptions.None
            };
            Debug.Log(EditorUserBuildSettings.GetBuildLocation(BuildTarget.Android));

            // Set the scripting define symbols
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, defineSymbols);

            // Build the game
            BuildReport buildPlayer = BuildPipeline.BuildPlayer(buildPlayerOptions);
            
            ReportSummary(buildPlayer.summary);
            if(shutdown)
                ExitWithResult(buildPlayer.summary.result);
        }
        
        private static Dictionary<string, string> GetValidatedOptions()
        {
            ParseCommandLineArguments(out var validatedOptions);

            // if (!validatedOptions.TryGetValue("projectPath", out _))
            // {
            //     Console.WriteLine("Missing argument -projectPath");
            //     EditorApplication.Exit(110);
            // }
            //
            // if (!validatedOptions.TryGetValue("buildTarget", out var buildTarget))
            // {
            //     Console.WriteLine("Missing argument -buildTarget");
            //     EditorApplication.Exit(120);
            // }
            //
            // if (!Enum.IsDefined(typeof(BuildTarget), buildTarget ?? string.Empty))
            //     EditorApplication.Exit(121);
            //
            // if (validatedOptions.TryGetValue("buildPath", out var buildPath))
            //     validatedOptions["customBuildPath"] = buildPath;
            //
            // if (validatedOptions.TryGetValue("customBuildPath", out _))
            //     return validatedOptions;
            //
            // Console.WriteLine("Missing argument -customBuildPath");
            // EditorApplication.Exit(130);

            return validatedOptions;
        }
        
        private static void ParseCommandLineArguments(out Dictionary<string, string> providedArguments)
        {
            providedArguments = new Dictionary<string, string>();
            var args = Environment.GetCommandLineArgs();

            Console.WriteLine(
                $"{Eol}" +
                $"###########################{Eol}" +
                $"#    Parsing settings     #{Eol}" +
                $"###########################{Eol}" +
                $"{Eol}"
            );

            // Extract flags with optional values
            for (int current = 0, next = 1; current < args.Length; current++, next++)
            {
                // Parse flag
                var isFlag = args[current].StartsWith("-");
                if (!isFlag) continue;
                var flag = args[current].TrimStart('-');

                // Parse optional value
                var flagHasValue = next < args.Length && !args[next].StartsWith("-");
                var value = flagHasValue ? args[next].TrimStart('-') : "";
                var isSecret = Secrets.Contains(flag);
                var displayValue = isSecret ? "*HIDDEN*" : "\"" + value + "\"";

                // Assign
                Console.WriteLine($"Found flag \"{flag}\" with value {displayValue}.");
                providedArguments.Add(flag, value);
            }
        }
        
        
        // static void KeystoreDebug(Dictionary<string,string> options)
        // {
        //     string androidKeystorePass = options["androidKeystorePass"];
        //     string androidKeyAliasName = options["androidKeyaliasName"];
        //     string androidKeyAliasPass = options["androidKeyaliasPass"];
        //     Console.WriteLine(
        //         $"{Eol}" +
        //         $"###########################{Eol}" +
        //         $"#     Keystore Debug      #{Eol}" +
        //         $"###########################{Eol}" +
        //         $"{Eol}" +
        //         $"ANDROID_KEYALIAS_NAME: {androidKeyAliasName}{Eol}" +
        //         $"ANDROID_KEYALIAS_PASS: {androidKeyAliasPass}{Eol}" +
        //         $"ANDROID_KEYSTORE_PASS: {androidKeystorePass} bytes{Eol}" +
        //         $"{Eol}"
        //     );
        //     
        //     Console.WriteLine(
        //         $"{Eol}" +
        //         $"################################{Eol}" +
        //         $"#  Keystore Debug Peak values  #{Eol}" +
        //         $"################################{Eol}" +
        //         $"{Eol}" +
        //         $"ANDROID_KEYALIAS_NAME: {androidKeyAliasName.Replace('a', 'o')}{Eol}" +
        //         $"ANDROID_KEYALIAS_PASS: {androidKeyAliasPass.Replace('a', 'o')}{Eol}" +
        //         $"ANDROID_KEYSTORE_PASS: {androidKeystorePass.Replace('a', 'o')} bytes{Eol}" +
        //         $"{Eol}"
        //     );
        // }
        
        static void ReportSummary(BuildSummary summary)
        {
            Console.WriteLine(
                $"{Eol}" +
                $"###########################{Eol}" +
                $"#      Build results      #{Eol}" +
                $"###########################{Eol}" +
                $"{Eol}" +
                $"Duration: {summary.totalTime.ToString()}{Eol}" +
                $"Warnings: {summary.totalWarnings.ToString()}{Eol}" +
                $"Errors: {summary.totalErrors.ToString()}{Eol}" +
                $"Size: {summary.totalSize.ToString()} bytes{Eol}" +
                $"{Eol}"
            );
        }

        static void ExitWithResult(BuildResult result)
        {
            switch (result)
            {
                case BuildResult.Succeeded:
                    Console.WriteLine("Build succeeded!");
                    EditorApplication.Exit(0);
                    break;
                case BuildResult.Failed:
                    Console.WriteLine("Build failed!");
                    EditorApplication.Exit(101);
                    break;
                case BuildResult.Cancelled:
                    Console.WriteLine("Build cancelled!");
                    EditorApplication.Exit(102);
                    break;
                case BuildResult.Unknown:
                default:
                    Console.WriteLine("Build result is unknown!");
                    EditorApplication.Exit(103);
                    break;
            }
        }
    }
}