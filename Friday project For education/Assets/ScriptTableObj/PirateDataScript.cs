using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Create Pirate Card")]
public class PirateDataScript : ScriptableObject
{
    public int take_card;
    public int power_enemy;//พลังของอุปสรรค
    public bool have_effect;
    public string effect_text;
    public int num_effect;//0 : not thing,
                          //1 : ค่าความอันตรายตามจำนวนการ์ดเพิ่มอายุที่เข้ากอง คูณ 2 (เอาออกไปแล้วก็นับ)
                          //2 : การ์ดต่อสู้แต่ละใบที่หยิบเพิ่มต้องเสียงพลังชีวิด 2 ชิ้น
                          //3 : การ์ดที่หงายจะ + 1 พลัง
                          //4 : ใช้ความสามารถการ์ดที่งายได้เพียงครึ่งเดี่ยว (บังคับใช้การ์ดอายุ) (ปัดชึ้น) //not do
                          //5 : ต่อสู้กับการ์ดอันตรายที่เหลือทั้งหมด //not do
    public Sprite image;
}
