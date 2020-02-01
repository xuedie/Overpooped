// GENERATED AUTOMATICALLY FROM 'Assets/InputAction/InputGamePlay.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputGamePlay : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputGamePlay()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputGamePlay"",
    ""maps"": [
        {
            ""name"": ""ControllerInput"",
            ""id"": ""b9a03f22-51a0-46b9-85eb-2777d8ccf244"",
            ""actions"": [
                {
                    ""name"": ""Bear"",
                    ""type"": ""Button"",
                    ""id"": ""a16765ef-9a81-4ad3-8a74-8214f5d99058"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""df1a0956-cdfa-4876-a71c-9b5ce99cadf8"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Bear"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8332e743-d269-45a9-b78f-d2b45b9edf6c"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Bear"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // ControllerInput
        m_ControllerInput = asset.FindActionMap("ControllerInput", throwIfNotFound: true);
        m_ControllerInput_Bear = m_ControllerInput.FindAction("Bear", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // ControllerInput
    private readonly InputActionMap m_ControllerInput;
    private IControllerInputActions m_ControllerInputActionsCallbackInterface;
    private readonly InputAction m_ControllerInput_Bear;
    public struct ControllerInputActions
    {
        private @InputGamePlay m_Wrapper;
        public ControllerInputActions(@InputGamePlay wrapper) { m_Wrapper = wrapper; }
        public InputAction @Bear => m_Wrapper.m_ControllerInput_Bear;
        public InputActionMap Get() { return m_Wrapper.m_ControllerInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControllerInputActions set) { return set.Get(); }
        public void SetCallbacks(IControllerInputActions instance)
        {
            if (m_Wrapper.m_ControllerInputActionsCallbackInterface != null)
            {
                @Bear.started -= m_Wrapper.m_ControllerInputActionsCallbackInterface.OnBear;
                @Bear.performed -= m_Wrapper.m_ControllerInputActionsCallbackInterface.OnBear;
                @Bear.canceled -= m_Wrapper.m_ControllerInputActionsCallbackInterface.OnBear;
            }
            m_Wrapper.m_ControllerInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Bear.started += instance.OnBear;
                @Bear.performed += instance.OnBear;
                @Bear.canceled += instance.OnBear;
            }
        }
    }
    public ControllerInputActions @ControllerInput => new ControllerInputActions(this);
    public interface IControllerInputActions
    {
        void OnBear(InputAction.CallbackContext context);
    }
}
