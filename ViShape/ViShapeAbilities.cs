using UnityEngine;

namespace ViShape;

public static class ViShapeAbilities
{
    public static void PerformShapeshift(byte targetId, bool isUndo)
    {
        if (PlayerControl.LocalPlayer == null) return;
        ViShapeRpc.SendShapeshift(PlayerControl.LocalPlayer.PlayerId, targetId, isUndo);
    }

    public static void PerformShapeshiftLocal(byte sourceId, byte targetId, bool isUndo)
    {
        ViShapeRole.SetDisguised(sourceId, !isUndo); var sourcePlayer = Utils.GetPlayerById(sourceId);
        if (isUndo) { if (sourcePlayer != null) sourcePlayer.RpcUnsetDisguise(); }
        else
        {
            var targetPlayer = Utils.GetPlayerById(targetId);
            if (sourcePlayer != null && targetPlayer != null) { sourcePlayer.RpcSetDisguise(targetPlayer); ViShapeEffects.CreateGasCloud(sourcePlayer.transform.position, 1.5f, 3f); }
        }
    }

    public static void ReleaseViperGas()
    {
        if (PlayerControl.LocalPlayer == null) return;
        ViShapeRpc.SendGas(PlayerControl.LocalPlayer.PlayerId, PlayerControl.LocalPlayer.transform.position);
    }

    public static void ReleaseViperGasLocal(byte sourceId, Vector2 position) { ViShapeEffects.CreateGasCloud(position, ViShapeSettings.GasDuration, ViShapeSettings.GasRadius); }

    public static void AcidKill(byte targetId)
    {
        if (PlayerControl.LocalPlayer == null) return;
        ViShapeRpc.SendAcidKill(PlayerControl.LocalPlayer.PlayerId, targetId);
    }

    public static void AcidKillLocal(byte killerId, byte targetId)
    {
        var killer = Utils.GetPlayerById(killerId); var target = Utils.GetPlayerById(targetId);
        if (killer == null || target == null) return;
        Vector2 targetPos = target.transform.position; target.Die(DeathReason.Kill);
        ViShapeEffects.MeltBodyOfPlayer(targetId); ViShapeEffects.CreateAcidPool(targetPos, ViShapeSettings.AcidPoolDuration);
    }
}
