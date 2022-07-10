using System;
using UnityEngine;

public class PlayerAnimationListener : MonoBehaviour
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }

    public void EndAnimation()
    {
        _playerController.IsAttack = false;
    }
}
