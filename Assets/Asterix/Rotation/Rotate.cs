using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
	// Parameters provided by Unity that will vary per object

	public float speed = 50f; // Speed of the rotation
	public Vector3 axis = Vector3.up; // Axis of rotation
	public float maxRot = 170f; // Minimum angle of rotation (to contstrain movement)
	public float minRot = -170f; // Maximim angle of rotation (if == min then unconstrained)
	public bool isFast = false; // Flag to allow speed-up on selection
	public bool isStopped = false; // Flag to allow stopping

	// Internal variable to track overall rotation (if constrained)

	private float rot = 0f;

	void Update()
	{
		if (isStopped)
			return;

		// Calculate the rotation amount as speed x time
		// (may get reduced to a smaller amount if near the angle limits)

		var locRot = speed * Time.deltaTime * (isFast ? 2f : 1f);

		// If we're constraining movement (via min & max angles)...

		if (minRot != maxRot)
		{
			// Then track the overall rotation

			if (locRot + rot < minRot)
			{
				// Don't go below the minimum angle

				locRot = minRot - rot;
			}
			else if (locRot + rot > maxRot)
			{
				// Don't go above the maximum angle

				locRot = maxRot - rot;
			}

			rot += locRot;

			// And reverse the direction if we're at a limit

			if (rot <= minRot || rot >= maxRot)
			{
				speed = -speed;
			}
		}

		// Perform the rotation itself

		transform.Rotate(axis, locRot);
	}
}