using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UI.Tables;

namespace UI.Dates
{
    public class DatePicker_DayButton : DatePicker_Button
    {
        public DateTime Date;
        public TableCell Cell;

      
        public DatePicker DatePicker;


        public void OnEnable()
        { 
        //transform.GetComponent<Button>().
        
        }
        public void Clicked()
        {
            if (!Button.interactable) return;

            DatePicker.DayButtonClicked(Date);
            DatePicker.Hide();
        }

        public void MouseOver()
        {
            if (!Button.interactable) return;

            DatePicker.DayButtonMouseOver(Date);
        }        
    }
}
