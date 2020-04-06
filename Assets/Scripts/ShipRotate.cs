using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotationSpeed = 5.0f;
        if (Input.GetMouseButton(0) == true)
        {
            Vector3 shipPos = this.transform.position;
            Vector3 mousePos = Input.mousePosition;
            Vector2 direction = Camera.main.ScreenToWorldPoint(mousePos) - shipPos;
            float angle =( Mathf.Atan2(direction.y, direction.x) - Mathf.PI / 2)* Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        }
    }
}
