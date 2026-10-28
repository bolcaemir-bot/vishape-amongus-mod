using UnityEngine;

namespace ViShape;

public static class Utils
{
    public static PlayerControl GetPlayerById(byte id) { foreach (var p in PlayerControl.AllPlayerControls) { if (p.PlayerId == id) return p; } return null; }
    public static PlayerControl GetClosestTarget(float maxRange)
    {
        PlayerControl closest = null; float minDist = maxRange; Vector2 localPos = PlayerControl.LocalPlayer.transform.position;
        foreach (var p in PlayerControl.AllPlayerControls) { if (p.PlayerId == PlayerControl.LocalPlayer.PlayerId || p.Data.Disconnected || p.Data.IsDead || p.Data.Role.IsImpostor) continue; float dist = Vector2.Distance(localPos, p.transform.position); if (dist < minDist) { minDist = dist; closest = p; } } return closest;
    }
}
