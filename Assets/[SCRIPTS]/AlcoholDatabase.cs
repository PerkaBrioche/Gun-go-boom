using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "AlcoholDatabase", menuName = "Scriptable Objects/AlcoholDatabase")]
public class AlcoholDatabase : ScriptableObject
{
   public List<AlcoholData> datas = new List<AlcoholData>();
}
