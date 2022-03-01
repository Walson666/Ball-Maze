using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTextUpdater : MonoBehaviour
{

    [SerializeField]TextMeshProUGUI _textMeshProUGUI;

    private void Start()
    {
        _textMeshProUGUI.text = "Level " + (GameManager.Instance.CurentLevel + 1).ToString();    
    }
}
