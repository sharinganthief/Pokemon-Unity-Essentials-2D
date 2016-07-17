using UnityEngine;


//pockets, with their associated names
public enum PBPockets {

  [System.ComponentModel.Description("Items")]
  ITEMS,
  [System.ComponentModel.Description("Medicine")]
  MEDICINE,
  [System.ComponentModel.Description("Pokéballs")]
  POKEBALLS,
  [System.ComponentModel.Description("TMs & HMs")]
  TMSANDHMS,
  [System.ComponentModel.Description("Berries")]
  BERRIES,
  [System.ComponentModel.Description("Mail")]
  MAIL,
  [System.ComponentModel.Description("Battle Items")]
  BATTLEITEMS,
  [System.ComponentModel.Description("Key Items")]
  KEYITEMS
};



//Get name of pocket
public class PocketsEnum {
  public static string GetEnumDescription(PBPockets value)
  {
      System.Reflection.FieldInfo fi = value.GetType().GetField(value.ToString());

      System.ComponentModel.DescriptionAttribute[] attributes =
          (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

      if (attributes != null && attributes.Length > 0)
          return attributes[0].Description;
      else
          return value.ToString();
  }
}
