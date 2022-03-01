using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private bool _isColored;
    [SerializeField] private MeshRenderer _meshRenderer;
    public void Colorize(Color color)
    {
        if (_isColored) return;
        _meshRenderer.material.color = color;
        GameManager.Instance.UnregisterFloorPiece(this);
        _isColored = true;
    }
}
