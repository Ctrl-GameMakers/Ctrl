using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitModelingObject : MonoBehaviour
{
    Transform tr;
    GameObject go;

    void Awake()
    {        
        go = gameObject;
        tr = go.GetComponent<Transform>();
    }

    public void SetUnitObject(Transform parent)
    {
        if (!go.activeSelf)
            go.SetActive(true);

        tr.parent = parent;
        tr.localPosition = Vector3.zero;
        tr.localRotation = Quaternion.identity;
    }

    public void ReturnUnitObject()
    {
        if (UnitModelingObjectPoolMgr.Instance.activeSelf)
        {
            UnitModelingObjectPoolMgr.Instance.EnqueueObject(this);
        }

        go.SetActive(false);
    }

    public GameObject GetGameObject() { return go; }
    public Transform GetTransform()  { return tr; }
}
