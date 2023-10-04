using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
        Main Idea: Place an object inside the Player, which would be used to interact with the in-game camera.
        The camera will have a certain distance and will be on a certain angle from the 'follow' object. This 
        relationship will help in maintaining the fixed camera while moving the player.
*/
namespace RPG.Core
{
    
    public class FollowCamera : MonoBehaviour
    {
        public Transform target;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = target.position;
        }
    }

}
