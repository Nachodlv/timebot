﻿using System;
using System.Collections.Generic;

public class StateMachine
{
   private IState _currentState;
   
   private Dictionary<int, List<Transition>> _transitions = new Dictionary<int,List<Transition>>();
   private List<Transition> _currentTransitions = new List<Transition>();
   private List<Transition> _anyTransitions = new List<Transition>();
   
   private static List<Transition> EmptyTransitions = new List<Transition>(0);

   public void Tick()
   {
      var transition = GetTransition();
      if (transition != null)
         SetState(transition.To);
      
      _currentState?.Tick();
   }

   public void FixedTick()
   {
      _currentState?.FixedTick();
   }

   public void SetState(IState state)
   {
      if (state == _currentState)
         return;
      
      _currentState?.OnExit();
      _currentState = state;
      
      _transitions.TryGetValue(_currentState.GetHashCode(), out _currentTransitions);
      if (_currentTransitions == null)
         _currentTransitions = EmptyTransitions;
      
      _currentState.OnEnter();
   }

   public void AddTransition(IState from, IState to, Func<bool> predicate)
   {
      if (_transitions.TryGetValue(from.GetHashCode(), out var transitions) == false)
      {
         transitions = new List<Transition>();
         _transitions[from.GetHashCode()] = transitions;
      }
      
      transitions.Add(new Transition(to, predicate));
   }

   public void AddAnyTransition(IState state, Func<bool> predicate)
   {
      _anyTransitions.Add(new Transition(state, predicate));
   }

   private class Transition
   {
      public Func<bool> Condition {get; }
      public IState To { get; }

      public Transition(IState to, Func<bool> condition)
      {
         To = to;
         Condition = condition;
      }
   }

   private Transition GetTransition()
   {
      foreach(var transition in _anyTransitions)
         if (transition.Condition())
            return transition;
      
      foreach (var transition in _currentTransitions)
         if (transition.Condition())
            return transition;

      return null;
   }
}