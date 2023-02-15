//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scenes/Audrey/Player Controls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Controls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""f9dc6dd1-496d-4aab-8f2d-dbacb634c346"",
            ""actions"": [
                {
                    ""name"": ""Accélérer"",
                    ""type"": ""Button"",
                    ""id"": ""6224445d-4d47-4bf0-9212-e95394a70acc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Arrêter"",
                    ""type"": ""Button"",
                    ""id"": ""a273f2ca-be9c-486c-90a7-7cc3423e33de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Bonus"",
                    ""type"": ""Button"",
                    ""id"": ""427763b4-53fe-4cea-9930-6525f5571063"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Avancer"",
                    ""type"": ""Button"",
                    ""id"": ""82231c51-b9c1-4bf5-930e-e39bc0df1816"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Tourner Gauche"",
                    ""type"": ""Button"",
                    ""id"": ""d061cf67-88e1-46fc-86f3-aedb3381f4e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Tourner Droite"",
                    ""type"": ""Button"",
                    ""id"": ""0c8a54df-5cc3-4dc4-910a-db870969f02c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ffed55fc-a934-4ede-a8e6-12052866664c"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accélérer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4761aa58-b004-4825-a730-2a80f89506ce"",
                    ""path"": ""<DualShockGamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Arrêter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6543f52-7e61-4586-b333-e3b2c39977e6"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Bonus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65c8c6f8-ce5a-439a-8d28-df0a8bdef2fa"",
                    ""path"": ""<DualShockGamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Avancer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3962d71e-8288-4bfd-a7a5-7a0af76ecfbe"",
                    ""path"": ""<DualShockGamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tourner Gauche"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bea81cef-3616-406c-bc50-209355e77306"",
                    ""path"": ""<DualShockGamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tourner Droite"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Accélérer = m_Gameplay.FindAction("Accélérer", throwIfNotFound: true);
        m_Gameplay_Arrêter = m_Gameplay.FindAction("Arrêter", throwIfNotFound: true);
        m_Gameplay_Bonus = m_Gameplay.FindAction("Bonus", throwIfNotFound: true);
        m_Gameplay_Avancer = m_Gameplay.FindAction("Avancer", throwIfNotFound: true);
        m_Gameplay_TournerGauche = m_Gameplay.FindAction("Tourner Gauche", throwIfNotFound: true);
        m_Gameplay_TournerDroite = m_Gameplay.FindAction("Tourner Droite", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Accélérer;
    private readonly InputAction m_Gameplay_Arrêter;
    private readonly InputAction m_Gameplay_Bonus;
    private readonly InputAction m_Gameplay_Avancer;
    private readonly InputAction m_Gameplay_TournerGauche;
    private readonly InputAction m_Gameplay_TournerDroite;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Accélérer => m_Wrapper.m_Gameplay_Accélérer;
        public InputAction @Arrêter => m_Wrapper.m_Gameplay_Arrêter;
        public InputAction @Bonus => m_Wrapper.m_Gameplay_Bonus;
        public InputAction @Avancer => m_Wrapper.m_Gameplay_Avancer;
        public InputAction @TournerGauche => m_Wrapper.m_Gameplay_TournerGauche;
        public InputAction @TournerDroite => m_Wrapper.m_Gameplay_TournerDroite;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Accélérer.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAccélérer;
                @Accélérer.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAccélérer;
                @Accélérer.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAccélérer;
                @Arrêter.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnArrêter;
                @Arrêter.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnArrêter;
                @Arrêter.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnArrêter;
                @Bonus.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBonus;
                @Bonus.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBonus;
                @Bonus.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBonus;
                @Avancer.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAvancer;
                @Avancer.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAvancer;
                @Avancer.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAvancer;
                @TournerGauche.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTournerGauche;
                @TournerGauche.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTournerGauche;
                @TournerGauche.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTournerGauche;
                @TournerDroite.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTournerDroite;
                @TournerDroite.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTournerDroite;
                @TournerDroite.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTournerDroite;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Accélérer.started += instance.OnAccélérer;
                @Accélérer.performed += instance.OnAccélérer;
                @Accélérer.canceled += instance.OnAccélérer;
                @Arrêter.started += instance.OnArrêter;
                @Arrêter.performed += instance.OnArrêter;
                @Arrêter.canceled += instance.OnArrêter;
                @Bonus.started += instance.OnBonus;
                @Bonus.performed += instance.OnBonus;
                @Bonus.canceled += instance.OnBonus;
                @Avancer.started += instance.OnAvancer;
                @Avancer.performed += instance.OnAvancer;
                @Avancer.canceled += instance.OnAvancer;
                @TournerGauche.started += instance.OnTournerGauche;
                @TournerGauche.performed += instance.OnTournerGauche;
                @TournerGauche.canceled += instance.OnTournerGauche;
                @TournerDroite.started += instance.OnTournerDroite;
                @TournerDroite.performed += instance.OnTournerDroite;
                @TournerDroite.canceled += instance.OnTournerDroite;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnAccélérer(InputAction.CallbackContext context);
        void OnArrêter(InputAction.CallbackContext context);
        void OnBonus(InputAction.CallbackContext context);
        void OnAvancer(InputAction.CallbackContext context);
        void OnTournerGauche(InputAction.CallbackContext context);
        void OnTournerDroite(InputAction.CallbackContext context);
    }
}
