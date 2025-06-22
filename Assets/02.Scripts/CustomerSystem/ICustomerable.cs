using System.Collections.Generic;
using UnityEngine;

public interface ICustomerable
{
    public void OnReady(List<Transform> root);
    public void OnStart();
    public void OnEnd();
}
