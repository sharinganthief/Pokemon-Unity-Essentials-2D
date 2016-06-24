using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

[System.Serializable]
public struct Ability {
  public string internalName;
  public string name;
  public string description;

  public Ability(string p_internalName, string p_name, string p_description){
    internalName = p_internalName;
    name = p_name;
    description = p_description;
  }
}


public class AbilityManager : MonoBehaviour {

  public static List<Ability> abilityList = new List<Ability>();


  public static void addAbility(string p_internalName, string p_name, string p_description){
    abilityList.Add(new Ability(p_internalName, p_name, p_description));
  }

  public static void clearList(){
    abilityList.Clear();
  }

  public static void printEachDesc(){
    foreach(Ability ability in abilityList){
      Debug.Log(ability.description);
    }
  }

  public static void saveDataFile(){
    using (Stream stream = File.Open("Assets/Resources/Data/Abilities", FileMode.Create))
    {
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

        List<Ability> newAbilityList = new List<Ability>();
        string tempInternalName;
        string tempName;
        string tempDesc;
        foreach(Ability ability in abilityList){
          tempInternalName = System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(ability.internalName));
          tempName = System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(ability.name));
          tempDesc = System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(ability.description));
          newAbilityList.Add(new Ability(tempInternalName, tempName, tempDesc));
        }
        binaryFormatter.Serialize(stream, newAbilityList);
    }
  }

  public static void loadDataFile(){
    AbilityManager.clearList();
    List<Ability> tempAbilityList;
    using (Stream stream = File.Open("Assets/Resources/Data/Abilities", FileMode.Open))
    {
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        abilityList.Clear();
        tempAbilityList = (List<Ability>)binaryFormatter.Deserialize(stream);
    }

    string tempInternalName;
    string tempName;
    string tempDesc;
    foreach(Ability ability in tempAbilityList){
      tempInternalName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(ability.internalName));
      tempName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(ability.name));
      tempDesc = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(ability.description));
      abilityList.Add(new Ability(tempInternalName, tempName, tempDesc));
    }
  }

}
