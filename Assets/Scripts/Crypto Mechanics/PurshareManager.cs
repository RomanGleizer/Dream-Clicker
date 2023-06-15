using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurshareManager : MonoBehaviour
{
    public Predicate<UpgradableItem> IsItemWasBought = x => x.Level > 0;
}
