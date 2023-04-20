using System;
using System.IO;
using System.Threading;
namespace ConsoleApplication1
{
    class BD
    {
        FileStream f;
        AsyncCallback callback;
        string[] s1=new string[999];
        public void UserInput()	// диалог с пользователем
        {
            string s; 
            
            do
            { 
                Console.WriteLine("Введите ФИО. Enter для завершения" ); 
                s = Console.ReadLine();
        
                if(s != "") {
                    Console.WriteLine("\nСохранить в файл? Enter чтобы сохранить | Esc не сохранить");
                    if(Console.ReadKey().Key == ConsoleKey.Enter) {
                        StreamWriter his = new StreamWriter("1.txt", true);
                        his.WriteLine((this.Linex()-1)+" "+ s);
                        Console.WriteLine("Сохранено!\n");
                        his.Close();
                    }
                }
                Console.WriteLine("Вывести элемент? Enter да | Esc нет\n");
                if(Console.ReadKey().Key == ConsoleKey.Enter) {
                        Console.WriteLine("Введите индекс!\n");
                        int i=Convert.ToInt32(Console.ReadLine());
                        AsyncRead(i);
                    }
                
            } 
            while (s.Length != 0 );
            
            
            
        }
        public void OnCompletedRead( IAsyncResult ar )	// 1
        {
        int bytes = f.EndRead( ar );
        Console.WriteLine( "Считано " + bytes );
        }

        public int Linex()
        {
            int linesCount = 1;
            int nextLine = '\n';
            using (var streamReader = new StreamReader(
                new BufferedStream(
                    File.OpenRead(@"1.txt"), 10 * 1024 * 1024)))
            {
                while(!streamReader.EndOfStream)
                {
                    if (streamReader.Read() == nextLine) linesCount++;
                }
            }
            return linesCount;
        }
        public async void AsyncRead(int i)
        {
            string f = "1.txt";
            string text = await File.ReadAllTextAsync(f);
            s1 = text.Split('\n');
            if(Linex()>=i)
                Console.WriteLine(s1[i-1]);
            else
                Console.WriteLine("Неизвестный индекс");
        }


        

    }
    class Program 
    { 
        static void Main() 
        {
            BD d = new BD();
            d.UserInput();
            
        }
    }
}
