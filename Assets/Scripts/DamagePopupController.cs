using UnityEngine;
using System.Collections;

public class DamagePopupController : MonoBehaviour
{
    static DamagePopup dmgPopupText;
    static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if(!dmgPopupText)
            dmgPopupText = Resources.Load<DamagePopup>("Prefabs/Damage Popup Parent");
    }

    public static void CreateDamagePopup(string text, Transform transform)
    {
        DamagePopup instance = Instantiate(dmgPopupText);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPos;
        instance.SetText(text);
    }
	
}
