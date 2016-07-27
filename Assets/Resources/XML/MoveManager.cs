using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

[System.Serializable]
public struct Moves {
  public PBMoves internalName;
  public string name;
  public PBMoves moveEffectType;
  public int baseDamage;
  public PBTypes moveType;
  public MoveDamageCategory moveCategory;
  public int moveAccuracy;
  public int basePP;
  public int additionalEffectChance;
  public MoveTargetType moveTargetType;
  public int priority;
  public bool makesContact;
  public bool blockedByProtect;
  public bool blockedByMagicBounce;
  public bool canBeSnatched;
  public bool canBeCopied;
  public bool affectedByKingsRock;
  public bool thawsUser;
  public bool highCritChance;
  public bool bitingMove;
  public bool punchingMove;
  public bool soundBasedMove;
  public bool powderBasedMove;
  public bool pulseBasedMove;
  public bool bombBasedMove;
  public string moveDesc;

  public Moves(PBMoves p_internalName, string p_name, PBMoves p_moveEffectType, int p_baseDamage, PBTypes p_moveType, MoveDamageCategory p_moveCategory,
              int p_moveAccuracy, int p_basePP, int p_additionalEffectChance, MoveTargetType p_moveTargetType, int p_priority, bool p_makesContact,
              bool p_blockedByProtect, bool p_blockedByMagicBounce, bool p_canBeSnatched, bool p_canBeCopied, bool p_affectedByKingsRock,
              bool p_thawsUser, bool p_highCritChance, bool p_bitingMove, bool p_punchingMove, bool p_soundBasedMove, bool p_powderBasedMove,
              bool p_pulseBasedMove, bool p_bombBasedMove, string p_moveDesc) {
    internalName = p_internalName;
    name = p_name;
    moveEffectType = p_moveEffectType;
    baseDamage = p_baseDamage;
    moveType = p_moveType;
    moveCategory = p_moveCategory;
    moveAccuracy = p_moveAccuracy;
    basePP = p_basePP;
    additionalEffectChance = p_additionalEffectChance;
    moveTargetType = p_moveTargetType;
    priority = p_priority;
    makesContact = p_makesContact;
    blockedByProtect = p_blockedByProtect;
    blockedByMagicBounce = p_blockedByMagicBounce;
    canBeSnatched = p_canBeSnatched;
    canBeCopied = p_canBeCopied;
    affectedByKingsRock = p_affectedByKingsRock;
    thawsUser = p_thawsUser;
    highCritChance = p_highCritChance;
    bitingMove = p_bitingMove;
    punchingMove = p_punchingMove;
    soundBasedMove = p_soundBasedMove;
    powderBasedMove = p_powderBasedMove;
    pulseBasedMove = p_pulseBasedMove;
    bombBasedMove = p_bombBasedMove;
    moveDesc = p_moveDesc;
  }

}


public class MoveManager : MonoBehaviour {

  public static List<Moves> moveList = new List<Moves>();


  public static void addMove(PBMoves p_internalName, string p_name, PBMoves p_moveEffectType, int p_baseDamage, PBTypes p_moveType, MoveDamageCategory p_moveCategory,
              int p_moveAccuracy, int p_basePP, int p_additionalEffectChance, MoveTargetType p_moveTargetType, int p_priority, bool p_makesContact,
              bool p_blockedByProtect, bool p_blockedByMagicBounce, bool p_canBeSnatched, bool p_canBeCopied, bool p_affectedByKingsRock,
              bool p_thawsUser, bool p_highCritChance, bool p_bitingMove, bool p_punchingMove, bool p_soundBasedMove, bool p_powderBasedMove,
              bool p_pulseBasedMove, bool p_bombBasedMove, string p_moveDesc)  {
    moveList.Add(new Moves(p_internalName, p_name, p_moveEffectType, p_baseDamage, p_moveType, p_moveCategory,
                p_moveAccuracy, p_basePP, p_additionalEffectChance, p_moveTargetType, p_priority, p_makesContact,
                p_blockedByProtect, p_blockedByMagicBounce, p_canBeSnatched, p_canBeCopied, p_affectedByKingsRock,
                p_thawsUser, p_highCritChance, p_bitingMove, p_punchingMove, p_soundBasedMove, p_powderBasedMove,
                p_pulseBasedMove, p_bombBasedMove, p_moveDesc)) ;
  }

  public static void clearList() {
    moveList.Clear();
  }

  public static void printEachMoveName() {
    foreach(Moves move in moveList) {
      Debug.Log(move.name);
    }
  }

  public static int getNumMoves() {
    return moveList.Count;
  }

  public static void saveDataFile() {
    using (Stream stream = File.Open("Assets/Resources/Data/Move", FileMode.Create))
    {
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

        List<Moves> newMoveList = new List<Moves>();
        string tempName;
        string tempMoveDesc;


        foreach(Moves move in moveList) {

          tempName = System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(move.name));
          tempMoveDesc = System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(move.moveDesc));


          newMoveList.Add(new Moves(move.internalName, tempName, move.moveEffectType, move.baseDamage, move.moveType,
                      move.moveCategory, move.moveAccuracy, move.basePP, move.additionalEffectChance, move.moveTargetType,
                      move.priority, move.makesContact, move.blockedByProtect, move.blockedByMagicBounce, move.canBeSnatched,
                      move.canBeCopied, move.affectedByKingsRock, move.thawsUser, move.highCritChance, move.bitingMove,
                      move.punchingMove, move.soundBasedMove, move.powderBasedMove, move.pulseBasedMove, move.bombBasedMove,
                      tempMoveDesc));
        }

        binaryFormatter.Serialize(stream, newMoveList);
    }
  }

  public static void loadDataFile() {
    MoveManager.clearList();
    List<Moves> tempMoveList;
    using (Stream stream = File.Open("Assets/Resources/Data/Move", FileMode.Open))
    {
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        moveList.Clear();
        tempMoveList = (List<Moves>)binaryFormatter.Deserialize(stream);
    }

    string tempName;
    string tempMoveDesc;

    foreach(Moves move in tempMoveList) {
      tempName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(move.name));
      tempMoveDesc = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(move.moveDesc));

      moveList.Add(new Moves(move.internalName, tempName, move.moveEffectType, move.baseDamage, move.moveType,
                  move.moveCategory, move.moveAccuracy, move.basePP, move.additionalEffectChance, move.moveTargetType,
                  move.priority, move.makesContact, move.blockedByProtect, move.blockedByMagicBounce, move.canBeSnatched,
                  move.canBeCopied, move.affectedByKingsRock, move.thawsUser, move.highCritChance, move.bitingMove,
                  move.punchingMove, move.soundBasedMove, move.powderBasedMove, move.pulseBasedMove, move.bombBasedMove,
                  tempMoveDesc));
    }
  }

}
