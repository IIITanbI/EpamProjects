﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project3.Interfaces;

namespace Project3
{
    /*
        Разработать набор классов для моделирования работы автоматический телефонной станции (АТС) и простейшей биллинговой системы. 

        Компания-оператор АТС заключает договора с клиентами, присваивает им абонентские номера, предоставляет порты для подключения абонентских терминалов и выдаёт каждому абоненту терминал (телефон). 
        Каждый терминал соответствует только одному номеру. 
        Абонент может самостоятельно отключать/подключать телефон к порту станции (станция умеет отслеживать изменения состояния порта – отключен, подключен, звонок, и т.п.). 
        Абоненты могут звонить друг другу в только пределах станции. 
        Звонки платные, существует несколько тарифных планов для тарификации звонков, абонент может изменить тарифный план один раз в месяц. 
        Способ оплаты - кредитный (т.е. абоненты оплачивают разговоры предыдущего месяца до N-ного числа текущего). 
        Абонент может просмотреть детализированный отчет по звонкам (продолжительность/стоимость/абонент) как минимум за предыдущий месяц, выполнять фильтрацию по дате звонка, сумме, абоненту.
    */
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dt = DateTime.Now;
            DateTime add = new DateTime();
            TimeSpan addd = new TimeSpan(2000000000);
            Console.WriteLine(addd.TotalMinutes);
            
            add = add.AddMonths(1);
            Console.WriteLine(dt.TimeOfDay.TotalDays);
            Console.WriteLine(DateTime.MaxValue.TimeOfDay.TotalDays);
            PhoneNumber n1 = new PhoneNumber("123");
            ITerminal terminal1 = new Terminal(n1);

            PhoneNumber n2 = new PhoneNumber("256");
            ITerminal terminal2 = new Terminal(n2);

            PhoneNumber n3 = new PhoneNumber("512");
            ITerminal terminal3 = new Terminal(n3);

            Station station = new Station(new List<IPort>(), new List<ITerminal>());
            station.Add(new Port());
            station.Add(new Port());
            station.Add(new Port());
       


            station.Add(terminal1);
            station.Add(terminal2);
            station.Add(terminal3);
            //station.Remove(terminal1);
            terminal1.Plug();
            //terminal2.Plug();
            terminal3.Plug();
            //terminal1.UnPlug();
            terminal1.Call(terminal2.PhoneNumber);
            terminal3.Call(terminal1.PhoneNumber);
           // terminal1.Interrupt();
            station.Remove(terminal1);
            Console.Read();
        }
    }
}
