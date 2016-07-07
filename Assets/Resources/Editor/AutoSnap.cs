using UnityEngine;
 using UnityEditor;

 public class AutoSnap : EditorWindow
 {
	 private Vector3 prevPosition;
	 private bool doSnap = true;
	 private float snapValueX = 0.307355f;
	 private float snapValueY = 0.307385f;
	 private float snapValueZ = 1.0f;

	 [MenuItem( "Edit/Auto Snap %_l" )]

	 static void Init() {
			 var window = (AutoSnap)EditorWindow.GetWindow( typeof( AutoSnap ) );
			 window.maxSize = new Vector2( 200, 100 );
	 }

	 void OnEnable() {
		 EditorApplication.update += Update;
	 }


	 public void OnGUI() {
			 doSnap = EditorGUILayout.Toggle( "Auto Snap", doSnap );
	 }

	 public void Update() {
			 if ( doSnap
					 && !EditorApplication.isPlaying
					 && Selection.transforms.Length > 0
					 && Selection.transforms[0].position != prevPosition
					 && (Selection.activeGameObject.name.Equals("Player")
           || Selection.activeGameObject.tag.Equals("Event")))
			 {
					 Snap();
					 prevPosition = Selection.transforms[0].position;
			 }
	 }

	 private void Snap() {
			 foreach ( var transform in Selection.transforms )
			 {
					 var t = transform.position;
					 t.x = Round( t.x, snapValueX ) ;
					 t.y = Round( t.y, snapValueY ) ;
					 t.z = Round( t.z, snapValueZ );
					 transform.position = t;

			 }
	 }

	 private float Round( float input, float snapValue) {
			 return (snapValue * Mathf.Floor( ( input / snapValue ) ) + snapValue/2) ;
	 }
 }
