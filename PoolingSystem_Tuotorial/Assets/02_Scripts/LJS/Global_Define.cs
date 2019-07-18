using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace global_define
{
    public interface IPoolObject
    {
        void OnSpawnObject(Vector3 _direction);
    }
}