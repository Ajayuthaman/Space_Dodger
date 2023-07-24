using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float _scrollSpeed = 0.1f;
    private MeshRenderer _meshRenderer;

    private float _xScroll;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        Scroll();
    }

    void Scroll()
    {
        _xScroll = Time.time * _scrollSpeed;

        Vector2 offset = new Vector2(_xScroll, 0);

        _meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
