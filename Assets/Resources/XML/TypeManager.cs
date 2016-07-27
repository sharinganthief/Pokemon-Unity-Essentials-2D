using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

[System.Serializable]
public struct Types {
  public string internalName;
  public string name;
  public bool isSpecialType;
  public List<string> weaknesses;
  public List<string> resistances;
  public List<string> immunitiies;

  public Types(string p_internalName, string p_name, bool p_isSpecialType, List<string> p_weaknesses, List<string> p_resistances, List<string> p_immunitiies) {
    internalName = p_internalName;
    name = p_name;
    isSpecialType = p_isSpecialType;
    weaknesses = new List<string>(p_weaknesses);
    resistances = new List<string>(p_resistances);
    immunitiies = new List<string>(p_immunitiies);
  }

}


public class TypeManager : MonoBehaviour {

  public static List<Types> typeList = new List<Types>();


  public static void addType(string p_internalName, string p_name, bool p_isSpecialType, List<string> p_weaknesses, List<string> p_resistances, List<string> p_immunitiies) {
    typeList.Add(new Types(p_internalName, p_name, p_isSpecialType, p_weaknesses, p_resistances, p_immunitiies));
  }

  public static void clearList() {
    typeList.Clear();
  }

  public static void printEachTypeName() {
    foreach(Types ability in typeList) {
      Debug.Log(ability.name);
    }
  }

  public static int getNumTypes() {
    return typeList.Count;
  }

  public static void saveDataFile() {
    using (Stream stream = File.Open("Assets/Resources/Data/Type", FileMode.Create))
    {
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

        List<Types> newTypeList = new List<Types>();
        string tempInternalName;
        string tempName;
        List<string> tempWeaknesses;
        List<string> tempResistances;
        List<string> tempImmunities;
        foreach(Types ability in typeList) {

          tempInternalName = System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(ability.internalName));
          tempName = System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(ability.name));

          tempWeaknesses = new List<string>();
          tempResistances = new List<string>();
          tempImmunities = new List<string>();

          foreach(string weakness in ability.weaknesses) {
            tempWeaknesses.Add(System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(weakness)));
          }

          foreach(string resistance in ability.resistances) {
            tempResistances.Add(System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(resistance)));
          }

          foreach(string immunity in ability.immunitiies) {
            tempImmunities.Add(System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(immunity)));
          }

          newTypeList.Add(new Types(tempInternalName, tempName, ability.isSpecialType, tempWeaknesses, tempResistances, tempImmunities));
        }
        binaryFormatter.Serialize(stream, newTypeList);
    }
  }

  public static void loadDataFile() {
    TypeManager.clearList();
    List<Types> tempAbilityList;
    using (Stream stream = File.Open("Assets/Resources/Data/Type", FileMode.Open))
    {
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        typeList.Clear();
        tempAbilityList = (List<Types>)binaryFormatter.Deserialize(stream);
    }

    string tempInternalName;
    string tempName;
    List<string> tempWeaknesses;
    List<string> tempResistances;
    List<string> tempImmunities;

    foreach(Types ability in tempAbilityList) {
      tempInternalName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(ability.internalName));
      tempName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(ability.name));

      tempWeaknesses = new List<string>();
      tempResistances = new List<string>();
      tempImmunities = new List<string>();

      foreach(string weakness in ability.weaknesses) {
        tempWeaknesses.Add(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(weakness)));
      }

      foreach(string resistance in ability.resistances) {
        tempResistances.Add(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(resistance)));
      }

      foreach(string immunity in ability.immunitiies) {
        tempImmunities.Add(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(immunity)));
      }

      typeList.Add(new Types(tempInternalName, tempName, ability.isSpecialType, tempWeaknesses, tempResistances, tempImmunities));
    }
  }

}
