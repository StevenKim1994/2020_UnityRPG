using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private float width;

    void Reposition()
    {
        Vector2 offset = new Vector2(width* 2f,0);
        transform.position = (Vector2) transform.position + offset;
    }

    private void Awake()
    {
        BoxCollider2D backgrBoxCollider2D = GetComponent<BoxCollider2D>();
        width = backgrBoxCollider2D.size.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -width)
        {
            Reposition();
        }
    }
}
