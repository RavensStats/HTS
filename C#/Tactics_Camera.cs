using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tactics_Camera : MonoBehaviour
{
    private Vector3 target = new Vector3(0f, 0f, 0f);
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;

    public string targetClick;

    public void Rotate_Left()
    {
        transform.RotateAround(target, Vector3.up, 90);
    }

    public void Rotate_Right()
    {
        transform.RotateAround(target, Vector3.up, -90);
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())    // is the touch on the GUI
        {
            // GUI Action
            return;
        }
        else if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetClick = hit.collider.tag;
            }

            if(targetClick != "Unit")
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

        }
        if (Input.touchCount == 2 && targetClick != "ManualUnit" && targetClick != "Unit")
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0) && targetClick != "ManualUnit" && targetClick != "Unit")
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));

    }

    public void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

}
