﻿using System;
using System.Linq;
using UnityEngine;

namespace Enemy.Ai.States
{
    public class PlayAnimationState : MoverState
    {
        public bool Finished { get; private set; }

        private float _remainingTime;
        private readonly int _animationTrigger;
        private bool _hasRemainingTime;

        public PlayAnimationState(Animator animator, Transform player, Mover mover, string animationTrigger,
            float animationTime) : base(mover, animator, player)
        {
            _animationTrigger = Animator.StringToHash(animationTrigger);
            _remainingTime = animationTime;
        }

        public override void Tick()
        {
            _remainingTime -= Time.deltaTime;
            if (_remainingTime <= 0) Finished = true;
        }

        public override void FixedTick()
        {
        }

        public override void OnEnter()
        {
            LookAtTarget();
            Animator.SetTrigger(_animationTrigger);
        }

        public override void OnExit()
        {
            _remainingTime = 0;
            Finished = false;
            _hasRemainingTime = false;
        }
    }
}