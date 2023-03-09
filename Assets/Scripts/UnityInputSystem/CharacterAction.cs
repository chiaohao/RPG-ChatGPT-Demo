// GENERATED AUTOMATICALLY FROM 'Assets/CharacterAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CharacterAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CharacterAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CharacterAction"",
    ""maps"": [
        {
            ""name"": ""Protagonist"",
            ""id"": ""d6e74964-4093-491b-9141-b20400e88b2f"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""c522406d-283c-4117-9eaa-dc3b9221bf84"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""ae380576-e9b2-409e-a66b-b03ae826c063"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""4570047f-1589-4d20-ba1d-f1eba26e6861"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bb8e76a9-e7eb-4889-894c-d234abe191a7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9c8c60b1-5faa-49cb-b071-7bf875a77f49"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""77df1fe1-0746-4fda-90a2-0536aac60591"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""419e0b14-2139-4e08-a9d2-23c9f27df163"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2284bf57-834c-4cd8-8e53-65be41d9807b"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Protagonist
        m_Protagonist = asset.FindActionMap("Protagonist", throwIfNotFound: true);
        m_Protagonist_Movement = m_Protagonist.FindAction("Movement", throwIfNotFound: true);
        m_Protagonist_Rotation = m_Protagonist.FindAction("Rotation", throwIfNotFound: true);
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

    // Protagonist
    private readonly InputActionMap m_Protagonist;
    private IProtagonistActions m_ProtagonistActionsCallbackInterface;
    private readonly InputAction m_Protagonist_Movement;
    private readonly InputAction m_Protagonist_Rotation;
    public struct ProtagonistActions
    {
        private @CharacterAction m_Wrapper;
        public ProtagonistActions(@CharacterAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Protagonist_Movement;
        public InputAction @Rotation => m_Wrapper.m_Protagonist_Rotation;
        public InputActionMap Get() { return m_Wrapper.m_Protagonist; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ProtagonistActions set) { return set.Get(); }
        public void SetCallbacks(IProtagonistActions instance)
        {
            if (m_Wrapper.m_ProtagonistActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_ProtagonistActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ProtagonistActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ProtagonistActionsCallbackInterface.OnMovement;
                @Rotation.started -= m_Wrapper.m_ProtagonistActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_ProtagonistActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_ProtagonistActionsCallbackInterface.OnRotation;
            }
            m_Wrapper.m_ProtagonistActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
            }
        }
    }
    public ProtagonistActions @Protagonist => new ProtagonistActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IProtagonistActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
    }
}
