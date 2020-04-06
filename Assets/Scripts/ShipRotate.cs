using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotate : MonoBehaviour
{
    [SerializeField]
    // Start is called before the first frame
    float rotationSpeed = 5.0f;
    public bool isUnicorn = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) == true)
        {
            Vector3 shipPos = this.transform.position;
            Vector3 mousePos = Input.mousePosition;
            Vector2 direction = Camera.main.ScreenToWorldPoint(mousePos) - shipPos;

            float rad = Mathf.Atan2(direction.y, direction.x);
            if(isUnicorn == false)// offset 90░
                rad -= Mathf.PI / 2;
            float angle =( rad )* Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        }
    }
}
