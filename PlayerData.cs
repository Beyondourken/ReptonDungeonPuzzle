using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/PlayerData/BaseData")]
public class PlayerData : ScriptableObject {
    
//[Header(move state)]
 
   public float movementVelocity = 10f;
  public float jumpVelocity = 15f;
  public int amountOfJumps = 1;
  public float coyoteTime = 0.2f;
  public float variableJumpHeightMultiplier =  0.5f;
   public float groundCheckRadius = 0.3f;
  public LayerMask whatIsGround;
  
}
