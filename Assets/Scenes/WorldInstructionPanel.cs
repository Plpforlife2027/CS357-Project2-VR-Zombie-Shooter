using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Builds the small instruction panel at the top of the player's view.
/// </summary>
public class WorldInstructionPanel : MonoBehaviour
{
    [SerializeField] private Font instructionFont;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (instructionFont == null)
        {
            instructionFont = UnityEditor.AssetDatabase.LoadAssetAtPath<Font>(
                "Assets/BoldPixels/Assets/font/BoldPixels.ttf");
        }
    }
#endif

    private void Awake()
    {
        GameObject canvasObject = new GameObject(
            "Instruction Canvas",
            typeof(Canvas),
            typeof(CanvasScaler));

        Canvas canvas = canvasObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 10;

        CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);

        GameObject textObject = new GameObject(
            "Instructions",
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(Text));
        textObject.transform.SetParent(canvasObject.transform, false);

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0.5f, 1f);
        textRect.anchorMax = new Vector2(0.5f, 1f);
        textRect.pivot = new Vector2(0.5f, 1f);
        textRect.anchoredPosition = new Vector2(0f, -40f);
        textRect.sizeDelta = new Vector2(900f, 160f);

        Text text = textObject.GetComponent<Text>();
        text.font = instructionFont;
        text.text = "VR ZOMBIE SHOOTER\nGrab the rifle and shoot the zombies.";
        text.fontSize = 48;
        text.alignment = TextAnchor.UpperCenter;
        text.color = new Color(1f, 0.9f, 0.25f, 1f);
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.verticalOverflow = VerticalWrapMode.Overflow;
        text.raycastTarget = false;
    }
}
