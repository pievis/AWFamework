using System.Collections;
using AWFramework;
//should be used only if well sure the model is used only in UnityEngine
using UnityEngine;

/**
 * It's the model of the cube
 **/
public class CubeModel : MonoBehaviour, IModel
{

	float xRotDegree = 0;
	float yRotDegree = 0;
	float zRotDegree = 0;
	public Color color;

	float YRotDegree {
		get {
			return this.yRotDegree;
		}
	}

	float XRotDegree {
		get {
			return this.xRotDegree;
		}
	}

	float ZRotDegree {
		get {
			return this.zRotDegree;
		}
	}

	public Vector3 GetEuclidRot (){
		return new Vector3 (this.XRotDegree, this.YRotDegree, this.ZRotDegree);
	}

	void SetEuclidRot(Vector3 rot){
		this.xRotDegree = rot.x;
		this.yRotDegree = rot.y;
		this.zRotDegree = rot.z;
	}

	public Color GetColor(){
		return this.color;
	}

	//Shared hologram methods

	public void SetColor(Color color){
		this.color = color;
	}

	public void SetColor(int colorInt){
		Color color = colorInt == 1 ? Color.red : Color.blue;
		SetColor(color);
	}

	public void Rotate(Vector3 axis, float degree){
		Vector3 newRot = GetEuclidRot() + (axis * degree);
		SetEuclidRot(newRot);
		Debug.Log ("Model: changed rotation to : " + newRot);
	}
}
