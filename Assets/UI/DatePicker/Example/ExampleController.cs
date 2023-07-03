using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UI.Dates
{
    [ExecuteInEditMode]
    public class ExampleController : MonoBehaviour
    {        

        public DatePicker InlineDatePicker;
        
        public void ToggleNextPreviousMonthButtons(bool on)
        {

            print("点击下一个月按钮");
            InlineDatePicker.Config.Header.ShowNextAndPreviousMonthButtons = on;
            InlineDatePicker.UpdateDisplay();
        }

        public void ToggleNextPreviousYearButtons(bool on)
        {
            print("点击下一年按钮");
            InlineDatePicker.Config.Header.ShowNextAndPreviousYearButtons = on;
            InlineDatePicker.UpdateDisplay();
        }

        public void ToggleWeekNumberDisplay(bool on)
        {
            print("星期");
            InlineDatePicker.Config.WeekDays.ShowWeekNumbers = on;
            InlineDatePicker.UpdateDisplay();
        }

        public void ToggleShowDatesInOtherMonths(bool on)
        {
            print("显示数据在其他月");
            InlineDatePicker.Config.Misc.ShowDatesInOtherMonths = on;
            InlineDatePicker.UpdateDisplay();
        }

        public void ToggleAllowMultipleDateSelection(bool on)
        {
            print("显示数据在其他月");
            InlineDatePicker.DateSelectionMode = on ? DateSelectionMode.MultipleDates : DateSelectionMode.SingleDate;
            InlineDatePicker.UpdateDisplay();
        }        
    }
}
