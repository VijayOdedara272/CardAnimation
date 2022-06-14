using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class editorinharite : MonoBehaviour
{
    [SerializeField] GameObject cardparent;
    [SerializeField] float gapbetweencards;
    public Manager manager;

    [Header("Position Entry in Manager Array")]
    public GameObject parentofchilds;

    [System.Obsolete]
    public void SetPos()
    {
        float increment = gapbetweencards;
        int childcount = cardparent.transform.childCount;

        for (int i = childcount - 1; i >= 0; i--)
        {
            GameObject obj = cardparent.transform.GetChild(i).gameObject;
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + increment, obj.transform.position.z);

            increment += gapbetweencards;
        }
    }

    [System.Obsolete]
    public void EnterPosintoArray()
    {
        int childcount = parentofchilds.transform.childCount;

        for (int i = 0; i < childcount; i++)
        {
            manager.decksectioncardsposition[i] = parentofchilds.transform.GetChild(i).gameObject.transform;
        }
    }

    public void SetHorizontalPos()
    {
        float increment = gapbetweencards;
        int childcount = cardparent.transform.childCount;

        for (int i = 0; i < childcount; i++)
        {
            GameObject obj = cardparent.transform.GetChild(i).gameObject;
            obj.transform.position = new Vector3(obj.transform.position.x + increment, obj.transform.position.y, obj.transform.position.z);

            increment += gapbetweencards;
        }
    }
}
