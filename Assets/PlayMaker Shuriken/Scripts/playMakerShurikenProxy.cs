using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

public class playMakerShurikenProxy : MonoBehaviour {
	

	private ParticleSystem.CollisionEvent[] collisionEvents = new ParticleSystem.CollisionEvent[16];
	
	private PlayMakerFSM _fsm;
	
	void Start()
	{
		_fsm = this.GetComponent<PlayMakerFSM>();
		
		if (_fsm==null)
		{
			Debug.LogError("No Fsm found",this);
		}

	}
	
	public ParticleSystem.CollisionEvent[] GetCollisionEvents()
	{
		return collisionEvents;
	}
	
	
    void OnParticleCollision(GameObject other) {
		
        ParticleSystem particleSystem;
		
        particleSystem = other.GetComponent<ParticleSystem>();
		
        int safeLength = particleSystem.safeCollisionEventSize;
       // if (collisionEvents.Length < safeLength)
           
		
		collisionEvents = new ParticleSystem.CollisionEvent[safeLength];
		int numCollisionEvents = particleSystem.GetCollisionEvents(gameObject, collisionEvents);
		
	
        
		FsmEventData _data = new FsmEventData();
		_data.GameObjectData = other;
		_data.IntData = numCollisionEvents;
		PlayMakerUtils.SendEventToGameObject(_fsm,this.gameObject,"ON PARTICLE COLLISION");
		
		
		/*
       
        int i = 0;
        while (i < numCollisionEvents) {
            if (gameObject.rigidbody) {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * 10;
                gameObject.rigidbody.AddForce(force);
            }
            i++;
        }
        
		*/
	}
}
