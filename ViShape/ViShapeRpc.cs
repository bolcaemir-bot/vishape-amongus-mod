using System.Collections.Generic;
using Reactor.Networking.Attributes;
using Reactor.Networking.Rpc;
using Hazel;
using UnityEngine;

namespace ViShape;

public enum ViShapeRpcId : byte { Assign = 1, SyncSettings = 2, Shapeshift = 3, Gas = 4, AcidKill = 5 }

[RegisterRpc]
public class ViShapeRpc : CustomRpc
{
    public override uint RpcId => 21051;

    public static void SendAssign(List<byte> targets)
    {
        var writer = CreateWriter(); writer.Write((byte)ViShapeRpcId.Assign);
        writer.Write((byte)targets.Count); foreach (var id in targets) writer.Write(id); Send(writer);
    }

    public static void SendSyncSettings()
    {
        var writer = CreateWriter(); writer.Write((byte)ViShapeRpcId.SyncSettings);
        writer.Write(ViShapeSettings.Serialize()); Send(writer);
    }

    public static void SendShapeshift(byte sourceId, byte targetId, bool isUndo)
    {
        var writer = CreateWriter(); writer.Write((byte)ViShapeRpcId.Shapeshift);
        writer.Write(sourceId); writer.Write(targetId); writer.Write(isUndo); Send(writer);
    }

    public static void SendGas(byte sourceId, Vector2 position)
    {
        var writer = CreateWriter(); writer.Write((byte)ViShapeRpcId.Gas);
        writer.Write(sourceId); writer.Write(position.x); writer.Write(position.y); Send(writer);
    }

    public static void SendAcidKill(byte killerId, byte targetId)
    {
        var writer = CreateWriter(); writer.Write((byte)ViShapeRpcId.AcidKill);
        writer.Write(killerId); writer.Write(targetId); Send(writer);
    }

    public override void Handle(MessageReader reader, byte sendMode)
    {
        var id = (ViShapeRpcId)reader.ReadByte();
        switch (id)
        {
            case ViShapeRpcId.Assign:
                ViShapeRole.Clear(); byte count = reader.ReadByte();
                for (int i = 0; i < count; i++) ViShapeRole.Add(reader.ReadByte());
                ViShapeRole.ResetTimersForRound(); break;
            case ViShapeRpcId.SyncSettings:
                ViShapeSettings.Deserialize(reader.ReadString()); break;
            case ViShapeRpcId.Shapeshift:
                byte src = reader.ReadByte(); byte tgt = reader.ReadByte(); bool undo = reader.ReadBoolean();
                ViShapeAbilities.PerformShapeshiftLocal(src, tgt, undo); break;
            case ViShapeRpcId.Gas:
                byte gSrc = reader.ReadByte(); Vector2 gPos = new Vector2(reader.ReadSingle(), reader.ReadSingle());
                ViShapeAbilities.ReleaseViperGasLocal(gSrc, gPos); break;
            case ViShapeRpcId.AcidKill:
                byte kId = reader.ReadByte(); byte tId = reader.ReadByte();
                ViShapeAbilities.AcidKillLocal(kId, tId); break;
        }
    }
}
