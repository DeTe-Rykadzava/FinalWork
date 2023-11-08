// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;
using FinalWork.Core.Base;
using FinalWork.Core.ConsoleReader;

var calcManager = new CalculatorManager();

Clear(calcManager.GetAllAbleCalcOperations());

List<double> operands = new List<double>(2) { 0 , 0 };

List<CalculatorResult> history = new List<CalculatorResult>();

int currentOperandNum = 0;

char? operation = null;

while (true)
{
    if (currentOperandNum > 0)
    {
        Console.WriteLine($"{operands[currentOperandNum - 1]} {operation}");
    }

    Console.WriteLine($"\t{operands[currentOperandNum]}");
    var inputKeyInfo = Console.ReadKey();
    if (char.IsDigit(inputKeyInfo.KeyChar))
    {
        operands[currentOperandNum] = double.Parse($"{operands[currentOperandNum]}{inputKeyInfo.KeyChar}");
    }
    else
    {
        if (inputKeyInfo.Key == ConsoleKey.Backspace)
        {
            if(operands[currentOperandNum].ToString().Length > 0)
            {
                string newValue = operands[currentOperandNum].ToString().Remove(operands[currentOperandNum].ToString().Length - 1, 1);
                if (string.IsNullOrWhiteSpace(newValue))
                    newValue = "0";
                operands[currentOperandNum] = double.Parse(newValue);
            }
        }

        if (inputKeyInfo.Key == ConsoleKey.C)
        {
            operation = null;
            currentOperandNum = 0;
            operands[0] = 0;
            operands[1] = 0;
            Clear(calcManager.GetAllAbleCalcOperations());
        }

        if (inputKeyInfo.Key == ConsoleKey.H)
        {
            if (history.Count != 0)
            {
                Console.WriteLine("History: ");
                for (int i = 0; i < history.Count; i++)
                {
                    Console.WriteLine($"\t({i+1})  {history[i].StringResult}");
                }

                var userInput = ConsoleReaderManager.GetUserInput("Please, select needed result operation (input number in history)", UserInputType.Int);
                if (userInput != null)
                {
                    operands[0] = history[(int)userInput - 1].Result;
                }
            }
            else
            {
                Console.WriteLine("History is empty");
            }
        }

        if (inputKeyInfo.Key == ConsoleKey.S)
        {
            Console.WriteLine("Select foreground color (enter button):");
            Console.WriteLine("{R} - Red");
            Console.WriteLine("{G} - Green");
            Console.WriteLine("{B} - Blue");
            Console.WriteLine("{W} - White");
            Console.WriteLine("{Y} - Yellow");
            Console.WriteLine("{Enter} - reset color");

            var inputKey = Console.ReadKey();
            
            switch (inputKey.Key)
            {
                case ConsoleKey.R:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case ConsoleKey.G:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case ConsoleKey.B:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case ConsoleKey.W:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ConsoleKey.Y:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case ConsoleKey.Enter:
                    Console.ResetColor();
                    break;
            }
        }

        if (inputKeyInfo.Key == ConsoleKey.Enter || inputKeyInfo.KeyChar == '=' && operation != null)
        {
            var result = calcManager.ProcessOperation(operation, operands);
            history.Add(result);
            operands[0] = result.Result;
            operands[1] = 0;
        }

        if (calcManager.CanProcessOperation(inputKeyInfo.KeyChar))
        {
            if (currentOperandNum + 1 < 2)
            {
                currentOperandNum++;
            }
            else
            {
                var result = calcManager.ProcessOperation(operation, operands);
                history.Add(result);
                operands[0] = result.Result;
                operands[1] = 0;
            }
            operation = calcManager.GetAllAbleCalcOperations().FirstOrDefault(x => x == inputKeyInfo.KeyChar);
        }
        
    }
    Clear(calcManager.GetAllAbleCalcOperations());
}


void ShowInfo(List<char> supportedOperations)
{
    Console.WriteLine("===============================================INFO===============================================");
    Console.WriteLine($"\tUse numbers from 0 to 9 and {string.Join(',',supportedOperations)} and = to calculate");
    Console.WriteLine("\t{Enter} is =");
    Console.WriteLine("\t{C} is clear");
    Console.WriteLine("\t{S} is settings");
    Console.WriteLine("\t{H} is history of the results of operations (will restore only the result of the operation),\n" +
                      "\tshows only that the history is not empty");
    Console.WriteLine("\tЕсли результат сразу получается, то это особенность данного калькулятора\n" +
                      "\tпоэтому нужно быть осторожнее с нажатием на клавиши (если вы например введёте\n" +
                      "\t1+1 а после сразу / дважды, то может вывестись бесконечность из-за деления на ноль)\n" +
                      "\tданная ситуация программой ошибкой не считается ввиду что пользователь поспешил\n" +
                      "\tрешением в данном случае - восстановить ответ до этого {H}\n" +
                      "\tТакже если у вас написано 1 + 0 то сменить операцию не получится\n" +
                      "\tнажатием на другие операции (сначала выполнится 1+0, а потом сменится вид операции\n" +
                      "\tПример использования 1 + 1 {Enter} или 1 + 1 + 2 / 2 {Enter}\n" +
                      "\tДля пояснения вверху слува от ввода - ответ прошлой операции или 1-ый операнд");
    Console.WriteLine("==================================================================================================");
}

void Clear(List<char> supportedOperations)
{
    Console.Clear();
    Console.WriteLine("Calc T5000");
    ShowInfo(supportedOperations);
}
