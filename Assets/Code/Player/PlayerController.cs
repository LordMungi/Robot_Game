using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private PlayerParents playerParents;
    
    [SerializeField] private Transform mainCamera;
    
    private BehaviourFSM _behaviourFSM;
    private MouseTargetPosition _mouseTargetPosition;

    private void Awake()
    {
        ServiceProvider.Instance.AddService<EventBus>(new EventBus());
        PlayerData data;
        data.player = this;
        data.controller = GetComponent<CharacterController>();
        data.config = playerConfig;
        data.cameraTransform = mainCamera;
        
        _behaviourFSM = new BehaviourFSM(ref data, ref playerParents);
        _mouseTargetPosition = new MouseTargetPosition(ref playerParents);
    }

    void Update()
    {
        _behaviourFSM.CurrentState.Update();
        _mouseTargetPosition.Update();
    }
}
