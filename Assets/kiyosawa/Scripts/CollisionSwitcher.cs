using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSwitcher : MonoBehaviour
{
    [SerializeField] private Collider2D _colA;
    [SerializeField] private Collider2D _colB;

    public bool ignoreCollision;
    private bool _ignoreCollisionCache;

    void Start()
    {
        _ignoreCollisionCache = ignoreCollision;
    }

    void Update()
    {
        if (_ignoreCollisionCache != ignoreCollision)
        {
            Physics2D.IgnoreCollision(_colA, _colB, ignoreCollision);

            _ignoreCollisionCache = ignoreCollision;
        }
    }
}
