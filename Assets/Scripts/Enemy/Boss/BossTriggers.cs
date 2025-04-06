using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggers : EnemyAnimationTriggers
{
    private Enemy_Boss boss => GetComponentInParent<Enemy_Boss>();
    private void Relocate() => boss.FindPosition();

    private void MakeInvisiable() => boss.fx.MakeTransprent(true);
    private void MakeVisiable() => boss.fx.MakeTransprent(false);
}
