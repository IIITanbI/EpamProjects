using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



            PhoneNumber n1 = new PhoneNumber("123");
            Terminal terminal1 = new Terminal(n1);

            PhoneNumber n2 = new PhoneNumber("256");
            Terminal terminal2 = new Terminal(n2);

            Station station = new Station();
            station.Add(new Port());
            station.Add(new Port());
            station.Add(new Port());
            station.Add(new Port());

            station.Add(terminal1);
            station.Add(terminal2);
            terminal1.Plug();
            terminal1.Call(terminal2.PhoneNumber);

            Console.Read();
        }
    }
}
