using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayer : MonoBehaviour
{
    public InputActionAsset inputActionAsset;

    private InputActionMap _inputActionMap;

    private float _actionLeft;
    private float _actionRight;
    private float _actionFire;

    public float horizontalValue => -_actionLeft + _actionRight;
    public bool isFire => _actionFire > 0.9f;

    private void Awake()
    {
        _inputActionMap = inputActionAsset.FindActionMap("Player");
    }

    private void OnEnable()
    {
        inputActionAsset.Enable();
    }

    public void Update()
    {
        _actionLeft = _inputActionMap.FindAction("Left").ReadValue<float>();
        _actionRight = _inputActionMap.FindAction("Right").ReadValue<float>();
        _actionFire = _inputActionMap.FindAction("Fire").ReadValue<float>();
    }

    private void OnDisable()
    {
        inputActionAsset.Disable();
    }
}