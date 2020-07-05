using System;
using desExt.Runtime.Variables;
using UnityEngine;

namespace desExt.Runtime.References
{
    [Serializable]
    public class GameObjectReference : SimpleReference<GameObject, GameObjectVariable>
    {
    }
}