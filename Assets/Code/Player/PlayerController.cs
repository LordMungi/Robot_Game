using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private PlayerParents playerParents;

    private BehaviourFSM _behaviourFSM;
    private MouseTargetPosition _mouseTargetPosition;

    private void Awake()
    {
        ServiceProvider.Instance.AddService<EventBus>(new EventBus());

        PlayerData data;
        data.player = this;
        data.controller = GetComponent<CharacterController>();
        data.config = playerConfig;

        _behaviourFSM = new BehaviourFSM(ref data, ref playerParents);
        _mouseTargetPosition = new MouseTargetPosition(ref playerParents);
    }

    void Update()
    {
        _behaviourFSM.CurrentState.Update();
        _mouseTargetPosition.Update();
    }
}
