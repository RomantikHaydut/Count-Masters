using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Options : MonoBehaviour
{
    [Header("Option 1")]
    public OptionController optionController1;
    [SerializeField] int value1;
    [SerializeField] private bool sum1;
    [SerializeField] public bool minus1;
    [SerializeField] public bool divide1;
    [SerializeField] public bool multiplier1;

    [Header("Option 2")]
    public OptionController optionController2;
    [SerializeField] int value2;
    [SerializeField] private bool sum2;
    [SerializeField] public bool minus2;
    [SerializeField] public bool divide2;
    [SerializeField] public bool multiplier2;


    private void Awake()
    {
        SetOptions();
    }


    private void SetOptions()
    {

        optionController1.myValue = value1;
        optionController1.sum = sum1;
        optionController1.minus = minus1;
        optionController1.divide = divide1;
        optionController1.multiplier = multiplier1;
        SetOptionText(value1, optionController1, sum1, minus1, divide1, multiplier1);

        optionController2.myValue = value2;
        optionController2.sum = sum2;
        optionController2.minus = minus2;
        optionController2.divide = divide2;
        optionController2.multiplier = multiplier2;
        SetOptionText(value2, optionController2, sum2, minus2, divide2, multiplier2);
    }

    private void SetOptionText(int value, OptionController option, bool sum, bool minus, bool divide, bool multiplier)
    {
        TMP_Text text = option.GetComponentInChildren(typeof(TMP_Text)) as TMP_Text;
        if (sum)
        {
            text.text = "+" + value;
        }
        else if (minus)
        {
            text.text = "-" + value;
        }
        else if (divide)
        {
            text.text = "/" + value;
        }
        else if (multiplier)
        {
            text.text = "X" + value;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stickman"))
        {
            if (FindObjectOfType<PlayerController>().transform.position.x < transform.position.x)
            {
                optionController1.MyFunction();
            }
            else
            {
                optionController2.MyFunction();
            }
            gameObject.SetActive(false);
            //Component[] options = transform.parent.gameObject.GetComponentsInChildren(typeof(OptionController));
            //foreach (OptionController option in options)
            //{
            //    if (option != this)
            //    {
            //        option.enabled = false;
            //    }
            //}

            //transform.parent.GetComponent<Options>().optionController1.enabled = false;
            //transform.parent.GetComponent<Options>().optionController2.enabled = false;
            //gameObject.SetActive(false);

        }
    }



}
