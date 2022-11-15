using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScroll : MonoBehaviour
{
    public GameObject Lava;
    private Renderer Scroll;

    float scrollSpeed = 0.03f;
    // Start is called before the first frame update
    void Start()
    {
        Scroll = Lava.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 textureOffset = new Vector2(Time.time * scrollSpeed, 0);
        Scroll.material.mainTextureOffset = textureOffset;
    }
}
