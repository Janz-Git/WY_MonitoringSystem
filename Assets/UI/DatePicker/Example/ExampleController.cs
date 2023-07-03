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

            print("�����һ���°�ť");
            InlineDatePicker.Config.Header.ShowNextAndPreviousMonthButtons = on;
            InlineDatePicker.UpdateDisplay();
        }

        public void ToggleNextPreviousYearButtons(bool on)
        {
            print("�����һ�갴ť");
            InlineDatePicker.Config.Header.ShowNextAndPreviousYearButtons = on;
            InlineDatePicker.UpdateDisplay();
        }

        public void ToggleWeekNumberDisplay(bool on)
        {
            print("����");
            InlineDatePicker.Config.WeekDays.ShowWeekNumbers = on;
            InlineDatePicker.UpdateDisplay();
        }

        public void ToggleShowDatesInOtherMonths(bool on)
        {
            print("��ʾ������������");
            InlineDatePicker.Config.Misc.ShowDatesInOtherMonths = on;
            InlineDatePicker.UpdateDisplay();
        }

        public void ToggleAllowMultipleDateSelection(bool on)
        {
            print("��ʾ������������");
            InlineDatePicker.DateSelectionMode = on ? DateSelectionMode.MultipleDates : DateSelectionMode.SingleDate;
            InlineDatePicker.UpdateDisplay();
        }        
    }
}
