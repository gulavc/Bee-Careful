using UnityEngine;

public enum GlowType
{
    Resource,
    Danger,
    LoadZone

}

public static class GlowTypeExtension
{
    public static Color GetColor(this GlowType self)
    {
        switch (self)
        {
            case GlowType.Resource:
                return Color.green;
            case GlowType.Danger:
                return Color.red;
            case GlowType.LoadZone:
                return Color.blue;
            default:
                return Color.white;
        }
    }
}
