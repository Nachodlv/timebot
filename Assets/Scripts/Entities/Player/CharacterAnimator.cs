using System;
using DefaultNamespace;
using Player.Attack;
using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(Animator))]
	public class CharacterAnimator : MonoBehaviour
	{
		[SerializeField] private CharacterController characterController;
		[SerializeField] private WallJumper wallJumper;
		[SerializeField] private PlayerAttacker playerAttacker;

		public event Action OnAttackAnimation;
		
		private static readonly int JumpTrigger = Animator.StringToHash("jump");
		private static readonly int GroundTrigger = Animator.StringToHash("ground");
		private static readonly int WallBool = Animator.StringToHash("grabWall");
		private static readonly int Speed = Animator.StringToHash("speed");
		private static readonly int AttackTrigger = Animator.StringToHash("attack");

		private Animator _animator;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
			characterController.OnJumpEvent += Jump;
			characterController.OnLandEvent += Land;
			wallJumper.OnTouchingWall += GrabWall;
			playerAttacker.OnAttack += Attack;
		}

		private void Update()
		{
			_animator.SetFloat(Speed, characterController.Grounded ? Mathf.Abs(characterController.Velocity.x) : 0);
		}

		private void Jump()
		{
			_animator.SetTrigger(JumpTrigger);
		}

		private void Land()
		{
			_animator.SetTrigger(GroundTrigger);
		}

		private void GrabWall(bool isGrabbed, bool isRight)
		{
			_animator.SetBool(WallBool, isGrabbed);
		}

		private void Attack()
		{
			_animator.SetTrigger(AttackTrigger);
		}

		private void MakeAttack()
		{
			OnAttackAnimation?.Invoke();
		}
	}
}