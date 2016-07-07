using UnityEngine;
using System.Collections;


public class AnimatedTextureExtendedUV : MonoBehaviour
{
	public int colCount = 4;
	public string sheetname;
  private Sprite[] sprites;
  private SpriteRenderer sr;
  private string[] names;
	private PlayerMovement playerMovement;
	private SpriteRenderer spRend;
	private Sprite sprite;
	private int hIndex = 0;
	private int vIndex = 0;
	private string charName;
	private int updateCount = 0;

	private int facing;
	private bool is_walking;


	void Start(){
		playerMovement = GetComponent<PlayerMovement> ();
		sprites = Resources.LoadAll <Sprite> ("Graphics/Characters/"+sheetname);

    sr = GetComponent<SpriteRenderer> ();
    names = new string[sprites.Length];

    for(int i = 0; i < names.Length; i++)
    {
       names[i] = sprites[i].name;
    }
	}


	//SetSpriteAnimation
	public IEnumerator UpdateSpriteAnimation(){
		// split into horizontal and vertical index

		int counter = 0;

		for (int i = 0; i<2; i++){
			hIndex += 1;
			counter+=1;

			if (hIndex>3)
				hIndex = 0;

			if (facing==1){
					vIndex = 3;
			} else if (facing == -1){
					vIndex = 0;
			} else if (facing == 2){
					vIndex = 2;
			} else if (facing == -2){
					vIndex = 1;
			}

			ChangeSprite(4*vIndex + hIndex);
			yield return new WaitForSeconds(1f/8f);
		}
		ChangeSprite(4*vIndex + hIndex);
	}

	void ChangeSprite( int index )
  {
       sr.sprite = sprites[index];
  }

  void ChangeSpriteByName( string name )
  {
       sr.sprite = sprites[System.Array.IndexOf(names, name)];
  }

	public void setWalking(bool p_is_walking){
		is_walking = p_is_walking;
	}

	public void setFacing(int p_direction){
		facing = p_direction;
		if (facing==1){
				vIndex = 3;
		} else if (facing == -1){
				vIndex = 0;
		} else if (facing == 2){
				vIndex = 2;
		} else if (facing == -2){
				vIndex = 1;
		}
		ChangeSprite(4*vIndex + hIndex);
	}

}
