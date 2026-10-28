using System;
using System.Globalization;

namespace ViShape;

public static class ViShapeSettings
{
    public static bool Enabled = true;
    public static int MaxCount = 1;
    public static float ShapeshiftCooldown = 20f;
    public static float ShapeshiftDuration = 20f;
    public static float GasCooldown = 30f;
    public static float GasDuration = 10f;
    public static float GasRadius = 3f;
    public static float GasVision = 0f;
    public static float AcidCooldown = 30f;
    public static float AcidRange = 1.4f;
    public static float AcidPoolDuration = 25f;
    public static float AcidSlow = 0.6f;
    public static bool VentGas = true;

    private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

    public static string Serialize()
    {
        return string.Join(";", new[] {
            Enabled ? "1" : "0", MaxCount.ToString(Inv), ShapeshiftCooldown.ToString(Inv),
            ShapeshiftDuration.ToString(Inv), GasCooldown.ToString(Inv), GasDuration.ToString(Inv),
            GasRadius.ToString(Inv), GasVision.ToString(Inv), AcidCooldown.ToString(Inv),
            AcidRange.ToString(Inv), AcidPoolDuration.ToString(Inv), AcidSlow.ToString(Inv), VentGas ? "1" : "0"
        });
    }

    public static void Deserialize(string data)
    {
        var p = data.Split(';');
        if (p.Length < 13) return;
        Enabled = p[0] == "1"; MaxCount = int.Parse(p[1], Inv);
        ShapeshiftCooldown = float.Parse(p[2], Inv); ShapeshiftDuration = float.Parse(p[3], Inv);
        GasCooldown = float.Parse(p[4], Inv); GasDuration = float.Parse(p[5], Inv);
        GasRadius = float.Parse(p[6], Inv); GasVision = float.Parse(p[7], Inv);
        AcidCooldown = float.Parse(p[8], Inv); AcidRange = float.Parse(p[9], Inv);
        AcidPoolDuration = float.Parse(p[10], Inv); AcidSlow = float.Parse(p[11], Inv);
        VentGas = p[12] == "1";
    }
}
