using UnityEngine;
using System.Collections;

namespace LightCycleSystem
{
    [HelpURL("https://assetstore.unity.com/packages/slug/226235")]
    public class TrailLine : MonoBehaviour
    {
        public static WaitForSeconds delay = new WaitForSeconds(1.5f), updateDelay = new WaitForSeconds(0.025f);
        public static int pointCount = 400;

        private Vector3[] points;
        private LineRenderer lineRenderer;

        /// <summary>
        /// Managing the length of an array with light trail points
        /// </summary>
        /// <returns></returns>
        private IEnumerator Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            points = new Vector3[pointCount];
            for (int i = 0; i < pointCount; i++)
                points[i] = transform.position;
            lineRenderer.positionCount = pointCount;
            lineRenderer.enabled = false;
            yield return delay;
            lineRenderer.enabled = true;
            while (true)
            {
                for (int i = 0; i < points.Length - 1; i++)
                    points[i] = points[i + 1];
                yield return updateDelay;
            }
        }

        /// <summary>
        /// Updating the extreme point of the light trail
        /// </summary>
        private void LateUpdate()
        {
            points[pointCount - 1] = transform.position;
            lineRenderer.SetPositions(points);
        }
    }
}