using UnityEngine;
using System.Xml;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;


public class CompileXMLFiles : MonoBehaviour {

  public static void compileAbilities() {

    //textwriter, for writing to abilityEnum.cs
    using (TextWriter abilityEnumTW = File.CreateText( "Assets/Resources/XML/AbilityEnum.cs"))
    {
      //write the basics to the enum file
      abilityEnumTW.WriteLine("using UnityEngine;");
      abilityEnumTW.WriteLine();
      abilityEnumTW.WriteLine("public enum PBAbilities {");

      string curAbilityEnum = "";
      string curAbilityName = "";
      string curAbilityDesc = "";

      //clear the abilities list
      AbilityManager.clearList();

      try {
        XDocument reader = XDocument.Load("Assets/Resources/XML/Abilities.xml");

        //read each ability
        foreach (XElement xe in reader.Descendants("Ability")) {
          //write previously found ability because the last ability found shouldn't have a comma after it
          if (!curAbilityEnum.Equals("")) {
            abilityEnumTW.WriteLine("\t{0}\t,",curAbilityEnum);
          }
          curAbilityEnum = "";
          curAbilityName = "";
          curAbilityDesc = "";
          //read each element in the ability (InternalName, Name, and Description in this case)
          foreach(XElement childXe in xe.Elements()) {
            switch(childXe.Name.ToString()) {
              case "InternalName":
                curAbilityEnum = childXe.Value;
                //check validity of the Internal Name
                //must be uppercase, and no non-alphanumeric characters
                if (!System.Text.RegularExpressions.Regex.IsMatch(curAbilityEnum, "^[A-Z0-9]*$")) {
                  Debug.Log("Invalid InternalName of "+curAbilityEnum+" must be all caps (Example:  DRIZZLE), and be all alphanumeric characters.  Ability skipped.");
                  curAbilityEnum = "";
                }
                break;
              case "Name":
                curAbilityName = childXe.Value;
                break;
              case "Description":
                curAbilityDesc = childXe.Value;
                break;
              default:
                Debug.Log("Invalid elecment " + childXe.Name + " for ability " + xe.Value + ".  Document compiled, but element is not included");
                break;
            }
          }
          //invalidate ability if not containing all required elements
          if (curAbilityName.Equals("")) {
            Debug.Log("Ability " + curAbilityEnum + "is missing a 'Name' field.  Ability skipped");
            curAbilityEnum = "";
          }
          if (curAbilityDesc.Equals("")) {
            Debug.Log("Ability " + curAbilityEnum + "is missing a 'Description' field.  Ability skipped");
            curAbilityEnum = "";
          }
          //add the found ability to AbilityManager, so it can save it.  don't add if invalid
          if (!curAbilityEnum.Equals("")) {
            AbilityManager.addAbility(curAbilityEnum, curAbilityName, curAbilityDesc);
          }
        }
        //try to ensure (as best as possible) that there are no compilation errors in abilityEnum.cs
        //so when we fix our Abilities.xml, we can just compile compile it right away
        if (!curAbilityEnum.Equals("")) {
          abilityEnumTW.WriteLine("\t{0}\t",curAbilityEnum);
        }
     } catch {
       if (!curAbilityEnum.Equals("")) {
         abilityEnumTW.WriteLine("\tBADVALUE\t");
       }
     }
     abilityEnumTW.WriteLine("}");

     //save all data writen to file, the load it back
     AbilityManager.saveDataFile();
     AbilityManager.loadDataFile();
     //AbilityManager.printEachDesc();
    }
  }


  public static void compileTypes() {

    //textwriter, for writing to TypesEnum.cs
    using (TextWriter typeEnumTW = File.CreateText( "Assets/Resources/XML/TypesEnum.cs"))
    {
      //write the basics to the enum file
      typeEnumTW.WriteLine("using UnityEngine;");
      typeEnumTW.WriteLine();
      typeEnumTW.WriteLine("public enum PBTypes {");


      string curTypeEnum = "";
      string curTypeName = "";
      bool curIsSpecial = false;
      List<string> curWeaknesses = new List<string>();
      List<string> curResistances = new List<string>();
      List<string> curImmunities = new List<string>();


      //write enum file first, so we can use it for validation of other elements
      try {
        XDocument reader = XDocument.Load("Assets/Resources/XML/Types.xml");

        //read each ability
        foreach (XElement xe in reader.Descendants("Types")) {
          foreach (XElement childXE in xe.Elements("Type")) {
            //write previously found ability because the last ability found shouldn't have a comma after it
            if (!curTypeEnum.Equals("")) {
              typeEnumTW.WriteLine("\t{0}\t,",curTypeEnum);
            }
            curTypeEnum = "";
            if (xe.Element("Type") != null) {
              curTypeEnum = childXE.Element("InternalName").Value;
              if (!System.Text.RegularExpressions.Regex.IsMatch(curTypeEnum, "^[A-Z]*$")) {
                Debug.Log("Invalid InternalName of "+curTypeEnum+" must be all caps (Example: FIRE), and be all alphabetical characters.  Ability skipped.");
                curTypeEnum = "";
              }
            }
          }
        }
        //try to ensure (as best as possible) that there are no compilation errors in abilityEnum.cs
        //so when we fix our Abilities.xml, we can just compile compile it right away
        if (!curTypeEnum.Equals("")) {
          typeEnumTW.WriteLine("\t{0}\t",curTypeEnum);
        }
     } catch {
       if (!curTypeEnum.Equals("")) {
         typeEnumTW.WriteLine("\tBADVALUE\t");
       }
     }
     typeEnumTW.WriteLine("}");
     typeEnumTW.Close();


      //clear the types list
      TypeManager.clearList();


      try {
        XDocument reader = XDocument.Load("Assets/Resources/XML/Types.xml");

        //add each type to TypeManager
        foreach (XElement xe in reader.Descendants("Type")) {
          curTypeEnum = "";
          curTypeName = "";
          curIsSpecial = false;
          curWeaknesses.Clear();
          curResistances.Clear();
          curImmunities.Clear();


          //read each element in the ability (InternalName, Name, and Description in this case)
          foreach(XElement childXe in xe.Elements()) {
            switch(childXe.Name.ToString()) {
              case "InternalName":
                curTypeEnum = childXe.Value;
                //check validity of the Internal Name
                //must be uppercase, and no non-alphanumeric characters
                if (!System.Text.RegularExpressions.Regex.IsMatch(curTypeEnum, "^[A-Z]*$")) {
                  curTypeEnum = "";
                }
                break;
              case "Name":
                curTypeName = childXe.Value;
                break;
              case "IsSpecial":
              case "IsSpecialType":
              case "IsSpecialType?":
              case "IsSpecial?":
                if (bool.TryParse(childXe.Value.ToString(), out curIsSpecial)) {
                  curIsSpecial = bool.Parse(childXe.Value.ToString());
                } else {
                  Debug.Log("The value " + childXe.Value.ToString() + " is not accepted for IsSpecial? , it must be 'True' or 'False'");
                  curTypeEnum = "";
                }
                break;
              case "Weakness":
                if (System.Enum.IsDefined(typeof(PBTypes), childXe.Value.ToString())) {
                  curWeaknesses.Add(childXe.Value.ToString());
                } else {
                  Debug.Log("The value " + childXe.Value.ToString() + " is not accepted for Weakness , it must be all caps, and a type defined in this XML file");
                  curTypeEnum = "";
                }
                break;
              case "Resistance":
                if (System.Enum.IsDefined(typeof(PBTypes), childXe.Value.ToString())) {
                  curResistances.Add(childXe.Value.ToString());
                } else {
                  Debug.Log("The value " + childXe.Value.ToString() + " is not accepted for Resistance , it must be all caps, and a type defined in this XML file");
                  curTypeEnum = "";
                }
                break;
              case "Immunity":
                if (System.Enum.IsDefined(typeof(PBTypes), childXe.Value.ToString())) {
                  curImmunities.Add(childXe.Value.ToString());
                } else {
                  Debug.Log("The value " + childXe.Value.ToString() + " is not accepted for Immunity , it must be all caps, and a type defined in this XML file");
                  curTypeEnum = "";
                }
                break;
              default:
                Debug.Log("Invalid elecment " + childXe.Name + " for ability " + xe.Value + ".  Document compiled, but element is not included");
                break;
            }
          }
          //invalidate ability if not containing all required elements
          if (curTypeName.Equals("")) {
            Debug.Log("Ability " + curTypeEnum + "is missing a 'Name' field.  Ability skipped");
            curTypeEnum = "";
          }
          //add the found ability to AbilityManager, so it can save it.  don't add if invalid
          if (!curTypeEnum.Equals("")) {
            TypeManager.addType(curTypeEnum, curTypeName, curIsSpecial, curWeaknesses, curResistances , curImmunities );
          }
        }

     } catch {

     }


     //save all data writen to file, the load it back
     if (TypeManager.getNumTypes()>0) {
       TypeManager.saveDataFile();
       TypeManager.loadDataFile();
       //TypeManager.printEachTypeName();
     } else {
       Debug.Log("You have 0 types successfully defined, please check Types.xml to remedy this");
     }

    }
  }


  public static void compileMoves() {

    //textwriter, for writing to TypesEnum.cs
    using (TextWriter moveEnumTW = File.CreateText( "Assets/Resources/XML/MovesEnum.cs"))
    {
      //write the basics to the enum file
      moveEnumTW.WriteLine("using UnityEngine;");
      moveEnumTW.WriteLine();
      moveEnumTW.WriteLine("public enum PBMoves {");
      moveEnumTW.WriteLine("\tNONE\t,");


      string curMoveEnum = "";
      string curMoveName = "";
      string curMoveEffectType = "";
      int curBaseDamage = -1;
      string curMoveType = "";
      string curMoveCategory = "";
      int curMoveAccuracy = -1;
      int curBasePP = -1;
      int curAdditionalEffectChance = -1;
      string curMoveTargetType = "";
      int curPriority = -7;
      bool curMakesContact = false;
      bool curBlockedByProtect = false;
      bool curBlockedByMagicBounce = false;
      bool curCanBeSnatched = false;
      bool curCanBeCopied = false;
      bool curAffectedByKingsRock = false;
      bool curThawsUser = false;
      bool curHighCritChance = false;
      bool curBitingMove = false;
      bool curPunchingMove = false;
      bool curSoundBasedMove = false;
      bool curPowderBasedMove = false;
      bool curPulseBasedMove = false;
      bool curBombBasedMove = false;
      string curMoveDesc = "";

      bool skipMove = false;
      string lastValidMove = "";


      //write enum file first, so we can use it for validation of other elements
      try {
        XDocument reader = XDocument.Load("Assets/Resources/XML/Moves.xml");

        //read each ability
        foreach (XElement xe in reader.Descendants("Moves")) {
          foreach (XElement childXE in xe.Elements("Move")) {
            //write previously found ability because the last ability found shouldn't have a comma after it
            if (!curMoveEnum.Equals("")) {
              moveEnumTW.WriteLine("\t{0}\t,",curMoveEnum);
            }
            curMoveEnum = "";
            if (xe.Element("Move") != null) {
              curMoveEnum = childXE.Element("InternalName").Value;
              if (!System.Text.RegularExpressions.Regex.IsMatch(curMoveEnum, "^[A-Z]*$")) {
                Debug.Log("Invalid InternalName of "+curMoveEnum+", must be all caps (Example: THUNDERBOLT), and be all alphabetical characters.  Move skipped.");
                curMoveEnum = "";
              }
            }
          }
        }
        //try to ensure (as best as possible) that there are no compilation errors in MovesEnum.cs
        //so when we fix our Moves.xml, we can just compile compile it right away
        if (!curMoveEnum.Equals("")) {
          moveEnumTW.WriteLine("\t{0}\t",curMoveEnum);
        }
     } catch (Exception e) {
       if (!curMoveEnum.Equals("")) {
         moveEnumTW.WriteLine("\tBADVALUE\t");
       }

       Debug.Log(e.ToString());


     }
     moveEnumTW.WriteLine("}");
     moveEnumTW.Close();


      //clear the moves list
      MoveManager.clearList();


      try {
        XDocument reader = XDocument.Load("Assets/Resources/XML/Moves.xml");
        //add each type to MoveManager
        foreach (XElement xe in reader.Descendants("Move")) {
          curMoveEnum = "";
          curMoveName = "";
          curMoveEffectType = "";
          curBaseDamage = -1;
          curMoveType = "";
          curMoveCategory = "";
          curMoveAccuracy = -1;
          curBasePP = -1;
          curAdditionalEffectChance = -1;
          curMoveTargetType = "";
          curPriority = -7;
          curMakesContact = false;
          curBlockedByProtect = false;
          curBlockedByMagicBounce = false;
          curCanBeSnatched = false;
          curCanBeCopied = false;
          curAffectedByKingsRock = false;
          curThawsUser = false;
          curHighCritChance = false;
          curBitingMove = false;
          curPunchingMove = false;
          curSoundBasedMove = false;
          curPowderBasedMove = false;
          curPulseBasedMove = false;
          curBombBasedMove = false;
          curMoveDesc = "";
          skipMove = false;


          //read each element in the move
          foreach(XElement childXe in xe.Elements()) {
            switch(childXe.Name.ToString()) {
              case "InternalName":
                curMoveEnum = childXe.Value;
                //check validity of the Internal Name
                //must be uppercase, and no non-alphanumeric characters
                if (!System.Text.RegularExpressions.Regex.IsMatch(curMoveEnum, "^[A-Z]*$")) {
                  curMoveEnum = "";
                } else {
                  lastValidMove = curMoveEnum;
                }
                break;
              case "Name":
                curMoveName = childXe.Value;
                if (!(curMoveName.Length>0)) {
                  curMoveEnum = "";
                }
                break;
              case "MoveEffectType":
                if (System.Enum.IsDefined(typeof(PBMoves), childXe.Value)) {
                  curMoveEffectType = childXe.Value;
                } else {
                  Debug.Log("The value " + childXe.Value + " is not accepted for Weakness , it must be all caps, and a type defined in this XML file");
                  curMoveEnum = "";
                  skipMove = true;
                }
                break;
              case "BaseDamage":
                int.TryParse(childXe.Value, out curBaseDamage);
                if (curBaseDamage<0) {
                  curMoveEnum = "";
                  skipMove = true;
                  Debug.Log(childXe.Value + "is not an acceptable base damage for " + curMoveEnum  + ", it must be greater than 0.");
                }
                break;
              case "MoveType":
                if (System.Enum.IsDefined(typeof(PBTypes), childXe.Value)) {
                  curMoveType = childXe.Value;
                } else {
                  Debug.Log("The value " + childXe.Value + " is not accepted for MoveType , it must be all caps, and a type defined in PBTypes");
                  curMoveEnum = "";
                  skipMove = true;
                }
                break;
              case "MoveCategory":
                if (System.Enum.IsDefined(typeof(MoveDamageCategory), childXe.Value)) {
                  curMoveCategory = childXe.Value;
                } else {
                  Debug.Log("The value " + childXe.Value.ToString() + " is not accepted for MoveCategory , it must be all caps, and a type defined in the enum MoveDamageCategory");
                  curMoveEnum = "";
                  skipMove = true;
                }
                break;
              case "Accuracy":
                int.TryParse(childXe.Value, out curMoveAccuracy);
                if (curMoveAccuracy<0 || curMoveAccuracy>100) {
                  curMoveEnum = "";
                  skipMove = true;
                  Debug.Log(childXe.Value + "is not an acceptable accuracy for " + curMoveEnum  + ", it must between 0 and 100.");
                }
                break;
              case "BasePP":
                int.TryParse(childXe.Value, out curBasePP);
                if (curBasePP<=0) {
                  curMoveEnum = "";
                  skipMove = true;
                  Debug.Log(childXe.Value + "is not an acceptable base PP for " + curMoveEnum  + ", it must between greater than 0.");
                }
                break;
              case "AdditionalEffectChance":
                int.TryParse(childXe.Value, out curAdditionalEffectChance);
                if (curAdditionalEffectChance<0 || curAdditionalEffectChance>100) {
                  curMoveEnum = "";
                  skipMove = true;
                  Debug.Log(childXe.Value + "is not an acceptable accuracy for " + curMoveEnum  + ", it must between 0 and 100.");
                }
                break;
              case "TargetType":
                if (System.Enum.IsDefined(typeof(MoveTargetType), childXe.Value)) {
                  curMoveTargetType = childXe.Value;
                } else {
                  Debug.Log("The value " + childXe.Value + " is not an accepted Target Type , it must be all caps, and a type defined in the enum MoveTargetType");
                  curMoveEnum = "";
                  skipMove = true;
                }
                break;
              case "Priority":
                int.TryParse(childXe.Value, out curPriority);
                if (curPriority<-6 || curPriority>6) {
                  curMoveEnum = "";
                  skipMove = true;
                  Debug.Log(childXe.Value + "is not an acceptable priority for " + curMoveEnum  + ", it must between -6 and 6.");
                }
                break;
              case "Flags":
                foreach (XElement flagChildXE in childXe.Elements()) {
                  if (skipMove) {
                    break;
                  }
                  switch(flagChildXE.Name.ToString()) {
                      case "MakesContact":
                        if (bool.TryParse(flagChildXE.Value, out curMakesContact)) {
                          curMakesContact = bool.Parse(flagChildXE.Value);
                        } else {
                          Debug.Log("The value " + flagChildXE.Value + " is not accepted for MakesContact , it must be 'True' or 'False'");
                          curMoveEnum = "";
                          skipMove = true;
                        }
                        break;
                      case "BlockedByProtect":
                        if (bool.TryParse(flagChildXE.Value, out curBlockedByProtect)) {
                          curBlockedByProtect = bool.Parse(flagChildXE.Value);
                        } else {
                          Debug.Log("The value " + flagChildXE.Value + " is not accepted for BlockedByProtect , it must be 'True' or 'False'");
                          curMoveEnum = "";
                          skipMove = true;
                        }
                        break;
                      case "BlockedByMagicBounce":
                        if (bool.TryParse(flagChildXE.Value, out curBlockedByMagicBounce)) {
                          curBlockedByMagicBounce = bool.Parse(flagChildXE.Value);
                        } else {
                          Debug.Log("The value " + flagChildXE.Value + " is not accepted for BlockedByMagicBounce , it must be 'True' or 'False'");
                          curMoveEnum = "";
                          skipMove = true;
                        }
                        break;
                      case "CanBeSnatched":
                        if (bool.TryParse(flagChildXE.Value, out curCanBeSnatched)) {
                          curCanBeSnatched = bool.Parse(flagChildXE.Value);
                        } else {
                          Debug.Log("The value " + flagChildXE.Value + " is not accepted for CanBeSnatched , it must be 'True' or 'False'");
                          curMoveEnum = "";
                          skipMove = true;
                        }
                        break;
                      case "CanBeCopied":
                        if (bool.TryParse(flagChildXE.Value, out curCanBeCopied)) {
                          curCanBeCopied = bool.Parse(flagChildXE.Value);
                        } else {
                          Debug.Log("The value " + flagChildXE.Value + " is not accepted for CanBeCopied , it must be 'True' or 'False'");
                          curMoveEnum = "";
                          skipMove = true;
                        }
                        break;
                      case "AffectedByKingsRock":
                        if (bool.TryParse(flagChildXE.Value, out curAffectedByKingsRock)) {
                          curAffectedByKingsRock = bool.Parse(flagChildXE.Value);
                        } else {
                          Debug.Log("The value " + flagChildXE.Value + " is not accepted for AffectedByKingsRock , it must be 'True' or 'False'");
                          curMoveEnum = "";
                          skipMove = true;
                        }
                        break;
                      case "ThawsUser":
                        if (bool.TryParse(flagChildXE.Value, out curThawsUser)) {
                          curThawsUser = bool.Parse(flagChildXE.Value);
                        } else {
                          Debug.Log("The value " + flagChildXE.Value + " is not accepted for ThawsUser , it must be 'True' or 'False'");
                          curMoveEnum = "";
                          skipMove = true;
                        }
                        break;
                      case "HighCritChance":
                        if (bool.TryParse(flagChildXE.Value, out curHighCritChance)) {
                          curHighCritChance = bool.Parse(flagChildXE.Value);
                        } else {
                          Debug.Log("The value " + flagChildXE.Value + " is not accepted for HighCritChance , it must be 'True' or 'False'");
                          curMoveEnum = "";
                          skipMove = true;
                        }
                        break;
                      case "BitingMove":
                        if (bool.TryParse(flagChildXE.Value, out curBitingMove)) {
                          curBitingMove = bool.Parse(flagChildXE.Value);
                        } else {
                          Debug.Log("The value " + flagChildXE.Value + " is not accepted for BitingMove , it must be 'True' or 'False'");
                          curMoveEnum = "";
                          skipMove = true;
                        }
                        break;
                      case "PunchingMove":
                        if (bool.TryParse(flagChildXE.Value, out curPunchingMove)) {
                          curPunchingMove = bool.Parse(flagChildXE.Value);
                        } else {
                          Debug.Log("The value " + flagChildXE.Value + " is not accepted for PunchingMove , it must be 'True' or 'False'");
                          curMoveEnum = "";
                          skipMove = true;
                        }
                        break;
                      case "SoundBasedMove":
                        if (bool.TryParse(flagChildXE.Value, out curSoundBasedMove)) {
                          curSoundBasedMove = bool.Parse(flagChildXE.Value);
                        } else {
                          Debug.Log("The value " + flagChildXE.Value + " is not accepted for SoundBasedMove , it must be 'True' or 'False'");
                          curMoveEnum = "";
                          skipMove = true;
                        }
                        break;
                      case "PowderBasedMove":
                        if (bool.TryParse(flagChildXE.Value, out curPowderBasedMove)) {
                          curPowderBasedMove = bool.Parse(flagChildXE.Value);
                        } else {
                          Debug.Log("The value " + flagChildXE.Value + " is not accepted for PowderBasedMove , it must be 'True' or 'False'");
                          curMoveEnum = "";
                          skipMove = true;
                        }
                        break;
                      case "PulseBasedMove":
                        if (bool.TryParse(flagChildXE.Value, out curPulseBasedMove)) {
                          curPulseBasedMove = bool.Parse(flagChildXE.Value);
                        } else {
                          Debug.Log("The value " + flagChildXE.Value + " is not accepted for PulseBasedMove , it must be 'True' or 'False'");
                          curMoveEnum = "";
                          skipMove = true;
                        }
                        break;
                      case "BombBasedMove":
                        if (bool.TryParse(flagChildXE.Value, out curBombBasedMove)) {
                          curBombBasedMove = bool.Parse(flagChildXE.Value);
                        } else {
                          Debug.Log("The value " + flagChildXE.Value + " is not accepted for BombBasedMove , it must be 'True' or 'False'");
                          curMoveEnum = "";
                          skipMove = true;
                        }
                        break;
                      default:
                        Debug.Log("Invalid flag " + flagChildXE.Name + " for move " + flagChildXE.Value + ".  Does not fail compile, but flag is not included");
                        break;

                  }
                }
                break;
              case "MoveDescription":
                curMoveDesc = childXe.Value;
                if (!(curMoveDesc.Length>0)) {
                  curMoveEnum = "";
                  skipMove = true;
                }
                break;
              default:
                Debug.Log("Invalid elecment " + childXe.Name + " for move " + xe.Value + ".  Does not fail compile, but element is not included");
                break;
            }
            if (skipMove) {
              break;
            }
          }
          if (!skipMove) {
            if (!curMoveEnum.Equals("")) {
              //invalidate move if not containing all required elements, inform user of each missing field
              if (curMoveName.Equals("")) {
                Debug.Log("Move " + curMoveEnum + "is missing a 'Name' field.");
                skipMove = true;
              }
              if (curMoveEffectType.Equals("")) {
                Debug.Log("Move " + curMoveEnum + "is missing a 'MoveEffectType' field.");
                skipMove = true;
              }
              if (curBaseDamage<0) {
                Debug.Log("Move " + curMoveEnum + "is missing a 'BaseDamage' field.");
                skipMove = true;
              }
              if (curMoveType.Equals("")) {
                Debug.Log("Move " + curMoveEnum + "is missing a 'MoveType' field.");
                skipMove = true;
              }
              if (curMoveCategory.Equals("")) {
                Debug.Log("Move " + curMoveEnum + "is missing a 'MoveCategory' field.");
                skipMove = true;
              }
              if (curMoveAccuracy<0) {
                Debug.Log("Move " + curMoveEnum + "is missing an 'Accuracy' field.");
                skipMove = true;
              }
              if (curBasePP<0) {
                Debug.Log("Move " + curMoveEnum + "is missing a 'BasePP' field.");
                skipMove = true;
              }
              if (curAdditionalEffectChance<0) {
                Debug.Log("Move " + curMoveEnum + "is missing an 'AdditionalEffectChance' field.");
                skipMove = true;
              }
              if (curMoveTargetType.Equals("")) {
                Debug.Log("Move " + curMoveEnum + "is missing a 'MoveTargetType' field.");
                skipMove = true;
              }
              if (curPriority<-6) {
                Debug.Log("Move " + curMoveEnum + "is missing a 'Priority' field.");
                skipMove = true;
              }
              if (curMoveDesc.Equals("")) {
                Debug.Log("Move " + curMoveEnum + "is missing a 'MoveDescription' field.");
                skipMove = true;
              }

              if (skipMove) {
                Debug.Log("One or more fields for " + curMoveEnum + " are invalid, skipping move.\n\n");
              } else {
                //if all required fields are present, add move
                MoveManager.addMove( (PBMoves)Enum.Parse(typeof(PBMoves), curMoveEnum), curMoveName,
                            (PBMoves)Enum.Parse(typeof(PBMoves), curMoveEffectType), curBaseDamage, (PBTypes)Enum.Parse(typeof(PBTypes), curMoveType),
                            (MoveDamageCategory)Enum.Parse(typeof(MoveDamageCategory), curMoveCategory), curMoveAccuracy,
                            curBasePP, curAdditionalEffectChance, (MoveTargetType)Enum.Parse(typeof(MoveTargetType), curMoveTargetType),
                            curPriority, curMakesContact, curBlockedByProtect, curBlockedByMagicBounce, curCanBeSnatched, curCanBeCopied,
                            curAffectedByKingsRock, curThawsUser, curHighCritChance, curBitingMove, curPunchingMove, curSoundBasedMove,
                            curPowderBasedMove, curPulseBasedMove, curBombBasedMove, curMoveDesc);
              }
            } else {
              //print error report to the user to let know of missing InternalName
              if (curMoveName.Equals("")) {
                Debug.Log("Move following " + lastValidMove + "is missing an 'InternalName' field.  Move skipped");
              } else {
                Debug.Log("Move " + curMoveName + "is missing an 'InternalName' field.  Move skipped");
              }
            }
          } else {

          }
        }

     } catch {

     }


     //save all data writen to file, the load it back
     if (MoveManager.getNumMoves()>0) {
       MoveManager.saveDataFile();
       MoveManager.loadDataFile();
       //MoveManager.printEachMoveName();
     } else {
       Debug.Log("You have 0 moves successfully defined, please check Types.xml to remedy this");
     }

    }
  }



}
