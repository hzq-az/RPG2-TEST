using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IceAndFire Effect", menuName = "Data/Item Effect/Ice And Fire")]

public class IceAndFire_Effect : ItemEffect
{
    [SerializeField] private GameObject iceAndFirePrefab;
    [SerializeField] private float xVelocity;
    public override void ExecuteEffect(Transform _respawnPosition)
    {
        //Transform player = PlayerManager.instance.player.transform;
        Player player = PlayerManager.instance.player;
       // bool thirdAttack = player.GetComponent<Player>().primaryAttack.comboCounter == 2;
        bool thirdAttack = player.primaryAttack.comboCounter == 2;
        if (thirdAttack)
        {
            GameObject newIceAndFire = Instantiate(iceAndFirePrefab, _respawnPosition.position, player.transform.rotation);
            newIceAndFire.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * player.facingDir,0);

            Destroy(newIceAndFire, 8);
        }
    }
}
