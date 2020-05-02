﻿using DefaultNamespace;
using Input;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(WallJumper))]
public class CharacterInputHandler : MonoBehaviour
{
	private CharacterController _characterController;
	private WallJumper _wallJumper;
	private float _movement;
	private bool _jump;
	private bool _crouch;
	private InputActionController _controller;

	private void Awake()
	{
		_characterController = GetComponent<CharacterController>();
		_wallJumper = GetComponent<WallJumper>();
		_controller = new InputActionController();
		_controller.Player.Jump.performed += ctx => Jump();
		_controller.Player.Crouch.performed += ctx => Crouch();
	}

	private void Update()
	{
		_movement = _controller.Player.Move.ReadValue<float>();
	}

	private void FixedUpdate()
	{
		_characterController.Move(_movement, _crouch);
		if (_jump)
		{
			if(_wallJumper.CanWallJump()) _wallJumper.Jump();
			else _characterController.Jump();
			_jump = false;
		}
		_movement = 0;
		_crouch = false;
	}

	private void Jump()
	{
		_jump = true;
	}

	private void Crouch()
	{
		_crouch = true;
	}
	
	public void OnEnable()
	{
		_controller.Enable();
	}

	public void OnDisable()
	{
		_controller.Disable();
	}

}
