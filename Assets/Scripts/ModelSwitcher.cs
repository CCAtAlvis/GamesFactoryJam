using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSwitcher : MonoBehaviour
{
    public GameObject modelA;
    public GameObject modelB;

    private int modelNumber;

    void Start()
    {
        modelNumber = 1;
        modelB.SetActive(false);
    }

    void ModelSwitch()
    {
        if (modelNumber == 1)
        {
            modelA.SetActive(false);
            modelB.transform.position = modelA.transform.position;
            modelB.transform.rotation = modelA.transform.rotation;
            modelB.SetActive(true);
            modelNumber = 2;
        }
        else if (modelNumber == 2)
        {
            modelB.SetActive(false);
            modelA.transform.position = modelB.transform.position;
            modelA.transform.rotation = modelB.transform.rotation;
            modelA.SetActive(true);
            modelNumber = 1;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ModelSwitch();
        }

    }
}
