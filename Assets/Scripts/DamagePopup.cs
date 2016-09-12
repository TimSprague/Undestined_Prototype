using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamagePopup : MonoBehaviour
{
    public Animator animator;

    Text damageText;

    void OnEnable()
    {
        int popupNum = Random.Range(1, 3);
        animator.SetInteger("Popup", popupNum);
        Destroy(gameObject, 1.0f);
        damageText = animator.GetComponent<Text>();
    }

    public void SetText(string value)
    {
        damageText.text = value;
    }
}
