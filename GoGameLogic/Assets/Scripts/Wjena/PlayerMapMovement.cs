using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapMovement : MonoBehaviour
{
    public float speed = 5f;
    private bool flag = false;

	IEnumerator Rotate(int RequiredAngle)
	{
		int playerangle = (int)gameObject.transform.rotation.eulerAngles.y;
		int Diff = playerangle - RequiredAngle;
		/*if (playerangle == 0)
		{
			if (RequiredAngle == 0)
			{
				//StartCoroutine("WalkUp");
				yield return null;
			}
			if (RequiredAngle == 90)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0333f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
				//StartCoroutine("WalkRight");
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 6, 0);
					yield return new WaitForSeconds(0.0333f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
				//StartCoroutine("WalkDown");
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0333f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
				//StartCoroutine("WalkLeft");
				yield return null;
			}
		}*/
		if (playerangle == 90)
		{
			if (RequiredAngle == 90)
			{
				StartCoroutine("WalkRight");
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0333f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
				//StartCoroutine("WalkUp");
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0333f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
				//StartCoroutine("WalkDown");
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 6, 0);
					yield return new WaitForSeconds(0.0333f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
				StartCoroutine("WalkLeft");
				yield return null;
			}
		}
		/*if (playerangle == 180)
		{
			//Debug.Log("aaa");
			if (RequiredAngle == 180)
			{
				StartCoroutine("WalkDown");
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 6, 0);
					yield return new WaitForSeconds(0.0333f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
				StartCoroutine("WalkUp");
				yield return null;
			}
			if (RequiredAngle == 90)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0333f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
				StartCoroutine("WalkRight");
				yield return null;
			}
			if (RequiredAngle == 270)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0333f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
				StartCoroutine("WalkLeft");
				yield return null;
			}
		}*/
		if (playerangle == 270)
		{
			//Debug.Log("aaa");
			if (RequiredAngle == 270)
			{
				StartCoroutine("WalkLeft");
				yield return null;
			}
			if (RequiredAngle == 0)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 1);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y + 3, 0);
					yield return new WaitForSeconds(0.0333f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
				//StartCoroutine("WalkUp");
				yield return null;
			}
			if (RequiredAngle == 90)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 3);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 6, 0);
					yield return new WaitForSeconds(0.0333f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
				StartCoroutine("WalkRight");
				yield return null;
			}
			if (RequiredAngle == 180)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 2);
				for (int i = 0; i < 30; i++)
				{
					gameObject.transform.rotation = Quaternion.Euler(0, (int)gameObject.transform.rotation.eulerAngles.y - 3, 0);
					yield return new WaitForSeconds(0.0333f);
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("IsRotating", 0);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
				gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
				//StartCoroutine("WalkDown");
				yield return null;
			}
		}
		yield return null;
	}

	IEnumerator WalkRight()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
        for (float i = 0; i < 1; i += 0.01f)
        {
            transform.position += new Vector3(0.03f, 0, 0);
            yield return new WaitForSeconds(0.009f);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", false);
       
        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
        flag = false;
    }

    IEnumerator WalkLeft()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", true);
        for (float i = 0; i < 1; i += 0.01f)
        {
            transform.position += new Vector3(-0.031f, 0, 0);
            yield return new WaitForSeconds(0.009f);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsRunning", false);
        
        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
        flag = false;
    }

    private GameObject[] GetListOfCap()
    {
        GameObject[] ListOfCap = GameObject.FindGameObjectsWithTag("Cap");
        return ListOfCap;
    }

    public bool CheckIfCapExist(Vector3 PlayerCoord, int increment)
    {
        GameObject[] ListOfCap = GetListOfCap();
        for (int i = 0; i < ListOfCap.Length; i++)
        {
                if (ListOfCap[i].transform.position.x == PlayerCoord.x + increment
                && ListOfCap[i].transform.position.z + 0.5 == PlayerCoord.z)
                    return true;
        }
        return false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Time.timeScale == 1)
		{
			if (Input.GetKeyDown(KeyCode.A))
			{
				if (CheckIfCapExist(transform.position, -3))
				{
					if (flag == false)
					{
						flag = true;
						StartCoroutine("Rotate", 270);
						//StartCoroutine("WalkLeft");
					}
				}
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				if (CheckIfCapExist(transform.position, 3))
				{
					if (flag == false)
					{
						flag = true;
						StartCoroutine("Rotate", 90);
						//StartCoroutine("WalkRight");
					}
				}
			}
			/* if (Input.GetKey(KeyCode.W))
			 {
				 transform.position += new Vector3(0, 0, 1 * Time.deltaTime * speed);
			 }
			 if (Input.GetKey(KeyCode.S))
			 {
				 transform.position += new Vector3(0, 0, -1 * Time.deltaTime * speed);
			 }*/
		}
    }
}
