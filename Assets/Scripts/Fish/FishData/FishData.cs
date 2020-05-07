using UnityEngine;
public enum Rarity
{
    Common=0,
    Rare=1,
    UnderProtection=2,
    Endangered=3,
    Extinct=4
}

public class FishDataBase
{
    public string name;
    public string info;

    //其实是中文名
    public string briefInfo;

    //稀有度,最小为0
    public Rarity rarity;
    //海域深度,最小为0
    public int depth;

    public int minPopulation = 1;//种群参数。1为单独生成
    public int maxPopulation = 1;//种群参数。1为单独生成

    public int ID;
}


//[CreateAssetMenu]
[System.Serializable]
public class FishData:FishDataBase
{
    //移动参数
    //游动方式
    public float maxSpeed; 
    public float minSpeed;

    public float alignWeight = 10;
    public float cohesionWeight = 10;
    public float seperateWeight = 10;
    //shake
    public float shakeFrequency = 0;//shake频率
    public float shakeAmplitude = 0;  //shake振幅

    //rotate
    public float rotateRange=45;//旋转的最大角度。
                             //旋转实现方式：取transform.up绕transform.forward随机旋转random(0,360)取得转轴,然后transform绕转轴旋转random(0,rotateRange)
                             //旋转过程采用每帧lerp
    public float rotateInterval=2.5f;//旋转时间间隔，单位：秒

}
[System.Serializable]
public class JellyData:FishDataBase
{
    public float upRate;
}
