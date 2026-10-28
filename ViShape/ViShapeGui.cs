using UnityEngine;

namespace ViShape;

public class ViShapeGui : MonoBehaviour
{
    public static bool ShowSelector;

    private void OnGUI()
    {
        if (AmongUsClient.Instance == null) return;

        if (AmongUsClient.Instance.GameState == InnerNet.GameStates.Joined && AmongUsClient.Instance.AmHost)
        {
            GUILayout.BeginArea(new Rect(10, 150, 250, 400), "ViShape Mod Ayarlari", GUI.skin.box);
            ViShapeSettings.Enabled = GUILayout.Toggle(ViShapeSettings.Enabled, "Mod Aktif");
            if (ViShapeSettings.Enabled)
            {
                GUILayout.Label($"Maksimum Sayi: {ViShapeSettings.MaxCount}");
                ViShapeSettings.MaxCount = Mathf.RoundToInt(GUILayout.HorizontalSlider(ViShapeSettings.MaxCount, 1, 3));
                
                GUILayout.Label($"Gaz Cooldown: {ViShapeSettings.GasCooldown:F0}s");
                ViShapeSettings.GasCooldown = GUILayout.HorizontalSlider(ViShapeSettings.GasCooldown, 10, 60);

                GUILayout.Label($"Asit Cooldown: {ViShapeSettings.AcidCooldown:F0}s");
                ViShapeSettings.AcidCooldown = GUILayout.HorizontalSlider(ViShapeSettings.AcidCooldown, 10, 60);
            }
            GUILayout.EndArea();
        }

        if (ShowSelector && ViShapeRole.LocalIsViShape && !PlayerControl.LocalPlayer.Data.IsDead)
        {
            Rect windowRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 200, 300);
            GUILayout.BeginArea(windowRect, "Morf Hedefi Sec", GUI.skin.box);
            
            foreach (var p in PlayerControl.AllPlayerControls)
            {
                if (p.PlayerId == PlayerControl.LocalPlayer.PlayerId || p.Data.Disconnected || p.Data.IsDead) continue;
                
                if (GUILayout.Button(p.Data.PlayerName))
                {
                    ViShapeAbilities.PerformShapeshift(p.PlayerId, false);
                    ViShapeRole.DisguiseTimer = ViShapeSettings.ShapeshiftDuration;
                    ShowSelector = false;
                    break;
                }
            }
            if (GUILayout.Button("Kapat")) ShowSelector = false;
            GUILayout.EndArea();
        }
    }
}
