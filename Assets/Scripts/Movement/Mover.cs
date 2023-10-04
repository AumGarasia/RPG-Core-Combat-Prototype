using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
{
        [SerializeField] Transform target;
        NavMeshAgent navMeshAgent;
        TakeDamage health;

        private void Start() {
            health = GetComponent<TakeDamage>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            Moveto(destination);
        }
        
        public void Moveto(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }


        private void UpdateAnimator()   // Convert the global velocity 
        {
            Vector3 vel = navMeshAgent.velocity;
            Vector3 local_vel = transform.InverseTransformDirection(vel);
            //  Why are we converting the global velocity to local velocity?
            /*
                When we are creating our velocity vector,we are collecting 
                global coordinates..implying global velocity.
                Whereas, our animator just wants to know if we are running forward or not.
                This way, we can tell our animator that we are running in forward direction. 
            */
            float speed = local_vel.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }
}

}