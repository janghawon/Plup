//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/99.Setting/Input/PlayerInputControl.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputControl: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputControl"",
    ""maps"": [
        {
            ""name"": ""PlayerInputMap"",
            ""id"": ""532d9d23-f4f7-4b4b-ad61-459553c8e525"",
            ""actions"": [
                {
                    ""name"": ""PlayerMoveAction"",
                    ""type"": ""Button"",
                    ""id"": ""f6734318-b178-43fd-909f-1ee20151bf04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b842e22b-1f42-4db2-afb3-3ceefaec3843"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInput"",
                    ""action"": ""PlayerMoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59183297-83fd-4e32-badc-7b66a51b81ae"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInput"",
                    ""action"": ""PlayerMoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb20e972-09b2-441e-8986-c14fcb05c988"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInput"",
                    ""action"": ""PlayerMoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c429356d-8088-4cb0-a674-f1c8ef406869"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInput"",
                    ""action"": ""PlayerMoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PlayerInput"",
            ""bindingGroup"": ""PlayerInput"",
            ""devices"": []
        }
    ]
}");
        // PlayerInputMap
        m_PlayerInputMap = asset.FindActionMap("PlayerInputMap", throwIfNotFound: true);
        m_PlayerInputMap_PlayerMoveAction = m_PlayerInputMap.FindAction("PlayerMoveAction", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerInputMap
    private readonly InputActionMap m_PlayerInputMap;
    private List<IPlayerInputMapActions> m_PlayerInputMapActionsCallbackInterfaces = new List<IPlayerInputMapActions>();
    private readonly InputAction m_PlayerInputMap_PlayerMoveAction;
    public struct PlayerInputMapActions
    {
        private @PlayerInputControl m_Wrapper;
        public PlayerInputMapActions(@PlayerInputControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayerMoveAction => m_Wrapper.m_PlayerInputMap_PlayerMoveAction;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInputMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputMapActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerInputMapActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerInputMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerInputMapActionsCallbackInterfaces.Add(instance);
            @PlayerMoveAction.started += instance.OnPlayerMoveAction;
            @PlayerMoveAction.performed += instance.OnPlayerMoveAction;
            @PlayerMoveAction.canceled += instance.OnPlayerMoveAction;
        }

        private void UnregisterCallbacks(IPlayerInputMapActions instance)
        {
            @PlayerMoveAction.started -= instance.OnPlayerMoveAction;
            @PlayerMoveAction.performed -= instance.OnPlayerMoveAction;
            @PlayerMoveAction.canceled -= instance.OnPlayerMoveAction;
        }

        public void RemoveCallbacks(IPlayerInputMapActions instance)
        {
            if (m_Wrapper.m_PlayerInputMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerInputMapActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerInputMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerInputMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerInputMapActions @PlayerInputMap => new PlayerInputMapActions(this);
    private int m_PlayerInputSchemeIndex = -1;
    public InputControlScheme PlayerInputScheme
    {
        get
        {
            if (m_PlayerInputSchemeIndex == -1) m_PlayerInputSchemeIndex = asset.FindControlSchemeIndex("PlayerInput");
            return asset.controlSchemes[m_PlayerInputSchemeIndex];
        }
    }
    public interface IPlayerInputMapActions
    {
        void OnPlayerMoveAction(InputAction.CallbackContext context);
    }
}
