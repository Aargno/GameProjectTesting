using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BeardedManStudios.Forge.Networking.Generated;

public class PlayerCube : PlayerCubeBehavior
{

    /// <summary>
	/// The speed that this cube should move by when there are axis inputs
	/// </summary>
	public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // If this is not owned by the current network client then it needs to
        // assign it to the position and rotation specified
        if (!networkObject.IsOwner)
        {
            // Assign the position of this cube to the position sent on the network
            transform.position = networkObject.position;

            // Stop the function here and don't run any more code in this function
            return;
        }

        // Get the movement based on the axis input values
        Vector3 translation = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;

        // Scale the speed to normalize for processors
        translation *= speed * Time.deltaTime;

        // Move the object by the given translation
        transform.position += translation;

        // Since we are the owner, tell the network the updated position
        networkObject.position = transform.position;

        // Note: Forge Networking takes care of only sending the delta, so there
        // is no need for you to do that manually
    }

}
