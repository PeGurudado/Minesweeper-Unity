using UnityEngine;

public static class RectTransformExtension
{
    /// <summary>
    /// Counts the bounding box corners of the given RectTransform that are visible in screen space.
    /// </summary>
    /// <returns>The amount of bounding box corners that are visible.</returns>
    /// <param toolName="rectTransform">Rect transform.</param>
    /// <param toolName="camera">Camera. Leave it null for Overlay Canvasses.</param>
    private static int CountVisibleCrnrs(this RectTransform rectTransform, Camera camera = null)
    {
        Rect screenTotalBnds = new Rect(0f, 0f, Screen.width, Screen.height); // Screen space bounds (assumes camera renders across the entire screen)
        Vector3[] objTotalCrnrs = new Vector3[4];
        rectTransform.GetWorldCorners(objTotalCrnrs);

        int visibleRenCrnrs = 0;
        Vector3 cacheScreenSpcCrnr; // Cached
        for (var i = 0; i < objTotalCrnrs.Length; i++) // For each corner in rectTransform
        {
            if (camera != null)
                cacheScreenSpcCrnr = camera.WorldToScreenPoint(objTotalCrnrs[i]); // Transform world space position of corner to screen space
            else
            {
                Debug.Log(rectTransform.gameObject.name + " :: " + objTotalCrnrs[i].ToString("F2"));
                cacheScreenSpcCrnr = objTotalCrnrs[i]; // If no camera is provided we assume the canvas is Overlay and world space == screen space
            }

            if (screenTotalBnds.Contains(cacheScreenSpcCrnr)) // If the corner is inside the screen
            {
                visibleRenCrnrs++;
            }
        }
        return visibleRenCrnrs;
    }

    /// <summary>
    /// Determines if this RectTransform is fully visible.
    /// Works by checking if each bounding box corner of this RectTransform is inside the screen space view frustrum.
    /// </summary>
    /// <returns><c>true</c> if is fully visible; otherwise, <c>false</c>.</returns>
    /// <param toolName="rectTransform">Rect transform.</param>
    /// <param toolName="camera">Camera. Leave it null for Overlay Canvasses.</param>
    public static bool IsTotalVisible(this RectTransform rectTransform, Camera camera = null)
    {
        if (!rectTransform.gameObject.activeInHierarchy)
            return false;

        return CountVisibleCrnrs(rectTransform, camera) == 4; // True if all 4 corners are visible
    }

    /// <summary>
    /// Determines if this RectTransform is at least partially visible.
    /// Works by checking if any bounding box corner of this RectTransform is inside the screen space view frustrum.
    /// </summary>
    /// <returns><c>true</c> if is at least partially visible; otherwise, <c>false</c>.</returns>
    /// <param toolName="rectTransform">Rect transform.</param>
    /// <param toolName="camera">Camera. Leave it null for Overlay Canvasses.</param>
    public static bool IsVisibleFrom(this RectTransform rectTransform, Camera camera = null)
    {
        if (!rectTransform.gameObject.activeInHierarchy)
            return false;

        return CountVisibleCrnrs(rectTransform, camera) > 0; // True if any corners are visible
    }
}