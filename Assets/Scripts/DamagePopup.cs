using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamagePopup : MonoBehaviour
{
    public Animator animator;

    Text damageText;

    void OnEnable()
    {
        Destroy(gameObject, 2.0f);
        damageText = animator.GetComponent<Text>();
    }
    
    public void SetText(string value)
    {
        damageText.text = value;
    }
}
