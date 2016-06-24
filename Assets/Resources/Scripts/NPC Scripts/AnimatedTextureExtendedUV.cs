using UnityEngine;
using System.Collections;


public class AnimatedTextureExtendedUV : MonoBehaviour
{
	public int colCount = 4;
	public string sheetname;
  private Sprite[] sprites;
  private SpriteRenderer sr;
  private string[] names;
	private NPCController controller;
	private SpriteRenderer spRend;
	private Sprite sprite;
	private int hIndex = 0;
	private int vIndex = 0;
	private string charName;
	private int updateCount = 0;


	void Start(){
		controller = GetComponent<NPCController> ();
		sprites = Resources.LoadAll <Sprite> ("Graphics/Characters/"+sheetname);

    sr = GetComponent<SpriteRenderer> ();
    names = new string[sprites.Length];

    for(int i = 0; i < names.Length; i++)
    {
       names[i] = sprites[i].name;
    }
	}

	//Update
	void LateUpdate () {
	  SetSpriteAnimation();
	}

	//SetSpriteAnimation
	void SetSpriteAnimation(){

		int facing = controller.getFacing();
		bool is_walking = controller.getWalking();


		// split into horizontal and vertical index

		if (is_walking) {
			if (updateCount%10==0){
				Debug.Log(updateCount);
				hIndex+=1;
				hIndex = hIndex % colCount;
			}

			updateCount++;
		} else {
			if (hIndex==2){
				hIndex = 2;
			} else {
				hIndex = 0;
			}
			updateCount = 0;
		}

		if (facing==1){
				vIndex = 3;
		} else if (facing == -1){
				vIndex = 0;
		} else if (facing == 2){
				vIndex = 2;
		} else if (facing == -2){
				vIndex = 1;
		}

		//calculate new index
		int newIndex = (4*vIndex + hIndex);
		ChangeSprite(newIndex);
	}

	void ChangeSprite( int index )
  {
       sr.sprite = sprites[index];
  }

  void ChangeSpriteByName( string name )
  {
       sr.sprite = sprites[System.Array.IndexOf(names, name)];
  }
}
