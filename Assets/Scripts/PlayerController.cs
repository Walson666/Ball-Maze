using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("Color settings")]
    [SerializeField] private Color[] _colors;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Color _curentColor;
    [Header("Move Settings")]
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;
    [SerializeField] public bool _isMooving;

    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private Vector3 _direction;

    [Header("Bounce Settings")]
    [Range(0, 1)]
    [SerializeField] private float _punchSize = 0.3f;
    [SerializeField] private float _bounceDuration;
    [SerializeField] private int _vibrato;

    private void Start()
    {
        _curentColor = _colors[Random.Range(0, _colors.Length)];
        _meshRenderer.material.color = _curentColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMooving) return;
        if (Input.GetKeyDown(KeyCode.W)) Move(Vector3.forward);
        if (Input.GetKeyDown(KeyCode.A)) Move(Vector3.left);
        if (Input.GetKeyDown(KeyCode.S)) Move(Vector3.back);
        if (Input.GetKeyDown(KeyCode.D)) Move(Vector3.right);

    }

    public void Move(Vector3 direction)
    {
        _direction = direction;
        if (!GetEndPoint(direction, out _endPosition))
        {
            Debug.LogWarning("EndPoint not found", this);
            return;
        }
        DOTween.KillAll(true);
        transform.DOMove(_endPosition, _duration).SetEase(_ease).OnStart(OnStartMovement).OnComplete(OnEndMovement);
        
    }

    public void OnStartMovement()
    {
        
        Vector3 scale = new Vector3(Mathf.Abs(_direction.x), Mathf.Abs(_direction.y), Mathf.Abs(_direction.z));
        transform.DOPunchScale(scale * _punchSize, _duration, 1);
        _isMooving = true;
    }

    public void OnEndMovement()
    {
        _isMooving = false;
        transform.DOPunchScale(_direction * _punchSize, _bounceDuration, _vibrato);
    }

    public bool GetEndPoint(Vector3 direction, out Vector3 endPoint)
    {
        endPoint = Vector3.zero;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit))
        {
            if(hit.collider != null)
            {
                if (Vector3.Distance(transform.position, hit.transform.position) < transform.localScale.x + 1) return false;
                Vector3 hitPos = hit.transform.position - direction;
                endPoint = new Vector3(hitPos.x, transform.position.y, hitPos.z);
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        other.GetComponent<Floor>().Colorize(_curentColor);
    }

    
}
