using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private Transform text_container;

    public Transform GetTextContainer()
    {
        return text_container;
    }
}
