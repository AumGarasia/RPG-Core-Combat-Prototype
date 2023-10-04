using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction 
    {
        [SerializeField] float WeaponRange = 1.5f;
        [SerializeField] float time_bw_attack = 1f;
        [SerializeField] float WeaponDamage = 21f;
        TakeDamage target;
        float time_since_last_attack = Mathf.Infinity;       
        // Instead of zero, infinity is better
        // because it would mean we never attacked before
        // Rather, it would be better to say that "we attacked long ago"
        // this will alleviate a bug where there's a delay before attacking

        private void Update()
        {
            time_since_last_attack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead())    return;
            if (!InRange())
            {
                GetComponent<Mover>().Moveto(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                transform.LookAt(target.transform);
                AttackBehavior();
            }

        }

        public void Attack(GameObject  C_target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = C_target.GetComponent<TakeDamage>();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null)    {return false;}
            TakeDamage targetToTest = combatTarget.GetComponent<TakeDamage>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        private void AttackBehavior()
        {
            if(time_since_last_attack > time_bw_attack)
            {
                TriggerAttack();
                time_since_last_attack = 0;
                // At the Hit of the animation, the damage will take place.
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        //This Hit is called from the animator
        void Hit()
        {
            if(target == null)  return;
            target.Damage(WeaponDamage);
        }
        private bool InRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < WeaponRange;
        }

        public void Cancel()        // Method used for cancelling the attack
        {
            StopAttack();
            target = null;
            // If not done, the attack interaction will go on forever.
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
        }
    }
}