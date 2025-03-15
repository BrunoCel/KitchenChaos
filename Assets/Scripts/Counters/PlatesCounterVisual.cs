using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlatesCounter counter;
    
    private List<GameObject> plateVisualGameObjectsList;
    private float plateOffsetY = 0.1f;

    void Awake()
    {
        plateVisualGameObjectsList = new List<GameObject>();
    }
    void Start()
    {
        counter.OnPlateTake += RemovePlateVisual;
        counter.OnPlateSpawn += AddPlateVisual;
    }

    private void RemovePlateVisual(object sender, EventArgs e)
    {
       GameObject plateGameObject = plateVisualGameObjectsList[plateVisualGameObjectsList.Count - 1];
       plateVisualGameObjectsList.Remove(plateGameObject);
       Destroy(plateGameObject);
    }

    private void AddPlateVisual(object sender, EventArgs e)
    {
        Transform plateVisualTranform = Instantiate(plateVisualPrefab, counterTopPoint.position, Quaternion.identity);
        plateVisualTranform.localPosition = new Vector3(counterTopPoint.position.x, counterTopPoint.position.y + (plateOffsetY * plateVisualGameObjectsList.Count),counterTopPoint.position.z);
        
        plateVisualGameObjectsList.Add(plateVisualTranform.gameObject);
        Debug.Log("Plate Spawned");
    }
}
