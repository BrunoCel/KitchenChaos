using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyToRebindUI : MonoBehaviour
{
    private void Awake()
    {
        Hide();
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide() {  gameObject.SetActive(false); }
}
