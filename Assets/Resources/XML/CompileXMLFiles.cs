using UnityEngine;
using System.Xml;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;


public class CompileXMLFiles : MonoBehaviour {

  public static void compileAbilities(){

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
        foreach (XElement xe in reader.Descendants("Ability")){
          //write previously found ability because the last ability found shouldn't have a comma after it
          if (!curAbilityEnum.Equals("")){
            abilityEnumTW.WriteLine("\t{0}\t,",curAbilityEnum);
          }
          curAbilityEnum = "";
          curAbilityName = "";
          curAbilityDesc = "";
          //read each element in the ability (InternalName, Name, and Description in this case)
          foreach(XElement childXe in xe.Descendants()) {
            switch(childXe.Name.ToString()) {
              case "InternalName":
                curAbilityEnum = childXe.Value;
                //check validity of the Internal Name
                //must be uppercase, and no non-alphanumeric characters
                if (!System.Text.RegularExpressions.Regex.IsMatch(curAbilityEnum, "^[A-Z0-9]*$")){
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
          if (curAbilityName.Equals("")){
            Debug.Log("Ability " + curAbilityEnum + "is missing a 'Name' field.  Ability skipped");
            curAbilityEnum = "";
          }
          if (curAbilityDesc.Equals("")){
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
        if (!curAbilityEnum.Equals("")){
          abilityEnumTW.WriteLine("\t{0}\t",curAbilityEnum);
        }
     } catch {
       if (!curAbilityEnum.Equals("")){
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


  public static void compileTypes(){

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
      int count = 0;


      //write enum file first, so we can use it for validation of other elements
      try {
        XDocument reader = XDocument.Load("Assets/Resources/XML/Types.xml");

        //read each ability
        foreach (XElement xe in reader.Descendants("Types")){
          foreach (XElement childXE in xe.Descendants("Type")){
            //write previously found ability because the last ability found shouldn't have a comma after it
            if (!curTypeEnum.Equals("")){
              typeEnumTW.WriteLine("\t{0}\t,",curTypeEnum);
            }
            curTypeEnum = "";
            if (xe.Element("Type") != null){
              curTypeEnum = childXE.Element("InternalName").Value;
              if (!System.Text.RegularExpressions.Regex.IsMatch(curTypeEnum, "^[A-Z]*$")){
                Debug.Log("Invalid InternalName of "+curTypeEnum+" must be all caps (Example: FIRE), and be all alphabetical characters.  Ability skipped.");
                curTypeEnum = "";
              }
            }
          }
        }
        //try to ensure (as best as possible) that there are no compilation errors in abilityEnum.cs
        //so when we fix our Abilities.xml, we can just compile compile it right away
        if (!curTypeEnum.Equals("")){
          typeEnumTW.WriteLine("\t{0}\t",curTypeEnum);
        }
     } catch {
       if (!curTypeEnum.Equals("")){
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
        foreach (XElement xe in reader.Descendants("Type")){
          curTypeEnum = "";
          curTypeName = "";
          curIsSpecial = false;
          curWeaknesses.Clear();
          curResistances.Clear();
          curImmunities.Clear();


          //read each element in the ability (InternalName, Name, and Description in this case)
          foreach(XElement childXe in xe.Descendants()) {
            switch(childXe.Name.ToString()) {
              case "InternalName":
                curTypeEnum = childXe.Value;
                //check validity of the Internal Name
                //must be uppercase, and no non-alphanumeric characters
                if (!System.Text.RegularExpressions.Regex.IsMatch(curTypeEnum, "^[A-Z]*$")){
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
                if (bool.TryParse(childXe.Value.ToString(), out curIsSpecial)){
                  curIsSpecial = bool.Parse(childXe.Value.ToString());
                } else {
                  Debug.Log("The value " + childXe.Value.ToString() + " is not accepted for IsSpecial? , it must be 'True' or 'False'");
                  curTypeEnum = "";
                }
                break;
              case "Weakness":
                if (System.Enum.IsDefined(typeof(PBTypes), childXe.Value.ToString())){
                  curWeaknesses.Add(childXe.Value.ToString());
                } else {
                  Debug.Log("The value " + childXe.Value.ToString() + " is not accepted for Weakness , it must be all caps, and a type defined in this XML file");
                  curTypeEnum = "";
                }
                break;
              case "Resistance":
                if (System.Enum.IsDefined(typeof(PBTypes), childXe.Value.ToString())){
                  curResistances.Add(childXe.Value.ToString());
                } else {
                  Debug.Log("The value " + childXe.Value.ToString() + " is not accepted for Resistance , it must be all caps, and a type defined in this XML file");
                  curTypeEnum = "";
                }
                break;
              case "Immunity":
                if (System.Enum.IsDefined(typeof(PBTypes), childXe.Value.ToString())){
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
          if (curTypeName.Equals("")){
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
     if (TypeManager.getNumTypes()>0){
       TypeManager.saveDataFile();
       TypeManager.loadDataFile();
       //TypeManager.printEachTypeName();
     } else {
       Debug.Log("You have 0 types successfully defined, please check Types.xml to remedy this");
     }

    }
  }



}
