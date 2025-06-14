using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReseStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ClearStaticData();
        BaseCounter.ClearStaticData();
        TrashCounter.ClearStaticData();
    }
}
