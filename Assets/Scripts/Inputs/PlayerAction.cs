// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs/PlayerAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerAction"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""2ac447f0-7ab7-459f-8b6b-296dac719319"",
            ""actions"": [
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""Button"",
                    ""id"": ""12bee76c-3f42-42ea-b23e-154992461fd9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Brake"",
                    ""type"": ""Button"",
                    ""id"": ""d6ec2063-5db8-459b-a1fd-108302dbbceb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SubsystemDurability"",
                    ""type"": ""Button"",
                    ""id"": ""91859018-26c2-4915-a8fd-7874f1576332"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleRepair"",
                    ""type"": ""Button"",
                    ""id"": ""d6756d7a-b8c4-480c-a0fd-b667c8cc9428"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""0efaabd5-6a1e-4007-8ea2-b748c3418d96"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PickUpItem"",
                    ""type"": ""Button"",
                    ""id"": ""86a963b0-43bd-425f-a28f-4aabb680bdce"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""713d8b30-9665-4774-b9a8-664c0da1d01e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""885e32b9-6f2c-497e-b083-bfb3d0f50c67"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b294a5fe-f3b0-4be3-9d18-aa9bf9facc75"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7caaf340-513d-4bb2-b396-d3cfc7ed6647"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa2c855a-7f87-459d-b919-c32a4a5c3193"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SubsystemDurability"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b7c2efc-c804-4096-a5e6-820c56c43ae4"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleRepair"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ca1ca74-449e-48f4-9752-fb541cd83cfd"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad144f08-2d4a-4152-a753-c1756b2501e3"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUpItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Accelerate = m_Player.FindAction("Accelerate", throwIfNotFound: true);
        m_Player_Brake = m_Player.FindAction("Brake", throwIfNotFound: true);
        m_Player_SubsystemDurability = m_Player.FindAction("SubsystemDurability", throwIfNotFound: true);
        m_Player_ToggleRepair = m_Player.FindAction("ToggleRepair", throwIfNotFound: true);
        m_Player_Inventory = m_Player.FindAction("Inventory", throwIfNotFound: true);
        m_Player_PickUpItem = m_Player.FindAction("PickUpItem", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Accelerate;
    private readonly InputAction m_Player_Brake;
    private readonly InputAction m_Player_SubsystemDurability;
    private readonly InputAction m_Player_ToggleRepair;
    private readonly InputAction m_Player_Inventory;
    private readonly InputAction m_Player_PickUpItem;
    public struct PlayerActions
    {
        private @PlayerAction m_Wrapper;
        public PlayerActions(@PlayerAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Accelerate => m_Wrapper.m_Player_Accelerate;
        public InputAction @Brake => m_Wrapper.m_Player_Brake;
        public InputAction @SubsystemDurability => m_Wrapper.m_Player_SubsystemDurability;
        public InputAction @ToggleRepair => m_Wrapper.m_Player_ToggleRepair;
        public InputAction @Inventory => m_Wrapper.m_Player_Inventory;
        public InputAction @PickUpItem => m_Wrapper.m_Player_PickUpItem;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Accelerate.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAccelerate;
                @Accelerate.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAccelerate;
                @Accelerate.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAccelerate;
                @Brake.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBrake;
                @Brake.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBrake;
                @Brake.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBrake;
                @SubsystemDurability.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSubsystemDurability;
                @SubsystemDurability.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSubsystemDurability;
                @SubsystemDurability.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSubsystemDurability;
                @ToggleRepair.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleRepair;
                @ToggleRepair.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleRepair;
                @ToggleRepair.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleRepair;
                @Inventory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                @PickUpItem.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickUpItem;
                @PickUpItem.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickUpItem;
                @PickUpItem.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickUpItem;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Accelerate.started += instance.OnAccelerate;
                @Accelerate.performed += instance.OnAccelerate;
                @Accelerate.canceled += instance.OnAccelerate;
                @Brake.started += instance.OnBrake;
                @Brake.performed += instance.OnBrake;
                @Brake.canceled += instance.OnBrake;
                @SubsystemDurability.started += instance.OnSubsystemDurability;
                @SubsystemDurability.performed += instance.OnSubsystemDurability;
                @SubsystemDurability.canceled += instance.OnSubsystemDurability;
                @ToggleRepair.started += instance.OnToggleRepair;
                @ToggleRepair.performed += instance.OnToggleRepair;
                @ToggleRepair.canceled += instance.OnToggleRepair;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @PickUpItem.started += instance.OnPickUpItem;
                @PickUpItem.performed += instance.OnPickUpItem;
                @PickUpItem.canceled += instance.OnPickUpItem;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnAccelerate(InputAction.CallbackContext context);
        void OnBrake(InputAction.CallbackContext context);
        void OnSubsystemDurability(InputAction.CallbackContext context);
        void OnToggleRepair(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnPickUpItem(InputAction.CallbackContext context);
    }
}
