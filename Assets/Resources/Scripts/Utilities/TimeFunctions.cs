using UnityEngine;
using System.Collections;

public class TimeFunctions : MonoBehaviour {


	public static System.DateTime getCurrentDate() {
    return System.DateTime.Now;
  }


  public static int getHour() {
    System.DateTime date = TimeFunctions.getCurrentDate();
    return date.Hour;
  }

  public static int getMinute() {
    System.DateTime date = TimeFunctions.getCurrentDate();
    return date.Minute;
  }

	public static int getSecond() {
    System.DateTime date = TimeFunctions.getCurrentDate();
    return date.Second;
  }
}
