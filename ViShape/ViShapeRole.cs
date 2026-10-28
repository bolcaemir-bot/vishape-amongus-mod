using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ViShape;

public static class ViShapeRole
{
    public const string Name = "ViShape";
    public const string Description = "Sekil degistir, zehirle, asitle erit.";
    public static readonly Color RoleColor = new(0.55f, 0.95f, 0.20f, 1f);

    private static readonly HashSet<byte> players = new();
    private static readonly HashSet<byte> disguised = new();

    public static float ShapeshiftCd;
    public static float GasCd;
    public static float AcidCd;
    public static float DisguiseTimer;

    public static bool IsViShape(PlayerControl p) => p != null && players.Contains(p.PlayerId);
    public static bool LocalIsViShape => IsViShape(PlayerControl.LocalPlayer);
    public static bool IsDisguised(byte id) => disguised.Contains(id);
    public static bool LocalDisguised => PlayerControl.LocalPlayer != null && disguised.Contains(PlayerControl.LocalPlayer.PlayerId);

    public static void Add(byte id) => players.Add(id);
    public static void SetDisguised(byte id, bool on) { if (on) disguised.Add(id); else disguised.Remove(id); }

    public static void Clear()
    {
        players.Clear(); disguised.Clear();
        ShapeshiftCd = GasCd = AcidCd = DisguiseTimer = 0f;
    }

    public static void ResetTimersForRound()
    {
        ShapeshiftCd = Mathf.Min(10f, ViShapeSettings.ShapeshiftCooldown);
        GasCd = Mathf.Min(10f, ViShapeSettings.GasCooldown);
        AcidCd = Mathf.Min(10f, ViShapeSettings.AcidCooldown);
        DisguiseTimer = 0f;
    }

    public static List<byte> SelectViShapes()
    {
        var list = new List<byte>();
        var impostors = PlayerControl.AllPlayerControls.ToArray()
            .Where(p => p != null && p.Data != null && !p.Data.Disconnected && p.Data.Role != null && p.Data.Role.IsImpostor)
            .OrderBy(_ => Random.value).ToList();

        int count = Mathf.Min(ViShapeSettings.MaxCount, impostors.Count);
        for (int i = 0; i < count; i++) list.Add(impostors[i].PlayerId);
        return list;
    }
}
