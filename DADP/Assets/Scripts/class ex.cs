using UnityEngine;
using UnityEngine.UIElements;

public class classex : MonoBehaviour

{
    public float rotationSpeed = 5.0f; // so you can edit it in the Unity Inspector
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int coinValue = 25;
        int coinsCollected = 3;
        int totalCoins = coinValue * coinsCollected;

        Debug.Log("Total coins collected: " + totalCoins);

        bool hasKey = true;
        if (hasKey)
        {
            Debug.Log("You have the key to open the door.");
        }
        else
        {
            Debug.Log("You need to find the key to open the door.");
        }

        int playerHealth = 100;
        int damageTaken = 30;
        playerHealth -= damageTaken;

        Debug.Log("Player health after taking damage: " + playerHealth);


    }

    public class Cat : MonoBehaviour
    {
        public virtual void MakeSound()
        {
            Debug.Log("Meow!");
        }
    }

    public class Lion : Cat
    {
        public override void MakeSound()
        {
            Debug.Log("Lion roars: ROAR!");
        }
    }

    // Update is called once per frame
    private void Update()
    {

        transform.Rotate(0, rotationSpeed, 0); // Rotate the object around its Y axis eg. coin

    }
}
