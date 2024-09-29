using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    private Color _color;
    [SerializeField] private float _rotation; //corresponds to the z axis in rotation. -z is clockwise rotation

    public void setColor(Color col) {
        _color = col;
        this.GetComponent<Image>().color = _color;
    }

    public float getRotation() { return _rotation; }

    public void setRotation(float rotation)
    {
        //Get current euler angles
        _rotation = rotation;
        Vector3 rot = this.GetComponent<RectTransform>().rotation.eulerAngles;
        rot.z = rotation;

        //Set rotation
        var objectRot = this.GetComponent<RectTransform>();
        objectRot.transform.rotation = Quaternion.Euler(0,0,rot.z);
    }

}
