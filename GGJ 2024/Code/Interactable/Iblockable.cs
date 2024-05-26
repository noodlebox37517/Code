using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlockable
{
    public void AddBlocker(GameObject go);
    public void RemoveBlocker(GameObject go);
}
