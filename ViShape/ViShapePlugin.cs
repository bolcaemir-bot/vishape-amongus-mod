using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using Reactor;

namespace ViShape;

[BepInPlugin(Id, "ViShape", Version)]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
public class ViShapePlugin : BasePlugin
{
    public const string Id = "com.example.vishape";
    public const string Version = "1.0.0";
    public static ViShapePlugin Instance { get; private set; }
    public Harmony Harmony { get; } = new(Id);

    public override void Load()
    {
        Instance = this;
        ClassInjector.RegisterTypeInIl2Cpp<ViShapeGui>();
        AddComponent<ViShapeGui>();
        Harmony.PatchAll();
        Log.LogInfo("ViShape mod altyapisi yuklendi.");
    }
}
