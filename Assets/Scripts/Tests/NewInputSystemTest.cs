using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NewInputSystemTest : MonoBehaviour
{
    [SerializeField] private Text _outputText;

    private int _pressesCounter = 0;
    private TestControls _testControls;

    private void Awake()
    {
        _testControls = new TestControls();
        _testControls.AndroidTest.ScreenPress.performed += OnAndroidScreenPressPerformed;
        _testControls.PCTest.ScreenPress.performed += OnPCScreenPressPerformed;
        _testControls.Enable();
    }

    private void OnPCScreenPressPerformed(InputAction.CallbackContext obj)
    {
        _pressesCounter++;
        _outputText.text = $"Number of presses (PC): {_pressesCounter}";
    }

    private void OnAndroidScreenPressPerformed(InputAction.CallbackContext obj)
    {
        _pressesCounter++;
        _outputText.text = $"Number of presses (Android): {_pressesCounter}";
    }
}
