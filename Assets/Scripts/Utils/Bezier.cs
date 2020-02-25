using UnityEngine;

public class Bezier : MonoBehaviour
{
	[SerializeField]
	private Transform[] _controlPoints = null;

	private int _curveCount = 0;

	private float SUBCOUNT = 50;

	private void Start()
	{
		_curveCount = _controlPoints.Length / 3;
	}

	private void OnDrawGizmos()
	{
		_curveCount = _controlPoints.Length / 3;
		for (int i = 0; i < _controlPoints.Length; i++)
		{
			if (i % 3 == 0)
				Gizmos.color = Color.green;
			else
				Gizmos.color = Color.grey;

			Gizmos.DrawSphere(_controlPoints[i].position, 0.2f);
		}

		Vector3 lastPosition = _controlPoints[0].position;
		for (int j = 0; j < _curveCount; j++)
		{
			for (int i = 1; i <= SUBCOUNT; i++)
			{
				float t = i / SUBCOUNT;
				int nodeIndex = j * 3;
				Vector3 newPosition = CalculateCubicBezierPoint(t, _controlPoints[nodeIndex].position, _controlPoints[nodeIndex + 1].position, _controlPoints[nodeIndex + 2].position, _controlPoints[nodeIndex + 3].position);

				Gizmos.DrawLine(lastPosition, newPosition);
				lastPosition = newPosition;
			}
		}
	}

	private Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		float u = 1 - t;
		float tt = t * t;
		float uu = u * u;
		float uuu = uu * u;
		float ttt = tt * t;

		Vector3 p = uuu * p0;
		p += 3 * uu * t * p1;
		p += 3 * u * tt * p2;
		p += ttt * p3;

		return p;
	}

	public Vector3 GetPointByXAxis(float xPosition)
	{
		Vector3 result = Vector3.zero;
		for (int i = 0; i < _curveCount; i++)
		{
			int nodeIndex = i * 3;
			if (xPosition >= _controlPoints[nodeIndex].position.x &&
				xPosition < _controlPoints[nodeIndex + 3].position.x)
			{
				float distance = float.MaxValue;
				for (int j = 0; j <= SUBCOUNT; j++)
				{
					float t = j / SUBCOUNT;
					Vector3 position = CalculateCubicBezierPoint(t, _controlPoints[nodeIndex].position, _controlPoints[nodeIndex + 1].position, _controlPoints[nodeIndex + 2].position, _controlPoints[nodeIndex + 3].position);
					float computedDistance = Mathf.Abs(position.x - xPosition);
					if (computedDistance < distance)
					{
						result = position;
						distance = computedDistance;
					}
				}
			}
		}

		return result;
	}
}