using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    //ゲームタイム
    [SerializeField]
    private float count = 0f;
    /// <summary>
    ///ゲームの終了する時間
    /// </summary>
    [SerializeField]
    private float countLimit = 30f;
    public float num;
    public int digit = 16;
    public bool zeroFill = false;
    private List<Image> NumImageList = new List<Image>();
    [SerializeField] private Sprite[] spriteNumbers = new Sprite[10];
    //ほかのスクリプトから呼び出せるように
    public float Count
    {
        get
        {
            return count;
        }
    }
    //同じく呼び出せるように
    public float CountLimit
    {
        get
        {
            return countLimit;
        }
    }

    private void Awake()
    {
        Debug.Log(GetComponent<Text>().fontSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (count < countLimit)
        {
            if (NumImageList.Count == digit)
            {
                //桁数が揃っているので数値を表示する
                float num2 = num;
                int numDigit = 0;
                if (num2 > 0)
                {
                    numDigit = ((int)Mathf.Log10(num2) + 1);
                }
                if (numDigit > digit)
                {
                    //数値が桁数を超えている
                    for (int i = 0; i < NumImageList.Count; i++)
                    {
                        Image numImage = NumImageList.ToArray()[i];
                        if (numImage != null)
                        {
                            numImage.color = Color.white;
                            numImage.sprite = spriteNumbers[spriteNumbers.Length - 1];
                        }
                    }
                }
                else
                {
                    //数値が桁数を超えていない
                    int[] numIndexs = new int[numDigit];
                    for (int i = 0; i < numDigit; i++)
                    {
                        numIndexs[i] = (int)(num2 % 10);
                        num2 = num2 / 10;
                    }
                    for (int i = 0; i < NumImageList.Count; i++)
                    {
                        Image numImage = NumImageList.ToArray()[i];
                        if (numImage != null)
                        {
                            if (numDigit == 0 && i == 0)
                            {
                                //数値が0だった時の処理（1桁目は必ず0で表示）
                                numImage.color = Color.white;
                                numImage.sprite = spriteNumbers[0];
                            }
                            else if (i < numIndexs.Length)
                            {
                                //数値を反映する
                                numImage.color = Color.white;
                                numImage.sprite = spriteNumbers[numIndexs[i]];
                            }
                            else
                            {
                                if (zeroFill)
                                {
                                    //0埋め
                                    numImage.color = Color.white;
                                    numImage.sprite = spriteNumbers[0];
                                }
                                else
                                {
                                    //非表示
                                    numImage.color = Color.clear;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (NumImageList.Count < digit)
                {
                    //桁数が足りないので増やす
                    GameObject numImageObj = new GameObject();
                    if (numImageObj != null)
                    {
                        numImageObj.name = "NumberImage" + (NumImageList.Count + 1);
                        numImageObj.transform.SetParent(this.transform);
                        RectTransform thisRect = this.GetComponent<RectTransform>();
                        if (thisRect != null)
                        {
                            Image numImage = numImageObj.AddComponent<Image>();
                            if (numImage != null)
                            {
                                numImage.color = Color.clear;
                                RectTransform numImageRect = numImageObj.GetComponent<RectTransform>();
                                if (numImageRect != null)
                                {
                                    if (spriteNumbers != null && spriteNumbers.Length > 0)
                                    {
                                        numImageRect.sizeDelta = new Vector2(spriteNumbers[0].bounds.size.x * (thisRect.sizeDelta.y / spriteNumbers[0].bounds.size.y), thisRect.sizeDelta.y);
                                        if (NumImageList.Count == 0)
                                        {
                                            numImageObj.transform.localPosition = new Vector3(thisRect.sizeDelta.x / 2 - numImageRect.sizeDelta.x / 2, 0);
                                        }
                                        else
                                        {
                                            Image image = NumImageList.ToArray()[NumImageList.Count - 1];
                                            if (image != null)
                                            {
                                                numImageObj.transform.localPosition = new Vector3(image.transform.localPosition.x - numImageRect.sizeDelta.x, 0);
                                            }
                                        }
                                        NumImageList.Add(numImage);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //桁数が多いので減らす
                    Image image = NumImageList.ToArray()[NumImageList.Count - 1];
                    if (image != null)
                    {
                        NumImageList.RemoveAt(NumImageList.Count - 1);
                        Destroy(image.gameObject);
                    }
                }
            }
            count += Time.deltaTime; //スタートしてからの秒数を格納
            num = (long)count;
            //GetComponent<Text>().text = count.ToString("F2"); //小数2桁にして表示
        }
        else
        {
            num = countLimit;
            if (count < countLimit)
            {
                if (NumImageList.Count == digit)
                {
                    //桁数が揃っているので数値を表示する
                    float num2 = num;
                    int numDigit = 0;
                    if (num2 > 0)
                    {
                        numDigit = ((int)Mathf.Log10(num2) + 1);
                    }
                    if (numDigit > digit)
                    {
                        //数値が桁数を超えている
                        for (int i = 0; i < NumImageList.Count; i++)
                        {
                            Image numImage = NumImageList.ToArray()[i];
                            if (numImage != null)
                            {
                                numImage.color = Color.white;
                                numImage.sprite = spriteNumbers[spriteNumbers.Length - 1];
                            }
                        }
                    }
                    else
                    {
                        //数値が桁数を超えていない
                        int[] numIndexs = new int[numDigit];
                        for (int i = 0; i < numDigit; i++)
                        {
                            numIndexs[i] = (int)(num2 % 10);
                            num2 = num2 / 10;
                        }
                        for (int i = 0; i < NumImageList.Count; i++)
                        {
                            Image numImage = NumImageList.ToArray()[i];
                            if (numImage != null)
                            {
                                if (numDigit == 0 && i == 0)
                                {
                                    //数値が0だった時の処理（1桁目は必ず0で表示）
                                    numImage.color = Color.white;
                                    numImage.sprite = spriteNumbers[0];
                                }
                                else if (i < numIndexs.Length)
                                {
                                    //数値を反映する
                                    numImage.color = Color.white;
                                    numImage.sprite = spriteNumbers[numIndexs[i]];
                                }
                                else
                                {
                                    if (zeroFill)
                                    {
                                        //0埋め
                                        numImage.color = Color.white;
                                        numImage.sprite = spriteNumbers[0];
                                    }
                                    else
                                    {
                                        //非表示
                                        numImage.color = Color.clear;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (NumImageList.Count < digit)
                    {
                        //桁数が足りないので増やす
                        GameObject numImageObj = new GameObject();
                        if (numImageObj != null)
                        {
                            numImageObj.name = "NumberImage" + (NumImageList.Count + 1);
                            numImageObj.transform.SetParent(this.transform);
                            RectTransform thisRect = this.GetComponent<RectTransform>();
                            if (thisRect != null)
                            {
                                Image numImage = numImageObj.AddComponent<Image>();
                                if (numImage != null)
                                {
                                    numImage.color = Color.clear;
                                    RectTransform numImageRect = numImageObj.GetComponent<RectTransform>();
                                    if (numImageRect != null)
                                    {
                                        if (spriteNumbers != null && spriteNumbers.Length > 0)
                                        {
                                            numImageRect.sizeDelta = new Vector2(spriteNumbers[0].bounds.size.x * (thisRect.sizeDelta.y / spriteNumbers[0].bounds.size.y), thisRect.sizeDelta.y);
                                            if (NumImageList.Count == 0)
                                            {
                                                numImageObj.transform.localPosition = new Vector3(thisRect.sizeDelta.x / 2 - numImageRect.sizeDelta.x / 2, 0);
                                            }
                                            else
                                            {
                                                Image image = NumImageList.ToArray()[NumImageList.Count - 1];
                                                if (image != null)
                                                {
                                                    numImageObj.transform.localPosition = new Vector3(image.transform.localPosition.x - numImageRect.sizeDelta.x, 0);
                                                }
                                            }
                                            NumImageList.Add(numImage);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //桁数が多いので減らす
                        Image image = NumImageList.ToArray()[NumImageList.Count - 1];
                        if (image != null)
                        {
                            NumImageList.RemoveAt(NumImageList.Count - 1);
                            Destroy(image.gameObject);
                        }
                    }
                }
            }

            count = countLimit;
            GetComponent<Text>().text = count.ToString("F2");
        }

        
    }
}
