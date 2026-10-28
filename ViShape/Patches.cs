using HarmonyLib;
using UnityEngine;
using System.Linq;

namespace ViShape;

[HarmonyPatch(typeof(RoleManager), nameof(RoleManager.SelectRoles))]
public static class SelectRolesPatch
{
    public static void Postfix() { ViShapeRole.Clear(); ViShapeEffects.Reset(); if (AmongUsClient.Instance.AmHost && ViShapeSettings.Enabled) { var chosen = ViShapeRole.SelectViShapes(); ViShapeRpc.SendAssign(chosen); ViShapeRpc.SendSyncSettings(); } }
}

[HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public static class HudUpdatePatch
{
    public static void Postfix(HudManager __instance)
    {
        if (AmongUsClient.Instance.GameState != InnerNet.GameStates.Started) return;
        float dt = Time.deltaTime; ViShapeEffects.UpdateEffects(dt);
        if (ViShapeRole.LocalIsViShape)
        {
            if (ViShapeRole.GasCd > 0f) ViShapeRole.GasCd -= dt;
            if (ViShapeRole.AcidCd > 0f) ViShapeRole.AcidCd -= dt;
            if (ViShapeRole.ShapeshiftCd > 0f) ViShapeRole.ShapeshiftCd -= dt;
            if (ViShapeRole.LocalDisguised) { ViShapeRole.DisguiseTimer -= dt; if (ViShapeRole.DisguiseTimer <= 0f) { ViShapeAbilities.PerformShapeshift(byte.MaxValue, true); ViShapeRole.ShapeshiftCd = ViShapeSettings.ShapeshiftCooldown; } }
        }
        ViShapeHud.UpdateHud(__instance);
    }
}

[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
public static class MurderPlayerPatch
{
    public static bool Prefix(PlayerControl __instance, PlayerControl target) { if (ViShapeRole.IsViShape(__instance)) { ViShapeAbilities.AcidKill(target.PlayerId); return false; } return true; }
}

[HarmonyPatch(typeof(DeadBody), nameof(DeadBody.OnClick))]
public static class DeadBodyReportPatch
{
    public static bool Prefix(DeadBody __instance) { if (ViShapeEffects.MeltedBodies.Contains(__instance.ParentPlayerId)) return false; return true; }
}

[HarmonyPatch(typeof(Vent), nameof(Vent.EnterVent))]
public static class VentEnterPatch
{
    public static void Postfix(Vent __instance, PlayerControl player) { if (ViShapeSettings.VentGas && ViShapeRole.IsViShape(player)) { ViShapeAbilities.ReleaseViperGasLocal(player.PlayerId, __instance.transform.position); ViShapeRpc.SendGas(player.PlayerId, __instance.transform.position); } }
}

[HarmonyPatch(typeof(LightSource), nameof(LightSource.CanSee))]
public static class LightSourceVisionPatch
{
    public static void Postfix(LightSource __instance, Vector2 targetPos, ref bool __result) { if (__result && ViShapeEffects.InGas(targetPos)) { if (!ViShapeRole.LocalIsViShape) __result = Random.value < ViShapeSettings.GasVision; } }
}

[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
public static class PlayerSpeedPatch
{
    public static void Postfix(PlayerControl __instance) { if (ViShapeEffects.InAcid(__instance.transform.position)) { if (!ViShapeRole.IsViShape(__instance)) __instance.MyPhysics.Velocity *= ViShapeSettings.AcidSlow; } }
}
