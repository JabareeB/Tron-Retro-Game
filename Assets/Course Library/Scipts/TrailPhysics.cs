using UnityEngine;
using System.Collections;

namespace LightCycleSystem
{
    [HelpURL("https://assetstore.unity.com/packages/slug/226235")]
    public class TrailPhysics : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefab;

        private Transform[] cubes;

        /// <summary>
        /// Spawn cubes with collision to track collisions
        /// </summary>
        /// <returns></returns>
        private IEnumerator Start()
        {
            cubes = new Transform[TrailLine.pointCount];
            for (int i = 0; i < cubes.Length; i++)
            {
                Transform cube = Instantiate(prefab).transform;
                cube.position = Vector3.up * -100000f;
                cubes[i] = cube;
            }
            yield return TrailLine.delay;
            int index = cubes.Length;
            while (true)
            {
                index--;
                if (index < 0)
                    index = cubes.Length - 1;
                cubes[index].position = transform.position;
                cubes[index].rotation = transform.rotation;
                yield return TrailLine.updateDelay;
            }
        }
    }
}