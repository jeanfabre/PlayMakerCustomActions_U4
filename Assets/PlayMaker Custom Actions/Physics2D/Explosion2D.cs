// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("2D explosion.")]
	public class Explosion2D : FsmStateAction
	{
    public FsmOwnerDefault gameObject;

    [UIHint(UIHint.Tag)]
    [Tooltip("Filter by Tag.")]
    public FsmString collideTag;

    [Tooltip("Set the timer for the explosion to explode.")]
    public FsmFloat explosionTimer;

    [Tooltip("Set the rate in which the explosion will reach its max size.")]
    public FsmFloat explosionRate;

    [Tooltip("Set the size of the explosion.")]
    public FsmFloat explosionSize;

    [Tooltip("Set the power of the explosion.")]
    public FsmFloat explosionForce;

 
    [Tooltip("Add Force on X.")]
    public FsmFloat explodeX;

    [Tooltip("Add Force on Y.")]
    public FsmFloat explodeY;
	
	[ActionSection("Result")]
	[UIHint(UIHint.Variable)]
	public FsmFloat current_radius;

    [Tooltip("Event to send when the Timer is complete.")]
    public FsmEvent finishEvent;


    bool exploded = false;
	

    //Collider2D explosion_radius;
    CircleCollider2D explosion_radius;

    public override void Reset()
    {
        gameObject = null;
        collideTag = new FsmString() { UseVariable = true };
        explosionTimer = 1f;
        explosionRate = 1f;
        explosionSize = 15f;
        explosionForce = 3f;
        current_radius = 0f;
        explodeY = 2f;
        explodeX = 2f;
        finishEvent = null;
    }

    public override void OnPreprocess()
    {
        Fsm.HandleFixedUpdate = true;
    }

	// Use this for initialization
	public override void OnEnter () {

        if (Owner.GetComponent<CircleCollider2D>() == null)
        {
            this.Owner.AddComponent<CircleCollider2D>();
            Owner.GetComponent<Collider2D>().isTrigger = true;
            
        }

        explosion_radius = Owner.GetComponent<CircleCollider2D>();
	}

    public override void OnUpdate()
    {
        explosionTimer.Value -= Time.deltaTime;
        if (explosionTimer.Value < 0)
        {
            exploded = true;

        }
    }

    public override void OnFixedUpdate()
    {
        DoExplosion();
    }

    void DoExplosion()
    {
        if (exploded == true)
            {
            if (current_radius.Value < explosionSize.Value)
            {
                current_radius.Value += explosionRate.Value;
            }
            else
            {
                if (finishEvent != null)
                {
                    Fsm.Event(finishEvent);
                    Finish();
                }
                explosionForce = 0f;
                //Object.Destroy(this.Owner.transform.parent.gameObject);
            }

            explosion_radius.radius = current_radius.Value;
        }
    }
	
	public override void DoTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == collideTag.Value || collideTag.IsNone || string.IsNullOrEmpty(collideTag.Value)) {
	        if (exploded == true)
	        {
	            if (col.gameObject.GetComponent<Rigidbody2D>() != null)
	            {
	                Vector2 target = col.gameObject.transform.position;
	                Vector2 bomb = Owner.transform.position;

	                Vector2 direction = explosionForce.Value * (target - bomb);

	                col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x * explodeX.Value, direction.y * explodeY.Value));
	            }
	        }
       }
     }
    
  }
}
