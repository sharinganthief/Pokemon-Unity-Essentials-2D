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
	int hIndex = 0;
	int vIndex = 0;
	string charName;

	void Start(){
		controller = GetComponent<NPCController> ();
		sprites = Resources.LoadAll <Sprite> ("Characters/"+sheetname);

    sr = GetComponent<SpriteRenderer> ();
    names = new string[sprites.Length];

    for(int i = 0; i < names.Length; i++)
    {
       names[i] = sprites[i].name;
    }
	}

	//Update
	void Update () {
	  SetSpriteAnimation();
	}

	//SetSpriteAnimation
	void SetSpriteAnimation(){

		int facing = controller.getFacing();
		bool is_walking = controller.getWalking();


		// split into horizontal and vertical index

		if (is_walking) {
			hIndex+=1;
			hIndex = hIndex % colCount;
		}
		else {
			hIndex = 0;
		}
		switch(facing){
			case 1: //up
				vIndex = 3;
				break;
			case -1: //down
				vIndex = 0;
				break;
			case 2: //right
				vIndex = 2;
				break;
			case -2: //left
				vIndex = 1;
				break;
		}

		//calculate new index
		int newIndex = (4*vIndex + hIndex);
		ChangeSprite(newIndex);
	}

	void ChangeSprite( int index )
  {
       Sprite sprite = sprites[index];
       sr.sprite = sprite;
  }

  void ChangeSpriteByName( string name )
  {
       Sprite sprite = sprites[System.Array.IndexOf(names, name)];
       sr.sprite = sprite;
  }
}
