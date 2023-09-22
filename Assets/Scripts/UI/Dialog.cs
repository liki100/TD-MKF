using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Dialog : MonoBehaviour
{
    [SerializeField] private Button _backButton;

    private void Awake()
    {
        if (_backButton != null)
        {
            _backButton.onClick.AddListener(Hide);
        }
    }

    private void OnDestroy()
    {
        if (_backButton != null)
        {
            _backButton.onClick.RemoveListener(Hide);
        }
    }

    private void Hide()
    {
        Destroy(gameObject);
    }
}
