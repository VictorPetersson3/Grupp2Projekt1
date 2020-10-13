using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject myCamera = null;
    [SerializeField]
    private Vector2 myParallaxEffectMultiplier;
    [SerializeField]
    private bool myInfiniteVertical = false;
    [SerializeField]
    private bool myInfiniteHorizontal = false;

    private Vector3 myLastCameraPosition;
    private float myTextureUnitSizeX;
    private float myTextureUnitSizeY;

    private void Start()
    {
        myLastCameraPosition = myCamera.transform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        myTextureUnitSizeX = (texture.width / sprite.pixelsPerUnit);
        myTextureUnitSizeY = (texture.height / sprite.pixelsPerUnit);

    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = myCamera.transform.position - myLastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * myParallaxEffectMultiplier.x, deltaMovement.y * myParallaxEffectMultiplier.y);
        myLastCameraPosition = myCamera.transform.position;

        if (myInfiniteHorizontal)
        {
            if (Mathf.Abs(myCamera.transform.position.x - transform.position.x) >= myTextureUnitSizeX)
            {
                float offsetPositionX = (myCamera.transform.position.x - transform.position.x) % myTextureUnitSizeX;
                transform.position = new Vector3(myCamera.transform.position.x + offsetPositionX, transform.position.y);
            }
        }

        if (myInfiniteVertical)
        {
            if (Mathf.Abs(myCamera.transform.position.y - transform.position.y) >= myTextureUnitSizeY)
            {
                float offsetPositionY = (myCamera.transform.position.y - transform.position.y) % myTextureUnitSizeY;
                transform.position = new Vector3(transform.position.x, myCamera.transform.position.y + offsetPositionY);
            }
        }

    }
}
